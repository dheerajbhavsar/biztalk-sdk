//---------------------------------------------------------------------
// File: DotNetFileTransmitter.cs
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
using Microsoft.BizTalk.TransportProxy.Interop;

namespace Microsoft.BizTalk.SDKSamples.Adapters
{
	/// <summary>
	/// This is a singleton class for DotNetFile send adapter. All the messages, going to various
	/// send ports of this adapter type, will go through this class.
	/// </summary>
	public class DotNetFileTransmitter : AsyncTransmitter
	{
		internal static string DOTNET_FILE_NAMESPACE = "http://schemas.microsoft.com/BizTalk/2003/SDK_Samples/Messaging/Transports/dotnetfile-properties";

		public DotNetFileTransmitter() : base(
			".Net FILE Transmit Adapter",
			"1.0",
			"Send files form BizTalk to disk",
			".NetFILE",
			new Guid("024DB758-AAF9-415e-A121-4AC245DD49EC"),
			DOTNET_FILE_NAMESPACE,
			typeof(DotNetFileTransmitterEndpoint),
			DotNetFileTransmitProperties.BatchSize)
		{
		}
	
		protected override void HandlerPropertyBagLoaded ()
		{
			IPropertyBag config = this.HandlerPropertyBag;
			if (null != config)
			{
				XmlDocument handlerConfigDom = ConfigProperties.IfExistsExtractConfigDom(config);
				if (null != handlerConfigDom)
				{
					DotNetFileTransmitProperties.ReadTransmitHandlerConfiguration(handlerConfigDom);
				}
			}
		}
	}
}
