//---------------------------------------------------------------------
// File: MathHelper.cs
// 
// Summary: Contains MathHelper class which has Add and Substract methods which will
//          be called from the MethodCallService
//
// Sample: MethodCall
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

namespace Microsoft.Samples.BizTalk.MathLibrary
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	/// 
	[Serializable]
	public class MathHelper
	{
		public MathHelper()
		{
		}

		public int Add(int a, int b)
		{
			return a+b;
		}

		public int Subtract(int a, int b)
		{
			return a-b;
		}
	}

	public class Helper
	{
		private   Helper() {}
        public static  XmlDocument  ConstructDoc(int iSum)
		{
			XmlDocument doc = new System.Xml.XmlDocument();
			doc.LoadXml("<Root xmlns=\"http://MethodCallSample.OutputSchema\"><Result xmlns=\"\">"+iSum.ToString()+"</Result></Root>");
			return doc;

		}
	}
}
