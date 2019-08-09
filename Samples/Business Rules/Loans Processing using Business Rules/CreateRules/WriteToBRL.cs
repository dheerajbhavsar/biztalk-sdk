//---------------------------------------------------------------------
// File:	WriteToBRL.cs
// 
// Summary: 	Programmatically build rule sets for use by sample.
//
// Sample: 	Loan Processing Using Business Rules
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
using Microsoft.BizTalk.RuleEngineExtensions;
using System.IO;


namespace Microsoft.Samples.BizTalk.LoansProcessingUsingBusinessRules.CreateLoanProcessingPolicy

{
	
	/// Produces BRL Policy file for Loans Processing scenario
	
	class WriteToBRL
	{
		
		public void CreatePolicy() 
		{
		
			//Creating XML document bindings on the Case XSD schema

			//Document Binding that binds to the schema name and specifies the selector

			XPathPair xp_root = new XPathPair("/*[local-name()='Root' and namespace-uri()='http://LoansProcessor.Case']", "Root");
			XMLDocumentBinding xmlCase = new XMLDocumentBinding("Microsoft.Samples.BizTalk.LoansProcessor.Case",xp_root);
			
			//DocumentField Bindings that bind to the fields int the schema that are used in the defintion of "Income Rule" and the associated negation rule (the ELSE part)

			XPathPair xp_basicSalary = new XPathPair("/*[local-name()='Root' and namespace-uri()='http://LoansProcessor.Case']/*[local-name()='Income']/*[local-name()='BasicSalary']","Income/BasicSalary");
			XMLDocumentFieldBinding xmlCase_basicSalary = new XMLDocumentFieldBinding((Type.GetType("System.Int32")),xp_basicSalary,xmlCase);
			
			XPathPair xp_otherIncome = new XPathPair("/*[local-name()='Root' and namespace-uri()='http://LoansProcessor.Case']/*[local-name()='Income']/*[local-name()='OtherIncome']","Income/OtherIncome");
			XMLDocumentFieldBinding xmlCase_otherIncome = new XMLDocumentFieldBinding(Type.GetType("System.Int32"),xp_otherIncome,xmlCase);
			
			Constant c1 = new Constant("Income status is valid");

			XPathPair xp_incomeStatus = new XPathPair("/*[local-name()='Root' and namespace-uri()='http://LoansProcessor.Case']/*[local-name()='IncomeStatus']","IncomeStatus");
			XMLDocumentFieldBinding xmlCase_incomeStatus = new XMLDocumentFieldBinding(Type.GetType("System.String"),xp_incomeStatus,xmlCase,c1);

			Constant c1_1 = new Constant("Income status is not valid");
			XMLDocumentFieldBinding xmlCase_incomeStatusNegation = new XMLDocumentFieldBinding(Type.GetType("System.String"),xp_incomeStatus,xmlCase,c1_1);
			
			// Wrapping the XML bindings as User Functions

			UserFunction uf_basicSalary = new UserFunction(xmlCase_basicSalary);
			UserFunction uf_otherIncome = new UserFunction(xmlCase_otherIncome);
			UserFunction uf_incomeStatus = new UserFunction(xmlCase_incomeStatus);
			UserFunction uf_incomeStatusNegation = new UserFunction(xmlCase_incomeStatusNegation);
								
			// Condition 1: IF Basic Salary and Other Income > 0

			Constant zero = new Constant(0);
			GreaterThan ge1 = new GreaterThan(uf_basicSalary,zero);
			GreaterThan ge2 = new GreaterThan(uf_otherIncome,zero);
						
			LogicalExpressionCollection andList4 = new LogicalExpressionCollection();
			andList4.Add(ge1);
			andList4.Add(ge2);
			LogicalAnd la4 = new LogicalAnd(andList4);
			
			//Action 1 - THEN set Income Status in the incoming Case document to be Valid 
 
			ActionCollection actionList1 = new ActionCollection();
			actionList1.Add(uf_incomeStatus);
					
			/*  Name: Income Rule
				
				Description:			 
				IF Basic Salary and Other Income > 0
				THEN set Income Status in the incoming Case document to be Valid
				
			*/

			Microsoft.RuleEngine.Rule r1 = new Microsoft.RuleEngine.Rule("Income Status Rule",la4,actionList1);

			//Negation of Income Rule

			LogicalNot ln1 = new LogicalNot(la4);
			
			ActionCollection actionList1_1 = new ActionCollection();
			actionList1_1.Add(uf_incomeStatusNegation);

			Microsoft.RuleEngine.Rule r1_1 = new Microsoft.RuleEngine.Rule("Negation of Income Status Rule",ln1,actionList1_1);
							   	
			//Creating DB bindings on the NorthWind DB -> CustInfo table
			
			// DataRow Binding to bind to the Data Table, and provide the Data Set name (defaulting here to the DB name)

			DataConnectionBinding drb_CustInfo = new DataConnectionBinding("CustInfo","Northwind");

			// Column bindings to bind to the columns in the Data Table that need to be used in the Commitments Status Rule

			DatabaseColumnBinding col_ID = new DatabaseColumnBinding(Type.GetType("System.String"),"ID",drb_CustInfo);
			DatabaseColumnBinding col_creditCardBalance = new DatabaseColumnBinding(Type.GetType("System.Int32"),"CreditCardBalance",drb_CustInfo);
			
			// Creating the XML Document Field Bindings required in rule defintion

			XPathPair xp_ID = new XPathPair("/*[local-name()='Root' and namespace-uri()='http://LoansProcessor.Case']/*[local-name()='ID']","ID");
			XMLDocumentFieldBinding xmlCase_ID = new XMLDocumentFieldBinding(Type.GetType("System.String"),xp_ID,xmlCase);

			Constant c3 = new Constant("Compute Commitments");

			XPathPair xp_commitmentsStatus = new XPathPair("/*[local-name()='Root' and namespace-uri()='http://LoansProcessor.Case']/*[local-name()='CommitmentsStatus']","CommitmentsStatus");
			XMLDocumentFieldBinding xmlCase_commitmentsStatus = new XMLDocumentFieldBinding(Type.GetType("System.String"),xp_commitmentsStatus,xmlCase,c3);
			
			// Wrapping the DB and XML bindings as User Functions

			UserFunction uf_xmlID = new UserFunction(xmlCase_ID);
			UserFunction uf_commitmentsStatus = new UserFunction(xmlCase_commitmentsStatus);
			UserFunction uf_dbID = new UserFunction(col_ID);
			UserFunction uf_balance = new UserFunction(col_creditCardBalance);

			//Condition 2 : IF the ID in the XML document is equal to the ID in the DB record, and the Credit Card Balance for that record is > 500
			
			Equal eq2 = new Equal(uf_dbID,uf_xmlID);
            Constant c2 = new Constant(500);
			GreaterThan greaterThan = new GreaterThan(uf_balance,c2);
			
			LogicalExpressionCollection andList2 = new LogicalExpressionCollection();
			andList2.Add(eq2);
			andList2.Add(greaterThan);
			LogicalAnd la2 = new LogicalAnd(andList2);

			//Action 2 Set the Commitments Status field to "Compute Commitments"

			ActionCollection actionList2 = new ActionCollection();
			actionList2.Add(uf_commitmentsStatus);
			
			/* Rule 2 - 
							Name: Commitments Rule
							 
							 IF the ID in the Document is = to the ID column in the DB
							 
							 AND
							 
							 IF Credit Card Balance > 500
							   
							 Set Commitments Status in the XML document to be equal to "Compute Commitments"
			*/

			Microsoft.RuleEngine.Rule r2 = new Microsoft.RuleEngine.Rule("Commitments Status Rule",la2,actionList2);

			//Negation of Commitments Status Rule (the ELSE part)
			
			LogicalNot ln2 = new LogicalNot(greaterThan);

			LogicalExpressionCollection andList2negation = new LogicalExpressionCollection();
			andList2negation.Add(eq2);
			andList2negation.Add(ln2);
			LogicalAnd la2negation = new LogicalAnd(andList2negation);

			// Creating the XML Document Field Bindings required in rule defintion

			Constant ignore_string = new Constant("Ignore Commitments");
			XMLDocumentFieldBinding xmlCase_commitmentsStatusNegation = new XMLDocumentFieldBinding(Type.GetType("System.String"),xp_commitmentsStatus,xmlCase,ignore_string);
			UserFunction uf_commitmentsStatusNegation = new UserFunction(xmlCase_commitmentsStatusNegation);

			ActionCollection actionList2_1 = new ActionCollection();
			actionList2_1.Add(uf_commitmentsStatusNegation);
			
			Microsoft.RuleEngine.Rule r2_1 = new Microsoft.RuleEngine.Rule("Negation of Commitments Rule",la2negation,actionList2_1);
			
			// Creating the XML Document Field Bindings required in the Employment Status rule

			XPathPair xp_employmentTime = new XPathPair("/*[local-name()='Root' and namespace-uri()='http://LoansProcessor.Case']/*[local-name()='EmploymentType']/*[local-name()='TimeInMonths']","EmploymentType/TimeInMonths");
			XMLDocumentFieldBinding xmlCase_employmentTime = new XMLDocumentFieldBinding(Type.GetType("System.Int32"),xp_employmentTime,xmlCase);

			UserFunction uf_employmentTime = new UserFunction(xmlCase_employmentTime);
			Constant action3 = new Constant("Employment Status is valid");

			XPathPair xp_employmentStatus = new XPathPair("/*[local-name()='Root' and namespace-uri()='http://LoansProcessor.Case']/*[local-name()='EmploymentStatus']","EmploymentStatus");
			XMLDocumentFieldBinding xmlCase_employmentStatus= new XMLDocumentFieldBinding(Type.GetType("System.String"),xp_employmentStatus,xmlCase,action3);

			UserFunction uf_employmentStatus = new UserFunction(xmlCase_employmentStatus);

			// Condition 3: IF Employment Time > 18 months

			Constant lengthOfEmployment = new Constant(18);
			GreaterThanEqual greaterThanEqual1 = new GreaterThanEqual(uf_employmentTime,lengthOfEmployment);
								
			//Action 3 - THEN set Employment Status in the incoming Case document to be Valid 
 
			ActionCollection actionList3 = new ActionCollection();
			actionList3.Add(uf_employmentStatus);
					
			/*  Name: Employment Status Rule
				
				Description:			 
				IF Time of Employment > 18 months 
				THEN set Employment Status in the incoming Case document to be Valid
				
			*/

			Microsoft.RuleEngine.Rule r3 = new Microsoft.RuleEngine.Rule("Employment Status Rule",greaterThanEqual1,actionList3);
			
			// Negation of EmploymentStatus Rule (the ELSE part)

			LogicalNot ln3 = new LogicalNot(greaterThanEqual1);
			
			Constant empStatus_invalid = new Constant("Employment Status is Invalid");
			XMLDocumentFieldBinding xmlCase_employmentStatusInvalid = new XMLDocumentFieldBinding(Type.GetType("System.String"),xp_employmentStatus,xmlCase,empStatus_invalid);
			UserFunction uf_employmentStatusInvalid = new UserFunction(xmlCase_employmentStatusInvalid);

			ActionCollection actionList3_1 = new ActionCollection();
			actionList3_1.Add(uf_employmentStatusInvalid);
			
			Microsoft.RuleEngine.Rule r3_1 = new Microsoft.RuleEngine.Rule("Negation of Employment Status Rule",ln3,actionList3_1);
			
			// Creating the XML Document Field Bindings required in the Residence Status rule

			XPathPair xp_timeinResidence = new XPathPair("/*[local-name()='Root' and namespace-uri()='http://LoansProcessor.Case']/*[local-name()='PlaceOfResidence']/*[local-name()='TimeInMonths']","PlaceOfResidence/TimeInMonths");
			XMLDocumentFieldBinding xmlCase_timeinResidence = new XMLDocumentFieldBinding(Type.GetType("System.Int32"),xp_timeinResidence,xmlCase);
            
			UserFunction uf_timeinResidence = new UserFunction(xmlCase_timeinResidence);
			Constant lengthOfResidence = new Constant(3);
			
			Constant residencyStatus_valid = new Constant("Residency Status is valid");

			XPathPair xp_residencyStatus = new XPathPair("/*[local-name()='Root' and namespace-uri()='http://LoansProcessor.Case']/*[local-name()='ResidencyStatus']","ResidencyStatus");
			XMLDocumentFieldBinding xmlCase_residencyStatus = new XMLDocumentFieldBinding(Type.GetType("System.String"),xp_residencyStatus,xmlCase,residencyStatus_valid);
			UserFunction uf_residencyStatus = new UserFunction(xmlCase_residencyStatus);
			
			// Condition 4: IF Time of residence >= 3 years

			GreaterThanEqual greaterThanEqual2 = new GreaterThanEqual(uf_timeinResidence,lengthOfResidence);			
				
			//Action 3 - THEN set Resdiency Status in the incoming Case document to be Valid 
		
			ActionCollection actionList4 = new ActionCollection();
			actionList4.Add( uf_residencyStatus);
					
			/*  Name: Residency Status Rule
				
				Description:			 
				IF Time of Residency > 3 years 
				THEN set Residency Status in the incoming Case document to be Valid
				
			*/

			Microsoft.RuleEngine.Rule r4 = new Microsoft.RuleEngine.Rule("Residency Status Rule",greaterThanEqual2,actionList4);
			
			// Negation of Residency Status Rule

			LogicalNot ln4 = new LogicalNot(greaterThanEqual2);
			
			Constant residencyStatus_Invalid = new Constant("Residency Status is Invalid");
			XMLDocumentFieldBinding xmlCase_residencyStatusInvalid = new XMLDocumentFieldBinding(Type.GetType("System.String"),xp_residencyStatus,xmlCase,residencyStatus_Invalid);
			UserFunction uf_residencyStatusInvalid = new UserFunction(xmlCase_residencyStatusInvalid);

			ActionCollection actionList4_1 = new ActionCollection();
			actionList4_1.Add(uf_residencyStatusInvalid);
			
			Microsoft.RuleEngine.Rule r4_1 = new Microsoft.RuleEngine.Rule("Negation of Residency Status Rule",ln4,actionList4_1);

			// Creation of the rule-set (policy) and saving it into the file rulestore

			// Creating configuration information (date, version, description, creator, policy name, and fact retriever used to retrieve the required datasets at runtime)

			DateTime time = new DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, System.DateTime.Today.Day);
			VersionInfo vinf1 = new VersionInfo("Rules to process Loan applications",time,"BizRules",1,0);
			RuleSet rs1 = new RuleSet("LoanProcessing",vinf1);
			RuleSetExecutionConfiguration rec1 = new RuleSetExecutionConfiguration();

			Type factRetriever = typeof(myFactRetriever.DbFactRetriever);
			RuleEngineComponentConfiguration recc1 = new RuleEngineComponentConfiguration(factRetriever.Assembly.FullName, factRetriever.FullName);
			rec1.FactRetriever = recc1;
			rs1.ExecutionConfiguration = rec1;

			// Adding rules to the rule-set

			rs1.Rules.Add(r1);
			rs1.Rules.Add(r1_1);
			rs1.Rules.Add(r2);
			rs1.Rules.Add(r2_1);
			rs1.Rules.Add(r3);
			rs1.Rules.Add(r3_1);
			rs1.Rules.Add(r4);
			rs1.Rules.Add(r4_1);

			// Saving the rule-set (policy) to an XML file in the current directory

			FileRuleStore frs1 = new FileRuleStore("LoanProcessing.xml");
			frs1.Add(rs1);

			// Publish and deploy the ruleset to rulestore
			DeployRuleSet(rs1);
		}

		static void DeployRuleSet(RuleSet ruleset)
		{
			// Clean up the existing ruleset(s) in the rule store first 
			// before publishing and deploying veriosn 1.0
			CleanUp(ruleset.Name);

			// now we have the ruleset, need to copy it to the database
			Microsoft.BizTalk.RuleEngineExtensions.RuleSetDeploymentDriver dd = new Microsoft.BizTalk.RuleEngineExtensions.RuleSetDeploymentDriver();
			RuleStore sqlrs;
			sqlrs = dd.GetRuleStore();
			// add the RuleSet to the database (and publish it)
			try 
			{
				sqlrs.Add(ruleset, true);
			}
			catch (RuleStoreRuleSetAlreadyPublishedException)
			{
				Console.WriteLine("Warning: Ruleset \"" + ruleset.Name + "\" is already published");
			}
			catch 
			{
				throw;
			}
			// now deploy the ruleset
			try 
			{
				dd.Deploy(new RuleSetInfo(ruleset.Name, 
					ruleset.CurrentVersion.MajorRevision, ruleset.CurrentVersion.MinorRevision));
			}
			catch (RuleEngineDeploymentAlreadyDeployedException) 
			{
				Console.WriteLine("Warning: Ruleset \"" + ruleset.Name + "\" is already deployed");
			}
			catch
			{
				throw;
			}
		}

		static void CleanUp(string RSName)
		{
			// Clean up deployed ruleset
			Microsoft.BizTalk.RuleEngineExtensions.RuleSetDeploymentDriver dd = new Microsoft.BizTalk.RuleEngineExtensions.RuleSetDeploymentDriver();
			RuleStore sqlrs;
			bool bDeployed = false;
			
			sqlrs = dd.GetRuleStore();

			RuleSetInfoCollection rss = new RuleSetInfoCollection();
			rss = sqlrs.GetRuleSets(RSName, RuleStore.Filter.Latest);

			int i = 0;
			if (rss.Count > 0 )
			{
				for (i=0; i < rss.Count; i++)
				{
					bDeployed = dd.IsRuleSetDeployed(rss[i]);
					if (bDeployed)
					{
						dd.Undeploy(rss[i]);
					}
					sqlrs.Remove(rss[i]);
				}
			}
		} // End of Cleanup()


		public void Execute() 
		{
			
			//Using Policy Tester
			 
			// Retrieve Rule-Set from Policy file
			RuleStore ruleStore = new FileRuleStore("LoanProcessing.xml");
			RuleSetInfoCollection rsInfo = ruleStore.GetRuleSets("LoanProcessing", RuleStore.Filter.Latest);
			if (rsInfo.Count != 1)
			{
				// oops ... error
				throw new ApplicationException();
			}
			
			RuleSet ruleset = ruleStore.GetRuleSet(rsInfo[0]);
			
			// Create an instance of the DebugTrackingInterceptor
			DebugTrackingInterceptor dti = new DebugTrackingInterceptor("outputtraceforLoanPocessing.txt");

			//Create an instance of the Policy Tester class
			PolicyTester policyTester = new PolicyTester(ruleset);

			XmlDocument xd1 = new XmlDocument();
			xd1.Load("sampleLoan.xml");

            TypedXmlDocument doc1 = new TypedXmlDocument("Microsoft.Samples.BizTalk.LoansProcessor.Case", xd1);

			//Execute Policy Tester
			try
			{
				policyTester.Execute(doc1, dti);
			}
			catch (Exception e) 
			{
				System.Console.WriteLine(e.ToString());
			}
			
			FileInfo f = new FileInfo("LoanPocessingResults.xml");
			StreamWriter w = f.CreateText();
			w.Write(doc1.Document.OuterXml);
			w.Close();


		}
	
			
		[STAThread]
		static void Main(string[] args)
		{
			WriteToBRL createRules = new WriteToBRL();
			createRules.CreatePolicy();
            //createRules.Execute();
            //CleanUp("LoanProcessing");
		}
	}
}
