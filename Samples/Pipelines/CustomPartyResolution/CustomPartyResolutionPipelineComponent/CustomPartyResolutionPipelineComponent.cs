//---------------------------------------------------------------------
// File: CustomPartyResolutionPipelineComponent.cs
// 
// Sample: CustomPartyResolution
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
using System.IO;
using Microsoft.Win32;
using System.Collections;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Component.Interop;

using Microsoft.Samples.BizTalk.PartyLookup;


namespace Microsoft.Samples.BizTalk.Pipelines.CustomPartyResolution_Pipeline_Component
{
	/// <summary>
	/// Uses evidence from inbound message to determine SourcePartyID
	/// </summary>
	/// <remarks>
	/// This sample assumes that inbound messages contain a promoted property called SourceOrg.
	/// Queries the BizTalk Managment database to do a party ailias lookup to determine
	/// SourcePartyID (SID).   Once SourcePartyID and PartyName have been determined, 
	/// set them into the context of the message.
	/// </remarks>

	[ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
	[ComponentCategory(CategoryTypes.CATID_Any)]
	[ComponentCategory(CategoryTypes.CATID_PartyResolver)]
	[System.Runtime.InteropServices.Guid("48BEC85A-20EE-40ad-BFD0-319B59A0DDBC")]
	public class PartyResolutionFromSourceOrg :
		IBaseComponent,
		Microsoft.BizTalk.Component.Interop.IComponent,
		Microsoft.BizTalk.Component.Interop.IPersistPropertyBag,
		IComponentUI
	{
		private IBaseMessage mBaseMessage;

		/// <summary>
		/// Data to prepend at the beginning of a stream.
		/// </summary>

		public PartyResolutionFromSourceOrg()
		{
		}

		#region IBaseComponent

		/// <summary>
		/// Name of the component.
		/// </summary>
		[Browsable(false)]
		public string Name
		{
			get { return "CustomPartyResolution Pipeline Component"; }
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
			get { return "Uses evidence from inbound message to determine SourcePartyID"; }
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
			IBaseMessage outmsg = inmsg;
			//replace the default Stream class with our custom PartyResolutionStream, which provides callback notification
			//when various stream events occur.
			//In this case, we're only interested in knowing when the entire stream has been read, so a callback function is only 
			//provided for the LastReadOccurred event, the other events are not being used.
			Stream ThisPartyResolutionStream = new Microsoft.Samples.BizTalk.Pipelines.CustomPartyResolution_Pipeline_Component.CustomPartyResolution_Pipeline_Component.PartyResolutionStream(inmsg.BodyPart.Data, null, null, new Microsoft.Samples.BizTalk.Pipelines.CustomPartyResolution_Pipeline_Component.CustomPartyResolution_Pipeline_Component.PartyResolutionStream.LastReadOccurred(EndOfStream));

			//Specify use of our PartyResoultionStream as the BodyPart.Data
			outmsg.BodyPart.Data = ThisPartyResolutionStream;
			mBaseMessage = outmsg;

			return outmsg;
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
		}

		/// <summary>
		/// Saves the current component configuration into the property bag.
		/// </summary>
		/// <param name="pb">Configuration property bag.</param>
		/// <param name="fClearDirty">Not used.</param>
		/// <param name="fSaveAllProperties">Not used.</param>
		public void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, Boolean fClearDirty, Boolean fSaveAllProperties)
		{
		}

		#endregion

		#region IComponentUI

		/// <summary>
		/// Component icon to use in BizTalk Editor.
		/// </summary>
		[Browsable(false)]
		public IntPtr Icon
		{
			get { return IntPtr.Zero; }
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

		/// <summary>
		/// this function is used as the callback function for the EndOfStream delegate provided in PartyResolutionStream class
		/// This function gets called after the entire stream of the message has been read, guaranteeing that all promoted properties
		/// from the message have been populated to the message context.
		/// </summary>
		internal void EndOfStream()
		{
			string SourceDomain = null;
			BizTalkParty party;

			//obtain the SourceOrg string from the message
			SourceDomain = (string)mBaseMessage.Context.Read("SourceDomain", "http://CustomPartyResolution_Pipeline.RoutingPropertySchema");

			if (SourceDomain != null)
			{
				PartyResolver pr = new PartyResolver();
				party = pr.GetPartyFromAlias("Domain", "DomainName", SourceDomain);

				mBaseMessage.Context.Write("SourcePartyID", "http://schemas.microsoft.com/BizTalk/2003/system-properties", party.SID);
				mBaseMessage.Context.Write("PartyName", "http://schemas.microsoft.com/BizTalk/2003/messagetracking-properties", party.Name);
			}
		}
	}
}
