//---------------------------------------------------------------------
// File:	AddMapping.cs
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
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.BizTalk.SSOClient.Interop;

namespace Microsoft.Samples.BizTalk.SSO
{
	/// <summary>
	/// Summary description for AddMapping.
	/// </summary>
	public class AddMapping : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.TextBox User;
		public System.Windows.Forms.TextBox XU;
		public System.Windows.Forms.TextBox XP;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button OK;
		private System.Windows.Forms.Button Cancel;
		public System.Windows.Forms.ComboBox Applications;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.TextBox XPConfirm;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AddMapping()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			this.label1 = new System.Windows.Forms.Label();
			this.Applications = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.User = new System.Windows.Forms.TextBox();
			this.XU = new System.Windows.Forms.TextBox();
			this.XP = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.OK = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.XPConfirm = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Application:";
			// 
			// Applications
			// 
			this.Applications.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Applications.Location = new System.Drawing.Point(136, 16);
			this.Applications.Name = "Applications";
			this.Applications.Size = new System.Drawing.Size(240, 21);
			this.Applications.Sorted = true;
			this.Applications.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "User name:";
			// 
			// User
			// 
			this.User.Location = new System.Drawing.Point(136, 48);
			this.User.Name = "User";
			this.User.Size = new System.Drawing.Size(240, 20);
			this.User.TabIndex = 3;
			this.User.Text = SystemInformation.UserDomainName+"\\"+SystemInformation.UserName;
			this.User.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// XU
			// 
			this.XU.Location = new System.Drawing.Point(136, 104);
			this.XU.Name = "XU";
			this.XU.Size = new System.Drawing.Size(240, 20);
			this.XU.TabIndex = 6;
			this.XU.Text = "";
			this.XU.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// XP
			// 
			this.XP.Location = new System.Drawing.Point(136, 136);
			this.XP.Name = "XP";
			this.XP.PasswordChar = '*';
			this.XP.Size = new System.Drawing.Size(240, 20);
			this.XP.TabIndex = 8;
			this.XP.Text = "";
			this.XP.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 23);
			this.label3.TabIndex = 5;
			this.label3.Text = "External user name:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 136);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "External user password:";
			// 
			// OK
			// 
			this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OK.Enabled = false;
			this.OK.Location = new System.Drawing.Point(104, 208);
			this.OK.Name = "OK";
			this.OK.TabIndex = 11;
			this.OK.Text = "OK";
			this.OK.Click += new System.EventHandler(this.OK_Click);
			// 
			// Cancel
			// 
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel.Location = new System.Drawing.Point(208, 208);
			this.Cancel.Name = "Cancel";
			this.Cancel.TabIndex = 12;
			this.Cancel.Text = "Cancel";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 80);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(344, 16);
			this.label5.TabIndex = 4;
			this.label5.Text = "Specify a valid local windows account that already exists:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 168);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(128, 23);
			this.label6.TabIndex = 9;
			this.label6.Text = "Confirm password:";
			// 
			// XPConfirm
			// 
			this.XPConfirm.Location = new System.Drawing.Point(136, 168);
			this.XPConfirm.Name = "XPConfirm";
			this.XPConfirm.PasswordChar = '*';
			this.XPConfirm.Size = new System.Drawing.Size(240, 20);
			this.XPConfirm.TabIndex = 10;
			this.XPConfirm.Text = "";
			this.XPConfirm.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// AddMapping
			// 
			this.AcceptButton = this.OK;
			//this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScaleDimensions = new System.Drawing.Size(5, 13);
			this.CancelButton = this.Cancel;
			this.ClientSize = new System.Drawing.Size(384, 246);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.XPConfirm);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.OK);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.XP);
			this.Controls.Add(this.XU);
			this.Controls.Add(this.User);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.Applications);
			this.Controls.Add(this.label1);
			this.Name = "AddMapping";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add mapping";
			this.Load += new System.EventHandler(this.AddMapping_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void OnTextChanged(object sender, System.EventArgs e)
		{
			OK.Enabled=User.Text.Length>0 && XU.Text.Length>0 && XP.Text.Length>0 && XP.Text==XPConfirm.Text;
		}

		private void AddMapping_Load(object sender, System.EventArgs e)
		{
			string[] apps=SsoConfigurator.GetApplications();
			foreach(string app in apps)
			{
				Applications.Items.Add(app);
			}

			SsoPage ssoPage=(SsoPage)((SsoSampleWizard)Owner).GetPage(typeof(SsoPage));
			if(ssoPage.Applications.Count!=0)
			{
				foreach(SsoApplication application in ssoPage.Applications)
				{
					Applications.Items.Add(application.Name);
				}
			}

			Applications.SelectedIndex=0;
		}

		
		private void OK_Click(object sender, System.EventArgs e)
		{
			
		}		
	}
}
