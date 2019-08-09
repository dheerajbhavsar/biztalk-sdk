
//---------------------------------------------------------------------
// File: TransactionalReceiverEndpoint.cs
// 
// Summary: Implementation of an adapter framework sample adapter. 
//
// Sample: Adapter framework transactional adapter.
//
//---------------------------------------------------------------------
// This file is part of the Microsoft BizTalk Server SDK
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// This source code is intended only as a supplement to Microsoft BizTalk
// Server release and/or on-line documentation. See these other
// materials for detailed information regarding Microsoft code samples.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
// PURPOSE.
//---------------------------------------------------------------------

using System;
using System.IO;
using System.Xml;
using System.Threading;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.TransportProxy.Interop;
using Microsoft.Samples.BizTalk.Adapter.Common;

namespace Microsoft.Samples.BizTalk.Adapter.Transactional
{
    //  An instance of this class should exist for every enabled receive location.
    //
    //  This is where all the receive location specific work goes. For example, any listening or polling generally goes in here.
    //
    //  If you want to have multiple independent threads listening or working per receive location that might effectively
    //  be done as member objects from this endpoint class. In this particular example there is only a simgle timer doing some
    //  polling and it is contained in here.
    //
    //  After the Receiver creates a member of this class it calls Open and when the receive location is deleted Dispose is called.
    //  Otherwise all Updates are plumbed through to the Update function on this class.

    internal class TransactionalReceiverEndpoint : ReceiverEndpoint
    {
        public TransactionalReceiverEndpoint()
        {
        }

        public override void Open(string uri, Microsoft.BizTalk.Component.Interop.IPropertyBag config, IPropertyBag bizTalkConfig, Microsoft.BizTalk.Component.Interop.IPropertyBag handlerPropertyBag, IBTTransportProxy transportProxy, string transportType, string propertyNamespace, ControlledTermination control)
        {
            try
            {
                this.properties = new TransactionalReceiveProperties(uri);

                //  Handler properties
                XmlDocument handlerConfigDom = ConfigProperties.IfExistsExtractConfigDom(handlerPropertyBag);
                if (null != handlerConfigDom)
                    this.properties.HandlerConfiguration(handlerConfigDom);

                //  Location properties - possibly override some Handler properties
                XmlDocument locationConfigDom = ConfigProperties.ExtractConfigDom(config);
                this.properties.LocationConfiguration(locationConfigDom);

                //  this is our handle back to the EPM
                this.transportProxy = transportProxy;

                //  used to control whether the EPM can unload us
                this.control = control;

                this.uri = uri;
                this.transportType = transportType;
                this.propertyNamespace = propertyNamespace;
                this.messageFactory = this.transportProxy.GetMessageFactory();

                Start();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        //  update and delete
        public override void Update (IPropertyBag config, IPropertyBag bizTalkConfig, IPropertyBag handlerPropertyBag)
        {
            try
            {
                Stop();

                this.properties = new TransactionalReceiveProperties(this.properties.Uri);

                //  Handler properties
                XmlDocument handlerConfigDom = ConfigProperties.IfExistsExtractConfigDom(handlerPropertyBag);
                if (null != handlerConfigDom)
                    this.properties.HandlerConfiguration(handlerConfigDom);

                //  Location properties - possibly override some Handler properties
                XmlDocument locationConfigDom = ConfigProperties.ExtractConfigDom(config);
                this.properties.LocationConfiguration(locationConfigDom);

                Start();
            }
            finally
            {
            }
        }

        public override void Dispose ()
        {
            try
            {
                Stop();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        private IBaseMessage CreateMessage(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            IBaseMessagePart part = this.messageFactory.CreateMessagePart();
            part.Data = stream;

            IBaseMessage message = this.messageFactory.CreateMessage();
            message.AddPart("body", part, true);

            //  We must add these context properties
            SystemMessageContext context = new SystemMessageContext(message.Context);
            context.InboundTransportLocation = this.uri;
            context.InboundTransportType = this.transportType;

            //  Any particular application specific context properties
            //  you want on the message add them here...

            return message;
        }

        public void SubmitBatch()
        {
            bool needToLeave = false;

            CommittableTransaction transaction = null;

            try
            {
                //  used to block the Terminate from BizTalk 
                if (!this.control.Enter())
                {
                    needToLeave = false;
                    return;
                }
                needToLeave = true;

                ManualResetEvent orderedEvent = new ManualResetEvent(false);

                string connectionString = this.properties.ConnectionString;
                string cmdText = this.properties.CmdText;
                string rootElementName = "Root";

                MemoryStream stream = new MemoryStream();

                bool dataAvailable = false;

                //  Create the System.Transactions transaction
                transaction = new CommittableTransaction();

                //  Explicit interop with COM+ - this should not be necessary in future (on SQL 2005 for example) 
                using (TransactionScope ts = new TransactionScope(transaction, TimeSpan.FromHours(1), EnterpriseServicesInteropOption.Full))
                {
                    //  The connection is created inside the TransactionScope so the database will be included in the transaction
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(cmdText, connection);

                        //  The Encoding on the writer should be consistent with the BizTalk message.
                        //  If you change this to UTF8 then you should change the BizTalk message CharSet to UTF8.

                        XmlWriterSettings settings = new XmlWriterSettings();
                        settings.Encoding = Encoding.Unicode;
                        settings.Indent = true;
                        settings.OmitXmlDeclaration = false;

                        using (XmlWriter writer = XmlTextWriter.Create(stream, settings))
                        {
                            writer.WriteStartDocument();

                            //  Add the root element because the reader form the database doesn't
                            //  include the root element for the xml - it's just rows
                            writer.WriteStartElement(rootElementName);

                            //  WriteNode is a little opaque checking the size of the underlying stream
                            //  is a simple way to see whether any data was retrieved.
                            writer.Flush();
                            long beforeSize = stream.Length;

                            using (XmlReader reader = command.ExecuteXmlReader())
                            {
                                while (!reader.EOF)
                                {
                                    writer.WriteNode(reader, true);
                                }
                            }

                            writer.Flush();
                            long afterSize = stream.Length;

                            //  we only want to add a new message to BizTalk if the reader actually returned anything
                            dataAvailable = (afterSize > beforeSize);

                            writer.WriteEndElement();
                        }
                    }

                    //  An exception will have skipped this next line
                    ts.Complete();
                }

                //  Typically when polling like this you would want to loop checking dataAvailable and only
                //  return to the sleep state when there is no data to feed into BizTalk.
                //
                //  For simplicity this is not shown here. If this is written like this, consideration should also be
                //  given to the .NET thread pool. Sitting on a .NET thread pool thread for an extended period can
                //  result in scale out problems. It is advisable to build some type of yield into the loop, for example
                //  having the worker function BeginInvoke itself if there is dataAvailable might work better than
                //  a simple while loop.

                if (dataAvailable)
                {
                    //  Remember to Seek the stream or we won't get the data
                    stream.Seek(0, SeekOrigin.Begin);

                    //  Note the batch has been given the CommittableTransaction and it will take responsibility for Commit
                    using (Batch batch = new SingleMessageReceiveTxnBatch(this.transportProxy, this.control, transaction, orderedEvent))
                    {
                        batch.SubmitMessage(CreateMessage(stream));
                        batch.Done();
                    }

                    orderedEvent.WaitOne();
                }
                else
                {
                    //  No data so no batch but in a database scenario (like this) we might still wish to Commit.
                    //  Other transactional adapters might choose to Rollback in this circumstance.
                    transaction.Commit();
                }

                //  no exception in Done so we will be getting a BatchComplete which will do the necessary Leave
                needToLeave = false;
            }
            catch (Exception e)
            {
                this.transportProxy.SetErrorInfo(e);

                if (transaction != null)
                    transaction.Rollback();
            }
            finally
            {
                //  if this is true there must have been some exception in or before Done
                if (needToLeave)
                    this.control.Leave();
            }
        }

        private void Start()
        {
            this.timer = new Timer(new TimerCallback(TimerTask));
            this.timer.Change(0, this.properties.PollingInterval);
        }

        private void Stop()
        {
            this.timer.Dispose();
        }

        private void TimerTask(object state)
        {
            //might be useful to add a concurrency guard here
            SubmitBatch();
        }
        
        //  timer and buffer
        Timer timer;

        //  properties
        private TransactionalReceiveProperties properties;

        //  handle to the EPM
        private IBTTransportProxy transportProxy;
        private IBaseMessageFactory messageFactory;
        private ControlledTermination control;

        private string uri;
        private string transportType;
        private string propertyNamespace;
    }
}
