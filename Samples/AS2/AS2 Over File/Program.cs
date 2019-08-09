//=============================================================================
// This file is part of the Microsoft BizTalk Accelerator for RosettaNet 3.0
//
// Copyright (c) Microsoft Corporation.  All rights reserved.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
// PURPOSE.
//=============================================================================

using System;
using System.Resources;
using System.Collections;
using System.Reflection;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Management;
using System.Xml;

using Microsoft.BizTalk.Internal;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Component;
using Microsoft.BizTalk.Component.Interop;


namespace Microsoft.BizTalk.Sdk.Components
{
    /// <summary>
    /// Summary description for MessageEditor.
    /// This Class helps to modify the Value of a particular node in a Part Message. The user needs to provide the correct XPath Query and the Value.
    /// Using this XPath Query, the part will be scanned through and the node value is modified.
    /// </summary>
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    [ComponentCategory(CategoryTypes.CATID_Validate)]
    [System.Runtime.InteropServices.Guid("86481497-0D29-4855-BF2E-303D04B9A55E")]
    public class AS2OverFile :
        BaseCustomTypeDescriptor,
        IBaseComponent,
        Microsoft.BizTalk.Component.Interop.IComponent,
        IComponentUI,
        Microsoft.BizTalk.Component.Interop.IPersistPropertyBag
    {
        #region IComponent Members

        public AS2OverFile()
            : base(null)
        {
        }

        /// <summary>
        /// This method Executes the pipeline component
        /// </summary>
        /// <param name="pContext">pipelinecontext Interface</param>
        /// <param name="pInMsg">the incoming XLang Message on which we have to operate</param>
        /// <returns></returns>
        public IBaseMessage Execute(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            IPipelineContext pipelineContext;
            IBaseMessage baseMessage;

            pipelineContext = pContext;
            baseMessage = pInMsg;

            StreamReader sr = new StreamReader(baseMessage.BodyPart.Data);
            string line = string.Empty;
            string header = string.Empty;
            while (!string.IsNullOrEmpty((line = sr.ReadLine())))
            {
                if (!string.IsNullOrEmpty(header))
                    header += "\r\n";
                header += line;
            }
            sr.BaseStream.Seek((long)(header.Length + 4), SeekOrigin.Begin);

            baseMessage.BodyPart.Data = sr.BaseStream;
            baseMessage.Context.Write("InboundHttpHeaders", "http://schemas.microsoft.com/BizTalk/2003/http-properties", header);
            baseMessage.Context.Promote("InboundTransportType", "http://schemas.microsoft.com/BizTalk/2003/system-properties", "HTTP");

            return baseMessage;
        }

        #endregion

        #region IComponentUI Members

        public IEnumerator Validate(object projectSystem)
        {
            // TODO:  Add MessageEditor.Validate implementation
            return null;
        }

        [Browsable(false)]
        public System.IntPtr Icon
        {
            get
            {
                return IntPtr.Zero;
            }
        }

        #endregion

        #region IBaseComponent Members

        /// <summary>
        /// Name of the component.
        /// </summary>
        [Browsable(false)]
        public string Name
        {
            get { return "AS2 Over File Emulator"; }
        }

        /// <summary>
        /// Version of the component.
        /// </summary>
        [Browsable(false)]
        public string Version
        {
            get { return "1.0"; }
        }

        /// <summary>
        /// Description of the component.
        /// </summary>
        [Browsable(false)]
        public string Description
        {
            get { return "Allows the processing of an AS2 message submitted through a File receive location."; }
        }

        #endregion

        #region IPersistPropertyBag Members

        /// <summary>
        /// Gets class ID of component for usage from unmanaged code.
        /// </summary>
        /// <param name="classid">Class ID of the component.</param>
        public void GetClassID(out Guid classid)
        {
            classid = new System.Guid("86481497-0D29-4855-BF2E-303D04B9A55E}");
        }

        public void InitNew()
        {
        }

        public void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, Int32 errlog)
        { }

        public void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, Boolean fClearDirty, Boolean fSaveAllProperties)
        { }
        #endregion
    }
}
