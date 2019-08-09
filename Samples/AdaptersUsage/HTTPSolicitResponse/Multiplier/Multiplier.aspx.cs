//---------------------------------------------------------------------
// File: Multiplier.aspx.cs
// 
// Summary: Code that implements the logic of the Multiplier web appliation.
//
// Sample: HttpSolicitResponse
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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace Microsoft.Samples.BizTalk.Multiplier
{
	/// <summary>
	/// Summary description for Multiplier.
	/// </summary>
	public partial class Multiplier : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
		

		public string Multiply(HttpRequest req)
		{
			try
			{
				// retrieve the HTTP request stream
				StreamReader r = new StreamReader(req.InputStream);

				// use the forward-only, read-only XmlTextReader object to query the XML document
				XmlTextReader reader = new XmlTextReader(new StringReader(r.ReadToEnd()));
				reader.Namespaces = false;

				XmlNameTable nt = new NameTable();
				XmlDocument xmlDoc = new XmlDocument(nt);
				xmlDoc.Load(reader);

				// traverse the XML DOM and retrieve the elements in question
				XmlNode root = xmlDoc.SelectSingleNode("MultiplyRequest");
				XmlNode nodeA = root.SelectSingleNode("Operand1");
				XmlNode nodeB = root.SelectSingleNode("Operand2");

				// perform multiply operation on operands
				int n1 = Int32.Parse(nodeA.InnerText);
				int n2 = Int32.Parse(nodeB.InnerText);
				int result = n1 * n2;

				// create an XML object
				XmlDocument multiplyResponse = new XmlDocument();
				multiplyResponse.PreserveWhitespace = true;

				// create a string for the namespace of this XML document
				string targetNamespace = "http://HttpSolicitResponse.schemas.MultiplyResponse";

				// create the root node named MultiplyResponse
				XmlNode tempXmlNode = multiplyResponse.CreateNode(XmlNodeType.Element, "ns0", "MultiplyResponse", targetNamespace);
				multiplyResponse.AppendChild(tempXmlNode);

				// create the Item element
				tempXmlNode = multiplyResponse.CreateNode(XmlNodeType.Element, "ns0" ,"Result", targetNamespace);
				tempXmlNode.InnerText = result.ToString();
				multiplyResponse.DocumentElement.AppendChild(tempXmlNode);

				// return the XML document with the result
				return multiplyResponse.OuterXml;

			}
			catch (Exception ex) 
			{
				return ex.ToString();
			}

		}
	}
}
