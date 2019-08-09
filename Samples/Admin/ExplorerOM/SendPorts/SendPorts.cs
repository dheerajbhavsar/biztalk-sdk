//---------------------------------------------------------------------
// File: SendPorts.cs
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

namespace SendPorts
{
	class SendPorts
	{
		static void Main(string[] args)
		{
			CreateSendPort();
			EnumerateSendPorts();
			ConfigureSendPort();
			ChangeSendPortStatus();
			DeleteSendPorts();
		}

		static void CreateSendPort()
		{
			// connect to the local BizTalk configuration database
			BtsCatalogExplorer catalog = new BtsCatalogExplorer();
			catalog.ConnectionString = "Server=.;Initial Catalog=BizTalkMgmtDb;Integrated Security=SSPI;";

			try
			{
				// create a new static one-way SendPort
				SendPort myStaticOnewaySendPort = catalog.AddNewSendPort(false, false);
				myStaticOnewaySendPort.Name = "myStaticOnewaySendPort1";
				myStaticOnewaySendPort.PrimaryTransport.TransportType = catalog.ProtocolTypes[0];
				myStaticOnewaySendPort.PrimaryTransport.Address = "http://sample1";
				myStaticOnewaySendPort.SendPipeline = catalog.Pipelines["Microsoft.BizTalk.DefaultPipelines.XMLTransmit"];

				// create a new dynamic two-way sendPort
				SendPort myDynamicTwowaySendPort = catalog.AddNewSendPort(true, true);
				myDynamicTwowaySendPort.Name = "myDynamicTwowaySendPort1";
				myDynamicTwowaySendPort.SendPipeline = catalog.Pipelines["Microsoft.BizTalk.DefaultPipelines.XMLTransmit"];
				myDynamicTwowaySendPort.ReceivePipeline = catalog.Pipelines["Microsoft.BizTalk.DefaultPipelines.XMLReceive"];

				// persist changes to BizTalk configuration database
				catalog.SaveChanges();
			}
			catch (Exception e)
			{
				catalog.DiscardChanges();
				throw e;
			}
		}

		static void DeleteSendPorts()
		{
			// connect to the local BizTalk configuration database
			BtsCatalogExplorer catalog = new BtsCatalogExplorer();
			catalog.ConnectionString = "Server=.;Initial Catalog=BizTalkMgmtDb;Integrated Security=SSPI;";

			try
			{
				// delete specific sendports by name
				catalog.RemoveSendPort(catalog.SendPorts["myStaticOnewaySendPort1"]);
				catalog.RemoveSendPort(catalog.SendPorts["myDynamicTwowaySendPort1"]);

				// persist changes to BizTalk configuration database
				catalog.SaveChanges();
			}
			catch (Exception e)
			{
				catalog.DiscardChanges();
				throw e;
			}
		}

		static void EnumerateSendPorts()
		{
			// connect to the local BizTalk configuration database
			BtsCatalogExplorer catalog = new BtsCatalogExplorer();
			catalog.ConnectionString = "Server=.;Initial Catalog=BizTalkMgmtDb;Integrated Security=SSPI;";

			try
			{
				// display all sendports and status
				foreach (SendPort sendport in catalog.SendPorts)
				{
					Console.WriteLine(sendport.Name + ": " + sendport.Status.ToString());
				}
			}
			catch (Exception e)
			{
				catalog.DiscardChanges();
				throw e;
			}
		}

		static void ChangeSendPortStatus()
		{
			// connect to the local BizTalk configuration database
			BtsCatalogExplorer catalog = new BtsCatalogExplorer();
			catalog.ConnectionString = "Server=.;Initial Catalog=BizTalkMgmtDb;Integrated Security=SSPI;";

			try
			{
				SendPort sendport = catalog.SendPorts["myStaticOnewaySendPort1"];

				// start the sendport to begin processing messages
				sendport.Status = PortStatus.Started;
				Console.WriteLine(sendport.Name + ": " + sendport.Status.ToString());
				catalog.SaveChanges();

				// stop the sendport to stop processing and suspend messages
				sendport.Status = PortStatus.Stopped;
				Console.WriteLine(sendport.Name + ": " + sendport.Status.ToString());
				catalog.SaveChanges();

				// unenlist the sendport to  stop processing and discard messages
				sendport.Status = PortStatus.Bound;
				Console.WriteLine(sendport.Name + ": " + sendport.Status.ToString());
				catalog.SaveChanges();
			}
			catch (Exception e)
			{
				catalog.DiscardChanges();
				throw e;
			}
		}

		static void ConfigureSendPort()
		{
			// connect to the local BizTalk configuration database
			BtsCatalogExplorer catalog = new BtsCatalogExplorer();
			catalog.ConnectionString = "Server=.;Initial Catalog=BizTalkMgmtDb;Integrated Security=SSPI;";

			try
			{
				SendPort sendport = catalog.SendPorts["myStaticOnewaySendPort1"];

				// specify tracking settings for health and activity tracking
				sendport.Tracking = TrackingTypes.BeforeSendPipeline | TrackingTypes.AfterSendPipeline;

				// specify an encryption certificate for outgoing messages
				foreach (CertificateInfo certificate in catalog.Certificates)
				{
					if (certificate.UsageType == CertUsageType.Encryption)
					{
						sendport.EncryptionCert = certificate;
					}
				}

				// NOTE: do we need to doc the filter format
				// specify filters for content-based routing
				sendport.Filter = "<Filter><Group>" +
				   "<Statement Property='SMTP.Subject' Operator='0' Value='Purchase Order'/>" +
				   "<Statement Property='SMTP.From' Operator='0' Value='Customer'/>" +
				   "</Group></Filter>";

				// specify transform maps for document normalization
				foreach (Transform transform in catalog.Transforms)
				{
					if (transform.SourceSchema.FullName == "myPO" &&
					   transform.TargetSchema.FullName == "partnerPO")
					{
						sendport.OutboundTransforms.Add(transform);
					}
				}

				// persist changes to BizTalk configuration database
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
