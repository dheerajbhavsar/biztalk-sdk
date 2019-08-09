
//---------------------------------------------------------------------
// File: TransactionalAsyncBatch.cs
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
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using System.Threading;
using System.Runtime.InteropServices;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
using Microsoft.BizTalk.TransportProxy.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.Samples.BizTalk.Adapter.Common;

namespace Microsoft.Samples.BizTalk.Adapter.Transactional
{
    //  This is the core of the send side implementation.
    //
    //  Given the implementation of TransactionalTransmitter there will be an instance of this class
    //  for every batch of messages BizTalk has ready to send.
    //
    //  This batch is of type IBTTransmitterBatch and not to be confused with the message-batch object
    //  captured in the BaseAdapter framework with the Batch class. The IBTTransmitterBatch concept is
    //  much simpler and amounts to little more than a mechanism by which the BizTalk runtime gives a list
    //  of messages to a send side adapter.
    //
    //  Once BizTalk has called Done on the IBTTransmitterBatch - the Done method in this class. These messages
    //  have all been marked as in-memory in the BizTalk database and its the adapter's responsibility to resolve
    //  that state - either by successfully sending them and deleting them on the BizTalk database or following
    //  failure updating their state appripriately for later retry.
    //
    //  In this asynchronous implementation it is important that the adapter jumps off the BizTalk engine's thread
    //  as soon as possible. In this code this is achieved with a call to an asynchronous delegate.

    internal class TransactionalAsyncBatch : IBTTransmitterBatch
    {
        private delegate void WorkerDelegate();
        private delegate void BatchWorkerDelegate(string outboundTransportLocation, ArrayList batch);

        private WorkerDelegate worker;
        private BatchWorkerDelegate batchWorker;

        private IBTTransportProxy transportProxy;
        private ControlledTermination control;
        private string propertyNamespace;

        private int maxBatchSize;               //  from handler
        private ArrayList messageList;          //  the list of messages that BizTalk gives us to process

        public TransactionalAsyncBatch (IBTTransportProxy transportProxy, ControlledTermination control, string propertyNamespace, int maxBatchSize)
        {
            this.worker = new WorkerDelegate(Worker);
            this.batchWorker = new BatchWorkerDelegate(BatchWorker);

            this.transportProxy = transportProxy;
            this.control = control;
            this.propertyNamespace = propertyNamespace;
            this.maxBatchSize = maxBatchSize;
            this.messageList = new ArrayList();
        }
        
        public object BeginBatch (out int maxBatchSize)
        {
            maxBatchSize = this.maxBatchSize;
            return null;
        }

        public bool TransmitMessage (IBaseMessage message)
        {
            this.messageList.Add(message);
            return false;
        }

        public void Clear ()
        {
            this.messageList.Clear();
        }

        public void Done (IBTDTCCommitConfirm commitConfirm)
        {
            if (this.messageList.Count == 0)
            {
                Exception ex = new InvalidOperationException("Send adapter received an emtpy batch for transmission from BizTalk");
                this.transportProxy.SetErrorInfo(ex);

                return;
            }

            //  The Enter/Leave is used to implement the Terminate call from BizTalk.

            //  Do an "Enter" for every message
            int MessageCount = this.messageList.Count;
            for (int i = 0; i < MessageCount; i++)
            {
                if (!this.control.Enter())
                    throw new InvalidOperationException("Send adapter Enter call was false within Done. This is illegal and should never happen."); ;
            }

            try
            {
                //  we now have been given a list of messages so jump of BizTalk's thread and start sending them.
                this.worker.BeginInvoke(null, null);
            }
            catch (Exception)
            {
                //  If there was an error we had better do the "Leave" here
                for (int i = 0; i < MessageCount; i++)
                    this.control.Leave();
            }
        }

        private string CreateEndpointKey (IBaseMessage message)
        {
            // if this is a dynamic send then we need to update the key
            string config = (string)message.Context.Read("AdapterConfig", this.propertyNamespace);
            if (config == null)
            {

                return null;            
            }
            else
            {
                // for a static send port we could hash either the config or the spid - the latter is shorter
                string spid = (string)message.Context.Read("SPID", "http://schemas.microsoft.com/BizTalk/2003/system-properties");

                return spid;
            }
        }

        private Hashtable SortMessagesIntoBatches ()
        {
            Hashtable batches = new Hashtable();

            foreach (IBaseMessage message in this.messageList)
            {

                string endpointKey = CreateEndpointKey(message);

                ArrayList batch;
                if (!batches.ContainsKey(endpointKey))
                {
                    batch = new ArrayList();
                    batches.Add(endpointKey, batch);
                }
                else
                {
                    batch = (ArrayList)batches[endpointKey];
                }
                batch.Add(message);
            }

            return batches;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  thread to sort out the mess of a batch of messages for mixed endpoints 
        private void Worker ()
        {
            try
            {
                Hashtable batches = SortMessagesIntoBatches();

                foreach (DictionaryEntry entry in batches)
                {
                    this.batchWorker.BeginInvoke((string)entry.Key, (ArrayList)entry.Value, null, null);
                }
            }
            catch (Exception e)
            {
                this.transportProxy.SetErrorInfo(e);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  thread per endpoint batch
        private void BatchWorker (string outboundTransportLocation, ArrayList messages)
        {
            //  we did an enter for every message - so we should ensure we have a correspending leave
            int leaveCount = messages.Count;

            try
            {
                //  all the messages in this batch should have the same properties - so just take the first one
                IBaseMessage firstMessage = (IBaseMessage)messages[0];
                SystemMessageContext firstMessageContext = new SystemMessageContext(firstMessage.Context);
                TransactionalTransmitProperties properties = new TransactionalTransmitProperties(firstMessageContext.OutboundTransportLocation);

                string config = (string)firstMessage.Context.Read("AdapterConfig", this.propertyNamespace);
                if (config != null)
                {
                    // There is a configuration DOM so we are doing a Static Send
                    XmlDocument locationConfigDom = new XmlDocument();
                    locationConfigDom.LoadXml(config);
                    properties.LocationConfiguration(locationConfigDom);
                }
                else
                {
                    // Add dynamic send here
                }

                foreach (IBaseMessage message in messages)
                {
                    CommittableTransaction transaction = null;

                    try
                    {
                        //  create the System.Transactions transaction - this is not yet a DTC transaction
                        transaction = new CommittableTransaction();

                        //  give the CommittableTransaction to the batch and it will take the responsibility to Commit it.
                        //  the TransactionInterop.GetDtcTransaction call inside the base TxnBase class actually causes the
                        //  DTC transaction to be created - this can be observed in the COM+ Explorer while debugging this code
                        using (Batch batch = new TransactionalDeleteBatch(this.transportProxy, this.control, transaction))
                        {
                            //  note the options EnterpriseServicesInteropOption.Full in the future when resource managers
                            //  understand light weight transactions this might not be necessary but we need it for now
                            using (TransactionScope ts = new TransactionScope(transaction, TimeSpan.FromHours(1), EnterpriseServicesInteropOption.Full))
                            {
                                SendMessage(message, properties);

                                //  an exception will skip this next line
                                ts.Complete();
                            }

                            //  IMPORTANT: a Delete is part of the same transaction as the send operation
                            batch.DeleteMessage(message);

                            //  IMPORTANT: if there was a response to submit it would be added here
                            //  - in the same batch and same transaction as the Delete

                            batch.Done();
                        }
                    }
                    catch (Exception e)
                    {
                        // in this scenario we will explicitly Rollback the transaction on failure
                        if (transaction != null)
                            transaction.Rollback();

                        //  Remember to set the exception on the message itself - this will now appear in tracking in addition to the EventLog
                        message.SetErrorInfo(e);

                        //  Any failures need to be retried - but the change of state back on BizTalk is outside of the transaction
                        //  that has been used to attempt to send the message - after all that transaction will undoubtedly get rollback with the failure.
                        //  We say this batch is "non-transactional" from the adapter's point of view - though internally in BizTalk its still a transaction.
                        using (TransmitResponseBatch batch = new TransmitResponseBatch(this.transportProxy, new TransmitResponseBatch.AllWorkDoneDelegate(AllWorkDone)))
                        {
                            batch.Resubmit(message, false, null);
                            batch.Done();
                        }
                    }

                    // an exception will skip this line - an exception means the Done hasn't been called or was successful so we have a leave to do
                    leaveCount--;
                }
            }
            catch (Exception e)
            {
                this.transportProxy.SetErrorInfo(e);
            }
            finally
            {
                // perform any remain leaves - hopefully none - if everything was successful then leaveCount will be 0
                for (int i = 0; i < leaveCount; i++)
                    this.control.Leave();
            }
        }

        private void AllWorkDone()
        {
            this.control.Leave();
        }

        //  This function is running inside the TransactionScope and the SqlConnection should pick up the current transaction
        //  This will attempt to execute a stored procedure with a single parameter called @Data of type ntext
        private void SendMessage(IBaseMessage message, TransactionalTransmitProperties properties)
        {
            TextReader reader = new StreamReader(message.BodyPart.Data);
            string msgText = reader.ReadToEnd();

            string storedProcedureName = properties.CmdText;
            string connectionString = properties.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Data", msgText));
                command.ExecuteScalar();
            }
        }
    }
}
