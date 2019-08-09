//---------------------------------------------------------------------
// File: HttpAdapterProperties.cs
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
using System.Net;

using Microsoft.XLANGs.BaseTypes;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.Samples.BizTalk.Adapter.Common;

namespace Microsoft.Samples.BizTalk.Adapters.HttpTransmitter
{
    /// <summary>
    /// This class maintains send port properties associated with a message. These properties
    /// will be extracted from the message context for static send ports.
    /// </summary>
    public class HttpAdapterProperties : ConfigProperties
    {
        private static string[] HttpVerbs = new string[] { "GET", "HEAD", "POST" };
        private static string protocolAlias = "HTTP.NET://";

        public enum AuthenticationType
        {
            AnonymousAuthentication,
            KerberosAuthentication
        }

        // Handler properties...
        private static int                handlerTimeout    = 120000;

        // Endpoint properties...
        private string                    httpMethod;
        private int                        timeout            = HttpAdapterProperties.handlerTimeout;
        private string                    contentType        = "text/xml";
        private AuthenticationType        authType        = 0;
        private string uri;
        public string Uri { get { return uri; } }

        private static PropertyBase isSolicitResponseProp = new BTS.IsSolicitResponse();

        // If we needed to use SSO we will need this extra property. It is set in the
        // LocationConfiguration method below.
        // Additionally:
        //   TransmitLocation.xsd in the design-time project must also be edited to
        //   expose the necessary SSO properties.
        //   DotNetFileAsyncTransmitterBatch.cs within the run-time project must be
        //   edited to retrieve and populate the SSOResult class.
        //private string ssoAffiliateApplication;
        //public string AffiliateApplication { get { return ssoAffiliateApplication; } }

        private bool _isTwoWay;

        public bool IsTwoWay { get { return _isTwoWay; } }

        public HttpAdapterProperties(IBaseMessage message, string propertyNamespace)
        {
            XmlDocument locationConfigDom = null;

            //  get the adapter configuration off the message
            string config = (string) message.Context.Read("AdapterConfig", propertyNamespace);
            this._isTwoWay = (bool) message.Context.Read(isSolicitResponseProp.Name.Name, isSolicitResponseProp.Name.Namespace);

            //  the config can be null all that means is that we are doing a dynamic send
            if (null != config)
            {
                locationConfigDom = new XmlDocument();
                locationConfigDom.LoadXml(config);

                //  For Dynamic Sends the Location config can be null
                //  Location properties - possibly override some handler properties
                this.LocationConfiguration(locationConfigDom);
            }
        }

        public HttpAdapterProperties(string uri)
        {
            this.uri = uri;
            UpdateUriForDynamicSend();
        }

        public string HttpVerb
        {
            get { return this.httpMethod; }
        }

        public int Timeout
        {
            get { return this.timeout; }
        }

        public string ContentType
        {
            get { return this.contentType; }
        }

        public AuthenticationType AuthType
        {
            get { return this.authType; }
        }

        public static void TransmitHandlerConfiguration (XmlDocument configDOM)
        {
            // Handler properties
            HttpAdapterProperties.handlerTimeout = ExtractInt(configDOM, "/Config/timeout");
        }

        public void LocationConfiguration (XmlDocument configDOM)
        {
            // Uri must exist (all static send port scenarios!)
            this.uri = Extract(configDOM, "Config/uri", null);

            this.httpMethod = ExtractHttpVerb(configDOM);
            this.timeout = IfExistsExtractInt(configDOM, "/Config/timeout", 0);
            this.contentType = Extract(configDOM, "/Config/contentType", string.Empty);
            this.authType = ExtractAuthType(configDOM);
        }

        private AuthenticationType ExtractAuthType(XmlDocument configDOM)
        {
            AuthenticationType authType = AuthenticationType.KerberosAuthentication;

            string hv = IfExistsExtract(configDOM, "/Config/authType", "1");

            switch (hv) 
            {
                case "0":  
                    authType = AuthenticationType.KerberosAuthentication;
                    break;
                case "1":
                    authType = AuthenticationType.AnonymousAuthentication;
                    break;
                default:
                    authType = AuthenticationType.KerberosAuthentication;
                    break;
            }

            return authType;
        }

        private string ExtractHttpVerb(XmlDocument configDOM)
        {
            string httpVerb;

            string hv = IfExistsExtract(configDOM, "/Config/method", "2");

            switch (hv) 
            {
                case "0":  
                    httpVerb = HttpVerbs[0];
                    break;

                case "1":      
                    httpVerb = HttpVerbs[1];
                    break;

                case "2":      
                    httpVerb = HttpVerbs[2];
                    break;

                default:
                    httpVerb = HttpVerbs[2];
                    break;
            }

            return httpVerb;
        }

        public void UpdateUriForDynamicSend()
        {
            // Strip off the adapters alias
            if ( this.uri.StartsWith(protocolAlias, StringComparison.OrdinalIgnoreCase))
            {
                string newUri = this.uri.Substring(protocolAlias.Length);
                this.uri = newUri;
            }
        }
    }
}
