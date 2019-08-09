
//---------------------------------------------------------------------
// File: TransactionalTransmitter.cs
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
using System.Xml;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.TransportProxy.Interop;
using Microsoft.Samples.BizTalk.Adapter.Common;

namespace Microsoft.Samples.BizTalk.Adapter.Transactional
{
    //  The TransactionalTransmitter class is a Singleton.
    //
    //  This is the class that is loaded by the BizTalk runtime and so must be public.
    //
    //  The instance of this class used by the runtime is created on demand when messages are available.
    //
    //  The main role of this class is to hand back a "batch" object to BizTalk when it has messages to send.
    //  Note that is name is confusing - this batch object is unrelated to the message-batch class. The message-batch
    //  class is captured in the BaseAdapter framework with the Batch class. This AsyncBatch class is just a way for
    //  the BizTalk runtime to hand the adapter the list of messages it has ready to send.

	public class TransactionalTransmitter : AsyncTransmitter
	{
		private int transactionalMaxBatchSize;

		public TransactionalTransmitter() : base(
			"Transactional Transmitter",
			"1.0",
			"Send messages from BizTalk with transactions",
			"txn",
            new Guid("D370B180-AF97-4528-B758-B912891B0A6D"),
			"http://schemas.microsoft.com/BizTalk/2006/txn-properties",
            null,
			50)
		{
            this.transactionalMaxBatchSize = base.MaxBatchSize;
		}

		protected override void HandlerPropertyBagLoaded ()
		{
			int maximumFiles = 0;
			IPropertyBag config = this.HandlerPropertyBag;
			if (null != config)
			{
				XmlDocument handlerConfigDom = ConfigProperties.IfExistsExtractConfigDom(config);
				if (null != handlerConfigDom)
				{
				}
			}
            this.transactionalMaxBatchSize = (maximumFiles > 0 ? maximumFiles : base.MaxBatchSize);
		}

		protected override int MaxBatchSize { get { return this.transactionalMaxBatchSize; } }

        //  Create and return to BizTalk the object it will use to give the adapter the list of messages.
		protected override IBTTransmitterBatch CreateAsyncTransmitterBatch ()
		{
            return new TransactionalAsyncBatch(this.TransportProxy, this.ControlledTermination, this.PropertyNamespace, this.MaxBatchSize);
        }
	}
}
