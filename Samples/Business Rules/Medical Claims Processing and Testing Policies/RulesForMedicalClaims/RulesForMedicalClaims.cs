//---------------------------------------------------------------------
// File:	RulesForMedicalClaims.cs
// 
// Summary: 	Programmatically defines and stores the ruleset, 
//              constructs sample facts, and then executes the ruleset
//              using a PolicyTester object.
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

using Microsoft.RuleEngine;
using Microsoft.Samples.BizTalk.MedicalClaimsProcessingandTestingPolicies.Claims;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System;
using System.IO;
using System.Xml;

namespace Microsoft.Samples.BizTalk.MedicalClaimsProcessingandTestingPolicies.RulesForMedicalClaims
{
	
	class RulesCreator
	{
		
		// Method to create the rule-set or policy
		public void CreateRuleSet() 
		{
				
			//Creating XML document bindings on the Medical claims XSD schema

			//Document Binding that binds to the schema name and specifies the selector
			XMLDocumentBinding xmlClaim = new XMLDocumentBinding("MedicalClaims","/*[local-name()='Root' and namespace-uri()='http://BizTalk_Server_Project1.MedicalClaims']");
			
			//DocumentField Bindings that bind to the fields in the schema that need to be used in rule defintion

			XMLDocumentFieldBinding xmlClaim_Name = new XMLDocumentFieldBinding(Type.GetType("System.String"),"/*[local-name()='Root' and namespace-uri()='http://BizTalk_Server_Project1.MedicalClaims']/*[local-name()='Name']",xmlClaim);
			XMLDocumentFieldBinding xmlClaim_ID = new XMLDocumentFieldBinding(Type.GetType("System.String"),"/*[local-name()='Root' and namespace-uri()='http://BizTalk_Server_Project1.MedicalClaims']/*[local-name()='ID']",xmlClaim);
			XMLDocumentFieldBinding xmlClaim_Amount = new XMLDocumentFieldBinding(Type.GetType("System.Double"),"/*[local-name()='Root' and namespace-uri()='http://BizTalk_Server_Project1.MedicalClaims']/*[local-name()='Amount']",xmlClaim);
			XMLDocumentFieldBinding xmlClaim_Nights = new XMLDocumentFieldBinding(Type.GetType("System.Int32"),"/*[local-name()='Root' and namespace-uri()='http://BizTalk_Server_Project1.MedicalClaims']/*[local-name()='Nights']",xmlClaim);
			XMLDocumentFieldBinding xmlClaim_Date = new XMLDocumentFieldBinding(Type.GetType("System.DateTime"),"/*[local-name()='Root' and namespace-uri()='http://BizTalk_Server_Project1.MedicalClaims']/*[local-name()='Date']",xmlClaim);

			//Creating DB bindings on the NorthWind DB -> PolicyValidity table
			
			// DataRow Binding to bind to the Data Table, and provide the Data Set name (defaulting here to the DB name)

			DataConnectionBinding policyvalidityTable = new DataConnectionBinding("PolicyValidity","Northwind");
			
			// Column bindings to bind to the columns in the Data Table that need to be used in rule definition

			DatabaseColumnBinding policyvalidityTable_IDColumn = new DatabaseColumnBinding(Type.GetType("System.String"),"ID",policyvalidityTable);
			DatabaseColumnBinding policyvalidityTable_PolicyStatusColumn= new DatabaseColumnBinding(Type.GetType("System.String"),"PolicyStatus",policyvalidityTable);

			//Creating .NET class (property and member) bindings
			
			// Class Bindings to bind to the class defintions whose properties and members will be used in rule definition
			
			ClassBinding dateClass = new ClassBinding(typeof(TimeStamp));
			ClassBinding resultsClass = new ClassBinding(new Microsoft.Samples.BizTalk.MedicalClaimsProcessingandTestingPolicies.Claims.ClaimResults().GetType());
			

			// Member Bindings to bind to the properties and members in the TimeStamp class that need to be used in rule definition
			
			ClassMemberBinding today = new ClassMemberBinding("Value",dateClass);

			// Member bindings for ClaimResults.Status and ClaimResults.Reason properties

			ClassMemberBinding reason = new ClassMemberBinding("Reason",resultsClass);
			ClassMemberBinding status = new ClassMemberBinding("Status",resultsClass);
			
			
			// Member bindings for ClaimResults.Status and ClaimResults.Reason properties with different arguments

			// Creation of constant strings that are required as arguments for rule actions

			Constant c1 = new Constant(" REJECTED!");
			ArgumentCollection al1 = new ArgumentCollection();
			al1.Add(c1);

			Constant c2 = new Constant("Amount of claim has exceeded Policy limit");
			ArgumentCollection al2 = new ArgumentCollection();
			al2.Add(c2);

			Constant c3 = new Constant(" Claim Accepted!");
			ArgumentCollection al3 = new ArgumentCollection();
			al3.Add(c3);

			Constant c4 = new Constant(" Amount of Nights has exceeded Policy limit");
			ArgumentCollection al4 = new ArgumentCollection();
			al4.Add(c4);

			Constant c5 = new Constant(" Cannot submit claims for future dates!");
			ArgumentCollection al5 = new ArgumentCollection();
			al5.Add(c5);

			Constant c6 = new Constant("Policy ID is invalid");
			ArgumentCollection al6 = new ArgumentCollection();
			al6.Add(c6);

			ClassMemberBinding status_Rejected = new ClassMemberBinding("Status", resultsClass, al1);
			ClassMemberBinding reason_invalidAmount = new ClassMemberBinding("Reason", resultsClass, al2);
			ClassMemberBinding status_Accepted = new ClassMemberBinding("Status", resultsClass, al3);
			ClassMemberBinding reason_invalidNights = new ClassMemberBinding("Reason", resultsClass, al4);
			ClassMemberBinding reason_invalidDate = new ClassMemberBinding("Reason", resultsClass, al5);
			ClassMemberBinding reason_invalidID = new ClassMemberBinding("Reason", resultsClass, al6);
			
			
			// MemberBinding for the SendLead method in the ClaimResults class with the name and ID from the incoming XML document as arguments

			Term arg1 = new UserFunction (xmlClaim_Name);
			Term arg2 = new UserFunction (xmlClaim_ID);
			ArgumentCollection args1 = new ArgumentCollection();
			args1.Add(arg1);
			args1.Add(arg2);
			ClassMemberBinding sendLead = new ClassMemberBinding("SendLead", resultsClass, args1); 

			// Object reference binding to the ClaimResults object that is to be asserted back in to the initial fact base as part of the rule actions

			ObjectReference resultsObject = new ObjectReference(resultsClass);
            

			// Wrapping the XML bindings as User Functions 

			UserFunction uf_xmlName = new UserFunction(xmlClaim_Name);
			UserFunction uf_xmlID = new UserFunction(xmlClaim_ID);
			UserFunction uf_xmlAmount = new UserFunction(xmlClaim_Amount);
			UserFunction uf_xmlNights = new UserFunction(xmlClaim_Nights);
			UserFunction uf_xmlDate = new UserFunction(xmlClaim_Date);

			// Wrapping the DB bindings as User Functions 

			UserFunction uf_IDColumn = new UserFunction(policyvalidityTable_IDColumn);
			UserFunction uf_PolicyStatusColumn = new UserFunction(policyvalidityTable_PolicyStatusColumn);

			
			// Wrapping the .NET bindings as User Functions 

			UserFunction uf_today = new UserFunction(today);
			UserFunction uf_reason = new UserFunction(reason);
			UserFunction uf_status = new UserFunction(status);
			UserFunction uf_rejected = new UserFunction(status_Rejected);
			UserFunction uf_invalidAmount = new UserFunction(reason_invalidAmount);
			UserFunction uf_accepted = new UserFunction(status_Accepted);
			UserFunction uf_invalidNights = new UserFunction(reason_invalidNights);
			UserFunction uf_invalidDate = new UserFunction(reason_invalidDate);
			UserFunction uf_invalidID = new UserFunction(reason_invalidID);
			UserFunction uf_sendLead = new UserFunction(sendLead);
			

			// Rule 1: Amount Check (check to see if claim amount has exceeded the allowable limit)
					
			// Condition 1: IF the Amount in the XML document element is > 1000 AND IF the Claims.ClaimResults object has not been modified i.e if STATUS = null

			GreaterThan ge1 = new GreaterThan(uf_xmlAmount, new Constant(1000));
			Equal eq1 = new Equal(uf_status, new Constant(uf_status.Type, null as string));

			LogicalExpressionCollection conditionAnd1 = new LogicalExpressionCollection();
			conditionAnd1.Add(ge1);
			conditionAnd1.Add(eq1);

			LogicalAnd la1 = new LogicalAnd(conditionAnd1);
					
			//Action 1 - THEN Claims.ClaimResults.Status = "REJECTED" and Claims.ClaimResults.Reason = "Amount of claim has exceeded limit" && Assert this object back into working memory

 			ActionCollection actionList1 = new ActionCollection();
			actionList1.Add(uf_rejected);
			actionList1.Add(uf_invalidAmount);
			actionList1.Add(new Assert(resultsObject));

			// Amount Check Rule defintion
			Microsoft.RuleEngine.Rule r1 = new Microsoft.RuleEngine.Rule("Amount Check",la1, actionList1);

			// Rule 2: Nights Spent (check to see if no of nights has exceeded the allowable limit)
					
			// Condition 2: IF the number of nights in the XML document element is > 10 AND IF the Claims.ClaimResults object has not been modified i.e if STATUS = null

			GreaterThan ge2 = new GreaterThan(uf_xmlNights, new Constant(10));

			Equal eq2 = new Equal(uf_status,new Constant(uf_status.Type, null as string));

			LogicalExpressionCollection conditionAnd2 = new LogicalExpressionCollection();
			conditionAnd2.Add(ge2);
			conditionAnd2.Add(eq2);

			LogicalAnd la2 = new LogicalAnd(conditionAnd2);
					
			//Action 2 - THEN Claims.ClaimResults.Status = "REJECTED" and Claims.ClaimResults.Reason = "No of Nights has exceeded limit" && Assert this object back into working memory

			ActionCollection actionList2 = new ActionCollection();
			actionList2.Add(uf_rejected);
			actionList2.Add(uf_invalidNights);
			actionList2.Add(new Assert(resultsObject));

			//Nights Spent Rule Definition
			Microsoft.RuleEngine.Rule r2 = new Microsoft.RuleEngine.Rule("Nights Spent", la2, actionList2);
			
			// Rule 3: Date Validity (check to see if the date on the claim is greater than today's date)
					
			// Condition 3: IF date on the incoming XML claim is greater than Today than reject the claim AND IF the Claims.ClaimResults object has not been modified i.e if RESULT = null

			GreaterThan ge3 = new GreaterThan(uf_xmlDate,uf_today);

			Equal eq3 = new Equal(uf_status,new Constant(uf_status.Type, null as string));

			LogicalExpressionCollection conditionAnd3 = new LogicalExpressionCollection();
			conditionAnd3.Add(ge3);
			conditionAnd3.Add(eq3);

			LogicalAnd la3 = new LogicalAnd(conditionAnd3);
					
			//Action 3 - THEN Claims.ClaimResults.Status = "REJECTED" and Claims.ClaimResults.Reason = "Cannot submit claims in the future!" && Assert this object back into working memory

			ActionCollection actionList3 = new ActionCollection();
			actionList3.Add(uf_rejected);
			actionList3.Add(uf_invalidDate);
			actionList3.Add(new Assert(resultsObject));

			// Date Validity Rule defintion
			
			Microsoft.RuleEngine.Rule r3 = new Microsoft.RuleEngine.Rule("Date Validity Check", la3, actionList3);

			// Rule 4: Policy Validity (check to see if the policy ID is valid by inspecting the database)
					
			// Condition 4: IF the Policy is Invalid for the ID in the XML claim (where the validity record is stored in the DB) AND IF the Claims.ClaimResults object has not been modified i.e if RESULT = null

			Equal ceq1 = new Equal(uf_IDColumn,uf_xmlID);
			Constant invalid_string = new Constant("INVALID");
			Equal ceq2 = new Equal(uf_PolicyStatusColumn,invalid_string);
						
			LogicalExpressionCollection andList1 = new LogicalExpressionCollection();
			andList1.Add(ceq1);
			andList1.Add(ceq2);
			LogicalAnd cla1 = new LogicalAnd(andList1);

			Equal eq4 = new Equal(uf_status,new Constant(uf_status.Type, null as string));

			LogicalExpressionCollection conditionAnd4 = new LogicalExpressionCollection();
			conditionAnd4.Add(cla1);
			conditionAnd4.Add(eq4);

			LogicalAnd la4 = new LogicalAnd(conditionAnd4);

					
			//Action 4 - THEN Claims.ClaimResults.Status = "REJECTED" and Claims.ClaimResults.Reason = "Policy Invalid" && Assert this object back into working memory

			ActionCollection actionList4 = new ActionCollection();
			actionList4.Add(uf_rejected);
			actionList4.Add(uf_invalidID);
			actionList4.Add(new Assert(resultsObject));

			// Policy Validity Rule defintion

			Microsoft.RuleEngine.Rule r4 = new Microsoft.RuleEngine.Rule("Policy Validity Check", la4, actionList4);

			// Rule 5: (Send a Lead To Policy Department (by executing the SendLead Method) if the user's policy has expired and is invalid)
					
			// Condition 5: IF Claim.ClaimResults.Reason = "Policy invalid" 

			Constant Reason_String = new Constant("Policy ID is invalid");
			Equal eq5 = new Equal(uf_reason,Reason_String);

			//Action 5 - THEN Send a Lead to the Policy Department by invoking the function Claim.ClaimResults.SendLead wirh customer ID and Name

			ActionCollection actionList5 = new ActionCollection();
			actionList5.Add(uf_sendLead);
			
			Microsoft.RuleEngine.Rule r5 = new Microsoft.RuleEngine.Rule("Send Lead", eq5 ,actionList5);

			// Rule 6:  Set the Claim Status to be Valid if it the Status has not been set to Invalid
					
			// Condition 6: IF Claim.ClaimResults.Status = null 

			Equal eq6 = new Equal(uf_status, new Constant(uf_status.Type, null as string));

			//Action 6 - THEN Send a Set the Claim status to be Valid

			ActionCollection actionList6 = new ActionCollection();
			actionList6.Add(uf_accepted);
			
			//Claim Accepted Rule definition

			Microsoft.RuleEngine.Rule r6 = new Microsoft.RuleEngine.Rule("Claim Accepted", eq6, actionList6);

            // Since initially the condition is TRUE (the claim has not been rejected or accepted),
            // we need to make this rule a lower priority so that the rejection rules run first.
            // That way, if the claim is rejected, this rule will not execute.
            r6.Priority = -1;

			// Creation of the rule-set (policy) and saving it into the file rulestore

			// Creating configuration information (date, version, description, creator, policy name, and fact retriever used to retrieve the required datasets at runtime)

			DateTime time = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day);
			VersionInfo vinf1 = new VersionInfo("Rules to Process medical Insurance Claims",time,"BizRules",0,0);
			RuleSet rs1 = new RuleSet("MedicalInsurancePolicy",vinf1);
			RuleSetExecutionConfiguration rec1 = new RuleSetExecutionConfiguration();
			RuleEngineComponentConfiguration recc1= new RuleEngineComponentConfiguration("FactRetrieverForClaimsProcessing","Microsoft.Samples.BizTalk.MedicalClaimsProcessingandTestingPolicies.FactRetrieverForClaimsProcessing.DbFactRetriever");
			rec1.FactRetriever = recc1;
			rs1.ExecutionConfiguration = rec1;

			// Adding rules to the rule-set

			rs1.Rules.Add(r1);
			rs1.Rules.Add(r2);
			rs1.Rules.Add(r3);
			rs1.Rules.Add(r4);
			rs1.Rules.Add(r5);
			rs1.Rules.Add(r6);
			
			// Saving the rule-set (policy) to an XML file in the current directory

			FileRuleStore frs1 = new FileRuleStore("MedicalInsurancePolicy.xml");
			frs1.Add(rs1);
			
		}

		public void ExecutePolicy()
		{
			// Retrieve Rule-Set from the filestore

			RuleStore ruleStore = new FileRuleStore("MedicalInsurancePolicy.xml");
			
			//Grab the latest version of the rule-set

			RuleSetInfoCollection rsInfo = ruleStore.GetRuleSets("MedicalInsurancePolicy", RuleStore.Filter.Latest);
			if (rsInfo.Count != 1)
			{
				// oops ... error
				throw new ApplicationException();
			}
			
			RuleSet ruleset = ruleStore.GetRuleSet(rsInfo[0]);
			
			// Create an instance of the DebugTrackingInterceptor to provide an output trace
			DebugTrackingInterceptor dti = new DebugTrackingInterceptor("outputtrace.txt");

			//Create an instance of the Policy Tester class
			PolicyTester policyTester = new PolicyTester(ruleset);

			//Create the set of short term facts

			//XML facts
			
			XmlDocument xd1 = new XmlDocument();
			xd1.Load(@"..\..\sampleClaim.xml");
									
			TypedXmlDocument doc1 = new TypedXmlDocument("MedicalClaims",xd1);
			
			// .NET facts
			Microsoft.Samples.BizTalk.MedicalClaimsProcessingandTestingPolicies.Claims.ClaimResults results = new Microsoft.Samples.BizTalk.MedicalClaimsProcessingandTestingPolicies.Claims.ClaimResults();

			object[] shortTermFacts = new object[] { doc1, results, new TimeStamp(DateTime.Now) };

			//Execute Policy Tester
			try
			{
				policyTester.Execute(shortTermFacts, dti);
			}
			catch (Exception e) 
			{
				System.Console.WriteLine(e.ToString());
			}
			System.Console.WriteLine ("Status: " + results.Status);
			System.Console.WriteLine ("Reason: " + results.Reason);
							
		}

		static void CleanUp()
		{
			try
			{
				// Delete the SampleRuleStore.xml
				File.Delete("MedicalInsurancePolicy.xml");
			}
			catch
			{
				Console.Write("Cannot delete MedicalInsurancePolicy.xml as it is being edited currently. Please delete this file before you run the sample again.");
			}
		}
		
		
		static void Main(string[] args)
		{
			RulesCreator medicalInsurancePolicy = new RulesCreator();
			
			try
			{
				medicalInsurancePolicy.CreateRuleSet();
				medicalInsurancePolicy.ExecutePolicy();
				CleanUp();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
