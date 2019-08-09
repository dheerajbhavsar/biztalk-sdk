//---------------------------------------------------------------------
// File: PropertyAttributes.cs
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
using System.ComponentModel;
using System.Resources;

namespace Microsoft.Samples.BizTalk.Pipelines.CustomComponent
{
	/// <summary>
	/// Implements property name attribute to be used with design-time properties.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, Inherited=true, AllowMultiple=false)]
	public sealed class PropertyNameAttribute : Attribute
	{
		private string propertyName;

		/// <summary>
		/// Initializes PropertyNameAttribute instance with a property name.
		/// </summary>
		/// <param name="propertyName">A property name</param>
		public PropertyNameAttribute(string propertyName)
		{
			this.propertyName = propertyName;
		}

		/// <summary>
		/// Gets a property name.
		/// </summary>
		public string PropertyName
		{
			get { return this.propertyName; }
		}
	}

	/// <summary>
	/// Implements property description attribute to be used with design-time properties.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, Inherited=true, AllowMultiple=false)]
	public sealed class PropertyDescriptionAttribute : Attribute
	{
		private string propertyDescription;

		/// <summary>
		/// Initializes PropertyDescriptionAttribute instance with a property description.
		/// </summary>
		/// <param name="propertyName">A property description</param>
		public PropertyDescriptionAttribute(string propertyDescription)
		{
			this.propertyDescription = propertyDescription;
		}

		/// <summary>
		/// Gets a property description.
		/// </summary>
		public string PropertyDescription
		{
			get { return this.propertyDescription; }
		}
	}

	/// <summary>
	/// Implements a property descriptor for design-time properties.
	/// </summary>
	public class XPathPropertyHandlerPropertyDescriptor : PropertyDescriptor
	{
		private PropertyDescriptor descriptor;
		private ResourceManager resourceManager;

		public XPathPropertyHandlerPropertyDescriptor(PropertyDescriptor descriptor, ResourceManager resourceManager) : base(descriptor)
		{
			this.descriptor = descriptor;
			this.resourceManager = resourceManager;
		}

		public ResourceManager ResourceManager
		{
			get { return this.resourceManager; }
		}

		public override AttributeCollection Attributes 
		{
			get { return descriptor.Attributes; }
		}

		public override object GetEditor(Type editorBaseType)
		{
			return descriptor.GetEditor(editorBaseType);
		}

		public override string Category
		{
			get { return descriptor.Category; }
		}

		public override Type ComponentType
		{
			get { return descriptor.ComponentType; }
		}

		public override TypeConverter Converter
		{
			get { return descriptor.Converter; }
		}

		public override string Description
		{
			get
			{
				AttributeCollection attributes = descriptor.Attributes;

				PropertyDescriptionAttribute descriptionAttribute = 
					(PropertyDescriptionAttribute) attributes[typeof(PropertyDescriptionAttribute)];

				if (descriptionAttribute == null)
					return descriptor.Description;

				string strId = descriptionAttribute.PropertyDescription;
				if (resourceManager == null)
					return strId;

				return resourceManager.GetString(strId);
			}
		}

		public override bool DesignTimeOnly
		{
			get { return descriptor.DesignTimeOnly; }
		}

		public override bool IsBrowsable
		{
			get { return descriptor.IsBrowsable; }
		}

		public override bool IsLocalizable
		{
			get { return descriptor.IsLocalizable; }
		}

		public override bool IsReadOnly
		{
			get { return descriptor.IsReadOnly; }
		}

		public override Type PropertyType
		{
			get { return descriptor.PropertyType; }
		}

		public override bool ShouldSerializeValue(object o)
		{
			return descriptor.ShouldSerializeValue(o);
		}

		public override void AddValueChanged(object o, EventHandler handler)
		{
			descriptor.AddValueChanged(o, handler);
		}

		public override string DisplayName
		{
			get
			{
				AttributeCollection attributes = descriptor.Attributes;

				PropertyNameAttribute nameAttribute = 
					(PropertyNameAttribute)attributes[typeof(PropertyNameAttribute)];

				if (nameAttribute == null)
					return descriptor.DisplayName;

				string strId = nameAttribute.PropertyName;
				if (resourceManager == null)
					return strId;

				return resourceManager.GetString(strId);
			}
		}

		public override string Name
		{
			get { return descriptor.Name; }
		}

		public override Object GetValue(object o)
		{
			return descriptor.GetValue(o);
		}

		public override void ResetValue(object o)
		{
			descriptor.ResetValue(o);
		}

		public override bool CanResetValue(object o)
		{
			return descriptor.CanResetValue(o);
		}

		public override void SetValue(object obj1, object obj2)
		{
			descriptor.SetValue(obj1, obj2);
		}
	}

	/// <summary>
	/// Custom type description for design-time properties.
	/// </summary>
	public class BaseCustomTypeDescriptor : ICustomTypeDescriptor 
	{
		private ResourceManager resourceManager;

		public BaseCustomTypeDescriptor(ResourceManager resourceManager)
		{
			this.resourceManager = resourceManager;
		}

		public AttributeCollection GetAttributes()
		{
			return new AttributeCollection(null);
		}

		public virtual string GetClassName()
		{
			return null;
		}

		public virtual string GetComponentName()
		{
			return null;
		}

		public TypeConverter GetConverter()
		{
			return null;
		}

		public EventDescriptor GetDefaultEvent()
		{
			return null;
		}

		public PropertyDescriptor GetDefaultProperty()
		{
			return null;
		}

		public object GetEditor(Type editorBaseType)
		{
			return null;
		}

		public EventDescriptorCollection GetEvents()
		{
			return EventDescriptorCollection.Empty;
		}

		public EventDescriptorCollection GetEvents(Attribute[] filter)
		{
			return EventDescriptorCollection.Empty;
		}

		public virtual PropertyDescriptorCollection GetProperties() 
		{
			PropertyDescriptorCollection sourceProperties = TypeDescriptor.GetProperties(this.GetType());
			XPathPropertyHandlerPropertyDescriptor[] propertyDescriptors = new XPathPropertyHandlerPropertyDescriptor[sourceProperties.Count];

			int i = 0;
			foreach (PropertyDescriptor sourceDescriptor in sourceProperties)
			{
				XPathPropertyHandlerPropertyDescriptor destDescriptor = new XPathPropertyHandlerPropertyDescriptor(sourceDescriptor, resourceManager);
				AttributeCollection attributes = sourceDescriptor.Attributes;
				propertyDescriptors[i++] = destDescriptor;
			}
			PropertyDescriptorCollection destProperties = new PropertyDescriptorCollection(propertyDescriptors);
			return destProperties;
		}

		public virtual PropertyDescriptorCollection GetProperties(Attribute[] filter)
		{
			PropertyDescriptorCollection baseProps = TypeDescriptor.GetProperties(this);
			return baseProps;
		}

		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}
}
