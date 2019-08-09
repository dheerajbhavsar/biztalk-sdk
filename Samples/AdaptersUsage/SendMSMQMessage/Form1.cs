//---------------------------------------------------------------------
// File: Form1.cs
// 
// Summary: sample implementation
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
using System.Data;
using System.Messaging;
using System.IO;

namespace Microsoft.Samples.BizTalk.SendMSMQMessage
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox textBoxAddr;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonSend;
		private System.Windows.Forms.TextBox textBoxBody;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxMachine;
		private System.Windows.Forms.TextBox textBoxQueue;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textError;
		private IContainer components;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.components = new System.ComponentModel.Container();
			this.buttonSend = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.textBoxAddr = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.textBoxBody = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxMachine = new System.Windows.Forms.TextBox();
			this.textBoxQueue = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.textError = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// buttonSend
			// 
			this.buttonSend.Location = new System.Drawing.Point(152, 264);
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.Size = new System.Drawing.Size(144, 48);
			this.buttonSend.TabIndex = 5;
			this.buttonSend.Text = "Send wrapped";
			this.buttonSend.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(304, 264);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(128, 48);
			this.button2.TabIndex = 6;
			this.button2.Text = "Exit";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// textBoxAddr
			// 
			this.textBoxAddr.Location = new System.Drawing.Point(104, 56);
			this.textBoxAddr.Name = "textBoxAddr";
			this.textBoxAddr.ReadOnly = true;
			this.textBoxAddr.Size = new System.Drawing.Size(328, 20);
			this.textBoxAddr.TabIndex = 2;
			this.textBoxAddr.TabStop = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Address:";
			// 
			// textBoxBody
			// 
			this.textBoxBody.Location = new System.Drawing.Point(8, 96);
			this.textBoxBody.Multiline = true;
			this.textBoxBody.Name = "textBoxBody";
			this.textBoxBody.Size = new System.Drawing.Size(424, 64);
			this.textBoxBody.TabIndex = 3;
			this.textBoxBody.Text = "*** Message to send ***";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Message body:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 16);
			this.label3.TabIndex = 6;
			this.label3.Text = "BizTalk machine name:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(120, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "BizTalk Queue name:";
			// 
			// textBoxMachine
			// 
			this.textBoxMachine.Location = new System.Drawing.Point(136, 8);
			this.textBoxMachine.Name = "textBoxMachine";
			this.textBoxMachine.Size = new System.Drawing.Size(144, 20);
			this.textBoxMachine.TabIndex = 0;
			this.textBoxMachine.TextChanged += new System.EventHandler(this.textBoxMachine_TextChanged);
			// 
			// textBoxQueue
			// 
			this.textBoxQueue.Location = new System.Drawing.Point(136, 32);
			this.textBoxQueue.Name = "textBoxQueue";
			this.textBoxQueue.Size = new System.Drawing.Size(144, 20);
			this.textBoxQueue.TabIndex = 1;
			this.textBoxQueue.Text = "test";
			this.textBoxQueue.TextChanged += new System.EventHandler(this.textBoxQueue_TextChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(8, 264);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(136, 48);
			this.button1.TabIndex = 4;
			this.button1.Text = "Send exact";
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 168);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 16);
			this.label5.TabIndex = 12;
			this.label5.Text = "Status:";
			// 
			// textError
			// 
			this.textError.BackColor = System.Drawing.SystemColors.Info;
			this.textError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.textError.Location = new System.Drawing.Point(8, 184);
			this.textError.Multiline = true;
			this.textError.Name = "textError";
			this.textError.ReadOnly = true;
			this.textError.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textError.Size = new System.Drawing.Size(424, 72);
			this.textError.TabIndex = 13;
			this.textError.Text = "Test string to send.";
			// 
			// Form1
			// 
			this.ClientSize = new System.Drawing.Size(440, 320);
			this.Controls.Add(this.textError);
			this.Controls.Add(this.textBoxQueue);
			this.Controls.Add(this.textBoxMachine);
			this.Controls.Add(this.textBoxBody);
			this.Controls.Add(this.textBoxAddr);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.buttonSend);
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "Send MSMQ Message";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void textBoxMachine_TextChanged(object sender, System.EventArgs e)
		{
			genAddress();
		}

		private void textBoxQueue_TextChanged(object sender, System.EventArgs e)
		{
			genAddress();
		}

		private void genAddress() 
		{
			textBoxAddr.Text = "FormatName:DIRECT=OS:" + textBoxMachine.Text + "\\private$\\" + textBoxQueue.Text;
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			genAddress();		
		}

		// **********************************************************************************
		// 'Send Wrapped' - in this case .Net Framework will wrap your content into a simple XML
		// This example is presented here to demonstrate a common pitfall of programming MSMQ send.
		// **********************************************************************************
		// This wrapping is the result of direct assignment of message body.
		// You are strongly advised against using this method, because it may result in serious difficulties
		// in using non-passthrough pipelines or orchestrations.
		// **********************************************************************************
		private void button1_Click(object sender, System.EventArgs e)
		{
			try 
			{
				MessageQueue mq = new MessageQueue(textBoxAddr.Text);
				System.Messaging.Message msg = new System.Messaging.Message(textBoxBody.Text);
				msg.Label = "TestMessageLabel";

				// Uncomment this line if you want to send authenticated messages.
				// You need to be a part of domain for that
				// msg.UseAuthentication = true;

				mq.Send(msg,MessageQueueTransactionType.Single);
				textError.Text = "Success.";
			} 
			catch (Exception ex)
			{ 
				textError.Text = ex.ToString();
			}
		}

		// **********************************************************************************
		// 'Send Direct' - in this case .Net Framework will send your content as it is.
		// This a recommended way of sending MSMQ message to BizTalk.
		// **********************************************************************************
		private void button1_Click_1(object sender, System.EventArgs e)
		{
			try 
			{
				MessageQueue mq = new MessageQueue(textBoxAddr.Text);
				System.Messaging.Message msg = new System.Messaging.Message();
				msg.Label = "TestMessageLabel";

				// Uncomment this line if you want to send authenticated messages.
				// You need to be a part of domain for that
				// msg.UseAuthentication = true;

				StreamWriter wr = new StreamWriter(msg.BodyStream,System.Text.Encoding.Unicode);
				wr.Write(textBoxBody.Text);
				wr.Flush();

				mq.Send(msg,MessageQueueTransactionType.Single);
				textError.Text = "Success.";			
			} 
			catch (Exception ex)
			{ 
				textError.Text = ex.ToString();
			}
		}
	}
}
