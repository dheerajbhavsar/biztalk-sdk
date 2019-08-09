///////////////////////////////////////////////////////////////////////////////
// File: MessageHelper.cs
// 
// Summary: Helper class to work with BizTalk message objects.
//
// Sample: SubmitDirect SDK sample 
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
using Microsoft.BizTalk.Message.Interop;

namespace Microsoft.Samples.BizTalk.Adapters.TransportProxyUtils
{
	/// <summary>
	/// Helper class to work with IBaseMessage object.
	/// </summary>
	public class MessageHelper
	{
		public MessageHelper()
		{
		}

		/// <summary>
		/// Creates IBaseMessage object from string
		/// </summary>
		/// <param name="mf">Reference to BizTalk message factory object</param>
		/// <param name="url">Address of receive location where this message will be submitted</param>
		/// <param name="data">Payload of the message</param>
		/// <returns>BizTalk message object</returns>
		public static IBaseMessage CreateMessage(IBaseMessageFactory mf, string url, string data)
		{
			IBaseMessagePart		part	= null; 
			IBaseMessageContext		ctx		= null;
			IBaseMessage			msg		= null;
			SystemMessageContext	smc		= null;

			// Write the data to a new stream...
			StreamWriter sw = new StreamWriter(new MemoryStream());
			sw.Write(data);
			sw.Flush();
			sw.BaseStream.Seek(0, SeekOrigin.Begin);

			// Create a new message
			msg = mf.CreateMessage();
			part = mf.CreateMessagePart();
			part.Data = sw.BaseStream;
			ctx = msg.Context;
			msg.AddPart("body", part, true);

			// Set the system context properties
			smc = new SystemMessageContext(ctx);
			if ( null != url )
				smc.InboundTransportLocation = url;

			return msg;
		}

		/// <summary>
		/// Creates IBaseMessage object from stream
		/// </summary>
		/// <param name="mf">Reference to the BizTalk message factory</param>
		/// <param name="url">Address of receive location where this message will be submitted to</param>
		/// <param name="charset">Charset of the date</param>
		/// <param name="data">Message payload</param>
		/// <returns>BizTalk message object</returns>
		public static IBaseMessage CreateMessage(IBaseMessageFactory mf, string url, string charset, Stream data)
		{
			IBaseMessagePart		part	= null; 
			IBaseMessageContext		ctx		= null;
			IBaseMessage			msg		= null;
			SystemMessageContext	smc		= null;

			// Create a new message
			msg = mf.CreateMessage();
			part = mf.CreateMessagePart();
			part.Data = data;
			part.Charset = charset;
			ctx = msg.Context;
			msg.AddPart("body", part, true);

			// Set the system context properties
			smc = new SystemMessageContext(ctx);
			if ( null != url )
				smc.InboundTransportLocation = url;

			return msg;
		}

		/// <summary>
		/// Creates an array of IBaseMessage objects from array of strings
		/// </summary>
		/// <param name="mf">Reference to BizTalk message factory object</param>
		/// <param name="url">Address of receive location where this message will be submitted to</param>
		/// <param name="data">Payloads for each message</param>
		/// <returns>Array of BizTalk message objects</returns>
		public static IBaseMessage[] CreateMessages(IBaseMessageFactory mf, string url, string[] data)
		{
			IBaseMessagePart		part	= null; 
			IBaseMessageContext		ctx		= null;
			IBaseMessage[]			msgs	= null;
			SystemMessageContext	smc		= null;

			msgs = new IBaseMessage[data.Length];

			for ( int c = 0; c < data.Length; c++ )
			{
				// Write the data to a new stream...
				StreamWriter sw = new StreamWriter(new MemoryStream());
				sw.Write(data[c]);
				sw.Flush();
				sw.BaseStream.Seek(0, SeekOrigin.Begin);

				// Create a new message
				msgs[c] = mf.CreateMessage();
				part = mf.CreateMessagePart();
				part.Data = sw.BaseStream;
				ctx = msgs[c].Context;
				msgs[c].AddPart("body", part, true);

				// Set the system context properties
				smc = new SystemMessageContext(ctx);
				if ( null != url )
					smc.InboundTransportLocation = url;
			}

			return msgs;
		}

		/// <summary>
		/// Creates IBaseMessage objects from array of streams
		/// </summary>
		/// <param name="mf">Reference to BizTalk message factory object</param>
		/// <param name="url">Address of receive location where this message will be submitted to</param>
		/// <param name="data">Payloads for each message</param>
		/// <returns>Array of BizTalk message objects</returns>
		public static IBaseMessage[] CreateMessages(IBaseMessageFactory mf, string url, Stream[] data)
		{
			IBaseMessagePart		part	= null; 
			IBaseMessageContext		ctx		= null;
			IBaseMessage[]			msgs	= null;
			SystemMessageContext	smc		= null;

			msgs = new IBaseMessage[data.Length];

			for ( int c = 0; c < data.Length; c++ )
			{
				// Create a new message
				msgs[c] = mf.CreateMessage();
				part = mf.CreateMessagePart();
				part.Data = data[c];
				ctx = msgs[c].Context;
				msgs[c].AddPart("body", part, true);

				// Set the system context properties
				smc = new SystemMessageContext(ctx);
				if ( null != url )
					smc.InboundTransportLocation = url;
			}

			return msgs;
		}
	}
}
