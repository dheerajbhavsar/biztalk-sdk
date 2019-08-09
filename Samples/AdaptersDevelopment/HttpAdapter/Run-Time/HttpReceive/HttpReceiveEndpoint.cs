//---------------------------------------------------------------------
// File: HttpReceiveEndpoint.cs
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
using System.Diagnostics;
using Microsoft.BizTalk.TransportProxy.Interop;
using Microsoft.Samples.BizTalk.Adapter.Common;
using Microsoft.BizTalk.Component.Interop;

namespace Microsoft.Samples.BizTalk.Adapters.HttpReceive
{
    /// <summary>
    /// Summary description for HttpReceiveEndpoint.
    /// </summary>
    public class HttpReceiveEndpoint : ReceiverEndpoint
    {
        private HttpReceiveProperties    properties        = null;
        private string url;

        public HttpReceiveEndpoint()
        {
        }

        public override void Open(string uri, IPropertyBag config, IPropertyBag bizTalkConfig, IPropertyBag handlerPropertyBag, IBTTransportProxy transportProxy, string transportType, string propertyNamespace, ControlledTermination control)
        {
            this.url = uri;
            Trace.WriteLine(string.Format("HttpReceiveEndpoint.HttpReceiveEndpoint( url:{0} ) called", url), "HttpReceive: Info");

            this.properties = new HttpReceiveProperties(url);
            this.properties.LocationConfiguration(config, bizTalkConfig);
        }

        public override void Update(IPropertyBag config, IPropertyBag bizTalkConfig, IPropertyBag handlerPropertyBag)
        {
            Trace.WriteLine(string.Format("HttpReceiveEndpoint.Update( url:{0} ) called", url), "HttpReceive: Info" );

            // Read the new configuration...
            HttpReceiveProperties newProps = new HttpReceiveProperties(url);
            newProps.LocationConfiguration(config, bizTalkConfig);

            // Update the configuration...
            this.properties = newProps;
        }

        public HttpReceiveProperties Configuration
        { 
            get { return this.properties; }
        }
    }
}
