//---------------------------------------------------------------------
// File:	SsoPage.cs
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
	#region SsoApplication
	public struct SsoApplication
	{
		public string Name;
		public string Administrator;
		public string User;
	}
	#endregion

	#region SsoMapping
	public struct SsoMapping
	{
		public string Application;
		public string User;
		public string XU;
		public string XP;
	}
	#endregion

	#region SsoPage
	/// <summary>
	/// SSO page
	/// </summary>
	public class SsoPage : WorkPage
	{
		private System.Windows.Forms.Label label5;
		private System.ComponentModel.IContainer components=null;

		public	ArrayList	Applications=new ArrayList();
		private System.Windows.Forms.Button AddApplication;
		private System.Windows.Forms.Button AddMapping;
		public	ArrayList	Mappings=new ArrayList();

		/// <summary>
		/// Default constructor
		/// </summary>
		public SsoPage()
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
			this.label5 = new System.Windows.Forms.Label();
			this.AddApplication = new System.Windows.Forms.Button();
			this.AddMapping = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Title
			// 
			this.Title.Name = "Title";
			this.Title.Text = "Configure Enterprise Single Sign On";
			// 
			// Subtitle
			// 
			this.Subtitle.Name = "Subtitle";
			this.Subtitle.Text = "Enterprise Signle Sign On service allows you to use credentials from inbound sess" +
				"ion authentication for outbound authentication";
			// 
			// Header
			// 
			this.Header.Name = "Header";
			// 
			// Picture
			// 
			this.Picture.Name = "Picture";
			// 
			// Separator
			// 
			this.Separator.Name = "Separator";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(40, 112);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(408, 40);
			this.label5.TabIndex = 8;
			this.label5.Text = "In order to provide credentials used for outbound authentication this sample will" +
				" create afiiliate applications and credentials mappings corresponding to these a" +
				"pplications.";
			// 
			// AddApplication
			// 
			this.AddApplication.Location = new System.Drawing.Point(40, 168);
			this.AddApplication.Name = "AddApplication";
			this.AddApplication.Size = new System.Drawing.Size(184, 23);
			this.AddApplication.TabIndex = 0;
			this.AddApplication.Text = "Add application...";
			this.AddApplication.Click += new System.EventHandler(this.AddApplication_Click);
			// 
			// AddMapping
			// 
			this.AddMapping.Location = new System.Drawing.Point(248, 168);
			this.AddMapping.Name = "AddMapping";
			this.AddMapping.Size = new System.Drawing.Size(200, 23);
			this.AddMapping.TabIndex = 1;
			this.AddMapping.Text = "Add mapping...";
			this.AddMapping.Click += new System.EventHandler(this.AddMapping_Click);
			// 
			// SsoPage
			// 
			this.Controls.Add(this.AddMapping);
			this.Controls.Add(this.AddApplication);
			this.Controls.Add(this.label5);
			this.Name = "SsoPage";
			this.Controls.SetChildIndex(this.Header, 0);
			this.Controls.SetChildIndex(this.Separator, 0);
			this.Controls.SetChildIndex(this.Title, 0);
			this.Controls.SetChildIndex(this.Subtitle, 0);
			this.Controls.SetChildIndex(this.Picture, 0);
			this.Controls.SetChildIndex(this.label5, 0);
			this.Controls.SetChildIndex(this.AddApplication, 0);
			this.Controls.SetChildIndex(this.AddMapping, 0);
			this.ResumeLayout(false);

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
            
			Wizard.SetWizardButtons(WizardButtonType.Back | WizardButtonType.Next);

			return true;
		}

		private void AddApplication_Click(object sender, System.EventArgs e)
		{
			AddApplication dlg=new AddApplication();
			if(DialogResult.OK==dlg.ShowDialog(this))
			{
				SsoApplication application=new SsoApplication();
				application.Name=dlg.Application.Text;
				application.Administrator=dlg.Administrator.Text;
				application.User=dlg.User.Text;

				Applications.Add(application);
			}
		}

		private void AddMapping_Click(object sender, System.EventArgs e)
		{
			string[] apps=SsoConfigurator.GetApplications();

			if(Applications.Count==0 && apps.Length==0)
			{
				MessageBox.Show("You must create at least one affiliate application", "No affiliate applications",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				return;
			}

			AddMapping dlg=new AddMapping();
			if(DialogResult.OK==dlg.ShowDialog(this))
			{
				SsoMapping mapping=new SsoMapping();
				mapping.Application=dlg.Applications.Text;
				mapping.User=dlg.User.Text;
				mapping.XU=dlg.XU.Text;
				mapping.XP=dlg.XP.Text;

				Mappings.Add(mapping);
			}
		}

		public bool Exists(string application)
		{
			foreach(SsoApplication app in Applications)
			{
				if(app.Name.ToLower()==application.ToLower())
				{
					return true;
				}
			}

			return false;
		}
	}
	#endregion
}
