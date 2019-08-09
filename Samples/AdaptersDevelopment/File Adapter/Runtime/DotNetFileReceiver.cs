//---------------------------------------------------------------------
// File: DotNetFileReceiver.cs
// 
// Summary: Implementation of an adapter framework sample adapter. 
//
// Sample: Adapter framework runtime adapter.
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

namespace Microsoft.BizTalk.SDKSamples.Adapters
{
    /// <summary>
    /// Main class for DotNetFile receive adapters. It provides the implementations of
    /// core interfaces needed to comply with receiver adapter contract.
    /// (1) This class is actually a Singleton. That is there will only ever be one
    /// instance of it created however many locations of this type are actually defined.
    /// (2) Individual locations are identified by a URI and are associated with DotNetFileReceiverEndpoint
    /// (3) It is legal to have messages from different locations submitted in a single
    /// batch and this may be an important optimization. And this is fundamentally why
    /// the Receiver is a singleton.
    /// </summary>
    public class DotNetFileReceiver : Receiver 
    {
        public DotNetFileReceiver () : base(
            ".Net FILE Receive Adapter",
            "1.0",
            "Submits files from disk into BizTalk",
            ".NetFILE",
            new Guid("3D4B599E-2202-4bbb-9FC6-7ACA3906E5DE"),
            "http://schemas.microsoft.com/BizTalk/2003/dotnetfile-properties",
            typeof(DotNetFileReceiverEndpoint))
        {
        }
        /// <summary>
        /// This function is called when BizTalk runtime gives the handler properties to adapter.
        /// </summary>
        protected override void HandlerPropertyBagLoaded ()
        {
            IPropertyBag config = this.HandlerPropertyBag;
            if (null != config)
            {
                XmlDocument handlerConfigDom = ConfigProperties.IfExistsExtractConfigDom(config);
                if (null != handlerConfigDom)
                {
                    DotNetFileReceiveProperties.ReceiveHandlerConfiguration(handlerConfigDom);
                }
            }
        }
    }
}
