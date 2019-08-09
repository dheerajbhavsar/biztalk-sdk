//---------------------------------------------------------------------
// File: QueryShipmentCatalog.cs
// 
// Sample: PartyResolution
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
using System.Xml;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace Microsoft.Samples.BizTalk.QueryShipmentCatalog
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	/// 
	[Serializable()]
	public class QueryShipmentDetails
	{
		public QueryShipmentDetails()
		{
			// Get the install path for the BTS PartyResolution sample
			samplePath = Path.Combine(Registry.LocalMachine.OpenSubKey(
				@"SOFTWARE\Microsoft\BizTalk Server\3.0").GetValue("InstallPath").ToString(),
				@"SDK\Samples\Orchestrations\PartyResolution");
		}
		/// <summary>
		/// This Method is invoked by Supplier Orchestration.
		/// Shipment Agency is decided based on the country/region. This catalog information is stored in PartyResolution\Catalog\Catalog.xml
		/// Supplier Orchestration uses this method to query Catalog information
		/// </summary>
		/// <param name="strCountry"></param>
		/// <returns></returns>
		public string GetShipmentDetails(string strCountry)
		{
			// Validate the input:
			// Allow a string that has only alpha characters or spaces
			// between the start and end of the string
			if(!Regex.IsMatch(strCountry, @"^[a-zA-Z ]+$")) 
				throw new ArgumentException("Country/Region is not valid", "strCountry");

			XmlDocument objXmlDoc = new XmlDocument();
			objXmlDoc.Load(Path.Combine(samplePath, @"Catalog\Catalog.xml"));
			XmlNode shipmentNameNode = objXmlDoc.SelectSingleNode("//Provider[@Country='" + strCountry + "']/@Name");
			return shipmentNameNode != null ? shipmentNameNode.InnerText : "";
		}

		private string samplePath;
	}
}
