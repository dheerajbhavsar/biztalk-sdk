//---------------------------------------------------------------------
// File: BrowsingArtifacts.cs
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

namespace BrowsingArtifacts
{
	class BrowsingArtifacts
	{
		static void Main(string[] args)
		{
            EnumerateOrchestrationArtifacts();
            EnumerateArtifactsSample();
		}

		static void EnumerateOrchestrationArtifacts()
		{
			// Connect to the local BizTalk Configuration database
			BtsCatalogExplorer catalog = new BtsCatalogExplorer();
			catalog.ConnectionString = "Server=.;Initial Catalog=BizTalkMgmtDb;Integrated Security=SSPI;";

			// Enumerate all orchestrations and their ports/roles
			Console.WriteLine("ORCHESTRATIONS: ");
			foreach (BtsAssembly assembly in catalog.Assemblies)
			{
				foreach (BtsOrchestration orch in assembly.Orchestrations)
				{

					Console.WriteLine(" Name:{0}\r\n Host:{1}\r\n Status:{2}",
						orch.FullName, (orch.Host == null ? String.Empty : orch.Host.Name), orch.Status);

					// Enumerate ports and operations
					foreach (OrchestrationPort port in orch.Ports)
					{
						Console.WriteLine("\t{0} ({1})",
							port.Name, port.PortType.FullName);

						foreach (PortTypeOperation operation in port.PortType.Operations)
						{
							Console.WriteLine("\t\t" + operation.Name);
						}
					}

					// Enumerate used roles
					foreach (Role role in orch.UsedRoles)
					{
						Console.WriteLine("\t{0} ({1})",
							role.Name, role.ServiceLinkType);

						foreach (EnlistedParty enlistedparty in role.EnlistedParties)
						{
							Console.WriteLine("\t\t" + enlistedparty.Party.Name);
						}
					}

					// Enumerate implemented roles
					foreach (Role role in orch.ImplementedRoles)
					{
						Console.WriteLine("\t{0} ({1})",
							role.Name, role.ServiceLinkType);
					}
				}
			}
		}

		static void EnumerateArtifactsSample()
		{
			// Connect to the local BizTalk Configuration database
			BtsCatalogExplorer catalog = new BtsCatalogExplorer();
			catalog.ConnectionString = "Server=.;Initial Catalog=BizTalkMgmtDb;Integrated Security=SSPI;";

			// Enumerate all deployed assemblies
			Console.WriteLine("ASSEMBLIES: ");
			foreach (BtsAssembly assembly in catalog.Assemblies)
			{
				Console.WriteLine(assembly.Name);
			}

			// Enumerate all configured hosts
			Console.WriteLine("HOSTS: ");
			foreach (Host host in catalog.Hosts)
			{
				Console.WriteLine("{0} ({1})",
					host.Name, host.Type.ToString());
			}

			// Enumerate all parties and associated sendports/aliases
			Console.WriteLine("PARTIES: ");
			foreach (Party party in catalog.Parties)
			{
				Console.WriteLine(party.Name);

				// Enumerate all sendports
				foreach (SendPort sendport in party.SendPorts)
				{
					Console.WriteLine("\t" + sendport.Name);
				}

				// Enumerate all aliases
				foreach (PartyAlias alias in party.Aliases)
				{
					Console.WriteLine("\t{0}: {1} = {2}",
						alias.Name, alias.Qualifier, alias.Value);
				}
			}

			// Enumerate all the transforms
			Console.WriteLine("TRANSFORMS: ");
			foreach (Transform transform in catalog.Transforms)
			{
				Console.WriteLine("{0}:\r\n \t{1} ==> {2}",
					transform.FullName, transform.SourceSchema.FullName, transform.TargetSchema.FullName);
			}
		}
	}
}
