//---------------------------------------------------------------------
// File:   HelloWorld2.cs
// 
// Summary:    Contains methods to demonstrate creating a ruleset, 
//              saving a ruleset to a file, loading a ruleset from a 
//              file, deploy the ruleset to a shared SQL rule-store,
//              and then execute ruleset using a Policy object.
//
// Sample:    Business Rules Hello World 2
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
using System.Diagnostics;
using System.Threading;
using System.IO;
using Microsoft.RuleEngine;
using Microsoft.BizTalk.RuleEngineExtensions;
using System.Xml;
using Microsoft.Samples.BizTalk.BusinessRulesHelloWorld2.HelloWorld2Library;

namespace Microsoft.Samples.BizTalk.BusinessRulesHelloWorld2
{
	class REUsePolicy
	{
		const string RuleStoreFilename = "SampleRuleStore.xml";

		static RuleSet CreateRuleset(string rulesetName)
		{
			// create a simple rule
			// IF MySampleBusinessObject.MyValue != XMLdocument.ID
			// THEN MySampleBusinessObject.MySampleMethod1(5)
			
			//Creating the XML bindings on the SampleSchema XSD

			//Document Binding that binds to the schema name and specifies the selector
			XPathPair xp_root = new XPathPair("/*[local-name()='Root']", "Root");
			XMLDocumentBinding xdb = new XMLDocumentBinding("SampleSchema",xp_root);

			//DocumentField Bindings that bind to the fields in the schema that need to be used in rule defintion
			XPathPair xp_ID = new XPathPair("/*[local-name()='Root']/*[local-name()='ID']","ID");
			XMLDocumentFieldBinding xfb1 = new XMLDocumentFieldBinding(Type.GetType("System.Int32"),xp_ID,xdb);

			//Creating .NET class (property and member) bindings
			
			// Class Bindings to bind to the class defintions whose properties and memebers will be used in rule defintion
			ClassBinding cb = new ClassBinding(typeof(Microsoft.Samples.BizTalk.BusinessRulesHelloWorld2.HelloWorld2Library.HelloWorld2LibraryObject));

			// Member Bindings to bind to the properties and members in the MySampleBusinessObject class that need to be used in rule definition
			ClassMemberBinding myValue = new ClassMemberBinding("MyValue", cb);
			
			ArgumentCollection argList = new ArgumentCollection();
			argList.Add(new Constant(5));
			ClassMemberBinding method1 = new ClassMemberBinding("MySampleMethod", cb, argList);


			// create IF part
			LogicalExpression condition = new NotEqual(new UserFunction(myValue), new UserFunction(xfb1));

			// create then part
			ActionCollection actions = new ActionCollection();
			actions.Add(new UserFunction(method1));

			// create the rule
			Rule rule1 = new Rule("rule1", 0, condition, actions);

			//create the verion information and ruleset description

			DateTime time = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day);
			VersionInfo vinf1 = new VersionInfo("Sample RuleSet to demonstrate the use of the Policy object",time,"BizRules",1,0);

			// create the ruleset
			RuleSet rs1 = new RuleSet(rulesetName,vinf1);
			rs1.Rules.Add(rule1);
			return rs1;
		}

		static void SaveToFile(string filename, RuleSet rs)
		{
			// save it to a file
			RuleStore ruleStore = new FileRuleStore(filename);
			ruleStore.Save(rs);
		}

		static RuleSet LoadFromFile(string filename, string rulesetName)
		{
			// load the ruleset from a file
			RuleStore ruleStore = new FileRuleStore(filename);
			RuleSetInfoCollection rsInfo = ruleStore.GetRuleSets(rulesetName, RuleStore.Filter.Latest);
			if (rsInfo.Count != 1)
			{
				// oops ... error
				throw new ApplicationException();
			}
			RuleSet newRS = ruleStore.GetRuleSet(rsInfo[0]);
			return newRS;
		}

		static void DeployRuleSet(RuleSet ruleset)
		{
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

		static void ExecutePolicy()
		{
			
				// wait one minute for the deploy to propogate
				Console.WriteLine("Sleeping for 60 seconds (so that the deployed ruleset becomes effective) ...");
				Thread.Sleep(60000);

				// go get the policy
				Console.WriteLine("Grabbing the policy ...");
				Policy policy = new Policy("SampleRuleset");

				//create an instance of the XML object
				XmlDocument xd1 = new XmlDocument();
				xd1.Load(@"..\..\SampleDocumentInstance.xml");
				TypedXmlDocument doc1 = new TypedXmlDocument("SampleSchema",xd1);

				// create the array of short-term facts
				object[] shortTermFacts = new object[4];
				shortTermFacts[0] = doc1;
				shortTermFacts[1] = new Microsoft.Samples.BizTalk.BusinessRulesHelloWorld2.HelloWorld2Library.HelloWorld2LibraryObject(1);
				shortTermFacts[2] = new Microsoft.Samples.BizTalk.BusinessRulesHelloWorld2.HelloWorld2Library.HelloWorld2LibraryObject(2);
				shortTermFacts[3] = new Microsoft.Samples.BizTalk.BusinessRulesHelloWorld2.HelloWorld2Library.HelloWorld2LibraryObject(3);
			
				// now execute to see what happens
				Console.WriteLine("Executing the policy...");
				policy.Execute(shortTermFacts);
				Console.WriteLine("The major version of the policy was: " + policy.MajorRevision);
				Console.WriteLine("The minor version of the policy was: " + policy.MinorRevision);
				Console.WriteLine("Press the ENTER to continue after updating the policy...");
				Console.Read();
		}

		static void CleanUp()
		{
			// Clean up deployed ruleset
			Microsoft.BizTalk.RuleEngineExtensions.RuleSetDeploymentDriver dd = new Microsoft.BizTalk.RuleEngineExtensions.RuleSetDeploymentDriver();
			RuleStore sqlrs;
			bool bDeployed = false;
			
			sqlrs = dd.GetRuleStore();

			RuleSetInfoCollection rss = new RuleSetInfoCollection();
			rss = sqlrs.GetRuleSets("SampleRuleset", RuleStore.Filter.All);

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

			try
			{
				// Delete the SampleRuleStore.xml
				File.Delete("SampleRuleStore.xml");
			}
			catch
			{
				Console.Write("Cannot delete SampleRuleStore.xml as it is being edited currently. Please delete this file before you run the sample again.");
			}
		}

			
		
		static void Main(string[] args)
		{
			try
			{
				// create a ruleset
				Console.WriteLine("Creating a new ruleset ...");
				RuleSet rs = CreateRuleset("SampleRuleset");

				// save it to a file
				Console.WriteLine("Saving ruleset to " + RuleStoreFilename + " ...");
				SaveToFile(RuleStoreFilename, rs);

				// load the same ruleset from the file
				Console.WriteLine("Loading ruleset ...");
				RuleSet newRS = LoadFromFile(RuleStoreFilename, "SampleRuleset");

				// deploy the ruleset
				Console.WriteLine("Deploying the ruleset ...");
				DeployRuleSet(newRS);

				//Execute the Policy
				ExecutePolicy();

				ExecutePolicy();
			
				// Clean up deployed ruleset(s) from database
				CleanUp();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
