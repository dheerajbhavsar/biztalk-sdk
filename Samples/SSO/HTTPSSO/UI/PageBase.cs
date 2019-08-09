//---------------------------------------------------------------------
// File:	PageBase.cs
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
using System.Windows.Forms;

namespace Microsoft.Samples.BizTalk.SSO
{
	#region PageBase
	/// <summary>
	/// Wizard PageBase base class
	/// </summary>
	public class PageBase : UserControl
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public PageBase()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Wizard
		/// </summary>
		protected SsoSampleWizard Wizard
		{
			get
			{
				return (SsoSampleWizard)Parent;
			}
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			Name="PageBase";
			Size=new System.Drawing.Size(497, 313);
		}

		/// <summary>
		/// Called when PageBase is to be deactivated
		/// </summary>
		/// <returns>True if deactivation successfull, otherwise false</returns>
		public virtual bool OnKillActive()
		{
			return Validate();
		}

		/// <summary>
		/// Called when the PageBase is activated
		/// </summary>
		/// <returns>True if activation successfull, otherwise false</returns>
		public  virtual bool OnSetActive()
		{
			return true;
		}
        
		/// <summary>
		/// Wizard Back button handler
		/// </summary>
		public virtual void OnWizardBack()
		{	
		}

		/// <summary>
		/// Wizard Next button handler
		/// </summary>
		public virtual void OnWizardNext()
		{
		}
        
		/// <summary>
		/// Wizard Finish button handler
		/// </summary>
		/// <returns>True to finish wizard, otherwise false</returns>
		public virtual bool OnWizardFinish()
		{
			return true;
		}
	}
	#endregion
}
		
