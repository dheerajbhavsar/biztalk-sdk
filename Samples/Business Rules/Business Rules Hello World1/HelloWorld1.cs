//---------------------------------------------------------------------
// File:	HelloWorld1.cs
// 
// Summary: 	Contains methods to demonstrate creating a ruleset, 
//              saving a ruleset to a file, and loading a ruleset from 
//              a file, and surrounding code that calls these methods
//              and then executes the created ruleset
//
// Sample: 	Business Rules Hello World 1
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
using System.IO;
using Microsoft.RuleEngine;
using System.Xml;
using Microsoft.Samples.BizTalk.BusinessRulesHelloWorld1.MySampleLibrary;

namespace Microsoft.Samples.BizTalk.BusinessRulesHelloWorld1
{
	class RECreateAndSave
	{
		const string RuleStoreFilename = "SampleRuleStore.xml";

			static RuleSet CreateRuleset(string rulesetName)
			{
				// create a simple rule
				// IF MySampleBusinessObject.MyValue != XMLdocument.ID
				// THEN MySampleBusinessObject.MySampleMethod1(5)
			
				//Creating the XML bindings on the SampleSchema XSD

				//Document Binding that binds to the schema name and specifies the selector
				XMLDocumentBinding xdb = new XMLDocumentBinding("SampleSchema","/*[local-name()='Root']");

				//DocumentField Bindings that bind to the fields in the schema that need to be used in rule defintion
				XMLDocumentFieldBinding xfb1 = new XMLDocumentFieldBinding(Type.GetType("System.Int32"),"ID",xdb);

				//Creating .NET class (property and member) bindings
			
				// Class Bindings to bind to the class defintions whose properties and memebers will be used in rule defintion
				ClassBinding cb = new ClassBinding(typeof(Microsoft.Samples.BizTalk.BusinessRulesHelloWorld1.MySampleLibrary.MySampleBusinessObject));

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

		static void CleanUp()
		{
			try
			{
				// Delete the SampleRuleStore.xml
				File.Delete(RuleStoreFilename);
			}
			catch
			{
				Console.Write("Cannot delete " + RuleStoreFilename + " as it is being edited currently. Please delete this file before you run the sample again.");
			}
		}

		[STAThread]
		static void Main(string[] args)
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

			// create an engine for the ruleset
			Microsoft.RuleEngine.RuleEngine engine = new Microsoft.RuleEngine.RuleEngine((newRS), false);

			//create an instance of the XML object
			XmlDocument xd1 = new XmlDocument();
			xd1.Load(@"..\..\SampleDocumentInstance.xml");
			TypedXmlDocument doc1 = new TypedXmlDocument("SampleSchema",xd1);

			// create the array of short-term facts
			object[] shortTermFacts = new object[4];
			shortTermFacts[0] = doc1;
			shortTermFacts[1] = new Microsoft.Samples.BizTalk.BusinessRulesHelloWorld1.MySampleLibrary.MySampleBusinessObject(1);
			shortTermFacts[2] = new Microsoft.Samples.BizTalk.BusinessRulesHelloWorld1.MySampleLibrary.MySampleBusinessObject(2);
			shortTermFacts[3] = new Microsoft.Samples.BizTalk.BusinessRulesHelloWorld1.MySampleLibrary.MySampleBusinessObject(3);
						
			try
			{

				// assert several objects
				Console.WriteLine("Asserting objects ...");
				engine.Assert(shortTermFacts);
			
				// now execute to see what happens
				Console.WriteLine("Executing ...");
			
				engine.Execute();
			}

			
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			Console.WriteLine("Press any key to finish ...");
			System.Console.Read();

			// Clean up SampleRuleStore.xml
			CleanUp();

			// there should be 2 lines output:
			//   MySampleBusinessObject -- MySampleMethod executed for object 1 with parameter 5
			//   MySampleBusinessObject -- MySampleMethod executed for object 3 with parameter 5

			//This is because:

			/*
			 The rule created programmatically within the CreateRuleset() method says:

			IF

			MySampleBusinessObject.MyValue is not equal to the value of the ID element in the XML document.
			THEN

			Execute MySampleBusinessObject.MySampleMethod(int) with a hardcoded integer constant 5 ".
			
			In our case we assert 3 .NET objects with "MyValue" = 1,2, and 3 along with an XML document with ID = 1, as fact instances.
			
			Hence the rule fires twice for the 2 .NET property values that are != 1
			
			*/

		}
	}
}
