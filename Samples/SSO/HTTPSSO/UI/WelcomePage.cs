//---------------------------------------------------------------------
// File:	WeclomePage.cs
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

namespace Microsoft.Samples.BizTalk.SSO
{
	#region WelcomePage
	/// <summary>
	/// Welcome page
	/// </summary>
	public class WelcomePage : InfoPage
	{
		private System.Windows.Forms.Label WelcomeText;
		private System.Windows.Forms.Label SelectSteps;
		public System.Windows.Forms.CheckBox StepIis;
		public System.Windows.Forms.CheckBox StepSso;
		public System.Windows.Forms.CheckBox StepBizTalk;
		private System.Windows.Forms.Label NextPrompt;
		private System.ComponentModel.IContainer	components=null;

		/// <summary>
		/// Default constructor
		/// </summary>
		public WelcomePage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
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
			this.WelcomeText = new System.Windows.Forms.Label();
			this.NextPrompt = new System.Windows.Forms.Label();
			this.SelectSteps = new System.Windows.Forms.Label();
			this.StepIis = new System.Windows.Forms.CheckBox();
			this.StepSso = new System.Windows.Forms.CheckBox();
			this.StepBizTalk = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
			this.SuspendLayout();
			// 
			// Title
			// 
			this.Title.Size = new System.Drawing.Size(294, 39);
			this.Title.Text = "Microsoft BizTalk Server Enterprise SSO SDK Sample";
			this.Title.Click += new System.EventHandler(this.Title_Click);
			// 
			// WelcomeText
			// 
			this.WelcomeText.Location = new System.Drawing.Point(170, 65);
			this.WelcomeText.Name = "WelcomeText";
			this.WelcomeText.Size = new System.Drawing.Size(292, 31);
			this.WelcomeText.TabIndex = 2;
			this.WelcomeText.Text = "This sample demonstrates usage of SSO with BizTalk Server HTTP adapter";
			// 
			// NextPrompt
			// 
			this.NextPrompt.Location = new System.Drawing.Point(170, 274);
			this.NextPrompt.Name = "NextPrompt";
			this.NextPrompt.Size = new System.Drawing.Size(292, 20);
			this.NextPrompt.TabIndex = 3;
			this.NextPrompt.Text = "Click Next to continue.";
			// 
			// SelectSteps
			// 
			this.SelectSteps.Location = new System.Drawing.Point(170, 112);
			this.SelectSteps.Name = "SelectSteps";
			this.SelectSteps.Size = new System.Drawing.Size(292, 16);
			this.SelectSteps.TabIndex = 4;
			this.SelectSteps.Text = "Please select the sample steps to be performed:";
			// 
			// StepIis
			// 
			this.StepIis.Checked = true;
			this.StepIis.CheckState = System.Windows.Forms.CheckState.Checked;
			this.StepIis.Location = new System.Drawing.Point(184, 136);
			this.StepIis.Name = "StepIis";
			this.StepIis.Size = new System.Drawing.Size(278, 16);
			this.StepIis.TabIndex = 5;
			this.StepIis.Text = "Configure IIS virtual directories";
			// 
			// StepSso
			// 
			this.StepSso.Checked = true;
			this.StepSso.CheckState = System.Windows.Forms.CheckState.Checked;
			this.StepSso.Location = new System.Drawing.Point(184, 160);
			this.StepSso.Name = "StepSso";
			this.StepSso.Size = new System.Drawing.Size(278, 16);
			this.StepSso.TabIndex = 6;
			this.StepSso.Text = "Configure SSO application and mappings";
			// 
			// StepBizTalk
			// 
			this.StepBizTalk.Checked = true;
			this.StepBizTalk.CheckState = System.Windows.Forms.CheckState.Checked;
			this.StepBizTalk.Location = new System.Drawing.Point(184, 184);
			this.StepBizTalk.Name = "StepBizTalk";
			this.StepBizTalk.Size = new System.Drawing.Size(278, 16);
			this.StepBizTalk.TabIndex = 7;
			this.StepBizTalk.Text = "Configure BizTalk receive and send ports";
			// 
			// WelcomePage
			// 
			this.Controls.Add(this.StepBizTalk);
			this.Controls.Add(this.StepSso);
			this.Controls.Add(this.StepIis);
			this.Controls.Add(this.SelectSteps);
			this.Controls.Add(this.NextPrompt);
			this.Controls.Add(this.WelcomeText);
			this.Name = "WelcomePage";
			this.Controls.SetChildIndex(this.WelcomeText, 0);
			this.Controls.SetChildIndex(this.NextPrompt, 0);
			this.Controls.SetChildIndex(this.Title, 0);
			this.Controls.SetChildIndex(this.Picture, 0);
			this.Controls.SetChildIndex(this.SelectSteps, 0);
			this.Controls.SetChildIndex(this.StepIis, 0);
			this.Controls.SetChildIndex(this.StepSso, 0);
			this.Controls.SetChildIndex(this.StepBizTalk, 0);
			((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
			this.ResumeLayout(false);

		}

		/// <summary>
		/// Page activation handler
		/// </summary>
		/// <returns>True if activation successfull, otherwise false</returns>
		public override bool OnSetActive()
		{
			if(!base.OnSetActive())
			{
				return false;
			}
            
			Wizard.SetWizardButtons(WizardButtonType.Next);

			return true;
		}

		private void Title_Click(object sender, EventArgs e)
		{

		}
	}
	#endregion	
}
