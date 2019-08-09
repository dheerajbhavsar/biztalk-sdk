//---------------------------------------------------------------------
// File: ArbitraryXPathPropertyHandlerComp.cs
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
using System.Xml;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Drawing;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Component.Utilities;
using Microsoft.XLANGs.RuntimeTypes;
using Microsoft.XLANGs.BaseTypes;

namespace Microsoft.Samples.BizTalk.Pipelines.CustomComponent
{
	/// <summary>
	/// Pipeline component which can't be placed into any receive or send
	/// pipeline stage and do a property promotion / distinguished fields 
	/// writing based on arbitrary XPath.
	/// </summary>
	[ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
	[ComponentCategory(CategoryTypes.CATID_Any)]
	[Guid("C40F174F-2CBC-42a6-90C4-64694F90579E")]
	public class ArbitraryXPathPropertyHandlerComp : 
		BaseCustomTypeDescriptor,
		IBaseComponent, 
		Microsoft.BizTalk.Component.Interop.IComponent, 
		IPersistPropertyBag,
		IComponentUI
	{
		public ArbitraryXPathPropertyHandlerComp() : base(resourceManager)
		{
		}

		#region Design-time property(ies)

		/// <summary>
		/// Gets or sets a list of schemas.
		/// </summary>
		[PropertyName("DocumentSchemaListName")]
		[PropertyDescription("DocumentSchemaListDescription")]
		public SchemaList DocumentSchemaList
		{
			get
			{
				return this.documentSchemaList;
			}
			set
			{
				if (null == value)
					throw new ArgumentNullException("DocumentSchemaList");
				this.documentSchemaList = value;
			}
		}

		#endregion

		#region IBaseComponent Members

		/// <summary>
		/// Gets a component description.
		/// </summary>
		[Browsable(false)]
		public string Description
		{
			get
			{
				return resourceManager.GetString("CompDescription");
			}
		}

		/// <summary>
		/// Gets a component name.
		/// </summary>
		[Browsable(false)]
		public string Name
		{
			get
			{
				return resourceManager.GetString("CompName");
			}
		}

		/// <summary>
		/// Gets a component version.
		/// </summary>
		[Browsable(false)]
		public string Version
		{
			get
			{
				return "1.0";
			}
		}

		#endregion

		#region IComponentUI Members

		/// <summary>
		/// Validate component.
		/// </summary>
		/// <param name="projectSystem">Project system</param>
		/// <returns>Enumerator of error message collection</returns>
		public IEnumerator Validate(object projectSystem)
		{
			// Don't do any validation
			return null;
		}

		/// <summary>
		/// Gets a component icon.
		/// </summary>
		[Browsable(false)]
		public System.IntPtr Icon
		{
			get
			{
				return ((Bitmap) resourceManager.GetObject("CompIcon")).GetHicon();
			}
		}

		#endregion

		#region IComponent Members

		/// <summary>
		/// Executes a pipeline component.
		/// </summary>
		/// <param name="pContext">Pipeline context</param>
		/// <param name="pInMsg">Input message</param>
		/// <returns>Outgoing message</returns>
		public IBaseMessage Execute(IPipelineContext pContext, IBaseMessage pInMsg)
		{
			try
			{
				return ExecuteInternal(pContext, pInMsg);
			}
			catch(Exception e)
			{
				// Put component name as a source information in this exception,
				// so the event log in message could reflect this.
				e.Source = Name;
				throw e;
			}
		}

		#endregion

		#region IPersistPropertyBag Members

		/// <summary>
		/// Initialize component.
		/// </summary>
		public void InitNew()
		{
			// Do nothing
		}

		/// <summary>
		/// Get component class ID.
		/// </summary>
		/// <param name="classID"></param>
		public void GetClassID(out Guid classID)
		{
			classID = new Guid("C40F174F-2CBC-42a6-90C4-64694F90579E");
		}

		/// <summary>
		/// Load component properties from a property bag.
		/// </summary>
		/// <param name="propertyBag">Property bag</param>
		/// <param name="errorLog">Error log level</param>
		public void Load(IPropertyBag propertyBag, int errorLog)
		{
			if ( null == propertyBag )
				throw new ArgumentNullException("propertyBag");

			string documentSpecNamesString = (string) ReadPropertyBag(propertyBag, DocumentSpecNamesPropertyName);
			string [] documentSpecNamesArray = null;
			if (documentSpecNamesString != null && documentSpecNamesString.Length > 0)
			{
				documentSpecNamesArray = documentSpecNamesString.Split(DocumentSpecNamesSeparator);
			}

			string documentSpecNamespacesString = (string) ReadPropertyBag(propertyBag, DocumentSpecNamespacesPropertyName);
			string [] documentSpecNamespacesArray = null;
			if (documentSpecNamespacesString != null && documentSpecNamespacesString.Length > 0)
			{
				documentSpecNamespacesArray = documentSpecNamespacesString.Split(DocumentSpecNamesSeparator);
			}

			if (documentSpecNamespacesArray != null && 
				documentSpecNamesArray != null)
			{
				bool hasTargetNamespaces = documentSpecNamesArray.Length == documentSpecNamespacesArray.Length;

				for (int c = 0; c < documentSpecNamesArray.Length; ++c)
				{
					Schema schema = new Schema(documentSpecNamesArray[c]);
					if (hasTargetNamespaces)
					{
						schema.TargetNamespace = documentSpecNamespacesArray[c];
					}
					this.documentSchemaList.Add(schema);
				}
			}
		}

		/// <summary>
		/// Save component properties to a property bag.
		/// </summary>
		/// <param name="propertyBag">Property bag</param>
		/// <param name="clearDirty">Clear dirty flag</param>
		/// <param name="saveAllProperties">Save all properties flag</param>
		public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
		{
			if ( null == propertyBag )
				throw new ArgumentNullException("propertyBag");

			object value = null;

			StringBuilder documentSpecNamesStringBuilder = new StringBuilder();
			StringBuilder documentSpecNamespacesStringBuilder = new StringBuilder();
			for (int c = 0; c < this.documentSchemaList.Count; ++c)
			{
				documentSpecNamesStringBuilder.Append(documentSchemaList[c].DocSpecName);
				documentSpecNamespacesStringBuilder.Append(documentSchemaList[c].TargetNamespace);
				if (c < this.documentSchemaList.Count - 1)
				{
					documentSpecNamesStringBuilder.Append(DocumentSpecNamesSeparator);
					documentSpecNamespacesStringBuilder.Append(DocumentSpecNamesSeparator);
				}
			}

			value = documentSpecNamesStringBuilder.ToString();
			propertyBag.Write(DocumentSpecNamesPropertyName, ref value);

			value = documentSpecNamespacesStringBuilder.ToString();
			propertyBag.Write(DocumentSpecNamespacesPropertyName, ref value);
		}

		#endregion

		#region Private Members

		/// <summary>
		/// Executes the logic for this component.
		/// </summary>
		/// <param name="pContext">Pipeline context</param>
		/// <param name="pInMsg">Input message</param>
		/// <returns>Outgoing message</returns>
		private IBaseMessage ExecuteInternal(IPipelineContext pContext, IBaseMessage pInMsg)
		{
			// Check arguments
			if (null == pContext)
				throw new ArgumentNullException("pContext");

			if (null == pInMsg)
				throw new ArgumentNullException("pInMsg");

			if (null == pInMsg.BodyPart)
				throw new ArgumentNullException("pInMsg.BodyPart");

			if (null == pInMsg.BodyPart.GetOriginalDataStream())
				throw new ArgumentNullException("pInMsg.BodyPart.GetOriginalDataStream()");

			//
			// The logic behind this component is as follows:
			// 1. Create a seekable read-only stream over the input message body part stream
			//    (because input message can be both large and stream can be non-seekable,
			//     so it should have small memory footprint and change stream positions).
			// 2. Create a new outgoing message, new body part for it, assign seekable read-only stream
			//    to the new body part, clone body part properties, clone message context.
			// 3. Get a schema for the input message or based on schemas specified during
			//    design time.
			// 4. Load stream into XmlDocument.
			// 5. Walk through promoted properties and distinguished fields and promote/write
			//    them to the message context of the outgoing message.
			// 6. Return outgoing message.
			//

			//
			// 1. Create a seekable read-only stream over the input message body part stream.
			//

			// Create a virtual stream, using GetOriginalDataStream() method on the IBaseMessagePart because
			// this call doesn't clone stream (instead of IBaseMessagePart.Data property).
			SeekableReadOnlyStream stream = new SeekableReadOnlyStream(pInMsg.BodyPart.GetOriginalDataStream());

			//
			// 2. Create a new outgoing message, copy all required stuff.
			//

			// Create a new output message
			IBaseMessage outMessage = pContext.GetMessageFactory().CreateMessage();
			
			// Copy message context by reference
			outMessage.Context = pInMsg.Context;

			// Create new message body part
			IBaseMessagePart newBodyPart = pContext.GetMessageFactory().CreateMessagePart();

			// Copy body part properties by references.
			newBodyPart.PartProperties = pInMsg.BodyPart.PartProperties;

			// Set virtual stream as a data stream for the new message body part
			newBodyPart.Data = stream;

			// Copy message parts
			CopyMessageParts(pInMsg, outMessage, newBodyPart);

			//
			// 3. Get a schema for the message.
			//

			// Initialize schema map
			SchemaMap schemaMap = new SchemaMap(this.documentSchemaList);

			// Get message type from the message data stream
			string messageType = GetMessageType(stream);

			// Get a document spec from based on the message type
			IDocumentSpec documentSpec = schemaMap[messageType];
			if (null == documentSpec)
			{
				documentSpec = pContext.GetDocumentSpecByType(messageType);
			}

			// Promote BTS.MessageType message context property to allow orchestration schedule instances be activated
			// on produced message.
			outMessage.Context.Promote(messageTypeWrapper.Name.Name, messageTypeWrapper.Name.Namespace, messageType);

			//
			// 4. Load document stream into XmlDocument.
			//

			// Save new message stream's current position
			long position = stream.Position;

			// Load document into XmlDocument
			XmlDocument document = new XmlDocument();
			document.Load(stream);

			// Restore the 0 position for the virtual stream
			Debug.Assert(stream.CanSeek);

			// Restore new message stream's current position
			stream.Position = position;

			//
			// 5. Walk through promoted properties/distinguished fields and promote/write them.
			//

			// Walk through and promote properties
			IEnumerator annotations = documentSpec.GetPropertyAnnotationEnumerator();
			while (annotations.MoveNext())
			{
				IPropertyAnnotation annotation = (IPropertyAnnotation) annotations.Current;
				XmlNode propertyNode = document.SelectSingleNode(annotation.XPath);
				if (propertyNode != null)
				{
					// Promote context property to the message context as a typed value
					outMessage.Context.Promote(annotation.Name, annotation.Namespace, promotingMap.MapValue(propertyNode.InnerText, annotation.XSDType));
				}
			}

			// Walk through and write distinguished fields
			IDictionaryEnumerator distFields = documentSpec.GetDistinguishedPropertyAnnotationEnumerator();

			// IDocumentSpec.GetDistinguishedPropertyAnnotationEnumerator() can return null
			if (distFields != null)
			{
				while (distFields.MoveNext())
				{
					DistinguishedFieldDefinition distField = (DistinguishedFieldDefinition) distFields.Value;
					XmlNode distFieldNode = document.SelectSingleNode(distField.XPath);
					if (distFieldNode != null)
					{
						// Write distinguished field to the message context as a string value
						outMessage.Context.Write(distField.XPath, Globals.DistinguishedFieldsNamespace, distFieldNode.InnerText);
					}
				}
			}

			//
			// 6. Return outgoing message.
			//

			return outMessage;
		}

		/// <summary>
		/// Copy message parts from source to destination message.
		/// </summary>
		/// <param name="sourceMessage">Source message</param>
		/// <param name="destinationMessage">Destination message</param>
		/// <param name="newBodyPart">New message body part</param>
		private void CopyMessageParts(IBaseMessage sourceMessage, IBaseMessage destinationMessage, IBaseMessagePart newBodyPart)
		{
			string bodyPartName = sourceMessage.BodyPartName;
			for (int c = 0; c < sourceMessage.PartCount; ++c)
			{
				string partName = null;
				IBaseMessagePart messagePart = sourceMessage.GetPartByIndex(c, out partName);
				if (partName != bodyPartName)
				{
					destinationMessage.AddPart(partName, messagePart, false);
				}
				else
				{
					destinationMessage.AddPart(bodyPartName, newBodyPart, true);
				}
			}
		}

		/// <summary>
		/// Gets a message type from the XML stream.
		/// </summary>
		/// <param name="stream">Seekable stream</param>
		/// <returns>Message type</returns>
		/// <exception cref="InvalidOperationException">If message type can't be determined</exception>
		private string GetMessageType(Stream stream)
		{
			// Fail if message stream is not seekable
			if (!stream.CanSeek)
				throw new InvalidOperationException("An attempt to get a message type for non-seekable stream");

			// Save the current stream position
			long position = stream.Position;
			try
			{
				// Create XmlTextReader over the message stream
				XmlTextReader reader = new XmlTextReader(stream);
				while (reader.Read())
				{
					// Check if current node in XmlTextReader is element and it's global
					if (XmlNodeType.Element == reader.NodeType && 0 == reader.Depth)
					{
						// Found a global element, now build message type as <namespaceURI>#<localName> 
						// if namespaceURI is not empty and <localName> if namespaceURI is empty.
						if (reader.NamespaceURI != null && reader.NamespaceURI.Length > 0)
						{
							return reader.NamespaceURI + '#' + reader.LocalName;
						}
						else
						{
							return reader.LocalName;
						}
					}
				}

				// Not found, fail
				throw new InvalidOperationException("Message type can't be determined");
			}
			finally
			{
				// Restore the stream position
				stream.Position = position;
			}
		}

		/// <summary>
		/// Read property bag in order to eat the argument exception which is thrown
		/// when properties are not populated.
		/// </summary>
		/// <param name="propertyBag">Property bag</param>
		/// <param name="propertyName">Property name</param>
		/// <returns>Property value</returns>
		private object ReadPropertyBag(IPropertyBag propertyBag, string propertyName)
		{
			object value;
			try
			{
				propertyBag.Read(propertyName, out value, 0);
				return value;
			}
			catch (ArgumentException)
			{
				// IPropertyBag.Read throws an ArgumentException if there are no properties in there.
				// Just return null in this case.
				return null;
			}
		}

		private SchemaList documentSchemaList = new SchemaList();

		private static PromotingMap promotingMap = new PromotingMap();
		private static ResourceManager resourceManager = new ResourceManager("Microsoft.Samples.BizTalk.Pipelines.CustomComponent.ArbitraryXPathPropertyHandler", Assembly.GetExecutingAssembly());
		private static PropertyBase messageTypeWrapper = new BTS.MessageType();

		private const string DocumentSpecNamesPropertyName = "DocumentSpecNames";
		private const string DocumentSpecNamespacesPropertyName = "DocumentSpecNamespaces";
		private const char   DocumentSpecNamesSeparator = '|';
		
		#endregion
	}
}
