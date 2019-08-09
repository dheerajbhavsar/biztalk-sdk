//---------------------------------------------------------------------
// File: ContentBasedRouting.cs
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

namespace CBR
{
	class ContentBasedRouting
	{
		static void Main(string[] args)
		{
			// connect to the local BizTalk Configuration database
			BtsCatalogExplorer catalog = new BtsCatalogExplorer();
			catalog.ConnectionString = "Server=.;Initial Catalog=BizTalkMgmtDb;Integrated Security=SSPI;";
            Application cbrApplication = catalog.Applications["CBRApplication"];

            if (cbrApplication != null)
            {

                try
                {
                    //***************************************************
                    // create Sendport for processing US orders          
                    //***************************************************
                    SendPort sendportUSOrders = cbrApplication.AddNewSendPort(false, false);
                    sendportUSOrders.Name = "SendportUSOrders";
                    sendportUSOrders.PrimaryTransport.TransportType = catalog.ProtocolTypes[0];
                    sendportUSOrders.PrimaryTransport.Address = "http://process_orders_US.asp";
                    sendportUSOrders.SendPipeline = catalog.Pipelines["Microsoft.BizTalk.DefaultPipelines.XMLTransmit"];

                    //***************************************************
                    // specify filter for content-based routing of US orders
                    // In the sample project the CountryCode for US is 100
                    //***************************************************
                    sendportUSOrders.Filter =
                        "<Filter><Group>" +
                        "<Statement Property='BTS.ReceivePortName' Operator='0' Value='ReceivePortPO'/>" +
                        "<Statement Property='CBRSample.CountryCode' Operator='0' Value='100'/>" +
                        "</Group></Filter>";

                    // enumerate all transforms and add a transform map that can normalize documents for US orders
                    foreach (Transform transform in catalog.Transforms)
                    {
                        if (transform.SourceSchema.FullName == "CBRSample.CBRInputSchema" &&
                           transform.TargetSchema.FullName == "CBRSample.CBROutputSchemaUS")
                        {
                            sendportUSOrders.OutboundTransforms.Add(transform);
                            break;
                        }
                    }

                    // enlist and start the send port
                    sendportUSOrders.Status = PortStatus.Started;

                    //***************************************************
                    // create Sendport for processing CAN orders         
                    //***************************************************
                    SendPort sendportCANOrders = cbrApplication.AddNewSendPort(false, false);
                    sendportCANOrders.Name = "SendportCANOrders";
                    sendportCANOrders.PrimaryTransport.TransportType = catalog.ProtocolTypes[0];
                    sendportCANOrders.PrimaryTransport.Address = "http://process_orders_CAN.asp";
                    sendportCANOrders.SendPipeline = catalog.Pipelines["Microsoft.BizTalk.DefaultPipelines.XMLTransmit"];

                    //***************************************************
                    // specify filter for content-based routing of CAN orders
                    // In the sample project the CountryCode for CAN is 200
                    //***************************************************
                    sendportCANOrders.Filter =
                        "<Filter><Group>" +
                        "<Statement Property='BTS.ReceivePortName' Operator='0' Value='ReceivePortPO'/>" +
                        "<Statement Property='CBRSample.CountryCode' Operator='0' Value='200'/>" +
                        "</Group></Filter>";

                    // add a specific transform map to normalize documents for CAN orders
                    Transform map = catalog.Transforms["CBRSample.CBRInput2CANMap"];
                    if (map != null)
                        sendportCANOrders.OutboundTransforms.Add(map);

                    // enlist and start the send port
                    sendportCANOrders.Status = PortStatus.Started;

                    // persist changes to BizTalk Configuration database
                    catalog.SaveChanges();
                }
                catch (Exception e)
                {
                    catalog.DiscardChanges();
                    throw e;
                }
            }
            else // No CbrApplication
            {
                catalog.DiscardChanges();
                throw new Exception(@"You must deploy the SDK\Samples\Messaging\CBRSample in order to "
                    + "create the referenced application, schema, and maps.");
            }
		}
	}
}
