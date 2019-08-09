//---------------------------------------------------------------------
// File: App.cs
// 
// Summary: implementation of the SendLargeMessage
//
//---------------------------------------------------------------------
// This file is part of the Microsoft BizTalk Server SDK
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// This source code is intended only as a supplement to Microsoft BizTalk
// Server 2016 release and/or on-line documentation. See these other
// materials for detailed information regarding Microsoft code samples.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
// PURPOSE.
//---------------------------------------------------------------------

using System;
using System.IO;

using LargeMessages;


namespace SendLargeMessage
{
	/// <summary>
	/// The application class
	/// </summary>
	class App
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			if (args.Length < 2) 
			{
				PrintUsage();
				return;
			}

			string queueFormatName = args[0];
			string fileName = args[1];

			LargeMessage message = 
				new LargeMessage(new FileStream(fileName, FileMode.Open, FileAccess.Read));

			LargeMessageQueue queue = 
				new LargeMessageQueue(queueFormatName);

			queue.Open();
			try 
			{
				queue.Send(message);
			} 
			finally 
			{
				queue.Close();
			}
		}

		/// <summary>
		/// Prints usage
		/// </summary>
		static void PrintUsage() 
		{
			Console.WriteLine(@"Usage:SendLargeMessage <queue format name> <file name>");
			Console.WriteLine(@"Example:SendLargeMessage DIRECT=OS:MyBiztalkMachine\private$\MyQueue test.txt");
		}
	}
}
