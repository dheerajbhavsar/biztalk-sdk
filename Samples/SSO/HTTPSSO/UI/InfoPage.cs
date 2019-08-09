//---------------------------------------------------------------------
// File:	InfoPage.cs
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
	#region InfoPage
	/// <summary>
	/// Base page for welcome and finish pages
	/// </summary>
	public class InfoPage : PageBase
	{
		protected Label			Title;
		protected PictureBox	Picture;

		/// <summary>
		/// Default constructor
		/// </summary>
		public InfoPage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(InfoPage));

			this.Title = new System.Windows.Forms.Label();
			this.Picture = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// Title
			// 
			this.Title.BackColor = System.Drawing.Color.White;
			this.Title.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Title.Location = new System.Drawing.Point(170, 13);
			this.Title.Name = "Title";
			this.Title.Size = new System.Drawing.Size(292, 39);
			this.Title.TabIndex = 0;
			this.Title.Text = "Greeting page";
			// 
			// Picture
			// 
			this.Picture.BackColor = System.Drawing.Color.White;
			this.Picture.Image = ((System.Drawing.Image)(resources.GetObject("Picture.Image")));
			this.Picture.Location = new System.Drawing.Point(0, 0);
			this.Picture.Name = "Picture";
			this.Picture.Size = new System.Drawing.Size(164, 312);
			this.Picture.TabIndex = 1;
			this.Picture.TabStop = false;
			// 
			// InfoPage
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.Picture);
			this.Controls.Add(this.Title);
			this.Name = "InfoPage";
			this.ResumeLayout(false);

		}
	}
	#endregion
}
