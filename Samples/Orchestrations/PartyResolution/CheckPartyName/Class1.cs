//---------------------------------------------------------------------
// File: Class1.cs
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
using System.IO;
using System.Xml;
using Microsoft.Win32;

namespace Microsoft.Samples.BizTalk.CheckPartyName
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	/// 
	[Serializable()]
	public class Class1
	{
		public Class1()
		{
			// Get the install path for the BTS PartyResolution sample
			samplePath = Path.Combine(Registry.LocalMachine.OpenSubKey(
				@"SOFTWARE\Microsoft\BizTalk Server\3.0").GetValue("InstallPath").ToString(),
				@"SDK\Samples\Orchestrations\PartyResolution");
		}

		/// <summary>
		/// This method is invoked by Supplier Orchestration.
		/// GetPartName Method intakes the Party Object passed by Orchestration.
		/// This method extract the party name and updates Buyer.xml for File polling.exe to Alert the User
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public string GetPartName(Microsoft.XLANGs.BaseTypes.Party p)
		{
			XmlTextWriter xmlWriter = new XmlTextWriter(Path.Combine(samplePath, @"FileDrop\BuyerInformation\Buyer.xml"), null);
			xmlWriter.WriteElementString("Buyer", p.Name);
			xmlWriter.Close();
			return p.Name;
		}

		private string samplePath;
	}
}
