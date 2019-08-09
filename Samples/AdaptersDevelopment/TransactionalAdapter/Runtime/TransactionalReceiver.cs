
//---------------------------------------------------------------------
// File: TransactionalReceiver.cs
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
using Microsoft.Samples.BizTalk.Adapter.Common;

namespace Microsoft.Samples.BizTalk.Adapter.Transactional
{
    //  The TransactionalReceiver class is a Singleton.
    //
    //  This is the class that is loaded by the BizTalk runtime and so must be public.
    //
    //  The instance of this class used by the BizTalk runtime is created at startup (or enable).
    //
    //  Through the BaseAdapter adapter Receiver class it acts as a Factory for endpoints. In this
    //  case TransactionalReceiverEndpoint instances. There will be an endpoint instance for every
    //  logical receive location configured and enabled. The base class is given the type of endpoint
    //  to create.
    //
    //  The Receiver maintains a hashtable of active receive locations and calls Update and Delete
    //  appropriately.
    //
    //  If you have any state that should be shared amongst all active endpoints of this adapter you can
    //  put it here as an alternative to making it static in the endpoint class.

    public class TransactionalReceiver : Receiver 
	{
        public TransactionalReceiver()
            : base(
			"Transactional Adapter",
			"1.0",
			"Fetch remote messages into BizTalk with a transaction",
			"txn",
            new Guid("12F8CA70-4DE4-44e8-8F78-D947523F8DEF"),
			"http://schemas.microsoft.com/BizTalk/2006/txn-properties",
			typeof(TransactionalReceiverEndpoint))
		{
		}
	}
}