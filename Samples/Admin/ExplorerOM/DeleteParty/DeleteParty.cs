//---------------------------------------------------------------------
//  File:      DeleteParty.cs
// 
//  Summary:
//
//  The Delete Party sample demonstrates remove a Party from the catalog
//	using ExplorerOM
//
//  Sample:    Delete Party Sample
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

namespace Microsoft.Samples.BizTalk.ExplorerOM.DeleteParty
{
	/// <summary>
	/// DeletePartyClass is the wapper class for the Main function.
	/// </summary>
	class DeletePartyClass
	{
		/// <summary>
		/// The main entry point for the application.
		/// The only argument should be the name of the party to delete.
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
				// Get the requested party from the collection
				Party party = catalog.Parties[args[0]];
				if(null == party)
				{
					Console.WriteLine("Party named " + args[0] + " was not found.");
					return;
				}

				Console.WriteLine("Deleting party: " + party.Name);

				// Remove the party
				catalog.RemoveParty(party);
      
				// commit the changes
				catalog.SaveChanges();

				Console.WriteLine("Party deleted.");
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
			Console.WriteLine( "DeleteParty.exe <Party Name>");
			Console.WriteLine();
			Console.WriteLine(" Where: ");
			Console.WriteLine("  <Party Name> = The name of the Party to delete.");
			Console.WriteLine("       Example: 'MyBusinessParty'");
			Console.WriteLine();
		}
	}
}
