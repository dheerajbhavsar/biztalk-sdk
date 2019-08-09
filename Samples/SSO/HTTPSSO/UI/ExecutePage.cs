//---------------------------------------------------------------------
// File:	ExecutePage.cs
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
using System.Threading;
using System.Collections;
using System.Windows.Forms;

namespace Microsoft.Samples.BizTalk.SSO
{
	
	public delegate void ProgressBarWriteDelegate(System.Windows.Forms.ProgressBar progBar, int value);
	public delegate void LabelWriteDelegate(System.Windows.Forms.Label label, string text);
	public delegate void ProgressBarResetDelegate(System.Windows.Forms.ProgressBar progBar, int value);
	public delegate void MessageBoxShowDelegate(string title, string text);
	public delegate String GetApplicationsDelegate(System.Windows.Forms.ComboBox comboBox);
	public delegate void CursorsWriteDelegate(System.Windows.Forms.UserControl control, System.Windows.Forms.Cursor cursor);

	// Delegate class declaration with delegate methods

	public class DelegateClass
	{
		public void ProgressBarWriteDelegate(System.Windows.Forms.ProgressBar progBar, int value)
		{

			progBar.Value += value;

		}

		public void ProgressBarResetDelegate(System.Windows.Forms.ProgressBar progBar, int value)
		{

			progBar.Value = value;

		}

		public void LabelWriteDelegate(System.Windows.Forms.Label label, string text)
		{
			label.Text = text;
		}

		public void MessageBoxDelegate(string title, string text)
		{
			MessageBox.Show(title, text);
		}

		public String GetApplicationsDelegate(ComboBox comboBox)
		{
			return comboBox.Text;
		}

		public void CursorsWrite(System.Windows.Forms.UserControl control, System.Windows.Forms.Cursor cursor)
		{
			control.Cursor = cursor;
		}

		
	}

	#region ExecutePage
	/// <Summary>
	/// Execute page
	/// </Summary>
	public class ExecutePage : WorkPage
	{
		private System.Windows.Forms.ProgressBar ProgressBar;
		private System.Windows.Forms.Label Status;
		private System.Windows.Forms.Label Details;
		private System.ComponentModel.IContainer components=null;

		public static DelegateClass objDelegateClass = new DelegateClass();
		ProgressBarWriteDelegate ProgBarDelegate = new ProgressBarWriteDelegate(objDelegateClass.ProgressBarWriteDelegate);
		LabelWriteDelegate LabelDelegate = new LabelWriteDelegate(objDelegateClass.LabelWriteDelegate);
		ProgressBarResetDelegate ProgBarResetDelegate = new ProgressBarResetDelegate(objDelegateClass.ProgressBarResetDelegate);
		MessageBoxShowDelegate MessageBoxDelegate = new MessageBoxShowDelegate(objDelegateClass.MessageBoxDelegate);
		GetApplicationsDelegate GetApplicationsDelegate = new GetApplicationsDelegate(objDelegateClass.GetApplicationsDelegate);
		CursorsWriteDelegate CursorsWriteDelegate = new CursorsWriteDelegate(objDelegateClass.CursorsWrite);

		/// <Summary>
		/// Default constructor
		/// </Summary>
		public ExecutePage()
		{
			InitializeComponent();
		}

		/// <Summary>
		/// Clean up any resources being used.
		/// </Summary>
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

		/// <Summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </Summary>
		private void InitializeComponent()
		{	
			this.Status = new System.Windows.Forms.Label();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.Details = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// Title
			// 
			this.Title.Name = "Title";
			this.Title.Text = "Perform Chosen Configuration Steps";
			// 
			// Subtitle
			// 
			this.Subtitle.Name = "Subtitle";
			this.Subtitle.Text = "Now this wizard will perform the chosen configuration steps";
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
			// Status
			// 
			this.Status.Location = new System.Drawing.Point(40, 80);
			this.Status.Name = "Status";
			this.Status.Size = new System.Drawing.Size(408, 23);
			this.Status.TabIndex = 5;
			this.Status.Text = "Configuring IIS...";
			// 
			// ProgressBar
			// 
			this.ProgressBar.Location = new System.Drawing.Point(40, 104);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(408, 23);
			this.ProgressBar.TabIndex = 6;
			// 
			// Details
			// 
			this.Details.Location = new System.Drawing.Point(40, 136);
			this.Details.Name = "Details";
			this.Details.Size = new System.Drawing.Size(408, 64);
			this.Details.TabIndex = 7;
			this.Details.Text = "Creating receive virtual directory";
			// 
			// ExecutePage
			// 
			this.Controls.Add(this.Details);
			this.Controls.Add(this.ProgressBar);
			this.Controls.Add(this.Status);
			this.Name = "ExecutePage";
			this.Controls.SetChildIndex(this.Status, 0);
			this.Controls.SetChildIndex(this.ProgressBar, 0);
			this.Controls.SetChildIndex(this.Details, 0);
			this.Controls.SetChildIndex(this.Header, 0);
			this.Controls.SetChildIndex(this.Separator, 0);
			this.Controls.SetChildIndex(this.Title, 0);
			this.Controls.SetChildIndex(this.Subtitle, 0);
			this.Controls.SetChildIndex(this.Picture, 0);
			this.ResumeLayout(false);

		}

		/// <summary>
		/// Executes the chosen actions in a separate thread
		/// </summary>
		protected void Execute()
		{
			WelcomePage welcomePage=(WelcomePage)Wizard.GetPage(typeof(WelcomePage));
			IisPage iisPage=(IisPage)Wizard.GetPage(typeof(IisPage));
			SsoPage ssoPage=(SsoPage)Wizard.GetPage(typeof(SsoPage));
			BtsPage btsPage=(BtsPage)Wizard.GetPage(typeof(BtsPage));
			
			int actionsNumber=(welcomePage.StepIis.Checked?4:0)+
				(welcomePage.StepSso.Checked?2:0)+(welcomePage.StepBizTalk.Checked?2:0);

			// Create IIS configuration
			if(welcomePage.StepIis.Checked)
			{
				
				this.Invoke(LabelDelegate, new object[] { Status, "Creating IIS configuration..." });				

				this.Invoke(LabelDelegate, new object[] { Details, "Creating receive virtual directory" });				

				if(!IisConfigurator.CreateVirtualDirectory(iisPage.ReceiveName.Text, iisPage.ReceivePath.Text, false))
				{
					this.Invoke(MessageBoxDelegate, new object[] { "Failed to create virtual directory '"+iisPage.ReceiveName.Text+"'", 
						"IIS Configuration error" });
				}


				this.Invoke(ProgBarDelegate, new object[] { ProgressBar, 100 / actionsNumber });

				this.Invoke(LabelDelegate, new object[] { Details, "Configuring receive COM+ application identity" });

				if(!IisConfigurator.SetVirtualDirectoryHostIdentity(iisPage.ReceiveName.Text))
				{
					this.Invoke(MessageBoxDelegate, new object[] { "Failed to configure COM+ application '"+iisPage.ReceiveName.Text+"'", 
						"COM+ Configuration error" });
				}

				this.Invoke(ProgBarDelegate, new object[] { ProgressBar, 100 / actionsNumber });

				this.Invoke(LabelDelegate, new object[] { Details, "Creating send virtual directory" });

				if(!IisConfigurator.CreateVirtualDirectory(iisPage.SendName.Text, iisPage.SendPath.Text, true))
				{
					this.Invoke(MessageBoxDelegate, new object[] { "Failed to create virtual directory '"+iisPage.SendName.Text+"'", 
						"IIS Configuration error" });
				}
				
				this.Invoke(ProgBarDelegate, new object[] { ProgressBar, 100 / actionsNumber });

				this.Invoke(LabelDelegate, new object[] { Details, "Configuring send COM+ application identity" });

				if(!IisConfigurator.SetVirtualDirectoryHostIdentity(iisPage.SendName.Text))
				{
					this.Invoke(MessageBoxDelegate, new object[] { "Failed to configure COM+ application '"+iisPage.SendName.Text+"'", 
						"COM+ Configuration error" });
				}
				
				this.Invoke(ProgBarDelegate, new object[] { ProgressBar, 100 / actionsNumber });
			}

			// Create SSO configuration
			if(welcomePage.StepSso.Checked)
			{
				this.Invoke(LabelDelegate, new object[] { Status, "Creating SSO configuration..." });

				if(!SsoConfigurator.EnableTickets())
				{
					this.Invoke(MessageBoxDelegate, new object[] { "Failed to enable SSO tickets", 
						"SSO Configuration error" });
				}

				foreach(SsoApplication application in ssoPage.Applications)
				{
					this.Invoke(LabelDelegate, new object[] { Details, "Creating SSO application: " + application.Name });

					if(!SsoConfigurator.AddApplication(application.Name, application.Administrator, application.User))
					{
						this.Invoke(MessageBoxDelegate, new object[] { "Failed to create SSO application '"+application.Name+"'", 
							"SSO Configuration error" });
					}
				}
				
				this.Invoke(ProgBarDelegate, new object[] { ProgressBar, 100 / actionsNumber });

				this.Invoke(LabelDelegate, new object[] { Details, "Creating SSO mappings" });

				foreach(SsoMapping mapping in ssoPage.Mappings)
				{
					if(!SsoConfigurator.AddMapping(mapping.Application, mapping.User, mapping.XU, mapping.XP))
					{
						this.Invoke(MessageBoxDelegate, new object[] { "Failed to add mapping for SSO application '"+mapping.Application+"'", 
							"SSO Configuration error" });
					}
				}
				
				this.Invoke(ProgBarDelegate, new object[] { ProgressBar, 100 / actionsNumber });
			}

			// Create BizTalk configuration
			if(welcomePage.StepBizTalk.Checked)
			{
				this.Invoke(LabelDelegate, new object[] { Status, "Creating BizTalk configuration..." });

				this.Invoke(LabelDelegate, new object[] { Details, "Creating receive port and receive location" });

				string strBTSExpMsg = string.Empty;
				if(!BtsConfigurator.CreateHttpRequestResponsePort(iisPage.ReceiveName.Text,
					btsPage.ReceivePortName.Text, btsPage.ReceiveLocationName.Text, ref strBTSExpMsg))
				{
					this.Invoke(MessageBoxDelegate, new object[] { "Failed to create receive port: " + strBTSExpMsg, "BizTalk Configuration error" });
				}
				
				this.Invoke(ProgBarDelegate, new object[] { ProgressBar, 100 / actionsNumber });

				this.Invoke(LabelDelegate, new object[] { Details, "Creating send port" });

				strBTSExpMsg = string.Empty;
				
				string appText = this.Invoke(GetApplicationsDelegate, new object[] {btsPage.Applications}).ToString();

				if(!BtsConfigurator.CreateHttpSolicitResponsePort(btsPage.SendPortUri.Text, 
					btsPage.SendPortName.Text, btsPage.ReceivePortName.Text, appText, ref strBTSExpMsg))
				{
					this.Invoke(MessageBoxDelegate, new object[] { "Failed to create send port: " + strBTSExpMsg, "BizTalk Configuration error" });
				}
				
				this.Invoke(ProgBarDelegate, new object[] { ProgressBar, 100 / actionsNumber });
			}

			this.Invoke(LabelDelegate, new object[] { Details, "" });

			this.Invoke(LabelDelegate, new object[] { Status, "Configuration completed." });

			this.Invoke(ProgBarResetDelegate, new object[] { ProgressBar, 100 });

			this.Invoke(CursorsWriteDelegate, new object[] { this, Cursors.Default });
			
			Wizard.SetWizardButtons(WizardButtonType.Next);
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
            
			// Disable all buttons
			Wizard.SetWizardButtons(0);

			this.Invoke(CursorsWriteDelegate, new object[] { this, Cursors.WaitCursor });

			Thread thread=new Thread(new ThreadStart(this.Execute));
			thread.Start();

			return true;
		}
	}
	#endregion
}
