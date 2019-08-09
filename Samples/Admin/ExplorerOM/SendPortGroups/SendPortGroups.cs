//---------------------------------------------------------------------
// File: SendPortGroups.cs
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

namespace SendPortGroups
{
	class SendPortGroups
	{
		static void Main(string[] args)
		{
			CreateSendPortGroup();
			EnumerateSendPortGroups();
			DeleteSendPortGroup();
		}

		static void CreateSendPortGroup()
		{
			BtsCatalogExplorer root = new BtsCatalogExplorer();
			try
			{
				root.ConnectionString = "Integrated Security=SSPI;database=BizTalkMgmtDb;server=YOURSERVER";

				//create a new send port group
				SendPortGroup mySendPortGroup = root.AddNewSendPortGroup();
				mySendPortGroup.Name = "My Send Port Group";

				//try to commit the changes we made so far. If it fails, roll-back //everything we have done so far
				root.SaveChanges();
			}
			catch (Exception e)
			{
				root.DiscardChanges();
				throw e;
			}
		}

		static void EnumerateSendPortGroups()
		{
			BtsCatalogExplorer root = new BtsCatalogExplorer();
			try
			{
				root.ConnectionString = "Integrated Security=SSPI;database=BizTalkMgmtDb;server=YOURSERVER";

				//enumerate Send Port Groups
				foreach (SendPortGroup sendPortGroup in root.SendPortGroups)
					Console.WriteLine(sendPortGroup.Name);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		static void DeleteSendPortGroup()
		{
			BtsCatalogExplorer root = new BtsCatalogExplorer();

			try
			{
				root.ConnectionString = "Integrated Security=SSPI;database=BizTalkMgmtDb;server= YOURSERVER";

				// remove a send port group
				foreach (SendPortGroup sendPortGroup in root.SendPortGroups)
				{
					if (sendPortGroup.Name == "My Send Port Group")
					{
						root.RemoveSendPortGroup(sendPortGroup);
						break;
					}
				}

				//try to commit the changes we made so far. 
				root.SaveChanges();
			}
			catch (Exception e)//If it fails, roll-back everything we have done so far
			{
				root.DiscardChanges();
				throw e;
			}
		}

		static void AddSendPortToSendPortGroup(SendPort sendPort)
		{
			BtsCatalogExplorer root = new BtsCatalogExplorer();
			try
			{
				root.ConnectionString = "Integrated Security=SSPI;database=BizTalkMgmtDb;server= YOURSERVER";

				//remove a receive port 
				foreach (SendPortGroup sendPortGroup in root.SendPortGroups)
				{
					if (sendPortGroup.Name == "My Send Port Group")
					{
						sendPortGroup.SendPorts.Add(sendPort);
						break;
					}
				}

				//try to commit the changes we made so far. 
				//If it fails, roll-back everything we have done so far
				root.SaveChanges();
			}
			catch (Exception e)
			{
				root.DiscardChanges();
				throw e;
			}
		}

		static void DeleteSendPortFromSendPortGroup(SendPort sendPort)
		{
			BtsCatalogExplorer root = new BtsCatalogExplorer();
			try
			{
				root.ConnectionString = "Integrated Security=SSPI;database=BizTalkMgmtDb;server= YOURSERVER";

				//remove a receive port 
				foreach (SendPortGroup sendPortGroup in root.SendPortGroups)
				{
					if (sendPortGroup.Name == "My Send Port Group")
					{
						sendPortGroup.SendPorts.Remove(sendPort);
						break;
					}
				}

				//try to commit the changes we made so far. 
				//If it fails, roll-back everything we have done so far
				root.SaveChanges();
			}
			catch (Exception e)
			{
				root.DiscardChanges();
				throw e;
			}
		}

		static void StartSendPortGroup(SendPortGroup sendPortGroup)
		{
			try
			{
				sendPortGroup.Status = PortStatus.Started;

				// Also to start all the send ports under the send port group
				foreach (SendPort sendPort in sendPortGroup.SendPorts)
					sendPort.Status = PortStatus.Started;

				sendPortGroup.BtsCatalogExplorer.SaveChanges();
			}
			catch (Exception e)
			{
				sendPortGroup.BtsCatalogExplorer.DiscardChanges();
				throw e;
			}
		}

		static void StopSendPortGroup(SendPortGroup sendPortGroup)
		{
			try
			{
				sendPortGroup.Status = PortStatus.Stopped;

				// Also to stop all the send ports under the send port group
				foreach (SendPort sendPort in sendPortGroup.SendPorts)
					sendPort.Status = PortStatus.Stopped;

				sendPortGroup.BtsCatalogExplorer.SaveChanges();
			}
			catch (Exception e)
			{
				sendPortGroup.BtsCatalogExplorer.DiscardChanges();
				throw e;
			}
		}

		static void UnenlistSendPortGroup(SendPortGroup sendPortGroup)
		{
			try
			{
				sendPortGroup.Status = PortStatus.Bound;

				// Also to un-enlist all the send ports under the send port group
				foreach (SendPort sendPort in sendPortGroup.SendPorts)
					sendPort.Status = PortStatus.Bound;

				sendPortGroup.BtsCatalogExplorer.SaveChanges();
			}
			catch (Exception e)
			{
				sendPortGroup.BtsCatalogExplorer.DiscardChanges();
				throw e;
			}
		}
	}
}
