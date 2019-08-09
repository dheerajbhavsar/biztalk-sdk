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

namespace Microsoft.Samples.BizTalk.ExtendingMapper
{
	/// <summary>
	/// This is a helper class that is used by the BizTalk Server 2016 SDK 
	/// 'Extending Mapper' sample.  This class contains a single method
	/// that concatenates and then returns the input string arguments.
	/// </summary>
	public class MapperHelper
	{
		public MapperHelper()
		{
		}

		// This method takes two input string arguments and returns a 
		// concatenation of these strings.
		public string MyConcat(string str1, string str2)
		{
			return str1 + "_" + str2;
		}
	}
}
