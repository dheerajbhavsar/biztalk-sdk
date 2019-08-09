//---------------------------------------------------------------------
// File: SchemaMap.cs
// 
// Summary: A sample pipeline component which demonstrates how to promote message context
//          properties and write distinguished fields for XML messages using arbitrary
//          XPath expressions.
//
// Sample: Arbitrary XPath Property Handler Pipeline Component SDK 
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
using System.Collections;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Component.Utilities;

namespace Microsoft.Samples.BizTalk.Pipelines.CustomComponent
{
	/// <summary>
	/// Maps schema message type to the document spec name.
	/// </summary>
	public class SchemaMap
	{
		/// <summary>
		/// Initializes an instance of SchemaMap class.
		/// </summary>
		/// <param name="schemaList">A list of schemas</param>
		public SchemaMap(SchemaList schemaList)
		{
			if (null == schemaList)
				throw new ArgumentNullException("schemaList");

			foreach (Schema schema in schemaList)
			{
				// There is a known drawback in BizTalk Server 2016 when there is no way to find
				// out an assembly name for pipeline from a pipeline component, so local schemas are
				// always retrieved by message type.
				if (null == schema.AssemblyName)
					continue;

				IDocumentSpec documentSpec = new DocumentSpec(schema.DocSpecName, schema.AssemblyName);
				schemaMap.Add(documentSpec.DocType, documentSpec);
			}
		}

		/// <summary>
		/// Gets a IDocumentSpec instance by its message type.
		/// </summary>
		public IDocumentSpec this[string messageType]
		{
			get
			{
				return schemaMap[messageType] as IDocumentSpec;
			}
		}

		private Hashtable schemaMap = new Hashtable();
	}
}
