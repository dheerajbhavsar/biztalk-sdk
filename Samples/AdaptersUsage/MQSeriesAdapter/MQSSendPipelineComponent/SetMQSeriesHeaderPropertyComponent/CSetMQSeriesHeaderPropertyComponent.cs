//---------------------------------------------------------------------
// This file is part of the Microsoft BizTalk Adapter for MQSeries SDK
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// This source code is intended only as a supplement to Microsoft BizTalk
// Server 2009 release and/or on-line documentation. See these other
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
using System.Resources;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.Win32;

///This is a sample send pipeline component that reads a set of MQSeries header properties 
///from an xml file and writes them to the context
///After building this component, copy the pipeline component dll file to 
///C:\Program Files (x86)\Microsoft BizTalk Server 2016\Pipeline Components 
///(or where ever BizTalk 2009 is installed)
///next reference the pipeline component dll in your 
namespace SetMQSeriesHeaderPropertyComponent
{
	//This component is of type Any
	[ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
	[ComponentCategory(CategoryTypes.CATID_Any)]
	[System.Runtime.InteropServices.Guid("34835941-4E12-40f6-A29A-59B72DB344A4")]

	public class CSetMQSeriesHeaderPropertyComponent  :	
		IBaseComponent,
		Microsoft.BizTalk.Component.Interop.IComponent
	{

		public CSetMQSeriesHeaderPropertyComponent()
		{
		
		}

		#region IBaseComponent
        
		/// <summary>
		/// Name of the component.
		/// </summary>
		[Browsable(false)]
		public string Name
		{
			get {   return "Custom Component to Set MQseries header properties";  }
		}
        
		/// <summary>
		/// Version of the component.
		/// </summary>
		[Browsable(false)]
		public string Version
		{
			get {   return "1.0";   }
		}
        
		/// <summary>
		/// Description of the component.
		/// </summary>
		[Browsable(false)]
		public string Description
		{
			get {   return "Set MQSeries Header Properties Custom Pipeline Component"; }
		}
    
		#endregion

		#region IComponent

		public IBaseMessage Execute(IPipelineContext pc, IBaseMessage inmsg)
		{
			setHeaderProps(pc,inmsg);
			
			return inmsg;
		}

		 
	void setHeaderProps(IPipelineContext pc, IBaseMessage inmsg) 
		{
			uint numProps = inmsg.Context.CountProperties;

			System.Xml.XmlDocument xd		=	null;	

			xd						=	new XmlDocument();
	
			//A sample xml file is in this folder, called SetMQSMQMDHdrProps.xml
			//Change this variable(samplesDir) as appropriate.
			String samplesDir = @"c:\temp\";
			xd.Load(samplesDir + "SetMQSMQMDHdrProps.xml");
			System.Xml.XmlNodeList hdrList	=	xd.GetElementsByTagName("Header");
			System.Xml.XmlNode curNode		=	null;

			for (int i =0; i< hdrList.Count ; i++)
			{
				curNode		=	hdrList.Item(i);
				string Name = curNode.SelectSingleNode("./Name").FirstChild.Value;
				string Value = curNode.SelectSingleNode("./Value").FirstChild.Value;
				string type = curNode.SelectSingleNode("./Type").FirstChild.Value;
				if (type.ToUpper().Equals("UINT32"))
				{
					object ValueObj = null;
					ValueObj = new UInt32();
					ValueObj = System.Convert.ToUInt32( Value );
					inmsg.Context.Write(Name,"http://schemas.microsoft.com/BizTalk/2003/mqs-properties",ValueObj);
					continue;
				}
				else 
				{
					inmsg.Context.Write(Name,"http://schemas.microsoft.com/BizTalk/2003/mqs-properties",Value);
					continue;
				}
	
			}

		}

		#endregion

	}
}
