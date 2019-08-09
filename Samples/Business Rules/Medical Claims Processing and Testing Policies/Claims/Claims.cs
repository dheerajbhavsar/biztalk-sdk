//---------------------------------------------------------------------
// File:	Claims.cs
// 
// Summary: 	Records the result of the claims processing, called 
//              in the THEN portion of a rule.
//
// Sample: 	Medical Claims Processing and Testing Policies
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

namespace Microsoft.Samples.BizTalk.MedicalClaimsProcessingandTestingPolicies.Claims
{
	
	public class ClaimResults
	{
		
		private string status;
		private string reason;

		public string Status
		{
			get { return status; }
			set { status = value; }
		}

		public string Reason
		{
			get { return reason; }
			set { reason = value; }
		}

		public void SendLead(string name, string id)
		{
			// method definition excluded for brevity
			System.Console.WriteLine("Sending to Renewal Department for Customer " + name + " with Policy # " + id);
		}
		
	}
}
