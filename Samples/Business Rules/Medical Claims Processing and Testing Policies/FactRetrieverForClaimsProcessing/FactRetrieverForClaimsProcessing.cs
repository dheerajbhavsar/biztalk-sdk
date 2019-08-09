//---------------------------------------------------------------------
// File:	FactRetrieverForClaimsProcessing.cs
//
// Summary: 	Provides the long-term fact retriever that retrieves 
//              information from the PolicyValidity table created by this
//              sample
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
using System.Xml;
using System.Collections;
using Microsoft.RuleEngine;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Microsoft.Samples.BizTalk.MedicalClaimsProcessingandTestingPolicies.FactRetrieverForClaimsProcessing
{

    /// Fact Retriever class for Medical Claims Processing
    ///	This application implements the IFactRetriever interface and is used to provide the MedicalInsurancePolicy with the facts from the NorthWind -> PolicyValidity table

    public class DbFactRetriever : IFactRetriever, IFactRemover
    {
        public object UpdateFacts(RuleSetInfo rulesetInfo, Microsoft.RuleEngine.RuleEngine engine, object factsHandleIn)
        {
            object factsHandleOut;
            if (factsHandleIn == null)
            {
                // create the database connection since it doesn't exist
                SqlConnection con1 = new SqlConnection("Initial Catalog=Northwind;Data Source=(local);Integrated Security=SSPI;");
                DataConnection dc1 = new DataConnection("Northwind", "PolicyValidity", con1);
                factsHandleOut = dc1;
            }
            else
            {
                // reuse the database connection previously created
                factsHandleOut = factsHandleIn;
            }

            engine.Assert(factsHandleOut);
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
