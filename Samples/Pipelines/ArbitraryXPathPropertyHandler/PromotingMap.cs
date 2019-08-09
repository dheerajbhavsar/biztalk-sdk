//---------------------------------------------------------------------
// File: PromotingMap.cs
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
using System.Xml;

namespace Microsoft.Samples.BizTalk.Pipelines.CustomComponent
{
	/// <summary>
	/// Delegate used for type mapping
	/// </summary>
	internal delegate object ChangeTypeDelegate(string value);

	/// <summary>
	/// Implements a property promoting map.
	/// </summary>
	public class PromotingMap
	{
		/// <summary>
		/// Maps string value to the typed CLR value based on XSD data type.
		/// </summary>
		/// <param name="value">String value</param>
		/// <param name="XSDType">XSD data type</param>
		/// <returns>CLR value</returns>
		public object MapValue(string value, string XSDType)
		{
			InitializeMapping();

			ChangeTypeDelegate changeType = (ChangeTypeDelegate) propertyMapping[ XSDType ];
			if (null == changeType)
				throw new NotSupportedException(String.Format("XSD data type {0} is not supported for property promotion", XSDType));

			return changeType(value);
		}

		/// <summary>
		/// Initialize a type mapping hashtable.
		/// </summary>
		private static void InitializeMapping()
		{
			// Quick check if property mapping already initialized, don't need to be 
			// synchronized
			if (propertyMapping != null)
				return;

			// Lock all instances of PromotingMap class
			lock (typeof(PromotingMap))
			{
				propertyMapping = new Hashtable();

                propertyMapping[ "anyURI" ] = new ChangeTypeDelegate(ChangeTypeToString);
				propertyMapping[ "boolean" ] = new ChangeTypeDelegate(ChangeTypeToBoolean);
				propertyMapping[ "byte" ] = new ChangeTypeDelegate(ChangeTypeToSByte);
				propertyMapping[ "date" ] = new ChangeTypeDelegate(ChangeTypeToDateTime);
				propertyMapping[ "dateTime" ] = new ChangeTypeDelegate(ChangeTypeToDateTime);
				propertyMapping[ "decimal" ] = new ChangeTypeDelegate(ChangeTypeToDecimal);
				propertyMapping[ "double" ] = new ChangeTypeDelegate(ChangeTypeToDouble);
				propertyMapping[ "ENTITY" ] = new ChangeTypeDelegate(ChangeTypeToString);
				propertyMapping[ "float" ] = new ChangeTypeDelegate(ChangeTypeToSingle);
				propertyMapping[ "gDay" ] = new ChangeTypeDelegate(ChangeTypeToDateTime);
				propertyMapping[ "gMonth" ] = new ChangeTypeDelegate(ChangeTypeToDateTime);
				propertyMapping[ "gMonthDay" ] = new ChangeTypeDelegate(ChangeTypeToDateTime);
				propertyMapping[ "gYear" ] = new ChangeTypeDelegate(ChangeTypeToDateTime);
				propertyMapping[ "gYearMonth" ] = new ChangeTypeDelegate(ChangeTypeToDateTime);
				propertyMapping[ "ID" ] = new ChangeTypeDelegate(ChangeTypeToString);
				propertyMapping[ "IDREF" ] = new ChangeTypeDelegate(ChangeTypeToString);
				propertyMapping[ "int" ] = new ChangeTypeDelegate(ChangeTypeToInt32);
				propertyMapping[ "integer" ] = new ChangeTypeDelegate(ChangeTypeToDecimal);
				propertyMapping[ "language" ] = new ChangeTypeDelegate(ChangeTypeToString);
				propertyMapping[ "Name" ] = new ChangeTypeDelegate(ChangeTypeToString);
				propertyMapping[ "NCName" ] = new ChangeTypeDelegate(ChangeTypeToString);
				propertyMapping[ "negativeInteger" ] = new ChangeTypeDelegate(ChangeTypeToDecimal);
				propertyMapping[ "NMTOKEN" ] = new ChangeTypeDelegate(ChangeTypeToString);
				propertyMapping[ "nonNegativeInteger" ] = new ChangeTypeDelegate(ChangeTypeToDecimal);
				propertyMapping[ "nonPositiveInteger" ] = new ChangeTypeDelegate(ChangeTypeToDecimal);
				propertyMapping[ "normalizedString" ] = new ChangeTypeDelegate(ChangeTypeToString);
				propertyMapping[ "NOTATION" ] = new ChangeTypeDelegate(ChangeTypeToString);
				propertyMapping[ "positiveInteger" ] = new ChangeTypeDelegate(ChangeTypeToDecimal);
				propertyMapping[ "QName" ] = new ChangeTypeDelegate(ChangeTypeToString);
				propertyMapping[ "short" ] = new ChangeTypeDelegate(ChangeTypeToInt16);
				propertyMapping[ "string" ] = new ChangeTypeDelegate(ChangeTypeToString);
				propertyMapping[ "time" ] = new ChangeTypeDelegate(ChangeTypeToDateTime);
				propertyMapping[ "token" ] = new ChangeTypeDelegate(ChangeTypeToString);
				propertyMapping[ "unsignedByte" ] = new ChangeTypeDelegate(ChangeTypeToByte);
				propertyMapping[ "unsignedInt" ] = new ChangeTypeDelegate(ChangeTypeToUInt32);
				propertyMapping[ "unsignedShort" ] = new ChangeTypeDelegate(ChangeTypeToUInt16);
			}
		}

		private static object ChangeTypeToString(string value)
		{
			return value;
		}

		private static object ChangeTypeToBoolean(string value)
		{
			return XmlConvert.ToBoolean(value);
		}

		private static object ChangeTypeToSByte(string value)
		{
			return XmlConvert.ToSByte(value);
		}

		private static object ChangeTypeToDateTime(string value)
		{
			// Adjust DateTime value to be Universal after conversion, all date/time 
			// context properties must be in UTC
			return XmlConvert.ToDateTime(value,XmlDateTimeSerializationMode.Utc);
		}

		private static object ChangeTypeToDecimal(string value)
		{
			return XmlConvert.ToDecimal(value);
		}

		private static object ChangeTypeToDouble(string value)
		{
			return XmlConvert.ToDouble(value);
		}

		private static object ChangeTypeToSingle(string value)
		{
			return XmlConvert.ToSingle(value);
		}

		private static object ChangeTypeToInt32(string value)
		{
			return XmlConvert.ToInt32(value);
		}

		private static object ChangeTypeToInt16(string value)
		{
			return XmlConvert.ToInt16(value);
		}

		private static object ChangeTypeToByte(string value)
		{
			return XmlConvert.ToByte(value);
		}

		private static object ChangeTypeToUInt32(string value)
		{
			return XmlConvert.ToUInt32(value);
		}

		private static object ChangeTypeToUInt16(string value)
		{
			return XmlConvert.ToUInt16(value);
		}

		private static Hashtable propertyMapping = null;
	}
}
