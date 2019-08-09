//---------------------------------------------------------------------
// File: SubmitRequest.cs
// 
// Summary: A Sample of how to submit a request message into the server
//          and get a syncronous response.
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
using System.IO;
using System.Xml;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.Samples.BizTalk.Adapters.TransportProxyUtils;

namespace Microsoft.Samples.BizTalk.Adapters.SubmitRequest
{
	/// <summary>
	/// Submits a request XML message from the file into the server
	/// and prints out the response XML message.
	/// </summary>
	/// <remarks>
	/// This class loads the XML message from the file and submits it into the 
	/// server for orchestration schedule to process it. Then it gets response 
	/// message from orchestration and prints it out in console.
	/// </remarks>
	class Submitter
	{
		static void Main(string[] args)
		{
			IBaseMessage responseMsg = null;
			// Address of receive locations where to submit message to
			const string submitURI = "submit://submitrequest";

			// Check parameters
			if (0 == args.Length) 
			{
				Usage();
				return;
			}

			//Check if file exists
			if (!File.Exists(args[0])) 
			{
				Console.WriteLine("{0} does not exist.", args[0]);
				return;
			}
			
			// Create BizTalk message submission object
			BizTalkMessaging btm = new BizTalkMessaging();
			// Open the file stream
			FileStream fileStream = File.Open(args[0], System.IO.FileMode.Open);
			
			try
			{
				// Submit message and wait for response
				// The charset information is empty because it will be determined by XML disassembler from BOM of file
				responseMsg = btm.SubmitSyncMessage(btm.CreateMessageFromStream(submitURI, null, fileStream));

				// Get an XML from response and print it out in console window
				if (null != responseMsg)
				{
					if ((responseMsg.BodyPart!=null) && (responseMsg.BodyPart.Data!=null))
					{
						StreamReader sr = new StreamReader(responseMsg.BodyPart.Data);
						Console.WriteLine("Response message:");
						Console.WriteLine(sr.ReadToEnd());
					}
				}
			}
			catch(Exception e)
			{
				Console.WriteLine(e.ToString());
			}
			finally
			{
				// Close the message submission object
				btm.Close(); 
				
				// Close file stream
				fileStream.Close();
			}
		}
		
		/// <summary>
		/// Print out usage instructions
		/// </summary>
		static private void Usage()
		{
			Console.WriteLine("Usage: SubmitRequest <file path>");
		}
	}
}