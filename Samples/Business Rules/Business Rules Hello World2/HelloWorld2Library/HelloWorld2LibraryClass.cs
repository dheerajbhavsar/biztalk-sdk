//---------------------------------------------------------------------
// File:	HelloWorld2LibraryClass.cs
// 
// Summary: 	Contains the property referenced in the IF portion of
//              the created rule, and the method that may be called in
//              the THEN portion of the created rule.
//
// Sample: 	Business Rules Hello World 2
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

namespace Microsoft.Samples.BizTalk.BusinessRulesHelloWorld2.HelloWorld2Library
{
	
	public class HelloWorld2LibraryObject
	{
		private long myValue;

		public long MyValue
		{
			get { return myValue; }
		}

		public void MySampleMethod(int parm)
		{
			Console.WriteLine("MySampleBusinessObject Class -- MySampleMethod executed for object " + myValue.ToString() + 
				" with parameter " + parm.ToString()); 
			
		}

		public HelloWorld2LibraryObject(long val)
		{
			myValue = val;
		}
	}
}
