//---------------------------------------------------------------------
// File:	IisPage.cs
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
using System.IO;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Microsoft.Samples.BizTalk.SSO
{
	#region IisPage
	public class IisPage : WorkPage
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.TextBox SendPath;
		public System.Windows.Forms.TextBox SendName;
		public System.Windows.Forms.TextBox ReceivePath;
		public System.Windows.Forms.TextBox ReceiveName;
		private System.Windows.Forms.FolderBrowserDialog FolderDialog;
		private System.ComponentModel.IContainer components=null;

		/// <summary>
		/// Default constructor
		/// </summary>
		public IisPage()
		{
			InitializeComponent();

			// Default BTS path
			RegistryKey rkBts=Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\BizTalk Server\3.0");
			ReceivePath.Text=(string)rkBts.GetValue("InstallPath")+"HTTPReceive";

			// Current directory
			SendPath.Text=(string)rkBts.GetValue("InstallPath")+@"SDK\Samples\SSO\HTTPSSO\Scripts";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(IisPage));
			this.SendPath = new System.Windows.Forms.TextBox();
			this.SendName = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.ReceivePath = new System.Windows.Forms.TextBox();
			this.ReceiveName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.FolderDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.SuspendLayout();
			// 
			// Title
			// 
			this.Title.Name = "Title";
			this.Title.Text = "Configure IIS virtual directories";
			// 
			// Subtitle
			// 
			this.Subtitle.Name = "Subtitle";
			this.Subtitle.Text = "You need to configure IIS virtual directories corrresponding to the BizTalk recei" +
				"ve location and send port destination";
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
			// SendPath
			// 
			this.SendPath.Location = new System.Drawing.Point(102, 272);
			this.SendPath.Name = "SendPath";
			this.SendPath.Size = new System.Drawing.Size(328, 20);
			this.SendPath.TabIndex = 4;
			this.SendPath.Text = "textBox4";
			this.SendPath.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// SendName
			// 
			this.SendName.Location = new System.Drawing.Point(102, 248);
			this.SendName.Name = "SendName";
			this.SendName.Size = new System.Drawing.Size(352, 20);
			this.SendName.TabIndex = 3;
			this.SendName.Text = "SsoSampleServerApplication";
			this.SendName.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(48, 192);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(408, 40);
			this.label7.TabIndex = 10;
			this.label7.Text = "IIS virtual directory used by server application receiving messages from BizTalk " +
				"HTTP send port will be configured to run in High Isolation mode and uses Basic a" +
				"uthentication. This represents the backend in this sample.";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(42, 272);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 24);
			this.label2.TabIndex = 6;
			this.label2.Text = "Path:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(42, 248);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 24);
			this.label4.TabIndex = 9;
			this.label4.Text = "Name:";
			// 
			// button2
			// 
			this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
			this.button2.Location = new System.Drawing.Point(430, 272);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(24, 20);
			this.button2.TabIndex = 5;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
			this.button1.Location = new System.Drawing.Point(430, 144);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(24, 20);
			this.button1.TabIndex = 2;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// ReceivePath
			// 
			this.ReceivePath.Location = new System.Drawing.Point(102, 144);
			this.ReceivePath.Name = "ReceivePath";
			this.ReceivePath.Size = new System.Drawing.Size(328, 20);
			this.ReceivePath.TabIndex = 1;
			this.ReceivePath.Text = "textBox2";
			this.ReceivePath.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// ReceiveName
			// 
			this.ReceiveName.Location = new System.Drawing.Point(102, 120);
			this.ReceiveName.Name = "ReceiveName";
			this.ReceiveName.Size = new System.Drawing.Size(352, 20);
			this.ReceiveName.TabIndex = 0;
			this.ReceiveName.Text = "SsoSampleBizTalkHttpReceive";
			this.ReceiveName.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(44, 72);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(408, 40);
			this.label5.TabIndex = 7;
			this.label5.Text = "IIS virtual directory used by BizTalk HTTP receive location will be configured to" +
				" run in High Isolation mode and to require Windows Integrated authentication. Th" +
				"is is the front end to the user.";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(42, 144);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 24);
			this.label3.TabIndex = 6;
			this.label3.Text = "Path:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(40, 120);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 24);
			this.label1.TabIndex = 5;
			this.label1.Text = "Name:";
			// 
			// FolderDialog
			// 
			this.FolderDialog.Description = "Select the local folder that this IIS virtual directory will be pointing to (it i" +
				"s not recommended to change the default paths)";
			// 
			// IisPage
			// 
			this.Controls.Add(this.ReceiveName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.ReceivePath);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.SendPath);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.SendName);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label2);
			this.Name = "IisPage";
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.label7, 0);
			this.Controls.SetChildIndex(this.SendName, 0);
			this.Controls.SetChildIndex(this.button2, 0);
			this.Controls.SetChildIndex(this.SendPath, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.button1, 0);
			this.Controls.SetChildIndex(this.label5, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.ReceivePath, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.ReceiveName, 0);
			this.Controls.SetChildIndex(this.Header, 0);
			this.Controls.SetChildIndex(this.Separator, 0);
			this.Controls.SetChildIndex(this.Title, 0);
			this.Controls.SetChildIndex(this.Subtitle, 0);
			this.Controls.SetChildIndex(this.Picture, 0);
			this.ResumeLayout(false);

		}

		/// <summary>
		/// Page activatio handler
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

		private void button1_Click(object sender, System.EventArgs e)
		{
			FolderDialog.SelectedPath=ReceivePath.Text;
			if(DialogResult.OK==FolderDialog.ShowDialog())
			{
				ReceivePath.Text=FolderDialog.SelectedPath;
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			FolderDialog.SelectedPath=SendPath.Text;
			if(DialogResult.OK==FolderDialog.ShowDialog())
			{
				SendPath.Text=FolderDialog.SelectedPath;
			}
		}

		private void OnTextChanged(object sender, System.EventArgs e)
		{
			if(Wizard==null)
			{
				return;
			}

			if(ReceiveName.Text.Length>0 && SendName.Text.Length>0 &&									// names are not null
				ReceivePath.Text.Length>0 && SendPath.Text.Length>0 &&									// paths are not null
				ReceiveName.Text.ToLower()!=SendName.Text.ToLower())									// names are different
			{
				Wizard.SetWizardButtons(WizardButtonType.Back | WizardButtonType.Next);
			}
			else
			{
				Wizard.SetWizardButtons(WizardButtonType.Back);
			}
		}
	}
	#endregion
}
