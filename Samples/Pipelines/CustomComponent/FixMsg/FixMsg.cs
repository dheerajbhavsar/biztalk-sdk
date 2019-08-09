//---------------------------------------------------------------------
// File: FixMsg.cs
// 
// Summary: A Sample of how to write a custom pipeline component.
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

namespace Microsoft.Samples.BizTalk.Pipelines.CustomComponent
{
    using System;
    using System.Resources;
    using System.Drawing;
    using System.Collections;
    using System.Reflection;
    using System.ComponentModel;
    using System.Text;
    using System.IO;
    using Microsoft.BizTalk.Message.Interop;
    using Microsoft.BizTalk.Component.Interop;


    /// <summary>
    /// Implements custom pipeline component to append and/or prepend data to a stream.
    /// </summary>
    /// <remarks>
    /// FixMag class implements pipeline component that can be used in receive and
    /// send BizTalk pipelines. The pipeline component gets a data stream, appends
    /// and/or prepends user specified data to it and outputs modified stream.
    ///</remarks>
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    [ComponentCategory(CategoryTypes.CATID_Validate)]
    [System.Runtime.InteropServices.Guid("48BEC85A-20EE-40ad-BFD0-319B59A0DDBC")]
    public class FixMsg :
	BaseCustomTypeDescriptor,
        IBaseComponent, 
        Microsoft.BizTalk.Component.Interop.IComponent,
        Microsoft.BizTalk.Component.Interop.IPersistPropertyBag,
        IComponentUI
    {
		
        private string prependData  = null;
        private string appendData   = null;

	static	ResourceManager resManager = new ResourceManager("Microsoft.Samples.BizTalk.Pipelines.CustomComponent.FixMsg", Assembly.GetExecutingAssembly());

	/// <summary>
	/// Constructor initializes base class to allow custom names and description for component properies
	/// </summary>
	public FixMsg():
		base(resManager)
	{
	}
        /// <summary>
        /// Data to prepend at the beginning of a stream.
        /// </summary>	
	[
	FixMsgPropertyName("PropPrependData"),
	FixMsgDescription("DescrPrependData")
	]
        public string PrependData
        {
            get {   return prependData; }
            set {   prependData = value;}
        }
        /// <summary>
        /// Data to append at the end of stream.
        /// </summary>
        [
	FixMsgPropertyName("PropAppendData"),
	FixMsgDescription("DescrAppendData")
	]
        public string AppendData
        {
            get {   return appendData;  }
            set {   appendData = value; }
        }

        /// <summary>
        /// Converts a string to its byte representation.
        /// </summary>
        /// <param name="str">String to be converted to byte representation.</param>
        /// <returns>Array of bytes that represents the string.</returns>
        private byte[] ConvertToBytes(string str)
        {
            byte[] data = null;

            if (str != null)
                data = UTF8Encoding.UTF8.GetBytes(str);

            return data;
        }

        #region IBaseComponent
        
        /// <summary>
        /// Name of the component.
        /// </summary>
        [Browsable(false)]
        public string Name
        {
            get {   return "FixMsg Component";  }
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
            get {   return "FixMsg Pipeline Component"; }
        }
    
        #endregion
        
        #region IComponent

        /// <summary>
        /// Implements IComponent.Execute method.
        /// </summary>
        /// <param name="pc">Pipeline context</param>
        /// <param name="inmsg">Input message.</param>
        /// <returns>Processed input message with appended or prepended data.</returns>
        /// <remarks>
        /// IComponent.Execute method is used to initiate
        /// the processing of the message in pipeline component.
        /// </remarks>
        public IBaseMessage Execute(IPipelineContext pc, IBaseMessage inmsg)
        {
            IBaseMessagePart bodyPart = inmsg.BodyPart;
            if (bodyPart!=null)
            {
                byte[] prependByteData  = ConvertToBytes(prependData);
                byte[] appendByteData   = ConvertToBytes(appendData);
		Stream originalStrm		= bodyPart.GetOriginalDataStream();
		Stream strm = null;

		if (originalStrm != null)
		{
			strm             = new FixMsgStream(originalStrm, prependByteData, appendByteData, resManager);
			bodyPart.Data    = strm;
			pc.ResourceTracker.AddResource( strm );
		}
            }
            
            return inmsg;
        }
        #endregion

        #region IPersistPropertyBag
    
        /// <summary>
        /// Gets class ID of component for usage from unmanaged code.
        /// </summary>
        /// <param name="classid">Class ID of the component.</param>
        public void GetClassID(out Guid classid)
        {
            classid = new System.Guid("48BEC85A-20EE-40ad-BFD0-319B59A0DDBC");
        }
        
        /// <summary>
        /// Not implemented.
        /// </summary>
        public void InitNew()
        {
        }
        
        /// <summary>
        /// Loads configuration property for component.
        /// </summary>
        /// <param name="pb">Configuration property bag.</param>
        /// <param name="errlog">Error status (not used in this code).</param>
        public void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, Int32 errlog)
        {
            string val = (string)ReadPropertyBag(pb, "AppendData");
            if (val != null) appendData = val;

            val = (string)ReadPropertyBag(pb, "PrependData");
            if (val != null) prependData = val;
        }
        
        /// <summary>
        /// Saves the current component configuration into the property bag.
        /// </summary>
        /// <param name="pb">Configuration property bag.</param>
        /// <param name="fClearDirty">Not used.</param>
        /// <param name="fSaveAllProperties">Not used.</param>
        public void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, Boolean fClearDirty, Boolean fSaveAllProperties)
        {
            object val = (object)appendData;
            WritePropertyBag(pb, "AppendData", val);
            
            val = (object)prependData;
            WritePropertyBag(pb, "PrependData", val);
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

            catch(ArgumentException)
            {
                return val;
            }
            catch(Exception ex)
            {
                throw new ApplicationException( ex.Message);
            }
            return val;
        }

        /// <summary>
        /// Writes property values into a property bag.
        /// </summary>
        /// <param name="pb">Property bag.</param>
        /// <param name="propName">Name of property.</param>
        /// <param name="val">Value of property.</param>
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
		get
		{
			return ((Bitmap)resManager.GetObject("FixMsgBitmap")).GetHicon();
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
		IEnumerator enumerator = null;
		ArrayList   strList  = new ArrayList();

		// Validate prepend data property
		if ((prependData != null) &&
		(prependData.Length >= 64))
		{
			strList.Add(resManager.GetString("ErrorPrependDataTooLong"));
		}

		// validate append data property
		if ((appendData != null) &&
		(appendData.Length >= 64))
		{
			strList.Add(resManager.GetString("ErrorAppendDataTooLong"));
		}

		if (strList.Count > 0) 
		{
			enumerator = strList.GetEnumerator();
		}

		return enumerator;
        }

        #endregion
    }
}
