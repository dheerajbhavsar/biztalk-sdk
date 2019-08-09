//---------------------------------------------------------------------
// File:	FactRetrieverForLoansProcessing.cs
// 
// Summary: 	Used to retrieve information from the CustInfo table 
//              added earlier to the Northwind sample SQL database.
//
// Sample: 	Loan Processing using Business Rules
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
using Microsoft.RuleEngine;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Microsoft.Samples.BizTalk.LoansProcessingUsingBusinessRules.myFactRetriever
{
	
	/// Fact Retriever class for LoanProcessing Policy

    public class DbFactRetriever : IFactRetriever, IFactRemover
	{
		
		public object UpdateFacts(RuleSetInfo rulesetInfo, Microsoft.RuleEngine.RuleEngine engine, object factsHandleIn)
		{
			object factsHandleOut;

			// The following logic asserts the required DB rows only once and always uses the the same values (cached) during the first retrieval in subsequent execution cycles
			if (factsHandleIn == null) 
			{
			
				SqlConnection con1 = new SqlConnection("Initial Catalog=Northwind;Data Source=(local);Integrated Security=SSPI;");
				DataConnection dc1 = new DataConnection("Northwind", "CustInfo", con1);
				engine.Assert(dc1);
				factsHandleOut = dc1;
			}

			else
				factsHandleOut = factsHandleIn;

			return factsHandleOut;
		}

        public object UpdateFactsAfterExecution(RuleSetInfo rulesetInfo, Microsoft.RuleEngine.RuleEngine engine, object factsHandleIn)
        {
            if (factsHandleIn != null)
            {
                // retract the DataConnection object to clean up any rows fetched during policy execution
                engine.Retract(factsHandleIn);
            }
            return factsHandleIn;
        }


	}
}
