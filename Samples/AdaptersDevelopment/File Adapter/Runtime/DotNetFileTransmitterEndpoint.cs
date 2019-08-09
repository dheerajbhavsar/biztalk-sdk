//---------------------------------------------------------------------
// File: DotNetFileAsyncTransmitterBatch.cs
// 
// Summary: Implementation of an adapter framework sample adapter. 
//
// Sample: Adapter framework runtime adapter.
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
using System.IO;
using System.Xml;
using System.Text;
using System.Collections;
using System.Threading;
using Microsoft.BizTalk.TransportProxy.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.Samples.BizTalk.Adapter.Common;

namespace Microsoft.BizTalk.SDKSamples.Adapters
{
	/// <summary>
	/// There is one instance of HttpTransmitterEndpoint class for each every static send port.
	/// Messages will be forwarded to this class by AsyncTransmitterBatch
	/// </summary>
	internal class DotNetFileTransmitterEndpoint : AsyncTransmitterEndpoint
	{
		private AsyncTransmitter asyncTransmitter = null;
        private string propertyNamespace;
        private static int IO_BUFFER_SIZE = 4096;

        public DotNetFileTransmitterEndpoint(AsyncTransmitter asyncTransmitter) : base(asyncTransmitter)
		{
			this.asyncTransmitter = asyncTransmitter;
		}

        public override void Open(EndpointParameters endpointParameters, IPropertyBag handlerPropertyBag, string propertyNamespace)
        {
            this.propertyNamespace = propertyNamespace;
        }

		/// <summary>
		/// Implementation for AsyncTransmitterEndpoint::ProcessMessage
		/// Transmit the message and optionally return the response message (for Request-Response support)
		/// </summary>
		public override IBaseMessage ProcessMessage(IBaseMessage message)
		{   		
			Stream source = message.BodyPart.Data;

		    // build url
            DotNetFileTransmitProperties props = new DotNetFileTransmitProperties(message, propertyNamespace);
            string filePath = DotNetFileTransmitProperties.CreateFileName(message, props.Uri);

		    // Create the new file
			using (FileStream fileStream = new FileStream(filePath, props.FileMode))
			{
				// Seek to the end of the file in case we're appending to existing data
				fileStream.Seek(0, SeekOrigin.End);

				source.Seek(0, SeekOrigin.Begin);

				// Copy the data from the msg to the file
				byte[] buffer = new byte[IO_BUFFER_SIZE];
				int bytesRead;
				while(0 != (bytesRead = source.Read(buffer, 0, buffer.Length)))
				{
					fileStream.Write(buffer, 0, bytesRead);
				}
			}

			return null;
		}
	}
}
