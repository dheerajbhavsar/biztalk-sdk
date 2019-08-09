
//---------------------------------------------------------------------
// File: TransactionalDeleteBatch.cs
// 
// Summary: Implementation of an adapter framework sample adapter. 
//
// Sample: Adapter framework transactional adapter.
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
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.TransportProxy.Interop;
using Microsoft.Samples.BizTalk.Adapter.Common;

namespace Microsoft.Samples.BizTalk.Adapter.Transactional
{
    class TransactionalDeleteBatch : TxnBatch
    {
        public TransactionalDeleteBatch(IBTTransportProxy transportProxy, ControlledTermination control, CommittableTransaction transaction)
            : base(transportProxy, control, transaction, null, true)
        {
        }
        protected override void DeleteSuccess(IBaseMessage message, Int32 hrStatus, object userData)
        {
            base.SetComplete();
        }
        protected override void DeleteFailure(IBaseMessage message, Int32 hrStatus, object userData)
        {
            base.SetAbort();
        }
    }
}
