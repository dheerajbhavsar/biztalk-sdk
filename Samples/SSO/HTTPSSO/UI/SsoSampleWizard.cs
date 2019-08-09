//---------------------------------------------------------------------
// File:	SsoSampleWizard.cs
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
	
	#region WizardButtonType
	/// <summary>
	/// Wizard button types
	/// </summary>
	public enum WizardButtonType
	{
		Back			=0x00000001,
		Next			=0x00000002,
		Finish			=0x00000004,
		DisabledFinish	=0x00000008,
	}
	#endregion

	
	public delegate void SetButtonDelegate(Button button, WizardButtonType setting);
	
	public class DelegateClassWizard
	{
		public void SetButtonDelegate(Button button, WizardButtonType setting)
		{
			button.Enabled = (setting & WizardButtonType.Next) != 0;
			button.Visible = (setting & WizardButtonType.Finish) == 0 && (setting & WizardButtonType.DisabledFinish) == 0;
		}
	}

	#region SsoSampleWizard
	/// <summary>
	/// Wizard base class
	/// </summary>
	public class SsoSampleWizard : Form
	{
		private ArrayList	Pages=new ArrayList();
		private int			SelectedIndex=-1;
        
		protected Button	Back;
		protected Button	Next;
		protected Button	Cancel;
		protected Button	Finish;
		protected GroupBox	Separator;

		public static DelegateClassWizard objDelegateClass = new DelegateClassWizard();
		SetButtonDelegate SetButtonDelegate = new SetButtonDelegate(objDelegateClass.SetButtonDelegate);
		

		/// <summary>
		/// Default constructor
		/// </summary>
		public SsoSampleWizard()
		{
			InitializeComponent();
			Finish.Location=Next.Location;
			Controls.AddRange(new Control[] 
				{new WelcomePage(), new IisPage(), new SsoPage(), new BtsPage(), new ExecutePage(), new FinishPage()});
		}

		/// <summary>
		/// Gets page
		/// </summary>
		/// <param name="type">Page type</param>
		/// <returns>Page object</returns>
		public PageBase GetPage(Type type)
		{
			foreach(PageBase page in Pages)
			{
				if(page.GetType()==type)
				{
					return page;
				}
			}

			return null;
		}
        
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Back = new System.Windows.Forms.Button();
			this.Next = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			this.Finish = new System.Windows.Forms.Button();
			this.Separator = new System.Windows.Forms.GroupBox();
			this.SuspendLayout();
			// 
			// Back
			// 
			this.Back.Location = new System.Drawing.Point(252, 327);
			this.Back.Name = "Back";
			this.Back.TabIndex = 8;
			this.Back.Text = "< &Back";
			this.Back.Click += new System.EventHandler(this.OnClickBack);
			// 
			// Next
			// 
			this.Next.Location = new System.Drawing.Point(327, 327);
			this.Next.Name = "Next";
			this.Next.TabIndex = 9;
			this.Next.Text = "&Next >";
			this.Next.Click += new System.EventHandler(this.OnClickNext);
			// 
			// Cancel
			// 
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel.Location = new System.Drawing.Point(412, 327);
			this.Cancel.Name = "Cancel";
			this.Cancel.TabIndex = 11;
			this.Cancel.Text = "Cancel";
			this.Cancel.Click += new System.EventHandler(this.OnClickCancel);
			// 
			// Finish
			// 
			this.Finish.Location = new System.Drawing.Point(10, 327);
			this.Finish.Name = "Finish";
			this.Finish.TabIndex = 10;
			this.Finish.Text = "&Finish";
			this.Finish.Visible = false;
			this.Finish.Click += new System.EventHandler(this.OnClickFinish);
			// 
			// Separator
			// 
			this.Separator.Location = new System.Drawing.Point(0, 313);
			this.Separator.Name = "Separator";
			this.Separator.Size = new System.Drawing.Size(499, 2);
			this.Separator.TabIndex = 7;
			this.Separator.TabStop = false;
			// 
			// SsoSampleWizard
			// 
			this.AcceptButton = this.Finish;
			//this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScaleDimensions = new System.Drawing.Size(5, 13);
			this.CancelButton = this.Cancel;
			this.ClientSize = new System.Drawing.Size(497, 360);
			this.Controls.Add(this.Back);
			this.Controls.Add(this.Next);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.Finish);
			this.Controls.Add(this.Separator);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SsoSampleWizard";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Microsoft BizTalk Server SSO Sample";
			this.ResumeLayout(false);

		}

		/// <summary>
		/// Shows the PageBase with the given index
		/// </summary>
		/// <param name="index">Index of the PageBase to be shown</param>
		protected void ActivatePage(int index)
		{
			if(index<0 || index>=Pages.Count)
			{
				throw new ArgumentOutOfRangeException();
			}

			PageBase currentPage=null;
			if(SelectedIndex!=-1)
			{
				currentPage=(PageBase)Pages[SelectedIndex];
				if(!currentPage.OnKillActive())
				{
					return;
				}
			}

			PageBase newPage=(PageBase)Pages[index];
			if(!newPage.OnSetActive())
			{
				return;
			}

			if(SelectedIndex!=-1)
			{
				currentPage.Visible=false;
			}

			SelectedIndex=index;			
			newPage.Visible=true;
			newPage.Focus();
		}

		/// <summary>
		/// Handles the Back button click
		/// </summary>
		protected void OnClickBack(object sender, EventArgs e)
		{
			if(SelectedIndex!=-1)
			{
				((PageBase)Pages[SelectedIndex]).OnWizardBack();

				WelcomePage welcomePage=(WelcomePage)GetPage(typeof(WelcomePage));
				for(int i=SelectedIndex-1; i>=0; i--)
				{
					if(!welcomePage.StepIis.Checked && Pages[i].GetType()==typeof(IisPage))
					{
						continue;
					}

					if(!welcomePage.StepSso.Checked && Pages[i].GetType()==typeof(SsoPage))
					{
						continue;
					}

					if(!welcomePage.StepBizTalk.Checked && Pages[i].GetType()==typeof(BtsPage))
					{
						continue;
					}

					ActivatePage(i);
					break;
				}
			}
		}

		/// <summary>
		/// Handles the Finish button click
		/// </summary>
		protected void OnClickFinish(object sender, EventArgs e)
		{
			if(SelectedIndex!=-1)
			{
				PageBase page=(PageBase)Pages[SelectedIndex];
				if(page.OnWizardFinish())
				{
					if(page.OnKillActive())
					{
						DialogResult=DialogResult.OK;
						Close();
					}
				}
			}
		}

		/// <summary>
		/// Handles the Next button click
		/// </summary>
		protected void OnClickNext(object sender, EventArgs e)
		{
			if(SelectedIndex!=-1)
			{
				((PageBase)Pages[SelectedIndex]).OnWizardNext();
				
				WelcomePage welcomePage=(WelcomePage)GetPage(typeof(WelcomePage));
				for(int i=SelectedIndex+1; i<Pages.Count; i++)
				{
					if(!welcomePage.StepIis.Checked && Pages[i].GetType()==typeof(IisPage))
					{
						continue;
					}

					if(!welcomePage.StepSso.Checked && Pages[i].GetType()==typeof(SsoPage))
					{
						continue;
					}

					if(!welcomePage.StepBizTalk.Checked && Pages[i].GetType()==typeof(BtsPage))
					{
						continue;
					}

					if(!welcomePage.StepIis.Checked && !welcomePage.StepSso.Checked && 
						!welcomePage.StepBizTalk.Checked && Pages[i].GetType()==typeof(ExecutePage))
					{
						continue;
					}

					ActivatePage(i);
					break;
				}
			}
		}

		/// <summary>
		/// Handles the PageBase adding
		/// </summary>
		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);
            
			PageBase page=e.Control as PageBase;
			if(page!=null)
			{
				page.Visible=false;
				page.Location=new Point(0, 0);
				page.Size=new Size(Width, Separator.Location.Y);
				Pages.Add(page);

				if(SelectedIndex==-1)
				{
					SelectedIndex=0;
				}
			}
		}

		/// <summary>
		/// Handles the wizard loading
		/// </summary>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
            
			if(Pages.Count>0)
			{
				ActivatePage(0);
			}
		}
        
		/// <summary>
		/// Sets displayed wizard buttons
		/// </summary>
		/// <param name="buttons">Buttons</param>
		public void SetWizardButtons(WizardButtonType buttons)
		{
			Back.Enabled=(buttons & WizardButtonType.Back)!=0;
			this.Invoke(SetButtonDelegate, new object[] { Next, buttons });
	
			Finish.Enabled=(buttons & WizardButtonType.DisabledFinish) == 0;
			Finish.Visible=(buttons & WizardButtonType.Finish)!=0 || (buttons & WizardButtonType.DisabledFinish)!=0;
                
			AcceptButton=Finish.Visible?Finish:Next;
		}

		/// <summary>
		/// Handles the Cancel button click
		/// </summary>
		private void OnClickCancel(object sender, System.EventArgs e)
		{
			DialogResult=DialogResult.Cancel;
			this.Close();
		}
	}
	#endregion
}
