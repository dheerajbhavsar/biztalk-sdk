
//---------------------------------------------------------------------
// File: TransactionalAdapterManagement.cs
// 
// Summary: Transactional Adapter - BizTalk Admin Property Sheet Implementation. 
//
// Sample: Adapter framework transactional adapter.
//
//---------------------------------------------------------------------
// This file is part of the Microsoft BizTalk Server SDK
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// This source code is intended only as a supplement to Microsoft BizTalk
// Server release and/or on-line documentation. See these other
// materials for detailed information regarding Microsoft code samples.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
// PURPOSE.
//---------------------------------------------------------------------

using System;
using System.Data.OleDb;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Microsoft.BizTalk.Adapter.Framework;
using Microsoft.Samples.BizTalk.Adapter.Common;

namespace Microsoft.Samples.BizTalk.Adapter.TransactionalAdmin
{
	/// <summary>
	/// Summary description for AdapterManagement.
	/// </summary>
	[CLSCompliant(false)]
	public class TransactionalAdapterManagement :
		AdapterManagementBase,
		IAdapterConfig,
		IAdapterConfigValidation 
	{
		public TransactionalAdapterManagement()
		{
		}

		//  IAdapterConfig
		public string GetConfigSchema (ConfigType type)
		{
			switch (type)
			{
				case ConfigType.TransmitHandler:
                    return GetSchemaFromResource("Microsoft.Samples.BizTalk.Adapter.TransactionalAdmin.TransactionalTransmitHandler.xsd");

				case ConfigType.TransmitLocation:
                    return GetSchemaFromResource("Microsoft.Samples.BizTalk.Adapter.TransactionalAdmin.TransactionalTransmitLocation.xsd");

				case ConfigType.ReceiveHandler:
                    return GetSchemaFromResource("Microsoft.Samples.BizTalk.Adapter.TransactionalAdmin.TransactionalReceiveHandler.xsd");
				
				case ConfigType.ReceiveLocation:
                    return GetSchemaFromResource("Microsoft.Samples.BizTalk.Adapter.TransactionalAdmin.TransactionalReceiveLocation.xsd");
			}

			return String.Empty;
		}

		// used to get the WSDL file name for the selected WSDL.
		public string [] GetServiceDescription(string [] wsdls)
		{
			string []result = null;
			return result;
		}

		//used to acquire externally referenced xsd's
		public Result GetSchema( string xsdLocation, string xsdNamespace, out string xsdFileName)
		{
			xsdFileName = string.Empty;
			return Result.Continue;
		}

		public string ValidateConfiguration(ConfigType type, string config)
		{
			try
			{
				switch (type)
				{
					case ConfigType.TransmitHandler:
						return config;

					case ConfigType.TransmitLocation:
						return ValidateLocation(config);
					
					case ConfigType.ReceiveHandler:
						return config;
					
					case ConfigType.ReceiveLocation:
						return ValidateLocation(config);
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
			return String.Empty;
		}

		private string ValidateLocation (string config)
		{
			XmlDocument configDOM = new XmlDocument();
			configDOM.LoadXml(config);

            // important: must add the URI to the schema...
			AddUriNode(configDOM);

			StringWriter s = new StringWriter();
			XmlTextWriter writer = new XmlTextWriter(s);
			configDOM.WriteTo(writer);

			string result = s.ToString();
			return result;
		}

		private void AddUriNode (XmlDocument configDOM)
		{
            //  we are going to put the NULL URI address and action inside our BizTalk URI for the moment

            // get the fields from the configuration XML
			XmlNode nodeCookie = configDOM.SelectSingleNode("Config/cookie");
            if (nodeCookie == null || nodeCookie.InnerText == String.Empty)
			{
                throw new Exception("can find node Config/cookie");
			}
            string cookie = nodeCookie.InnerText;

            //  build the URI
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = "txn";
            uriBuilder.Host = Uri.EscapeDataString(cookie);

            string uri = uriBuilder.ToString();

            //  put the URI back into the configuration XML - so much for normalization!
			XmlNode uriNode = configDOM.SelectSingleNode("Config/uri");
			if (null == uriNode)
			{
				uriNode = configDOM.CreateNode(XmlNodeType.Element, "uri", String.Empty);
				XmlNode configNode = configDOM.SelectSingleNode("Config");
				configNode.AppendChild(uriNode);
			}
			uriNode.InnerText = uri;
		}
	}
}
