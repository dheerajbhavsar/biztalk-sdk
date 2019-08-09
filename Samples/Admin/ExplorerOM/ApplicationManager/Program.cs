//---------------------------------------------------------------------
// File: Program.cs
// 
// Sample: ExplorerOM
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



#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.ExplorerOM;

#endregion

namespace ApplicationManager
{
	class Program
	{
		static void Main(string[] args)
		{
			// Handle the command-line arguments and switches
			if (args.Length != 2)
			{
				PrintUsage();
				return;
			}

			if (args[0] != "start" && args[0] != "stop" )
			{
				PrintUsage();
				return;
			}

			// Create the root object and set the connection string
			BtsCatalogExplorer catalog = new BtsCatalogExplorer();
			catalog.ConnectionString = "SERVER=.;DATABASE=BizTalkMgmtDb;Integrated Security=SSPI";

			string applicationName = args[1];
			Application app = catalog.Applications[applicationName];
			if (null == app)
			{
				Console.WriteLine("Application " + applicationName + " does not exist.");
				return;
			}

			if (args[0] == "start")
			{
				app.Start(ApplicationStartOption.StartAll);
			}
			else
			{
				app.Stop(ApplicationStopOption.StopAll);
			}

			catalog.SaveChanges();
		}

		static private void PrintUsage()
		{
			Console.WriteLine("Usage:");
			Console.WriteLine();
			Console.WriteLine("ApplicationManager <start|stop> <Application Name>");
			Console.WriteLine();
			Console.WriteLine(" Where: ");
			Console.WriteLine("  <Application Name> = The name of the application that needs to be changed ");
			Console.WriteLine();
			Console.WriteLine("Example: ApplicationManager start Application1");
			Console.WriteLine();
		}
	}
}
