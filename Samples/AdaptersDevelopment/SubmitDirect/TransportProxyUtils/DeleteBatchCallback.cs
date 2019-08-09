///////////////////////////////////////////////////////////////////////////////
// File: DeleteBatchCallback.cs
// 
// Summary: Delete batch callback class for BizTalk messaging adapter.
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
using Microsoft.BizTalk.TransportProxy.Interop;

namespace Microsoft.Samples.BizTalk.Adapters.TransportProxyUtils
{
    internal class DeleteBatchCallback : IBTBatchCallBack
    {
        private readonly AutoResetEvent _resetEvent;

        public DeleteBatchCallback(AutoResetEvent resetEvent)
        {
            if(resetEvent == null)
            {
                throw new ArgumentNullException("resetEvent");
            }

            _resetEvent = resetEvent;
        }

        #region IBTBatchCallBack Members

        public void BatchComplete(int status, short opCount, BTBatchOperationStatus[] operationStatus, object callbackCookie)
        {
            //Ignoring BatchComplete for sample adapter. Ideally, the logic for status check and acting correspondingly should be present.
            _resetEvent.Set();
        }

        #endregion
    }
}