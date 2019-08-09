//---------------------------------------------------------------------
//  File:      EnumRLs.cs
// 
//  Summary:   This samples purpose is to enumerate all the properties of the installed 
//				receive locations for a particluar server.
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
	/// Enumerates installed RLs
	/// </summary>
	class EnumerateWMIClasses
	{
		// --------------------------------------------------------------------
		// Main ()
		//
		// Description:
		//   This function  shows how you can enumerate thru one of
		//	 the BizTalk's WMI objects and show all of its properties.
		//   The sample code is not intended for production use.
		//
		// Parameters:
		//  RLname or /?
		//  RLname is the specific name of the RL to enumerate
		//  /? will display a help screen
		//
		// Returns:
		//   none
		//
		// Throws:
		//   An error if it cannot enumerate the RLs.
		//
		// Notes :
		//   none
		// --------------------------------------------------------------------

		
		[STAThread]
		static void Main(string[] args)
		{
			// Display help information 
			if (args.Length > 0)
			{
					if (args[0] == "/?") 
					{
						Console.WriteLine();
						Console.WriteLine();
						Console.WriteLine("The correct usage of this sample is \"EnumRL.exe [RLName]\"");
						Console.WriteLine("Where [RLname] is the name of a particular receive location to enumerate.");
						Console.WriteLine("If receive location name contains spaces make sure to put it in quotes"); 
						Console.WriteLine();
						Console.WriteLine("Example #1 Enumerate all the receive locations.");
						Console.WriteLine("         EnumRL");
						Console.WriteLine();
						Console.WriteLine("Example#2 enumerate just the \"My Receive Location #3\" receive location."); 
						Console.WriteLine("         EnumRL \"My Receive Location #3\" ");
						Console.WriteLine();
						Console.WriteLine();
						Console.WriteLine("To get help use enumRL.exe /?");
						return;
					}
			}

			try 
			{	
				//Create the WMI search object.
				ManagementObjectSearcher Searcher = new ManagementObjectSearcher();

				// create the scope node so we can set the WMI root node correctly.
				ManagementScope Scope = new ManagementScope("root\\MicrosoftBizTalkServer");
				Searcher.Scope = Scope;
			
				// Build a Query to enumerate the MSBTS_ReceiveLocation instances if an argument
				// is supplied use it to select only the matching RL.
				SelectQuery Query = new SelectQuery(); 
				if (args.Length == 0) 
					Query.QueryString="SELECT * FROM MSBTS_ReceiveLocation";
				else
					Query.QueryString="SELECT * FROM MSBTS_ReceiveLocation WHERE Name = '" + args[0] + "'";

				// Set the query for the searcher.
				Searcher.Query = Query;

				// Execute the query and determine if any results were obtained.
				ManagementObjectCollection QueryCol = Searcher.Get();

				// Use a bool to tell if we enter the for loop
				// below because Count property is not supported
				bool ReceiveLocationFound = false;

				// Enumerate all properties.
				foreach (ManagementBaseObject envVar in QueryCol)
				{
					// There is at least one Receive Location
					ReceiveLocationFound = true;

					Console.WriteLine("**************************************************");
					Console.WriteLine("Receive Location:  {0}", envVar["Name"]);
					Console.WriteLine("**************************************************");

					PropertyDataCollection envVarProperties = envVar.Properties;
					Console.WriteLine("Output in the form of: Property: {Value}");
				
					foreach (PropertyData envVarProperty in envVarProperties) 
					{					
						Console.WriteLine(envVarProperty.Name+ ":\t" + envVarProperty.Value);
					}
				}
					
				if (!ReceiveLocationFound) 
				{
					Console.WriteLine("No receive locations found matching the specified name.");
				} 
			}

			catch(Exception excep)
			{
				Console.WriteLine(excep.ToString());
			}
			
			Console.WriteLine("\r\n\r\nPress Enter to continue...");
			Console.Read();
		}
	}
}
