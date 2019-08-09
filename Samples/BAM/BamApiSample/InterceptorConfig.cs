//---------------------------------------------------------------------
// File:	InterceptorConfig.cs
// 
// Summary: 	Demonstrates how to instrument an application 
//				using BAM APIs to track useful information.
//
// Sample: 	BAM Api Sample
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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

using Microsoft.BizTalk.Bam.EventObservation;

namespace  Microsoft.Samples.BizTalk.InterceptorConfig
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine("---- PurchaseOrder ----");
			ActivityInterceptorConfiguration config=new ActivityInterceptorConfiguration("BAMApiPo");
			config.RegisterStartNew("locNewPo","@PoID");
			config.RegisterEnd("locPackaged");
			ConfigureInterceptor("BAMApiPo",config);

			Console.WriteLine("---- Shipment ----");
			config=new ActivityInterceptorConfiguration("BAMApiPo");
			config.RegisterContinue("locShipped","@ShipmentID");
			config.RegisterEnd("locDelivered");
			ConfigureInterceptor("Shipment",config);

			Console.WriteLine("---- Invoice ----");
			config=new ActivityInterceptorConfiguration("BAMApiInvoice");
			config.RegisterStartNew("locNewInvoice","@InvoiceID");
			config.RegisterEnd("locInvoicePaid");
			ConfigureInterceptor("BAMApiInvoice",config);
		}
		static void ConfigureInterceptor(string activity, ActivityInterceptorConfiguration config)
		{
			while (true)
			{
				Console.Write("Activity Milestone or Item Name      : ");
				string item=Console.ReadLine();
				if (""==item) break;

				Console.Write("Get this at what step (location)?    : ");
				string loc=Console.ReadLine();

				Console.Write("From what Xpath? (empty if Milestone): ");
				string xpath=Console.ReadLine();
				config.RegisterDataExtraction(item,loc,xpath);
				Console.WriteLine();
			}

			// Prepare interceptor instance to be used by the Business Implementation
			BAMInterceptor interceptor=new BAMInterceptor();
			config.UpdateInterceptor(interceptor); 

			BinaryFormatter bf=new BinaryFormatter();
			Stream f=File.Create(activity+"_interceptor.bin");
			bf.Serialize(f,interceptor);
			f.Close();

			// And output the configuration as xml for verification
			XmlSerializer xs=new XmlSerializer(typeof(ActivityInterceptorConfiguration));
			f=File.Create(activity+"_config.xml");
			xs.Serialize(f,config);
			f.Close();
		}
	}
}
