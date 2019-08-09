//---------------------------------------------------------------------
//  File:      EnlistOrc.cs
// 
//  Summary:
//
//  The EnlistOrch sample demonstrates the following specific capabilities for 
//   scripting against the BizTalk WMI Provider:
//
//  1.  How to query for a specific deployed Orchestration using the key fields "Name" and "AssemblyName".
//  2.  How to get the default host.
//  3.  How to enlist that Orchestration into a specific host.
//  4.  How to handle errors so that meaningful information can be returned to the user.
//
//  Sample:    WMI Sample
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
using System.Management;

namespace Microsoft.Samples.BizTalk.WmiAdmin
{

	/// <summary>
	/// Enlists an orchesteration
	/// </summary>
	class EnlistOrc
	{
		// --------------------------------------------------------------------
		// Main ()
		//
		// Description:
		//   The EnlistOrch sample demonstrates the following specific capabilities for 
		//   scripting against the BizTalk WMI Provider:
		//
		//  1.  How to query for a specific deployed Orchestration using the key fields "Name" and "AssemblyName".
		//  2.  How to get the default host.
		//  3.  How to enlist that Orchestration into a specific host.
		//  4.  How to handle errors so that meaningful information can be returned to the user.
		//
		// Parameters:
		//  <Orchestration Name> = The name of the orchestration you wish to enlist
		//  <Assembly Name>      = The name of the assembly in which the orchestration was deployed.
		//
		// Returns:
		//   none
		//
		// Throws:
		//   An error if it cannot enlist the orchestration
		//
		// Notes :
		//   This sample will use the default host as the host of the orchestration
		// --------------------------------------------------------------------

		
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				//Make sure the expected number of arguments were provided on the command line.
				//If not, print usage text and exit.
				if (args.Length != 2) 
				{ 
					PrintUsage();					
					return;
				}

				else
				{
					//set up a WMI query to acquire a list of orchestrations with the given Name and 
					//AssemblyName key values.  This should be a list of zero or one Orchestrations.
					//Create the WMI search object.
					ManagementObjectSearcher Searcher = new ManagementObjectSearcher();
					
					// create the scope node so we can set the WMI root node correctly.
					ManagementScope Scope = new ManagementScope("root\\MicrosoftBizTalkServer");
					Searcher.Scope = Scope;

					string HostName = "";
				
					// Build a Query to enumerate the MSBTS_Orchestration instances with the
					// specified name and assembly name.
					SelectQuery Query = new SelectQuery(); 	
					Query.QueryString = "SELECT * FROM MSBTS_HostSetting WHERE IsDefault ='TRUE'";
			
					// Set the query for the searcher.
					Searcher.Query = Query;
					ManagementObjectCollection QueryCol = Searcher.Get(); 
					
					foreach (ManagementObject Inst in QueryCol)
					{
						//Get the default host name.
						HostName = Inst.Properties["Name"].Value.ToString();

						Console.WriteLine("The Orchestration will be enlisted with the default host: " + HostName + ".");
					}
				
					// Build a Query to enumerate the MSBTS_Orchestration instances with the
					// specified name and assembly name.
					Query.QueryString = "SELECT * FROM MSBTS_Orchestration WHERE Name = '" + args[0]+ "' AND AssemblyName = '" + args[1] + "'";
			
					// Set the query for the searcher.
					Searcher.Query = Query;
					QueryCol = Searcher.Get(); 
					
					// Use a bool to tell if we enter the for loop
					// below because Count property is not supported
					bool OrchestrationFound = false;
					
					foreach (ManagementObject Inst in QueryCol)
					{
						// There is at least one Orchestration
						OrchestrationFound = true;

						ManagementBaseObject inParams = Inst.GetMethodParameters("Enlist");
						//Fill in input parameter values
						inParams["HostName"] = HostName;
				
						//Execute the method
						ManagementBaseObject outParams = Inst.InvokeMethod("Enlist",inParams,null);
						Console.WriteLine("The Orchestration was successfully enlisted.");
					}

					if(!OrchestrationFound)
					{
						Console.WriteLine("No Orchestration found matching that Name and AssemblyName.");
					}
				}
			}

				
			catch(Exception excep)
			{
				Console.WriteLine("An exception occurred " + excep.Message);
				Console.WriteLine(excep.StackTrace);
			}
			
			Console.WriteLine("\r\n\r\nPress Enter to continue...");
			Console.Read();
		}

		static private void PrintUsage()
		{
			Console.WriteLine("Usage:");
			Console.WriteLine();
			Console.WriteLine( "EnlistOrch.exe <Orchestration Name> <Assembly Name>");
			Console.WriteLine();
			Console.WriteLine(" Where: ");
			Console.WriteLine("  <Orchestration Name> = The name of the orchestration you wish to enlist.");
			Console.WriteLine("       Example: 'MyBusinessOrchestration'");
			Console.WriteLine();
			Console.WriteLine("  <Assembly Name>      = The name of the assembly in which the orchestration was deployed.");
			Console.WriteLine("       Example: 'MyBusinessAssembly'");
			Console.WriteLine();
		}
	}
}
