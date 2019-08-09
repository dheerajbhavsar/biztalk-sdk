//---------------------------------------------------------------------
// File: PartnerManagement.cs
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

namespace PartnerManagement
{
	class PartnerManagement
	{
		static void Main(string[] args)
		{
			CreateParty();
			EnlistParty();
			UnenlistParty();
			DeleteParty();
		}

		static void CreateParty()
		{
			// Create the root object and set the connection string
			BtsCatalogExplorer catalog = new BtsCatalogExplorer();
			catalog.ConnectionString = "SERVER=.;DATABASE=BizTalkMgmtDb;Integrated Security=SSPI";

			try
			{
				// Create a party
				Party myParty = catalog.AddNewParty();
				myParty.Name = "FedEx";
				// create a standard alias
				PartyAlias standardAlias = myParty.AddNewAlias();
				standardAlias.Name = "D-U-N-S (Dun & Bradstreet)";
				standardAlias.Value = "Value1";

				// Create a custom alias
				PartyAlias customAlias = myParty.AddNewAlias();
				customAlias.Name = "Telephone";
				customAlias.Qualifier = "100";
				customAlias.Value = "4257076302";

				// Add party send ports
				myParty.SendPorts.Add(catalog.SendPorts["NormalDelivery"]);
				myParty.SendPorts.Add(catalog.SendPorts["ExpressDelivery"]);

				// Specify a signature certificate, make sure the certificate is available
				// in the AddressBook store of the Local Machine
				foreach (CertificateInfo certificate in catalog.Certificates)
				{
					if (certificate.ShortName == "BR, Certisign Certificadora Digital Ltda., Certisign - Autoridade Certificadora - AC2")
					{
						myParty.SignatureCert = certificate;
						break;
					}
				}

				// Commit the changes
				catalog.SaveChanges();
			}
			catch (Exception e)
			{
				catalog.DiscardChanges();
				throw e;
			}
		}

		static void EnlistParty()
		{
			// Create the root object and set the connection string
			BtsCatalogExplorer catalog = new BtsCatalogExplorer();
			catalog.ConnectionString = "SERVER=.;DATABASE=BizTalkMgmtDb;Integrated Security=SSPI";

			try
			{
				Party myParty = catalog.Parties["FedEx"];

				// Search for the shipper role
				Role svcRole = null;
				foreach (Role role in catalog.Assemblies[0].Roles)
				{
					if (role.Name == "ShipperRole")
					{
						svcRole = role;
						break;
					}
				}

				// Enlist the party under the shipper role
				EnlistedParty enlistedParty = svcRole.AddNewEnlistedParty(myParty);
				enlistedParty.Mappings[0].SendPort = (SendPort)myParty.SendPorts[0]; // NormalDelivery
				enlistedParty.Mappings[1].SendPort = (SendPort)myParty.SendPorts[1]; // ExpressDelivery

				// Commit the changes
				catalog.SaveChanges();
			}
			catch (Exception e)
			{
				catalog.DiscardChanges();
				throw e;
			}
		}

		static void UnenlistParty()
		{
			// Create the root object and set the connection string
			BtsCatalogExplorer catalog = new BtsCatalogExplorer();
			catalog.ConnectionString = "SERVER=.;DATABASE=BizTalkMgmtDb;Integrated Security=SSPI";

			try
			{
				// Search for the shipper role
				Role svcRole = null;
				foreach (Role role in catalog.Assemblies[0].Roles)
				{
					if (role.Name == "ShipperRole")
					{
						svcRole = role;
						break;
					}
				}

				// Remove the enlisted party
				foreach (EnlistedParty enlistedparty in svcRole.EnlistedParties)
				{
					if (enlistedparty.Party.Name == "FedEx")
					{
						svcRole.RemoveEnlistedParty(enlistedparty);
						break;
					}
				}

				// Commit the changes
				catalog.SaveChanges();
			}
			catch (Exception e)
			{
				catalog.DiscardChanges();
				throw e;
			}
		}

		static void DeleteParty()
		{
			// Create the root object and set the connection string
			BtsCatalogExplorer catalog = new BtsCatalogExplorer();
			catalog.ConnectionString = "SERVER=.;DATABASE=BizTalkMgmtDb;Integrated Security=SSPI";

			try
			{
				Party party = catalog.Parties["FedEx"];
				// Remove the party
				catalog.RemoveParty(party);

				// commit the changes
				catalog.SaveChanges();
			}
			catch (Exception e)
			{
				catalog.DiscardChanges();
				throw e;
			}
		}
	}
}
