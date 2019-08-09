//---------------------------------------------------------------------
// File: HttpAdapterWorkItem.cs
// 
// Summary: Implementation of an adapter framework sample adapter. 
//
// Sample: HTTP Transmit Adapter, demonstrating solicit-response.
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
using System.IO;
using System.Xml;
using System.Text;
using System.Net;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

using Microsoft.BizTalk.Streaming;
using Microsoft.BizTalk.TransportProxy.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.Samples.BizTalk.Adapter.Common;
using Microsoft.XLANGs.BaseTypes;


namespace Microsoft.Samples.BizTalk.Adapters.HttpTransmitter
{
    /// <summary>
    /// There is one instance of HttpTransmitterEndpoint class for each every static send port.
    /// Messages will be forwarded to this class by AsyncTransmitterBatch (via HttpTransmitAdapterBatch)
    /// </summary>
    internal class HttpTransmitterEndpoint: AsyncTransmitterEndpoint
    {
        private IBaseMessage    solicitMsg                    = null;
        private IBTTransportProxy    transportProxy                = null;
        private AsyncTransmitter    asyncTransmitter            = null;
        private string propertyNamespace;
        private static int IO_BUFFER_SIZE = 4096;

        public HttpTransmitterEndpoint(AsyncTransmitter asyncTransmitter) : base(asyncTransmitter)
        {
            this.asyncTransmitter = asyncTransmitter;
            this.transportProxy = asyncTransmitter.TransportProxy;
        }

        public override void Open(EndpointParameters endpointParameters, IPropertyBag handlerPropertyBag, string propertyNamespace)
        {
            this.propertyNamespace = propertyNamespace;
        }


        /// <summary>
        /// Implementation for AsyncTransmitterEndpoint::ProcessMessage
        /// Transmit the message and optionally return the response message
        /// </summary>
        public override IBaseMessage ProcessMessage(IBaseMessage message)
        {
            this.solicitMsg = message;

            HttpAdapterProperties props = new HttpAdapterProperties(message, this.propertyNamespace);
            IBaseMessage responseMsg = null;
            
            if ( props.IsTwoWay )
            {
                WebResponse response = SendHttpRequest(this.solicitMsg, props);
                responseMsg = BuildResponseMessage(response);
            }
            else
            {
                WebResponse response = SendHttpRequest(this.solicitMsg, props);
                response.Close();
            }

            return responseMsg;
        }

        private IBaseMessage BuildResponseMessage(WebResponse webResponse)
        {
            IBaseMessage btsResponse = null;

            // Get the response stream, create a new message attaching
            // the response stream...
            using (Stream s = webResponse.GetResponseStream())
            {
                // NOTE: 
                // Copy the network stream into a virtual stream stream. If we were
                // to use a forward only stream (as in the response stream) we would 
                // not be able to suspend the response data on failure. The virtual 
                // stream will overflow to disc when it reaches a given threshold
                VirtualStream vs = new VirtualStream();
                int bytesRead = 0;
                byte[] buff = new byte[8 * 1024];
                while ((bytesRead = s.Read(buff, 0, buff.Length)) > 0)
                    vs.Write(buff, 0, bytesRead);

                webResponse.Close();

                // Seek the stream back to the start...
                vs.Position = 0;

                // Build BTS message from the stream
                IBaseMessageFactory mf = transportProxy.GetMessageFactory();
                btsResponse = mf.CreateMessage();
                IBaseMessagePart body = mf.CreateMessagePart();
                body.Data = vs;
                btsResponse.AddPart("Body", body, true);
            }

            return btsResponse;
        }

        private WebResponse SendHttpRequest(IBaseMessage msg, HttpAdapterProperties config)
        {
            #region SSO Usage Example
            // If we needed to use SSO to lookup the remote system credentials
            /*
            string ssoAffiliateApplication = props.AffiliateApplication;
            if (ssoAffiliateApplication.Length > 0)
            {
                SSOResult ssoResult = new SSOResult(msg, affiliateApplication);
                string userName  = ssoResult.UserName;
                string password  = ssoResult.Result[0];
                string etcEtcEtc = ssoResult.Result[1]; // (you can have additional metadata associated with the login in SSO) 
                    
                // use these credentials to login to the remote system
                // ideally zero off the password memory once we are done
            }                    
            */
            #endregion // SSO Usage Example

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(config.Uri);
            request.Method = config.HttpVerb;
            request.Timeout = config.Timeout;

            // Configure the authentication type...
            if (HttpAdapterProperties.AuthenticationType.KerberosAuthentication == config.AuthType)
            {
                request.AllowWriteStreamBuffering = true;
                request.PreAuthenticate = false;
                request.Credentials = CredentialCache.DefaultCredentials;
            }
            else
            {
                request.AllowWriteStreamBuffering = false;
                request.PreAuthenticate = true;
            }

            // Note: both the body part and the stream maybe null, so we need to 
            // check for them
            string charset = string.Empty;
            IBaseMessagePart bodyPart = msg.BodyPart;
            Stream btsStream;

            if (null != bodyPart && (null != (btsStream = bodyPart.GetOriginalDataStream())))
            {
                charset = bodyPart.Charset;
                string contentTypeCharset = config.ContentType;

                if (contentTypeCharset.ToLowerInvariant().StartsWith("text/"))
                {
                    // Append charset information (if it's not there already)!
                    if (charset != null && charset.Length > 0 && contentTypeCharset.ToLowerInvariant().IndexOf("charset=") == -1)
                        contentTypeCharset += "; charset=" + charset;
                }
                request.ContentType = contentTypeCharset;
                
                // Wrap the BTS stream in the seekable stream to obtain the content-legnth!
                btsStream = new VirtualStream(btsStream);
                request.ContentLength = btsStream.Length;

                // Get the request stream and write the data into it...
                Stream webStream = request.GetRequestStream();
                int bytesRead = 0;
                byte[] buff = new byte[IO_BUFFER_SIZE];
                while ((bytesRead = btsStream.Read(buff, 0, IO_BUFFER_SIZE)) > 0)
                    webStream.Write(buff, 0, bytesRead);

                webStream.Flush();
                webStream.Close();
            }

            return request.GetResponse();
        }
    }
}
