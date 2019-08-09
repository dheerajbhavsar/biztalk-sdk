//---------------------------------------------------------------------
// File:	AddApplication.cs
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

namespace Microsoft.Samples.BizTalk.SSO
{
	/// <summary>
	/// Summary description for AddApplication.
	/// </summary>
	public class AddApplication : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button OK;
		private System.Windows.Forms.Button Cancel;
		public System.Windows.Forms.TextBox Application;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.TextBox Administrator;
		public System.Windows.Forms.TextBox User;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AddApplication()
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
			this.Application = new System.Windows.Forms.TextBox();
			this.OK = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.Administrator = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.User = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Application name:";
			// 
			// Application
			// 
			this.Application.Location = new System.Drawing.Point(128, 16);
			this.Application.Name = "Application";
			this.Application.Size = new System.Drawing.Size(248, 20);
			this.Application.TabIndex = 0;
			this.Application.Text = "SsoSampleApplication";
			this.Application.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// OK
			// 
			this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OK.Location = new System.Drawing.Point(107, 120);
			this.OK.Name = "OK";
			this.OK.TabIndex = 3;
			this.OK.Text = "OK";
			// 
			// Cancel
			// 
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel.Location = new System.Drawing.Point(203, 120);
			this.Cancel.Name = "Cancel";
			this.Cancel.TabIndex = 4;
			this.Cancel.Text = "Cancel";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "Administrator account:";
			// 
			// Administrator
			// 
			this.Administrator.Location = new System.Drawing.Point(128, 48);
			this.Administrator.Name = "Administrator";
			this.Administrator.Size = new System.Drawing.Size(248, 20);
			this.Administrator.TabIndex = 1;
			this.Administrator.Text = SystemInformation.UserDomainName+"\\Domain Admins";
			this.Administrator.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 23);
			this.label3.TabIndex = 6;
			this.label3.Text = "User account:";
			// 
			// User
			// 
			this.User.Location = new System.Drawing.Point(128, 80);
			this.User.Name = "User";
			this.User.Size = new System.Drawing.Size(248, 20);
			this.User.TabIndex = 2;
			this.User.Text = SystemInformation.UserDomainName+"\\Domain Users";
			this.User.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// AddApplication
			// 
			this.AcceptButton = this.OK;
			//this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScaleDimensions = new System.Drawing.Size(5, 13);
			this.CancelButton = this.Cancel;
			this.ClientSize = new System.Drawing.Size(384, 158);
			this.Controls.Add(this.User);
			this.Controls.Add(this.Administrator);
			this.Controls.Add(this.Application);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.OK);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddApplication";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add application";
			this.Load += new System.EventHandler(this.AddApplication_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void OnTextChanged(object sender, System.EventArgs e)
		{
			SsoPage ssoPage=(SsoPage)((SsoSampleWizard)Owner).GetPage(typeof(SsoPage));
			OK.Enabled=Application.Text.Length>0 && Administrator.Text.Length>0 && User.Text.Length>0 &&
				!SsoConfigurator.Exists(Application.Text) && !ssoPage.Exists(Application.Text);
		}

		private void AddApplication_Load(object sender, System.EventArgs e)
		{
			OnTextChanged(null, null);
		}
	}
}
