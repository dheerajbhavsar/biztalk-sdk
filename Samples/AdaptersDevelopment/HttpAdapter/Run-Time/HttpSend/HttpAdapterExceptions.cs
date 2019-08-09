//---------------------------------------------------------------------
// File: HttpAdapterExceptions.cs
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
using System.Runtime.Serialization;
using Microsoft.Samples.BizTalk.Adapter.Common;

namespace Microsoft.Samples.BizTalk.Adapters.HttpTransmitter
{
	internal class HttpAdapterException : ApplicationException
	{
		public static string UnhandledTransmit_Error = "The .Net File Adapter encounted an error transmitting a batch of messages.";

		public HttpAdapterException () { }

		public HttpAdapterException (string msg) : base(msg) { }

		public HttpAdapterException (Exception inner) : base(String.Empty, inner) { }

		public HttpAdapterException (string msg, Exception e) : base(msg, e) { }

		protected HttpAdapterException (SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}

