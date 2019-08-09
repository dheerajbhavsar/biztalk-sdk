//---------------------------------------------------------------------
// File: ReceivePorts.cs
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

namespace ReceivePorts
{
	class ReceivePorts
	{
		static void Main(string[] args)
		{
			CreateReceivePort();
			EnumerateReceivePorts();
			DeleteReceivePort();
		}

		static void CreateReceivePort()
		{
			BtsCatalogExplorer root = new BtsCatalogExplorer();
			try
			{
				root.ConnectionString = "Server=.;Initial Catalog=BizTalkMgmtDb;Integrated Security=SSPI;";

				//Create a new one way receive port.
				ReceivePort myreceivePort = root.AddNewReceivePort(false);

				//Note that if you do not set the name property of the ReceivePort, 
				//it will use the default name generated.
				myreceivePort.Name = "My Receive Port";
				myreceivePort.Tracking = TrackingTypes.AfterReceivePipeline;

				//Try to commit the changes made so far. If it fails, roll-back 
				//all the changes.
				root.SaveChanges();
			}
			catch (Exception e)
			{
				root.DiscardChanges();
				throw e;
			}
		}

		static void EnumerateReceivePorts()
		{
			BtsCatalogExplorer root = new BtsCatalogExplorer();
			try
			{
				root.ConnectionString = "Server=.;Initial Catalog=BizTalkMgmtDb;Integrated Security=SSPI;";
				//Enumerate the receive ports.
				foreach (ReceivePort receivePort in root.ReceivePorts)
					Console.WriteLine(receivePort.Name);
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		static void DeleteReceivePort()
		{
			BtsCatalogExplorer root = new BtsCatalogExplorer();

			try
			{
				root.ConnectionString = "Server=.;Initial Catalog=BizTalkMgmtDb;Integrated Security=SSPI;";

				//Remove a receive port.
				foreach (ReceivePort receivePort in root.ReceivePorts)
				{
					if (receivePort.Name == "My Receive Port")
					{
						root.RemoveReceivePort(receivePort);
						break;
					}
				}

				//Try to commit the changes made so far. 
				root.SaveChanges();
			}
			catch (Exception e)//If it fails, roll-back everything we have done so far
			{
				root.DiscardChanges();
				throw e;
			}
		}

		static void ConfigureReceivePort()
		{
			BtsCatalogExplorer root = new BtsCatalogExplorer();
			try
			{
                root.ConnectionString = "Server=.;Initial Catalog=BizTalkMgmtDb;Integrated Security=SSPI;";

				//Create a new two-way receive port.
				//Passing the false value creates a one-way receive port.
				ReceivePort myreceivePort = root.AddNewReceivePort(true);
				myreceivePort.Name = "My Receive Port";//optional property
				myreceivePort.Tracking = TrackingTypes.AfterReceivePipeline;//optional 

				//Set the primary receive location.
				foreach (ReceiveLocation location in myreceivePort.ReceiveLocations)
				{
					if (location.Address == "http://abc.com")
					{
						myreceivePort.PrimaryReceiveLocation = location;//optional
                        break;
					}
				}

				//Assign the first send pipeline found to process the response message.
				foreach (Pipeline pipeline in root.Pipelines)
				{
					if (pipeline.Type == PipelineType.Send)
					{
						myreceivePort.SendPipeline = pipeline;//optional property
						break;
					}
				}
				myreceivePort.Authentication =
				   AuthenticationType.RequiredDropMessage;//optional

				//Try to commit the changes made so far. 
				//If the commit fails, roll-back all changes.
				root.SaveChanges();
			}
			catch (Exception e)
			{
				root.DiscardChanges();
				throw e;
			}
		}


	}
}
