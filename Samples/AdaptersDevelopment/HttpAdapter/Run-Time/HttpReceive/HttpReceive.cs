//---------------------------------------------------------------------
// File: HttpReceive.cs
// 
// Summary: Implementation of an adapter framework sample adapter. 
//
// Sample: HTTP Receive Adapter, demonstrating request-response and 
//           isolated receiver
//
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
using System.Collections;
using System.Web;
using System.IO;
using System.Threading;
using System.Diagnostics;

using Microsoft.BizTalk.TransportProxy.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Streaming;
using Microsoft.Samples.BizTalk.Adapter.Common;
using Microsoft.XLANGs.BaseTypes;


namespace Microsoft.Samples.BizTalk.Adapters.HttpReceive
{
    /// <summary>
    /// Utility class that implements the messaging engine contracts for dealing with
    /// the response message. It is used to wait for the response message (with timeout),
    /// retrieve the response message and notify the status of the transmission.
    /// The response message must be deleted from BizTalk message box, after it's transmitted
    /// successfully. If there is an error while processing the response message or if there is 
    /// a timeout, the response message should be suspeded.
    /// </summary>
    public sealed class HttpResponseHandler : IBTTransmitter
    {
        private AutoResetEvent responseEvent = new AutoResetEvent(false);
        private IBaseMessage message;
        private IBTTransportProxy proxy;

        private bool timeoutExceeded = false;

        public HttpResponseHandler(IBTTransportProxy proxy) { this.proxy = proxy; }
        public void Initialize(IBTTransportProxy proxy) {}
        public void Terminate() { }

        // This will be called by the messaging engine when the response becomes available
        public bool TransmitMessage(IBaseMessage msg) 
        {
            // Check if the timeout has happened, if so, reject the response.
            // Messaging engine will suspend this message
            if (timeoutExceeded)
                throw new InvalidOperationException("The response message did not arrive in time");

            this.message = msg;

            // Notify the thread calling "WaitForResponse"
            responseEvent.Set();
            return false; 
        }

        public IBaseMessage WaitForResponse(int timeoutSeconds)
        {
            if (responseEvent.WaitOne(timeoutSeconds * 1000, false))
            {
                // This means, the response is avaialble (i.e. EPM called transmit message
                // while we were waiting)
                return this.message;
            }

            // Wait() failed. That means the request timed out
            this.timeoutExceeded = true;
            return null;
        }

        public void ResponseConsumed(Exception transmitException)
        {
            if (this.message == null)
            {
                throw new InvalidOperationException("There is no response to consume");
            }

            Batch batch = new TransmitResponseBatch(proxy, null);
            if (transmitException == null)
            {
                batch.DeleteMessage(this.message);
            }
            else
            {
                this.message.SetErrorInfo(transmitException);
                batch.MoveToSuspendQ(this.message);
            }
            batch.Done();
        }
    }

    /// <summary>
    /// Main class for HTTP receive adapters. It provides the implementations of
    /// core interfaces needed to comply with receiver adapter contract.
    /// (1) This class is actually a Singleton. That is there will only ever be one
    /// instance of it created however many locations of this type are actually defined.
    /// (2) Individual locations are identified by a URI and are associated with HttpReceiveEndpoint
    /// (3) It is legal to have messages from different locations submitted in a single
    /// batch and this may be an important optimization. And this is fundamentally why
    /// the Receiver is a singleton.
    /// </summary>
    sealed public class HttpReceiveAdapter : Receiver

    {
        private IBTTransportProxy        transportProxy            = null;
        private IBaseMessageFactory msgFactory = null;

        private const string                    protocol                = "HTTP.NET";
        private const string description = "ASP.NET HTTP Receive Adapter SDK Sample";
        private static readonly Guid clsid = new Guid("883C611E-1505-4c55-96D7-E0F5ABF7E62C");
        private ControlledTermination    terminator                = new ControlledTermination();
        private const int                IO_BUFFER_SIZE = 8192;  // 8K
        private bool adapterRegistered = false;

        private static readonly PropertyBase InboundTransportLocationProperty = new BTS.InboundTransportLocation();
        private static readonly PropertyBase InboundTransportTypeProperty = new BTS.InboundTransportType();
        private static readonly PropertyBase LoopBackProperty = new BTS.LoopBack();
        private static readonly PropertyBase WindowsUserProperty = new BTS.WindowsUser();

        public HttpReceiveAdapter()
            : base(protocol, "1.0", description, protocol, clsid, null, typeof(HttpReceiveEndpoint))
        {
            // Since we are an Isolated Adapter, we need to create
            // this Adapters Transport Proxy and initialize it with
            // one of our urls...
            this.transportProxy = (IBTTransportProxy)new BTTransportProxy();
            base.Initialize(this.transportProxy);
        }

        private void Init(string url)
        {
            if (true == this.adapterRegistered)
                return;

            Trace.WriteLine(string.Format("HttpReceiveAdapter.Init( url:{0} ) called", url),"HttpReceive: Info" );

            lock(this)
            {
                if (false == this.adapterRegistered)
                {
                    // Register the adapter with messaging engine
                    this.transportProxy.RegisterIsolatedReceiver(url, (IBTTransportConfig)this);
                    this.msgFactory = this.transportProxy.GetMessageFactory();
                    this.adapterRegistered = true;
                }
            }
        }

        private void Term()
        {
            if (false == this.adapterRegistered)
                return;

            lock(this)
            {
                if (true == this.adapterRegistered)
                {
                    this.terminator.Terminate();

                    // Inform this adapter's Transport Proxy that this
                    // Adapter is shutting down...
                    this.transportProxy.TerminateIsolatedReceiver();
                }
            }
        }

        // Request processing method...
        public void ProcessRequest(HttpContext context)
        {
            string uri = context.Request.RawUrl.ToLower();;
            Init(uri);

            Trace.WriteLine(string.Format("HttpReceiveAdapter.ProcessRequest( uri:{0} ) called", uri),"HttpReceive: Info" );

            HttpReceiveEndpoint ep = base.GetEndpoint(uri) as HttpReceiveEndpoint;

            if (null == ep)
                throw new ApplicationException("BizTalk HTTP receive adapter failed to initialize itself. Possible reasons:\n" +
                                "1) Receive location URL is not created/configured correctly\n" +
                                "2) Receive location is not enabled\n" +
                                "3) HTTP receive adapter is not running under a user that has access to management and message databases\n" +
                                "4) Isolated host instance is not created for HTTP Receive adapter.\n");

            // Determine if the port is one or two and handle accordingly...
            if ( ep.Configuration.PortIsTwoWay )
            {
                ProcessRequestResponse(uri, context, ep);
            }
            else
            {
                ProcessRequest(uri, context, ep);
            }
        }

        private void ProcessRequestResponse(string uri, HttpContext context, HttpReceiveEndpoint ep)
        {
            // Create a new message...
            IBaseMessage msg = ConstructMessage(uri, context, ep);

            // Use the request-response handler to send the request message
            SyncReceiveSubmitBatch batch = new SyncReceiveSubmitBatch(this.transportProxy, terminator, 1);
            
            DateTime expDateTime = DateTime.Now.AddSeconds(ep.Configuration.Timeout);
            HttpResponseHandler responseHandler = new HttpResponseHandler(this.transportProxy);

            // Do two-way submit operation: EPM should generate a response in this case
            batch.SubmitRequestMessage(msg, Guid.NewGuid().ToString("D"), true, expDateTime, responseHandler);
            
            // Done will call "Enter" on the terminator to ensure proper ref-counting
            batch.Done();

            // Wait for the callback (this should always complete quickly)
            if (!batch.Wait())
            {
                throw new HttpException(500, "Failed to submit the Http request to BizTalk Server");
            }

            if(batch.FailedMessages.Count != 0)
            {
                throw new HttpException(500, "Failed to submit the Http request to BizTalk Server");
            }

            // It's a good idea to have a timeout on the response message
            IBaseMessage responseMsg = responseHandler.WaitForResponse(ep.Configuration.Timeout);
            if (responseMsg == null)
            {
                // Response did not arrive in time!
                throw new HttpException(500, "Failed to retrieve the Http response message from BizTalk Server");
            }

            // Copy the data from the response message into the HTTP response stream. 
            Exception transmitException = null;
            try
            {
                using (Stream httpStream = context.Response.OutputStream)
                {
                    // Set the response content-type (ideally, this should be extracted from message)
                    context.Response.ContentType = ep.Configuration.ReturnContentType;

                    Stream btsStream = responseMsg.BodyPart.GetOriginalDataStream();

                    // Note, the data is handled in a streaming fashion to 
                    // prevent us going out of memory for large messages.
                    int bytesRead = 0;
                    byte[] buff = new byte[IO_BUFFER_SIZE];
                    while ((bytesRead = btsStream.Read(buff, 0, IO_BUFFER_SIZE)) > 0)
                        httpStream.Write(buff, 0, bytesRead);

                    httpStream.Flush();
                }
            }
            catch (Exception ex)
            {
                transmitException = ex;
            }
            finally
            {
                // After processing the response message successfully, it MUST be deleted
                // from BizTalk message box (a.k.a. Application Queue). Otherwise, the message 
                // remain in the queue forever causing the system to become unresponsive. 
                responseHandler.ResponseConsumed(transmitException);
            }
        }

        private void ProcessRequest(string uri, HttpContext context, HttpReceiveEndpoint ep)
        {
            // Create a new message...
            IBaseMessage msg = ConstructMessage(uri, context, ep);

            // Submit the message using the StandardReceiveBatchHandler
            SyncReceiveSubmitBatch batch = new SyncReceiveSubmitBatch(transportProxy, terminator, 1);

            // Do one-way-submit
            batch.SubmitMessage(msg, null);
            batch.Done();

            if ( !batch.Wait() )
            {
                Trace.WriteLine("HttpReceiveAdapter.ProcessRequest(): Failed to process the Http Request!","HttpReceive: Error" );
                throw new HttpException(400, "Failed to process the Http Request");
            }

            context.Response.StatusCode = 202;
        }

        IBaseMessage ConstructMessage(string uri, HttpContext context, HttpReceiveEndpoint ep)
        {
            // Create a new message...
            IBaseMessage msg = this.msgFactory.CreateMessage();
            IBaseMessagePart body = this.msgFactory.CreateMessagePart();
            msg.AddPart("Body", body, true);

            // Attach the request stream to the message body part...
            Stream stream = context.Request.InputStream;
            if (!stream.CanSeek)
            {
                // The stream must be seekable for property error handling!
                stream = new VirtualStream(stream);
            }
            msg.BodyPart.Data = stream;

            // Determine the charset and content type and stamp on the message context...
            body.ContentType = context.Request.ContentType;
            body.Charset = context.Request.ContentEncoding.WebName;

            // If we have a Windows User, stamp it on the msg ctx...
            string user = context.User.Identity.Name;
            if(user.Length > 0)
            {
                msg.Context.Write(WindowsUserProperty.Name.Name, WindowsUserProperty.Name.Namespace, user);
            }
            
            // Note: this sample does not handle certificates. If it did it would 
            // stamp the Cert Thumb Print ont he message context property: SignatureCertificate

            // Promote the InboundTransportLocation and InboundTransportType
            msg.Context.Promote(InboundTransportLocationProperty.Name.Name, InboundTransportLocationProperty.Name.Namespace, uri );
            msg.Context.Promote(InboundTransportTypeProperty.Name.Name, InboundTransportTypeProperty.Name.Namespace, protocol );

            // If Loopback is configured, stamp it on the message context...
            if (ep.Configuration.LoopBack && ep.Configuration.PortIsTwoWay)
            {
                msg.Context.Write(LoopBackProperty.Name.Name, LoopBackProperty.Name.Namespace, true);
            }

            return msg;
        }
    }
}

