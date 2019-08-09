///////////////////////////////////////////////////////////////////////////////
// File: TpBatchAsynchResult.cs
// 
// Summary: Asynch result class for BizTalk messaging adapter.
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
using System.Threading;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.TransportProxy.Interop;
using Microsoft.BizTalk.Message.Interop;


namespace Microsoft.Samples.BizTalk.Adapters.TransportProxyUtils
{
	/// <summary>
	/// Asynch result class for BizTalk messaging adapter.
	/// </summary>
	internal class EpmAsyncResult : IAsyncResult
	{
		/**
		 * IAsyncResult properties
		 */
		private AsyncCallback			_asyncCallBack		= null;
		private Object					_state				= null;
		private TpBatchAsyncCallback	_batchCallBack		= null;
		private ResponseCallback		_responseCallback	= null;


		public EpmAsyncResult(		AsyncCallback					callBack, 
									Object							state )
		{
			this._asyncCallBack = callBack;
			this._state = state;
			_batchCallBack = new TpBatchAsyncCallback();
			_batchCallBack.Initialize((IAsyncResult)this, this._asyncCallBack);
		}

		internal AsyncCallback CallBack
		{
			get { return _asyncCallBack; }
		}

		internal TpBatchAsyncCallback BatchCallback
		{
			get { return _batchCallBack; }
		}

		internal ResponseCallback ResponseCallback
		{
			get { return _responseCallback; }
			set 
			{ 
				_responseCallback = value;
				_batchCallBack.ResponseCallback = _responseCallback;
			}
		}

#region IAsyncResult
		
		// IAsyncResult.completedSync
		public bool completedSync
		{
			get { return false; }
		}

		// IAsyncResult.IsCompleted
		public bool IsCompleted
		{
			get { return _batchCallBack.IsCompleted; }
		}

		// IAsyncResult.AsyncWaitHandle
		public WaitHandle AsyncWaitHandle
		{
			get 
			{ 
				if ( null != _responseCallback )
					return _responseCallback.AsyncWaitHandle;
				else
                    return _batchCallBack.AsyncWaitHandle; 
			}
		}

		// IAsyncResult.AsyncState
		public Object AsyncState
		{
			get { return _state; }
		}

		public bool CompletedSynchronously
		{
			get { return true; } // BUGBUG: TODO
		}

#endregion // IAsyncResult

	}
}
