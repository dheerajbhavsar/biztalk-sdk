//---------------------------------------------------------------------
// File: ReceiveLocations.cs
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

namespace ReceiveLocations
{
	class ReceiveLocations
	{
		static void Main(string[] args)
		{
			CreateAndConfigureReceiveLocation();
		}

		static void EnumerateReceiveLocations()
		{
			BtsCatalogExplorer root = new BtsCatalogExplorer();
			try
			{
				root.ConnectionString = "Server=.;Initial Catalog=BizTalkMgmtDb;Integrated Security=SSPI;";

				//Enumerate the receive locations in each of the receive ports.
				foreach (ReceivePort receivePort in root.ReceivePorts)
				{
					Console.WriteLine(receivePort.Name);
					//Enumerate the receive locations.
					foreach (ReceiveLocation location in
					   receivePort.ReceiveLocations)
						Console.WriteLine(location.Name);
				}
			}
			catch (Exception e)//If it fails, roll-back all changes.
			{
				throw e;
			}
		}
		static void DeleteReceiveLocation()
		{
			BtsCatalogExplorer root = new BtsCatalogExplorer();
			try
			{
				root.ConnectionString = "Server=.;Initial Catalog=BizTalkMgmtDb;Integrated Security=SSPI;";
				//Remove receive location with the name "My Receive Location" from 
				//the receive port.

				//Enumerate the receive locations in the recieve port.
				foreach (ReceivePort receivePort in root.ReceivePorts)
				{
					//Enumerate the receive locations.
					foreach (ReceiveLocation location in receivePort.ReceiveLocations)
					{
						if (location.Name == "My Receive Location")
						{
							receivePort.RemoveReceiveLocation(location);
							break;
						}
					}
				}

				//Try to commit the changes made so far. 
				root.SaveChanges();
			}
			catch (Exception e)//If it fails, roll-back all changes.
			{
				root.DiscardChanges();
				throw e;
			}
		}


		static void CreateAndConfigureReceiveLocation()
		{
			BtsCatalogExplorer root = new BtsCatalogExplorer();
			try
			{
				root.ConnectionString = "Server=.;Initial Catalog=BizTalkMgmtDb;Integrated Security=SSPI;";

				//First, create a new one way receive port.
				ReceivePort myreceivePort = root.AddNewReceivePort(false);

				//Note that if you dont set the name property for the receieve port, 
				//it will create a new receive location and add it to the receive       //port.
				myreceivePort.Name = "My Receive Port";

				//Create a new receive location and add it to the receive port
				ReceiveLocation myreceiveLocation = myreceivePort.AddNewReceiveLocation();

				foreach (ReceiveHandler handler in root.ReceiveHandlers)
				{
					if (handler.TransportType.Name == "HTTP")
					{
						myreceiveLocation.ReceiveHandler = handler;
						break;
					}
				}

				//Associate a transport protocol and URI with the receive location.
				foreach (ProtocolType protocol in root.ProtocolTypes)
				{
					if (protocol.Name == "HTTP")
					{
						myreceiveLocation.TransportType = protocol;
						break;
					}
				}

                myreceiveLocation.Address = "/home";
				//Assign the first receive pipeline found to process the message.
				foreach (Pipeline pipeline in root.Pipelines)
				{
					if (pipeline.Type == PipelineType.Receive)
					{
						myreceiveLocation.ReceivePipeline = pipeline;
						break;
					}
				}

				//Enable the receive location.
				myreceiveLocation.Enable = true;
				myreceiveLocation.FragmentMessages = Fragmentation.Yes;//optional property
				myreceiveLocation.ServiceWindowEnabled = false; //optional property

				//Try to commit the changes made so far. If the commit fails, 
				//roll-back all changes.
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
