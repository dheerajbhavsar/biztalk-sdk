//---------------------------------------------------------------------
// File:	WorkPage.cs
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
	#region WorkPage
	/// <summary>
	/// Base class that is used to represent an interior page within a
	/// wizard dialog.
	/// </summary>
	public class WorkPage : PageBase
	{
		protected Label			Title;
		protected Label			Subtitle;
		protected Panel			Header;
		protected PictureBox	Picture;
		protected GroupBox		Separator;
        
		/// <summary>
		/// Default constructor
		/// </summary>
		public WorkPage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WorkPage));
			this.Separator = new System.Windows.Forms.GroupBox();
			this.Header = new System.Windows.Forms.Panel();
			this.Title = new System.Windows.Forms.Label();
			this.Subtitle = new System.Windows.Forms.Label();
			this.Picture = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// Separator
			// 
			this.Separator.Location = new System.Drawing.Point(0, 58);
			this.Separator.Name = "Separator";
			this.Separator.Size = new System.Drawing.Size(499, 2);
			this.Separator.TabIndex = 3;
			this.Separator.TabStop = false;
			// 
			// Header
			// 
			this.Header.BackColor = System.Drawing.Color.White;
			this.Header.Location = new System.Drawing.Point(0, 0);
			this.Header.Name = "Header";
			this.Header.Size = new System.Drawing.Size(497, 58);
			this.Header.TabIndex = 0;
			// 
			// Title
			// 
			this.Title.BackColor = System.Drawing.Color.White;
			this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Title.Location = new System.Drawing.Point(20, 10);
			this.Title.Name = "Title";
			this.Title.Size = new System.Drawing.Size(410, 13);
			this.Title.TabIndex = 1;
			this.Title.Text = "Sample Header Title";
			// 
			// Subtitle
			// 
			this.Subtitle.BackColor = System.Drawing.Color.White;
			this.Subtitle.Location = new System.Drawing.Point(41, 25);
			this.Subtitle.Name = "Subtitle";
			this.Subtitle.Size = new System.Drawing.Size(389, 26);
			this.Subtitle.TabIndex = 2;
			this.Subtitle.Text = "Work page";
			// 
			// Picture
			// 
			this.Picture.BackColor = System.Drawing.Color.White;
			this.Picture.Image = ((System.Drawing.Image)(resources.GetObject("Picture.Image")));
			this.Picture.Location = new System.Drawing.Point(443, 5);
			this.Picture.Name = "Picture";
			this.Picture.Size = new System.Drawing.Size(49, 49);
			this.Picture.TabIndex = 4;
			this.Picture.TabStop = false;
			// 
			// WorkPage
			// 
			this.Controls.Add(this.Picture);
			this.Controls.Add(this.Subtitle);
			this.Controls.Add(this.Title);
			this.Controls.Add(this.Separator);
			this.Controls.Add(this.Header);
			this.Name = "WorkPage";
			this.ResumeLayout(false);

		}
	}
	#endregion
}
