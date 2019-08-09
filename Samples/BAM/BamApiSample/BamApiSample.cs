//---------------------------------------------------------------------
// File:	BAMapiSample.cs
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

//#define Interceptor

using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using Microsoft.BizTalk.Bam.EventObservation;


namespace  Microsoft.Samples.BizTalk.BAMapiSample
{
	class Global
	{
		public static int PoThreads=10;
		public static int ShipmentThreads=10;
		public static int InvoiceThreads=5;

		public const int PoAppMinSleepTime=10;		
		public const int PoAppMaxSleepTime=1000;		
		public const int ApprovalPercent=50;
		public const int ApprovalMinTime=10;
		public const int ApprovalMaxTime=1000;
		public const int PackagingMinTime=10;
		public const int PackagingMaxTime=1000;
		
		public const int ShipmentAppMinSleepTime=10;
		public const int ShipmentAppMaxSleepTime=1000;
		public const int ShipmentMinTime=100;
		public const int ShipmentMaxTime=1000;

		public const int InvoiceAppMinSleepTime=1000;
		public const int InvoiceAppMaxSleepTime=10000;
		public const int InvoicePoMinCount=5;
		public const int InvoicePoMaxCount=10;
		public const int InvoiceWaitMinTime=100;
		public const int InvoiceWaitMaxTime=10000;

		public static string[] Products;
		public static string[] Discounts;
		public static int[,] Prices;
		public static string[] ShipmentCarriers;
		public const int Customers=1000;

		public static EventStream es=null;
		public const int FlushTreshold=1;

		
		static Global()
		{
			
			Products="Infrared Decoder,Flash MC".Split(',');
			Discounts="WebSite,StoreSale,NoDiscount".Split(',');

			Prices=new int[Products.Length,Discounts.Length];
			for (int product=1; product<Products.Length; product++)
			{
				for (int discount=1; discount<Discounts.Length; discount++)
				{
					Prices[product,discount]=10+product*20+discount*10;
				}
			}

			ShipmentCarriers="UPS,Express Mail,DHL".Split(',');

			
		}

#if Interceptor
		public static DataExtractor dataExtractor=new DataExtractor();

		public static BAMInterceptor LoadInterceptor(string filename)
		{
			string exe = Assembly.GetExecutingAssembly().Location;
			string InterceptorDir=exe.Substring(0,exe.LastIndexOf("\\")+1);
			Stream f=File.Open(InterceptorDir+filename,FileMode.Open,FileAccess.Read,FileShare.Read);
			BinaryFormatter bf=new BinaryFormatter();
			BAMInterceptor interceptor=(BAMInterceptor)bf.Deserialize(f);
			f.Close();
			return interceptor;
		}
#endif
	}
	class PoApplication
	{
		static int ms_poCounter=0;

		void RunOnce()
		{
			Random r=new Random((int)DateTime.Now.Ticks/int.Parse(Thread.CurrentThread.Name));
			int sleep=r.Next(Global.PoAppMinSleepTime,Global.PoAppMaxSleepTime);
			Thread.Sleep(sleep);

			// Generate Random PurchaseOrder as XML message
			int poid=ms_poCounter++;				
			string sPOxml="<PurchaseOrder PoID=\""+poid.ToString()+"\">\n";
			int product=r.Next(Global.Products.Length);
			sPOxml+="    <Product>"+Global.Products[product]+"</Product>\n";
			int discount=r.Next(Global.Discounts.Length);
			sPOxml+="    <Discount>"+Global.Discounts[discount]+"</Discount>\n";
			sPOxml+="    <Price>"+Global.Prices[product,discount]+"</Price>\n";
			sPOxml+="    <Address>"+r.Next(10000).ToString()+" "+r.Next(1000).ToString()+" Str</Address>\n";
			sPOxml+="</PurchaseOrder>\n";

			XmlDocument xdPO=new XmlDocument();
			xdPO.LoadXml(sPOxml);
			XmlElement xePO=xdPO.DocumentElement;
			Console.WriteLine("New Purchase Order #"+xePO.GetAttribute("PoID")+" Received.");

#if Interceptor
			BAMInterceptor interceptor=Global.LoadInterceptor("BAMApiPo_interceptor.bin");
			interceptor.OnStep(Global.dataExtractor,"locNewPo",xePO,Global.es);
#else
			Global.es.BeginActivity("BAMApiPo",poid.ToString());
			Global.es.UpdateActivity("BAMApiPo",poid.ToString(),
				"Received",DateTime.UtcNow,
				"Product",xePO.SelectSingleNode("Product").InnerText,
				"Amount",xePO.SelectSingleNode("Price").InnerText);
#endif

			// Random Approval Decision				
			sleep=r.Next(Global.ApprovalMinTime,Global.ApprovalMaxTime);
			Thread.Sleep(sleep);

			int approve=r.Next(100);
			if (approve>Global.ApprovalPercent)
			{
				Console.WriteLine(xePO.GetAttribute("PoID")+" was Rejected.");

#if Interceptor
				interceptor.OnStep(Global.dataExtractor,"locRejected",xePO,Global.es);
#else
				Global.es.UpdateActivity("BAMApiPo",poid.ToString(),
					"Denied",DateTime.UtcNow);
#endif
				return;
			}
			Console.WriteLine(xePO.GetAttribute("PoID")+" was Approved.");

#if Interceptor
			interceptor.OnStep(Global.dataExtractor,"locApproved",xePO,Global.es);
#else
			Global.es.UpdateActivity("BAMApiPo",poid.ToString(),
				"Approved",DateTime.UtcNow);
#endif

			// Put the Package in the queue to be shipped
			sleep=r.Next(Global.PackagingMinTime,Global.PackagingMaxTime);
			Thread.Sleep(sleep);
			int packageNumber=r.Next(poid*10,(poid+1)*10);

			if (Global.ShipmentThreads>0)
			{
				XmlDocument xdShipment=new XmlDocument();
				string sShipXml="<Shipment ShipmentID=\"pkg#"+packageNumber.ToString()+"\"/>";
				xdShipment.LoadXml(sShipXml);
				XmlElement xeShipment=xdShipment.DocumentElement;
				XmlElement xeShipAddress=xdShipment.CreateElement("Address");
				XmlElement xePoAddress=(XmlElement)xePO.SelectSingleNode("Address");
				xeShipAddress.InnerText=xePoAddress.InnerText;
				xeShipment.AppendChild(xeShipAddress);
				
				lock(ShipmentApplication.ShipPackages)
				{
					ShipmentApplication.ShipPackages.Enqueue(xeShipment);
				}
			}

			// and register this PO to be included in some invoice
			if (Global.InvoiceThreads>0)
			{
				InvoiceApplication.PosToInvoice.Enqueue(xePO);
			}

			Console.WriteLine(xePO.GetAttribute("PoID")+
				" was shipped as pkg#"+packageNumber.ToString());

#if Interceptor
			interceptor.OnStep(Global.dataExtractor,"locPackaged",xePO,Global.es);
#else
			Global.es.UpdateActivity("BAMApiPo",poid.ToString(),
				"Packaged",DateTime.UtcNow);
			Global.es.EnableContinuation("BAMApiPo",poid.ToString(),"pkg#"+packageNumber.ToString());
			Global.es.EndActivity("BAMApiPo",poid.ToString());
#endif
		}

		public void Run()
		{
			try
			{
				while (true)
				{
					RunOnce();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception in PurchaseOrder thread \n\n"+ex.Message);
				if (null!=ex.InnerException)
				{
					Console.WriteLine("\nInnerException: \n"+ex.InnerException.Message);
				}
			}
		}
	}
	class ShipmentApplication
	{
		public static Queue ShipPackages=new Queue();

		public void RunOnce()
		{
			int sleep;

			// Get one Shipment from the queue
			XmlElement xeShipment=null;
			Random r=new Random((int)DateTime.Now.Ticks/int.Parse(Thread.CurrentThread.Name));
			while(true)
			{
				sleep=r.Next(Global.ShipmentAppMinSleepTime,Global.ShipmentAppMaxSleepTime);
				Thread.Sleep(sleep);

				lock(ShipPackages)
				{
					if (0==ShipPackages.Count)
						continue;
					xeShipment=(XmlElement)ShipPackages.Dequeue();
				}
				if (null!=xeShipment)
					break;
			}
			string shipID=xeShipment.GetAttribute("ShipmentID");
			string carrier=Global.ShipmentCarriers[r.Next(Global.ShipmentCarriers.Length)];
			xeShipment.SetAttribute("Carrier",carrier);
			
			Console.WriteLine("Shipping package "+shipID+" via "+carrier);
#if Interceptor
			BAMInterceptor interceptor=Global.LoadInterceptor("Shipment_interceptor.bin");
			interceptor.OnStep(Global.dataExtractor,"locShipped",xeShipment,Global.es);
#else
			Global.es.UpdateActivity("BAMApiPo",shipID,"Shipped",DateTime.UtcNow);
#endif

			// Simulate delay for Shipment
			sleep=r.Next(Global.ShipmentMinTime,Global.ShipmentMaxTime);
			Thread.Sleep(sleep);

			Console.WriteLine("Package "+xeShipment.GetAttribute("ShipmentID")+" was delivered");
#if Interceptor
			interceptor.OnStep(Global.dataExtractor,"locDelivered",xeShipment,Global.es);
#else
			Global.es.UpdateActivity("BAMApiPo",shipID,"Delivered",DateTime.UtcNow);
			Global.es.EndActivity("BAMApiPo",shipID);
#endif
		}

		public void Run()
		{
			try
			{
				while (true)
				{
					RunOnce();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception in Shipment thread \n\n"+ex.Message);
				if (null!=ex.InnerException)
				{
					Console.WriteLine("\nInnerException: \n"+ex.InnerException.Message);
				}
			}
		}
	}
	class InvoiceApplication
	{
		public static Queue PosToInvoice=new Queue();
		static int ms_InvoiceCounter=1;

		public void RunOnce()
		{
			int sleep;
			int invoiceNum;
			int total=0;
			string polist="";
			XmlDocument xdInvoice=new XmlDocument();
			xdInvoice.LoadXml("<Invoice/>");
			XmlElement xeInvoice=xdInvoice.DocumentElement;
			invoiceNum=ms_InvoiceCounter++;
			xeInvoice.SetAttribute("InvoiceID",invoiceNum.ToString());

			Random r=new Random((int)DateTime.Now.Ticks/int.Parse(Thread.CurrentThread.Name));
			sleep=r.Next(Global.InvoiceAppMinSleepTime,Global.InvoiceAppMaxSleepTime);
			Thread.Sleep(sleep);
#if Interceptor
			BAMInterceptor interceptor=Global.LoadInterceptor("BAMApiInvoice_interceptor.bin");
			interceptor.OnStep(Global.dataExtractor,"locNewInvoice",xeInvoice,Global.es);
#else
				Global.es.BeginActivity("BAMApiInvoice",invoiceNum.ToString());
#endif

			lock(PosToInvoice)
			{
				if (PosToInvoice.Count<Global.InvoicePoMinCount)
					return;

				while (PosToInvoice.Count>0)
				{
					XmlElement xePo=(XmlElement)PosToInvoice.Dequeue();
					string poid=xePo.GetAttribute("PoID");
					XmlElement xePoRef=xdInvoice.CreateElement("PoRef");
					xePoRef.SetAttribute("PoID",poid);
					xeInvoice.AppendChild(xePoRef);
					
					int amount=int.Parse(xePo.SelectSingleNode("Price").InnerText);
					total+=amount;
					polist+=poid+" ";

#if Interceptor
					interceptor.OnStep(Global.dataExtractor,"locAddPoToInvoice",xeInvoice,Global.es);
#else
					Global.es.AddRelatedActivity(
						"BAMApiInvoice",invoiceNum.ToString(),
						"BAMApiPo",poid.ToString());
#endif
				}
			}
			xeInvoice.SetAttribute("Total",total.ToString());
			Console.WriteLine("Invoice #"+invoiceNum+" send for { "+polist+"}");

#if Interceptor
			interceptor.OnStep(Global.dataExtractor,"locSendInvoice",xeInvoice,Global.es);
#else
			Global.es.UpdateActivity("BAMApiInvoice",invoiceNum.ToString(),
				"Send",DateTime.UtcNow,
				"Total",xeInvoice.GetAttribute("Total"));
#endif			
			// Simulate delay for Invoice
			sleep=r.Next(Global.InvoiceWaitMinTime,Global.InvoiceWaitMaxTime);
			Thread.Sleep(sleep);
			Console.WriteLine("Invoice "+invoiceNum+" was paid");
#if Interceptor
			interceptor.OnStep(Global.dataExtractor,"locInvoicePaid",xeInvoice,Global.es);
#else
			Global.es.UpdateActivity("BAMApiInvoice",invoiceNum.ToString(),"Paid",DateTime.UtcNow);
			Global.es.EndActivity("BAMApiInvoice",invoiceNum.ToString());
#endif
		}

		public void Run()
		{
			try
			{
				while (true)
				{
					RunOnce();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception in Invoice thread \n\n"+ex.Message);
				if (null!=ex.InnerException)
				{
					Console.WriteLine("\nInnerException: \n"+ex.InnerException.Message);
				}
			}
		}

	}



	class MainApp
	{	
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				Global.es=new DirectEventStream(
					"Integrated Security=SSPI;Data Source=.;Initial Catalog=BAMPrimaryImport",
					Global.FlushTreshold);

				for (int i=0; i<Global.PoThreads; i++)
				{
					PoApplication poApp=new PoApplication();
					Thread t=new Thread(new ThreadStart(poApp.Run));
					t.Name=((i+1)*10).ToString();
					t.Start();
				}
				for (int i=0; i<Global.ShipmentThreads; i++)
				{
					ShipmentApplication shApp=new ShipmentApplication();
					Thread t=new Thread(new ThreadStart(shApp.Run));
					t.Name=((i+1)*10).ToString();
					t.Start();
				}
				for (int i=0; i<Global.InvoiceThreads; i++)
				{
					InvoiceApplication invApp=new InvoiceApplication();
					Thread t=new Thread(new ThreadStart(invApp.Run));
					t.Name=((i+1)*10).ToString();
					t.Start();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception in Main thread \n\n"+ex.Message);
				if (null!=ex.InnerException)
				{
					Console.WriteLine("\nInnerException: \n"+ex.InnerException.Message);
				}
			}
		}
	}

	// Implementation of the callback that the Interceptor uses to retrieve data
	public class DataExtractor : IBAMDataExtractor
	{
		public object GetValue(object trackItem, object location, object data)
		{
			string xpath=(string)trackItem;
			if (xpath.Length>0)
			{
				XmlNode xn=(XmlNode)data;
				XmlNode dataItem=xn.SelectSingleNode(xpath);
				if (null==dataItem)
				{
					return null;
				}
				else
				{
					return dataItem.InnerText;
				}
			}
			else
			{
				return DateTime.UtcNow;
			}
		}
	}
}
