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
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Xml;
using System.Net;
using System.Text;
using System.IO;

namespace Microsoft.Samples.BizTalk.RequestResponse
{
	
	public partial class RequestResponseWebForm : System.Web.UI.Page
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

        protected void placeOrder_Click(object sender, System.EventArgs e) {
            // TODO: The following line of code must be changed to reflect your environment.
            // The format of the location is as follows:
            //
            // http://<servername>/<virtual dir>/BTSHTTPReceive.dll
            //
            // where <servername> = the machine name of the web server you will be posting to
            //       <virtual dir> = the virtual directory where this file resides


            // indicates the location where the HTTP request is to be sent
            string requestLocation = "http://localhost/HTTPRequestResponseSample/BTSHTTPReceive.dll";

            // create an XML object
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;

            // create the root node named PO
            XmlNode tempXmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "PO", "http://HTTPRequestResponse.POSchema");
            xmlDocument.AppendChild(tempXmlNode);

            // create the Item element
            tempXmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "Item", "");
            tempXmlNode.InnerText = item.SelectedItem.Text;
            xmlDocument.DocumentElement.AppendChild(tempXmlNode);

            // create the Price element
            tempXmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "Price", "");
            tempXmlNode.InnerText = estimatedPrice.Text;
            xmlDocument.DocumentElement.AppendChild(tempXmlNode);

            try {

                // create a web request and set the method of the request to POST
                HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(requestLocation);
                request.Method = "POST";

                // encode the data and store the byte representation in an array
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] requestData = encoding.GetBytes(xmlDocument.OuterXml);

                // set the content type of the data being posted.
                request.ContentType="application/x-www-form-urlencoded";

                // set the content length of the string being posted
                request.ContentLength = xmlDocument.OuterXml.Length;

                status.Text += "<br>" + System.Web.HttpUtility.HtmlEncode("Submitting order to BizTalk ISAPI extension.") + "<br>";

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(requestData,0,requestData.Length);

                // flush out the data to the underlying stream and close it
                requestStream.Flush();
                requestStream.Close();

                // retrieve the response from the web server
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                StreamReader responseData = new StreamReader(response.GetResponseStream());

                // output PO status to the user            
                status.Text += System.Web.HttpUtility.HtmlEncode("Thank-you, your purchase order has been processed.") + "<br>";

				// retrieve status information from server response and display to the user
				XmlDocument responseXmlDocument = new XmlDocument();
				responseXmlDocument.LoadXml(responseData.ReadToEnd());
				XmlNode statusXmlNode = responseXmlDocument.SelectSingleNode("/*[local-name()='POAck' and namespace-uri()='http://HTTPRequestResponse.POAck']/*[local-name()='Status' and namespace-uri()='']");
                status.Text += System.Web.HttpUtility.HtmlEncode("Your order status is: " + statusXmlNode.InnerXml);

            } catch (WebException wex) {

                status.Text = "Unable to complete web request. Web Exception error: " + wex.Message;

            }   
        }
	}
}
