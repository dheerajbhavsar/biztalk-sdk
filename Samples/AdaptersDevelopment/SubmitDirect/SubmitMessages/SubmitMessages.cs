//---------------------------------------------------------------------
// File: SubmitMessages.cs
// 
// Summary: A Sample of how to submit messages into the server.
//
// Sample: SubmitDirect SDK 
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
using Microsoft.BizTalk.Message.Interop;
using Microsoft.Samples.BizTalk.Adapters.TransportProxyUtils;

namespace Microsoft.Samples.BizTalk.Adapters.SubmitMessages
{
	/// <summary>
	/// Submits an array of string messages into the server
	/// </summary>
	/// <remarks>
	/// This class gets an array of strings from the command line and 
	/// submit each string as a BizTalk message 
	/// </remarks>
	class Submitter
	{
		// Address to submit messages to
		const string submitURI = @"submit://submitmessages";

		static void Main(string[] args)
		{
			// Check arguments from command line
			if (0 == args.Length) 
			{
				Usage();
				return;
			}
			
			// Create message submission object
			BizTalkMessaging btm = new BizTalkMessaging();

			// Create the array of messages to submit 
			IBaseMessage[] msgs = new IBaseMessage[args.Length];
			try
			{
				// Populate the array of messages
				for (int i = 0; i < args.Length; i++)
					msgs[i] = btm.CreateMessageFromString(submitURI, args[i]);

				// Submit the messages
				btm.SubmitMessages(msgs);
				Console.WriteLine("Messages have been successfully submitted into the receive location \"{0}\"", submitURI);
			}
			catch(Exception e)
			{
				Console.WriteLine(e.ToString());				
			}
			finally
			{
				// Close the message submission object
				btm.Close();
			}
		}

		/// <summary>
		/// Print out the usage instructions
		/// </summary>
		static private void Usage()
		{
			Console.WriteLine("Usage: SubmitMessages <list of messages>");
		}
	}
}