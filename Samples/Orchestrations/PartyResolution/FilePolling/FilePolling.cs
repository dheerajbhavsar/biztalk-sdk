//---------------------------------------------------------------------
// File: FilePolling.cs
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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Xml;
using System.Resources;
using System.Reflection;
using Microsoft.Win32;

namespace Microsoft.Samples.BizTalk.FilePolling
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Timer tmrPODeliveryReceipt;
		private System.Windows.Forms.Timer tmrBuyer;
		private System.Windows.Forms.Timer tmrAgency2;
		private System.Windows.Forms.Timer tmrAgency1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button exitButton;
		private System.Resources.ResourceManager rm; 
		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Get the install path for the BTS PartyResolution sample
			samplePath = Path.Combine(Registry.LocalMachine.OpenSubKey(
				@"SOFTWARE\Microsoft\BizTalk Server\3.0").GetValue("InstallPath").ToString(),
				@"SDK\Samples\Orchestrations\PartyResolution");
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.btnStart = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.tmrPODeliveryReceipt = new System.Windows.Forms.Timer(this.components);
			this.tmrBuyer = new System.Windows.Forms.Timer(this.components);
			this.tmrAgency2 = new System.Windows.Forms.Timer(this.components);
			this.tmrAgency1 = new System.Windows.Forms.Timer(this.components);
			this.exitButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(72, 184);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(120, 23);
			this.btnStart.TabIndex = 0;
			this.btnStart.Text = "&Start Polling";
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(416, 160);
			this.label1.TabIndex = 1;
			// 
			// tmrPODeliveryReceipt
			// 
			this.tmrPODeliveryReceipt.Tick += new System.EventHandler(this.tmrPODeliveryReceipt_Tick);
			// 
			// tmrBuyer
			// 
			this.tmrBuyer.Tick += new System.EventHandler(this.tmrBuyer_Tick);
			// 
			// tmrAgency2
			// 
			this.tmrAgency2.Tick += new System.EventHandler(this.tmrAgency2_Tick);
			// 
			// tmrAgency1
			// 
			this.tmrAgency1.Tick += new System.EventHandler(this.tmrAgency1_Tick);
			// 
			// exitButton
			// 
			this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.exitButton.Location = new System.Drawing.Point(243, 184);
			this.exitButton.Name = "exitButton";
			this.exitButton.Size = new System.Drawing.Size(120, 23);
			this.exitButton.TabIndex = 2;
			this.exitButton.Text = "&Exit";
			this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.exitButton;
			this.ClientSize = new System.Drawing.Size(432, 213);
			this.Controls.Add(this.exitButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnStart);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "Form1";
			this.Text = "File Polling";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
		/// <summary>
		/// Enable Timer controls and set the interval for polling
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnStart_Click(object sender, System.EventArgs e)
		{
			if (!Directory.Exists(Path.Combine(samplePath, @"FileDrop\BuyerInformation")))
			{
				MessageBox.Show(rm.GetString("MissingPollingLocationBuyer"),rm.GetString("MsgBoxTitle"));
				return;
			}

			if (!Directory.Exists(Path.Combine(samplePath, @"FileDrop\PODeliveryReceipt")))
			{
				MessageBox.Show(rm.GetString("MissingPollingLocationPODeliveryReceipt"),rm.GetString("MsgBoxTitle"));
				return;
			}

			if (!Directory.Exists(Path.Combine(samplePath, @"FileDrop\ShipmentAgency2")))
			{
				MessageBox.Show(rm.GetString("MissingPollingLocationShipmentAgency2"),rm.GetString("MsgBoxTitle"));
				return;
			}

			if (!Directory.Exists(Path.Combine(samplePath, @"FileDrop\ShipmentAgency1")))
			{
				MessageBox.Show(rm.GetString("MissingPollingLocationShipmentAgency1"),rm.GetString("MsgBoxTitle"));
				return;
			}

			// Set 2 seconds as time interval
			tmrBuyer.Interval = 2000;
			tmrBuyer.Enabled = true;

			tmrAgency2.Interval = 2000;
			tmrAgency2.Enabled = true;

			tmrAgency1.Interval = 2000;
			tmrAgency1.Enabled = true;

			tmrPODeliveryReceipt.Interval = 2000;
			tmrPODeliveryReceipt.Enabled = true;

			btnStart.Enabled = false;
			arrivedMessages = 0;
		}

		/// <summary>
		/// Poll for Buyer information. If found Alert the User with Buyer Information
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tmrBuyer_Tick(object sender, System.EventArgs e)
		{
			if (FileExistsAndReleased(Path.Combine(samplePath, @"FileDrop\BuyerInformation\Buyer.xml")))
			{
				tmrBuyer.Enabled = false;

				StreamReader objSR =  File.OpenText(Path.Combine(samplePath, @"FileDrop\BuyerInformation\Buyer.xml"));

				string strBuyer = objSR.ReadLine();

				XmlDocument objDom = new XmlDocument();
				objDom.LoadXml(strBuyer);

                MessageBox.Show(rm.GetString("BuyerInfoMsg") + "  " + objDom.SelectSingleNode("Buyer").InnerText, rm.GetString("MsgBoxTitleBuyerInfo"));

				if (++arrivedMessages == 4)
					btnStart.Enabled = true;

				objSR.Close();

				File.Delete(Path.Combine(samplePath, @"FileDrop\BuyerInformation\Buyer.xml"));

				tmrBuyer.Enabled = true;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_Load(object sender, System.EventArgs e)
		{
				rm = new ResourceManager("Microsoft.Samples.BizTalk.FilePolling.Form1",Assembly.GetAssembly(typeof(Form1)));
				label1.Text = rm.GetString("label1Text");
		}

		/// <summary>
		/// Poll for DeliveryReceipt to be send to Buyer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tmrPODeliveryReceipt_Tick(object sender, System.EventArgs e)
		{
			if (FileExistsAndReleased(Path.Combine(samplePath, @"FileDrop\PODeliveryReceipt\PODeliveryReceipt.xml")))
			{
				tmrPODeliveryReceipt.Enabled = false;

                MessageBox.Show(rm.GetString("strPODeliveryRcpt"), rm.GetString("MsgBoxTitleBuyerInfo")); 

				if (++arrivedMessages == 4)
					btnStart.Enabled = true;

				System.Guid objGUID =  System.Guid.NewGuid();

				File.Move(Path.Combine(samplePath, @"FileDrop\PODeliveryReceipt\PODeliveryReceipt.xml"), Path.Combine(Path.Combine(samplePath, @"FileDrop\PODeliveryReceipt\"), objGUID.ToString() + ".XML"));

				tmrPODeliveryReceipt.Enabled = true;
			}
		}

		/// <summary>
		/// Poll Order files for Agency2
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tmrAgency2_Tick(object sender, System.EventArgs e)
		{
			if (FileExistsAndReleased(Path.Combine(samplePath, @"FileDrop\ShipmentAgency2\ShipmentAgency2.txt")))
			{
				tmrAgency2.Enabled = false;

				XmlDocument objDom = new XmlDocument();

				objDom.Load(Path.Combine(samplePath, @"FileDrop\ShipmentAgency2\ShipmentAgency2.txt"));

				if (objDom.DocumentElement.Name =="ns0:ShipmentOrderRequest")
					MessageBox.Show(rm.GetString("SuppOrderRequest"),rm.GetString("MsgBoxTitleAgency2"));

				else if(objDom.DocumentElement.Name=="ns0:ShipmentAdvice")
					MessageBox.Show(rm.GetString("SuppAdviceRequest"),rm.GetString("MsgBoxTitleAgency2"));

				if (++arrivedMessages == 4)
					btnStart.Enabled = true;

				File.Move(Path.Combine(samplePath, @"FileDrop\ShipmentAgency2\ShipmentAgency2.txt"),
						Path.Combine(samplePath, @"FileDrop\ShipmentAgency2\ShipmentAgency2.xml"));

				tmrAgency2.Enabled = true;
			}
		}

		/// <summary>
		/// Poll for Order files for Agency1
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tmrAgency1_Tick(object sender, System.EventArgs e)
		{
			if (FileExistsAndReleased(Path.Combine(samplePath, @"FileDrop\ShipmentAgency1\ShipmentAgency1.txt")))
			{
				tmrAgency1.Enabled = false;

				XmlDocument objDom = new XmlDocument();

				objDom.Load(Path.Combine(samplePath, @"FileDrop\ShipmentAgency1\ShipmentAgency1.txt"));

				if (objDom.DocumentElement.Name =="ns0:ShipmentOrderRequest")
					MessageBox.Show(rm.GetString("SuppOrderRequest"),rm.GetString("MsgBoxTitleAgency1"));

				else if (objDom.DocumentElement.Name =="ns0:ShipmentAdvice")
					 MessageBox.Show(rm.GetString("SuppAdviceRequest"),rm.GetString("MsgBoxTitleAgency1"));
					
				if (++arrivedMessages == 4)
					btnStart.Enabled = true;

				File.Move(Path.Combine(samplePath, @"FileDrop\ShipmentAgency1\ShipmentAgency1.txt"),
							Path.Combine(samplePath, @"FileDrop\ShipmentAgency1\ShipmentAgency1.xml"));

				tmrAgency1.Enabled = true;
			}
		
		}

		private void exitButton_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private bool FileExistsAndReleased(string fileName)
		{
			// Check if file exists
			if (!File.Exists(fileName))
				return false;

			try
			{
				// Check whether file is released, i.e. can be opened without IOException
				FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				stream.Close();
			}
			catch (IOException)
			{
				// File is locked
				return false;
			}
			
			// File exists and released
			return true;
		}

		private string samplePath;
		private int arrivedMessages = 0;
	}
}
