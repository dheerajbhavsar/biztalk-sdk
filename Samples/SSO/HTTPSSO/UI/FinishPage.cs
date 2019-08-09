//---------------------------------------------------------------------
// File:	FinishPage.cs
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
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace Microsoft.Samples.BizTalk.SSO
{
	#region FinishPage
	/// <summary>
	/// Finish page
	/// </summary>
	public class FinishPage : InfoPage
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox StartBrowser;
		private System.Windows.Forms.TextBox Url;
		private System.ComponentModel.IContainer components=null;

		/// <summary>
		/// Default constructor
		/// </summary>
		public FinishPage()
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
			this.label3 = new System.Windows.Forms.Label();
			this.StartBrowser = new System.Windows.Forms.CheckBox();
			this.Url = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// Title
			// 
			this.Title.Name = "Title";
			this.Title.Text = "BizTalk SSO Sample Wizard FInished";
			// 
			// Picture
			// 
			this.Picture.Name = "Picture";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(170, 65);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(292, 26);
			this.label1.TabIndex = 2;
			this.label1.Text = "Here some info about source files (probably).";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(170, 274);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(292, 20);
			this.label3.TabIndex = 5;
			this.label3.Text = "Click Finish to close this wizard.";
			// 
			// StartBrowser
			// 
			this.StartBrowser.Location = new System.Drawing.Point(176, 208);
			this.StartBrowser.Name = "StartBrowser";
			this.StartBrowser.Size = new System.Drawing.Size(112, 24);
			this.StartBrowser.TabIndex = 6;
			this.StartBrowser.Text = "Start browser at:";
			this.StartBrowser.CheckedChanged += new System.EventHandler(this.StartBrowser_CheckedChanged);
			// 
			// Url
			// 
			this.Url.Enabled = false;
			this.Url.Location = new System.Drawing.Point(176, 240);
			this.Url.Name = "Url";
			this.Url.Size = new System.Drawing.Size(288, 20);
			this.Url.TabIndex = 7;
			this.Url.Text = "http://localhost/SsoStart/SsoSample.asp";
			// 
			// FinishPage
			// 
			this.Controls.Add(this.Url);
			this.Controls.Add(this.StartBrowser);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Name = "FinishPage";
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.Title, 0);
			this.Controls.SetChildIndex(this.Picture, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.StartBrowser, 0);
			this.Controls.SetChildIndex(this.Url, 0);
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

			IisPage iisPage=(IisPage)Wizard.GetPage(typeof(IisPage));
			Url.Text="http://localhost/"+iisPage.ReceiveName.Text+"/BTSHttpReceive.dll?<message/>";
            
			Wizard.SetWizardButtons(WizardButtonType.Finish);

			return true;
		}

		private void StartBrowser_CheckedChanged(object sender, System.EventArgs e)
		{
			Url.Enabled=StartBrowser.Checked;
		}

		/// <summary>
		/// Wizard Finish button handler
		/// </summary>
		public override bool OnWizardFinish()
		{
			if(StartBrowser.Checked)
			{
				ProcessStartInfo psi=new ProcessStartInfo("iexplore.exe");
				psi.Arguments=Url.Text;
				
				Process process=Process.Start(psi);
			}

			return true;
		}
	}
	#endregion
}
