//---------------------------------------------------------------------
// File: XslTransformer.cs
// 
// Summary: Pipeline component that applies Xsl transformation to convert XML messages to HTML documents.
//
// Sample: Xsl Transformation component SDK 
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
using System.Xml;
using System.Xml.Xsl;
using System.ComponentModel;
using System.Collections;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.Win32;

namespace Microsoft.BizTalk.SDKSamples.Pipelines.XslTransformComponent
{
	/// <summary>
	/// Implements a pipeline component that applies Xsl Transformations to XML messages
	/// </summary>
	/// <remarks>
	/// XslTransformer class implements pipeline components that can be used in send pipelines
	/// to convert XML messages to HTML format for sending using SMTP transport. Component can
	/// be placed only in Encoding stage of send pipeline
	/// </remarks>
	[ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
	[ComponentCategory(CategoryTypes.CATID_Encoder)]
	[System.Runtime.InteropServices.Guid("FA7F9C55-6E8E-4855-8DAC-FA1BC8A499E2")]
	public class XslTransformer		: Microsoft.BizTalk.Component.Interop.IBaseComponent, 
									  Microsoft.BizTalk.Component.Interop.IComponent, 
		                              Microsoft.BizTalk.Component.Interop.IPersistPropertyBag,
		                              Microsoft.BizTalk.Component.Interop.IComponentUI
	{	
		
		private string xsltPath	= null;
		
		/// <summary>
		/// Location of Xsl transform file.
		/// </summary>
		public string XsltFilePath
		{
			get {	return xsltPath;}
			set {	xsltPath = value;}
		}

		
		#region IBaseComponent
		
		/// <summary>
		/// Name of the component.
		/// </summary>
		[Browsable(false)]
		public string Name
		{
			get {	return "XSL Transform Component";	}
		}
		
		/// <summary>
		/// Version of the component.
		/// </summary>
		[Browsable(false)]
		public string Version
		{
			get	{	return "1.0";	}
		}
		
		/// <summary>
		/// Description of the component.
		/// </summary>
		[Browsable(false)]
		public string Description
		{
			get	{	return "XSL Transform Pipeline Component";	}
		}
	
		#endregion
		
		#region IComponent

		/// <summary>
		/// Implements IComponent.Execute method.
		/// </summary>
		/// <param name="pc">Pipeline context</param>
		/// <param name="inmsg">Input message.</param>
		/// <returns>Converted to HTML input message.</returns>
		/// <remarks>
		/// IComponent.Execute method is used to convert XML messages
		/// to HTML messages using provided Xslt file.
		/// It also sets the content type of the message part to be "text/html"
		/// which is necessary for client mail applications to correctly render
		/// the message
		/// </remarks>
		public IBaseMessage Execute(IPipelineContext pc, IBaseMessage inmsg)
		{
			inmsg.BodyPart.Data = TransformMessage(inmsg.BodyPart.Data);
			inmsg.BodyPart.ContentType = "text/html";
			return inmsg;
		}
		#endregion

		#region Helper function
		/// <summary>
		/// Transforms XML message in input stream to HTML message
		/// </summary>
		/// <param name="stm">Stream with input XML message</param>
		/// <returns>Stream with output HTML message</returns>
		private Stream TransformMessage(Stream stm)
		{
			MemoryStream ms = null;
			string validXsltPath = null;
			
			try 
			{
				// Get the full path to the Xslt file
				validXsltPath = GetValidXsltPath(xsltPath);
				
				// Load transform
				XslTransform transform = new XslTransform();
				transform.Load(validXsltPath);
				
				//Load Xml stream in XmlDocument.
				XmlDocument doc = new XmlDocument();
				doc.Load(stm);
				
				//Create memory stream to hold transformed data.
				ms = new MemoryStream();
				
				//Preform transform
				transform.Transform(doc, null, ms, null);
				ms.Seek(0, SeekOrigin.Begin);
			}
			catch(Exception e) 
			{
				System.Diagnostics.Trace.WriteLine(e.Message);
				System.Diagnostics.Trace.WriteLine(e.StackTrace);
				throw e;
			}

			return ms;
		}

		/// <summary>
		/// Get a valid full path to a Xslt file
		/// </summary>
		/// <param name="path">Path provided by user in Pipeline Designer</param>
		/// <returns>The full path</returns>
		/// <remarks>
		/// If user provides absolute path then it is used as long as the file can be opened there
		/// If user provides just a name of file or relative path then we try to open a file in 
		/// [Install foder]\Pipeline Components
		/// </remarks>
		private string GetValidXsltPath(string path)
		{
			string validPath = path;

			if (!System.IO.File.Exists(path))
			{
                		RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\BizTalk Server\3.0");
		                string InstallPath = string.Empty;
                
                		if (null != rk)
					InstallPath = (String)rk.GetValue("InstallPath");
                        
                		validPath = InstallPath + @"Pipeline Components\" + path;
				if (!System.IO.File.Exists(validPath))
				{
					throw new ArgumentException("The XSL transformation file " + path + " can not be found");
				}
			}	

			return validPath;
		}

		#endregion	
		

		#region IPersistPropertyBag
	
		/// <summary>
		/// Gets class ID of component for usage from unmanaged code.
		/// </summary>
		/// <param name="classid">Class ID of the component.</param>
		public void GetClassID(out Guid classid)
		{
			classid = new System.Guid("FA7F9C55-6E8E-4855-8DAC-FA1BC8A499E2");
		}
        
		/// <summary>
		/// Not implemented.
		/// </summary>
		public void InitNew()
		{
		}
        
		public void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, Int32 errlog)
		{
			string val = (string)ReadPropertyBag(pb, "XsltFilePath");
			if (val != null) xsltPath = val;
		}
        
		/// <summary>
		/// Saves the current component configuration into the property bag.
		/// </summary>
		/// <param name="pb">Configuration property bag.</param>
		/// <param name="fClearDirty">Not used.</param>
		/// <param name="fSaveAllProperties">Not used.</param>
		public void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, Boolean fClearDirty, Boolean fSaveAllProperties)
		{
			object val = (object)xsltPath;
			WritePropertyBag(pb, "XsltFilePath", val);            
		}

		/// <summary>
		/// Reads property value from property bag.
		/// </summary>
		/// <param name="pb">Property bag.</param>
		/// <param name="propName">Name of property.</param>
		/// <returns>Value of the property.</returns>
		private static object ReadPropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName)
		{
			object val = null;
			try
			{
				pb.Read(propName,out val,0);
			}

			catch(System.ArgumentException)
			{
				return val;
			}
			catch(Exception ex)
			{
				throw new ApplicationException( ex.Message);
			}
			return val;
		}

		private static void WritePropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName, object val)
		{
			try
			{
				pb.Write(propName, ref val);
			}
			catch(Exception ex)
			{
				throw new ApplicationException( ex.Message);
			}
		}
     
		#endregion

		#region IComponentUI
		/// <summary>
		/// Component icon to use in BizTalk Editor.
		/// </summary>
		[Browsable(false)]
		public IntPtr Icon
		{
			get	{	return IntPtr.Zero;	}
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
		public IEnumerator Validate(object projectSystem)
		{
			if (projectSystem==null)
				throw new System.ArgumentNullException("No project system");

			IEnumerator enumerator = null;
			ArrayList   strList  = new ArrayList();

			try
			{
				GetValidXsltPath(xsltPath);
			}
			catch(Exception e)
			{
				strList.Add(e.Message);
				enumerator = strList.GetEnumerator();
			}

			return enumerator;
		}

		#endregion

	
	}
}
