//---------------------------------------------------------------------
// File:	BtsPage.cs
// 
// Summary: 	None
//
// Sample: 	HTTP SSO
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
using System.Windows.Forms;
using Microsoft.BizTalk.SSOClient.Interop;

namespace Microsoft.Samples.BizTalk.SSO
{
	#region BtsPage
	/// <summary>
	/// BTS page
	/// </summary>
	public class BtsPage : WorkPage
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.TextBox ReceivePortName;
		public System.Windows.Forms.TextBox ReceiveLocationName;
		public System.Windows.Forms.TextBox SendPortName;
		public System.Windows.Forms.TextBox SendPortUri;
		private System.Windows.Forms.Label label7;
		public System.Windows.Forms.ComboBox Applications;
		private System.ComponentModel.IContainer components=null;

		/// <summary>
		/// Default constructor
		/// </summary>
		public BtsPage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing)
			{
				if(components!=null) 
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.ReceivePortName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.ReceiveLocationName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SendPortName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.SendPortUri = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.Applications = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
			this.SuspendLayout();
			// 
			// Title
			// 
			this.Title.Text = "Configure BizTalk receive and send ports";
			this.Title.Click += new System.EventHandler(this.Title_Click);
			// 
			// Subtitle
			// 
			this.Subtitle.Text = "This sample is using one receive port with one HTTP receive location and one HTTP" +
				" send port subscribed on messages received by this receive port";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(44, 112);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 23);
			this.label1.TabIndex = 5;
			this.label1.Text = "Receive port name:";
			// 
			// ReceivePortName
			// 
			this.ReceivePortName.Location = new System.Drawing.Point(172, 112);
			this.ReceivePortName.Name = "ReceivePortName";
			this.ReceivePortName.Size = new System.Drawing.Size(280, 20);
			this.ReceivePortName.TabIndex = 6;
			this.ReceivePortName.Text = "SsoSampleReceivePort";
			this.ReceivePortName.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(44, 136);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128, 23);
			this.label2.TabIndex = 7;
			this.label2.Text = "Receive location name:";
			// 
			// ReceiveLocationName
			// 
			this.ReceiveLocationName.Location = new System.Drawing.Point(172, 136);
			this.ReceiveLocationName.Name = "ReceiveLocationName";
			this.ReceiveLocationName.Size = new System.Drawing.Size(280, 20);
			this.ReceiveLocationName.TabIndex = 8;
			this.ReceiveLocationName.Text = "SsoSampleReceiveLocation";
			this.ReceiveLocationName.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(44, 216);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 23);
			this.label3.TabIndex = 9;
			this.label3.Text = "Send port name:";
			// 
			// SendPortName
			// 
			this.SendPortName.Location = new System.Drawing.Point(172, 216);
			this.SendPortName.Name = "SendPortName";
			this.SendPortName.Size = new System.Drawing.Size(280, 20);
			this.SendPortName.TabIndex = 10;
			this.SendPortName.Text = "SsoSampleSendPort";
			this.SendPortName.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(44, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(408, 40);
			this.label4.TabIndex = 11;
			this.label4.Text = "In this sample receive port is used to receive message from client over HTTP prot" +
				"ocol using Windows Integrated authentication and store the credentials used by c" +
				"lient for future use.";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(44, 168);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(408, 40);
			this.label5.TabIndex = 12;
			this.label5.Text = "Send port will be correspondingly configured to use stored user credentials for o" +
				"utbound HTTP Basic authentication with the server application receiving the mess" +
				"ages from this send port.";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(44, 240);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(112, 23);
			this.label6.TabIndex = 13;
			this.label6.Text = "Send port URI:";
			// 
			// SendPortUri
			// 
			this.SendPortUri.Location = new System.Drawing.Point(172, 240);
			this.SendPortUri.Name = "SendPortUri";
			this.SendPortUri.Size = new System.Drawing.Size(280, 20);
			this.SendPortUri.TabIndex = 14;
			this.SendPortUri.Text = "http://localhost/SsoSampleServerApplication/EmployeeData.aspx";
			this.SendPortUri.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(44, 264);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(116, 23);
			this.label7.TabIndex = 15;
			this.label7.Text = "Affiliate application:";
			// 
			// Applications
			// 
			this.Applications.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Applications.FormattingEnabled = true;
			this.Applications.Location = new System.Drawing.Point(172, 264);
			this.Applications.Name = "Applications";
			this.Applications.Size = new System.Drawing.Size(280, 21);
			this.Applications.TabIndex = 5;
			this.Applications.SelectedIndexChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// BtsPage
			// 
			this.Controls.Add(this.Applications);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.SendPortUri);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.SendPortName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.ReceiveLocationName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.ReceivePortName);
			this.Controls.Add(this.label1);
			this.Name = "BtsPage";
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.ReceivePortName, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.ReceiveLocationName, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.SendPortName, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.Header, 0);
			this.Controls.SetChildIndex(this.Separator, 0);
			this.Controls.SetChildIndex(this.Title, 0);
			this.Controls.SetChildIndex(this.Subtitle, 0);
			this.Controls.SetChildIndex(this.Picture, 0);
			this.Controls.SetChildIndex(this.label5, 0);
			this.Controls.SetChildIndex(this.label6, 0);
			this.Controls.SetChildIndex(this.SendPortUri, 0);
			this.Controls.SetChildIndex(this.label7, 0);
			this.Controls.SetChildIndex(this.Applications, 0);
			((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		/// <summary>
		/// Called when the PageBase is activated
		/// </summary>
		/// <returns>True if activation successfull, otherwise false</returns>
		public override bool OnSetActive()
		{
			if(!base.OnSetActive())
			{
				return false;
			}

			// Fill the applications drop down box
			string selected=null;
			
			if(Applications.Items.Count!=0)
			{
				selected=Applications.Text;
			}

			Applications.Items.Clear();

			string[] apps=SsoConfigurator.GetApplications();
			foreach(string app in apps)
			{
				Applications.Items.Add(app);
			}

			SsoPage ssoPage=(SsoPage)Wizard.GetPage(typeof(SsoPage));
			if(ssoPage.Applications.Count!=0)
			{
				foreach(SsoApplication application in ssoPage.Applications)
				{
					Applications.Items.Add(application.Name);
				}
			}

			if(selected!=null)
			{
				Applications.SelectedIndex=Applications.Items.IndexOf(selected);
			}
			else
			{
				if(Applications.Items.Count!=0)
				{
					Applications.SelectedIndex=0;
				}
			}

			// Fill the URI field
			IisPage iisPage=(IisPage)Wizard.GetPage(typeof(IisPage));
			int firstSlash=SendPortUri.Text.LastIndexOf("/");
			if(firstSlash!=-1)
			{
				int secondSlash=SendPortUri.Text.LastIndexOf("/", firstSlash-1);
				if(secondSlash!=-1)
				{
					SendPortUri.Text=SendPortUri.Text.Replace(
						SendPortUri.Text.Substring(secondSlash+1, firstSlash-secondSlash-1), 
						iisPage.SendName.Text);
				}
			}

			OnTextChanged(null, null);
			
			return true;
		}

		private void OnTextChanged(object sender, System.EventArgs e)
		{
			if(Wizard==null)
			{
				return;
			}

			if(ReceivePortName.Text.Length>0 && SendPortName.Text.Length>0 &&		// names are not empty
				ReceivePortName.Text.ToLower()!=SendPortName.Text.ToLower() &&		// names are different
				ReceiveLocationName.Text.Length>0 && Applications.Items.Count!=0 &&	// location and app are not empty
				SendPortUri.Text.Length>0)											// URI is not empty
			{
				Wizard.SetWizardButtons(WizardButtonType.Back | WizardButtonType.Next);
			}
			else
			{
				Wizard.SetWizardButtons(WizardButtonType.Back);
			}
		}

		private void Title_Click(object sender, EventArgs e)
		{

		}
	}
	#endregion
}
