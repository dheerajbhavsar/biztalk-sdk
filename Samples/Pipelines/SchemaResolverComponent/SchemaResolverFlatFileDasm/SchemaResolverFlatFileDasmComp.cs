//---------------------------------------------------------------------
// File: SchemaResolverFlatFileDasmComp.cs
// 
// Summary: A Sample of how to write a extend a flat file disassembler component.
//
// Sample: Custom Pipeline Component SDK 
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
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Component;
using Microsoft.Samples.BizTalk.Pipelines.CustomComponent;

namespace Microsoft.Samples.BizTalk.Pipelines.SchemaResolver
{
	/// <summary>
	/// Summary description for SchemaResolverFlatFileDasmComp.
	/// </summary>
	[ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
	[ComponentCategory(CategoryTypes.CATID_DisassemblingParser)]
	[Guid("E97DFAA6-EBD5-4999-A9C3-8C56DCAB898A")]
	public class SchemaResolverFlatFileDasmComp : IBaseComponent, IDisassemblerComponent, IProbeMessage, IComponentUI
	{
		
		public SchemaResolverFlatFileDasmComp()
		{
		}

		#region IBaseComponent Members

		public string Description
		{
			get
			{
				return "Flat file disassembler which resolves schemas using message contents";
			}
		}

		public string Name
		{
			get
			{
				return "Schema resolving Flat File Disassembler";
			}
		}

		public string Version
		{
			get
			{
				return "1.0";
			}
		}

		#endregion

		#region IDisassemblerComponent Members

		public IBaseMessage GetNext(IPipelineContext pContext)
		{
			// Delegate call to Flat File disassembler
			return disassembler.GetNext(pContext);
		}

		public void Disassemble(IPipelineContext pContext, IBaseMessage pInMsg)
		{
			// Delegate call to Flat File disassembler
			disassembler.Disassemble(pContext, pInMsg);
		}

		#endregion

		#region IProbeMessage Members

		public bool Probe(IPipelineContext pContext, IBaseMessage pInMsg)
		{
            // Check arguments
			if (null == pContext)
				throw new ArgumentNullException("pContext");

			if (null == pInMsg)
				throw new ArgumentNullException("pInMsg");

			// We need to determine a document schema to use based on message content. For the sake of simplicity of this
			// sample, we will check the first two characters in input stream and map them to some schema message types we
			// have predefined. The more sofisticated component could use UI configuration options to map identification 
			// text located at specified offsets in message stream and having specified length, which could map to specified
			// message type or document spec type name.

			// Check whether input message doesn't have a body part or it is set to null, fail probe in those cases
			if (null == pInMsg.BodyPart || null == pInMsg.BodyPart.GetOriginalDataStream())
				return false;

			SeekableReadOnlyStream stream = new SeekableReadOnlyStream(pInMsg.BodyPart.GetOriginalDataStream());
			Stream sourceStream = pInMsg.BodyPart.GetOriginalDataStream();

			// Check if source stream can seek
			if (!sourceStream.CanSeek)
			{
				// Create a virtual (seekable) stream
				SeekableReadOnlyStream seekableStream = new SeekableReadOnlyStream(sourceStream);

				// Set new stream for the body part data of the input message. This new stream will then used for further processing.
				// We need to do this because input stream may not support seeking, so we wrap it with a seekable stream.
				pInMsg.BodyPart.Data = seekableStream;

				// Replace sourceStream with a new seekable stream wrapper
				sourceStream = pInMsg.BodyPart.Data;
			}

			// Preserve the stream position
			long position = sourceStream.Position;

			char [] signature = new char[2];
			try
			{
				// Read signature from a stream
				StreamReader reader = new StreamReader(sourceStream);
				if (reader.Read(signature, 0, signature.Length) < signature.Length)
					return false;

				// Don't close stream reader to avoid closing of underlying stream
			}
			finally
			{
				// Restore the stream position
				sourceStream.Position = position;
			}

			// Get message type from signature
			string messageType = GetMessageType(new string(signature));

			// Fail if message type is unknown
			if (null == messageType)
				return false;

			// Get document spec from the message type
			IDocumentSpec documentSpec = pContext.GetDocumentSpecByType(messageType);

			// Instead of loading schema to get a document spec type name we could change implementation to return defined 
			// during a design time document spec type name and directly specify it in the call below:
			
			// Write document spec type name to the message context so Flat File disassembler could access this property and
			// do message processing for a schema which has document spec type name we've discovered
			pInMsg.Context.Write(DocumentSpecNamePropertyName, XmlNormNamespaceURI, documentSpec.DocSpecStrongName);

			// Delegate call to Flat File disassembler
			return disassembler.Probe(pContext, pInMsg);
		}

		#endregion

		#region IComponentUI Members


		public IntPtr Icon
		{
			get
			{

				return System.IntPtr.Zero;
			}

		}

		/// <summary>
		/// The Validate method is called by the BizTalk Editor during the build 
		/// of a BizTalk project.
		/// </summary>
		/// <param name="obj">Project system.</param>
		/// <returns>
		/// A list of error and/or warning messages encounter during validation
		/// of this component.
		/// </returns>
		public IEnumerator Validate(object obj)
		{
			return null;
		}

		#endregion

		#region Private Members

		/// <summary>
		/// Gets message type for a signature
		/// </summary>
		/// <param name="signature">A signature</param>
		/// <returns>Message type</returns>
		private string GetMessageType(string signature)
		{
			InitializeMapping();
			return (string) messageTypes[ signature ];
		}

		/// <summary>
		/// Lazily initializes signature to message type mapping
		/// </summary>
		private static void InitializeMapping()
		{
			if (messageTypes != null)
				return;

			lock (typeof(SchemaResolverFlatFileDasmComp))
			{
				messageTypes = new Hashtable();

				// Here document spec types names are hardcoded, in real implementation you need to specify them
				// using design time properties.
				messageTypes[ "PO" ] = "http://schemas.biztalk.org/PO#PurchaseOrder";
				messageTypes[ "PR" ] = "http://schemas.biztalk.org/PR#PurchaseRequest";
				messageTypes[ "SO" ] = "http://schemas.biztalk.org/SO#SalesOrder";
				messageTypes[ "SR" ] = "http://schemas.biztalk.org/SR#SalesRequest";
			}
		}

		private FFDasmComp disassembler = new FFDasmComp();
		private static Hashtable messageTypes = null;
		
		private const string XmlNormNamespaceURI = "http://schemas.microsoft.com/BizTalk/2003/xmlnorm-properties";
		private const string DocumentSpecNamePropertyName = "DocumentSpecName";

		#endregion
	}
}
