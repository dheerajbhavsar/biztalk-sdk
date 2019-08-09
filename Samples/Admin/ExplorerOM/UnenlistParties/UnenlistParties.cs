//---------------------------------------------------------------------
//  File:      UnenlistParties.cs
// 
//  Summary:
//
//  The Unenlist Parties sample demonstrates how to unenlist all Parties
//	from all the Roles of an Assembly using ExplorerOM
//
//  Sample:    Unenlist Parties Sample
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
using System.Windows.Forms;
using Microsoft.BizTalk.ExplorerOM;

namespace Microsoft.Samples.BizTalk.ExplorerOM.UnenlistParties
{
	/// <summary>
	/// UnenlistPartiesClass is the wapper class for the Main function.
	/// </summary>
	class UnenlistPartiesClass
	{
		/// <summary>
		/// The main entry point for the application.
		/// The only argument should be the name of the assembly with the Roles to clean up.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			// Handle the command-line arguments and switches
			if(args.Length != 1)
			{
				PrintUsage();
				return;
			}
			if(("/?" == args[0]) || ("-?" == args[0]))
			{
				PrintUsage();
				return;
			}

			// Create the root object and set the connection string
			BtsCatalogExplorer catalog = new BtsCatalogExplorer();
			catalog.ConnectionString = string.Format("SERVER={0};DATABASE={1};Integrated Security=SSPI", SystemInformation.ComputerName, "BizTalkMgmtDB");

			try
			{
				// Get the requested assembly from the collaction
				BtsAssembly assembly = catalog.Assemblies[args[0]];
				if(null == assembly)
				{
					Console.WriteLine("Assembly named " + args[0] + " was not found.");
					return;
				}

				Console.WriteLine("Removing enlisted parties from assembly " + assembly.Name + ".");

				// Go through each Role in the assembly
				foreach(Role role in assembly.Roles)
				{
					// Find the number of Parties enlisted
					int numberOfEnlistedParties = role.EnlistedParties.Count;

					// For each enlisted party, remove it
					for(int count = 0; count < numberOfEnlistedParties; count++)
					{
						EnlistedParty enlistedParty = role.EnlistedParties[0];

						Console.WriteLine("Unenlisting the " + enlistedParty.Party.Name + " Party from the " + enlistedParty.Role.Name + " Role.");

						role.RemoveEnlistedParty(enlistedParty);
					}
				}

				// commit the changes
				catalog.SaveChanges();

				Console.WriteLine("All enlisted parties for assemlby " + assembly.Name + " have been removed.");
			}
			catch(ConstraintException ce)
			{
				// Any changes need to be reverted
				// when there is an error
				catalog.DiscardChanges();

				// Since this is a common configruation excpetion
				// we don't need a stack trace
				Console.WriteLine(ce.Message);
			}
			catch(Exception e)
			{
				// Any changes need to be reverted
				// when there is an error
				catalog.DiscardChanges();

				Console.WriteLine(e.ToString());
			}
		}

		static private void PrintUsage()
		{
			Console.WriteLine("Usage:");
			Console.WriteLine();
			Console.WriteLine( "UnenlistParties.exe <Assembly Name>");
			Console.WriteLine();
			Console.WriteLine(" Where: ");
			Console.WriteLine("  <Assembly Name> = The name of the assembly that has the Roles ");
			Console.WriteLine("                    that the Parties will be unenlisted from.");
			Console.WriteLine("       Example: 'MyBusinessAssembly'");
			Console.WriteLine();
		}
	}
}
