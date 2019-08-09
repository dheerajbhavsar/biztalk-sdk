//---------------------------------------------------------------------
// File: HttpReceiveWebHandler.cs
// 
// Sample: HttpAdapter
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
using System.Web;

/// <summary>
/// Summary description for HttpReceive
/// </summary>
/// 
namespace Microsoft.Samples.BizTalk.Adapters.HttpReceive
{

	public class HttpReceiveWebHandler : IHttpHandler
	{
		public HttpReceiveWebHandler()
		{
		}

		#region IHttpHandler Members

		public bool IsReusable
		{
			get { return false; }
		}

        public void ProcessRequest(HttpContext context)
        {
			// Get the BizTalk HTTP Receive Adapter from the application cache...
			HttpReceiveAdapter httpAdapter = (HttpReceiveAdapter)context.Application["HttpReceiveAdapter"];

			// Pass the HttpContext to the Http Receive Adapter to be processed...
			httpAdapter.ProcessRequest(context);
		}

		#endregion
	}
}