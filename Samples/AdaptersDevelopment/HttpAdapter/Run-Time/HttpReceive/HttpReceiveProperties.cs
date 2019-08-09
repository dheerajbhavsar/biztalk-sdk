//---------------------------------------------------------------------
// File: HttpReceiveProperties.cs
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
using System.Xml;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.Samples.BizTalk.Adapter.Common;

namespace Microsoft.Samples.BizTalk.Adapters.HttpReceive
{
    /// <summary>
    /// Summary description for HttpReceiveProperties.
    /// </summary>
    public class HttpReceiveProperties : ConfigProperties
    {
        private string    uri                        = null;
        private bool    twoWay                    = false;
        private bool    loopBack                = false;
        private string    returnContentType        = "text/xml";
        private long    errorThreshold            = 10;
        private int requestTimeout = 90; // Default request-response-timeout = 90 seconds

        public HttpReceiveProperties(string url)
        {
        }

        public void LocationConfiguration(IPropertyBag config, IPropertyBag bizTalkConfig)
        {
            XmlDocument endpointConfig = ExtractConfigDomImpl(config, true);

            this.uri                = Extract(endpointConfig, "/Config/uri", String.Empty);
            this.loopBack            = IfExistsExtractBool(endpointConfig, "/Config/loopBack", false);
            this.returnContentType    = Extract(endpointConfig, "/Config/returnContentType", "text/xml");
            this.errorThreshold        = IfExistsExtractLong (endpointConfig, "/Config/errorThreshold", 10);

            // The property "TwoWayReceivePort" in the BizTalk Config
            // property bag indicates whether the port is one or two way...
            object obj = null;
            bizTalkConfig.Read("TwoWayReceivePort", out obj, 0);
            if ( null != obj )
                this.twoWay = (bool)obj;
        }

        public string VirtualDirectory 
        { 
            get {return this.uri;} 
        }

        public bool PortIsTwoWay 
        { 
            get {return this.twoWay;} 
        }

        public bool LoopBack 
        { 
            get {return this.loopBack;} 
        }

        public string ReturnContentType 
        {
            get { return this.returnContentType;} 
        }

        public long ErrorThreshold 
        {
            get {return this.errorThreshold;} 
        }

        public int Timeout
        {
            get { return requestTimeout; }
        }
    }
}
