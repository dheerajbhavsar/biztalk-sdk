//---------------------------------------------------------------------
// File: TpBatchStatus.cs
// 
// Summary: Response callback class for BizTalk messaging adapter.
//
// Sample: SubmitDirect SDK sample 
//
//---------------------------------------------------------------------
// This file is part of the Microsoft BizTalk Server SDK
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// This source code is intended only as a supplement to Microsoft BizTalk
// Server release and/or on-line documentation. See these other
// materials for detailed information regarding Microsoft code samples.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
// PURPOSE.
//---------------------------------------------------------------------

using System;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.TransportProxy.Interop;
using Microsoft.BizTalk.Message.Interop;


namespace Microsoft.Samples.BizTalk.Adapters.TransportProxyUtils
{
	/// <summary>
	/// Class for storing the status of batch operations
	/// </summary>
	internal class TpBatchStatus
	{
		internal int						status			= 0;
		internal short						opCount			= 0;
		internal BTBatchOperationStatus[]	operationStatus	= null;
		internal object						callbackCookie	= null;				
			

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="status">Overall status of batch processing</param>
		/// <param name="opCount">Number of operations performed on the batch</param>
		/// <param name="operationStatus">Array of statuses for each opearation</param>
		/// <param name="callbackCookie">Callback cookie</param>
		public TpBatchStatus(	int status, 
								short opCount, 
								BTBatchOperationStatus[] operationStatus, 
								object callbackCookie )
		{
			this.status = status;
			this.opCount = opCount;
			this.operationStatus = operationStatus;
			this.callbackCookie = callbackCookie;
		}
	}
}
