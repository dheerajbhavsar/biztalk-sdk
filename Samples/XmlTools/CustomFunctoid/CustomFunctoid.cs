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
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;
using System.Text;
using System.Collections;
using System.Globalization;

namespace Microsoft.Samples.BizTalk.CustomFunctoid
{
	/// <summary>
	/// The following Custom functoid does a simple string concatentation. It takes two inputs and
	/// return the concatenated string. This Custom Functoid does not provide any inline code for the 
	/// functoid. If you use this functoid in a Map, then make sure that this assembly is available in GAC
	/// so that during Test..Map and during Runtime, this assembly may be found.
	/// </summary>
	public class CustomStringConcatFunctoid : BaseFunctoid
	{
		public CustomStringConcatFunctoid() : base()
		{
			//ID for this functoid
			this.ID = 3001;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
			SetupResourceAssembly("Microsoft.Samples.BizTalk.CustomFunctoid.CustomFunctoidResources", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
			SetName("IDS_CUSTOMSTRINGCONCATFUNCTOID_NAME");
			SetTooltip("IDS_CUSTOMSTRINGCONCATFUNCTOID_TOOLTIP");
			SetDescription("IDS_CUSTOMSTRINGCONCATFUNCTOID_DESCRIPTION");
			SetBitmap("IDB_CUSTOMSTRINGCONCATFUNCTOID_BITMAP");

			this.SetMinParams(2);
			this.SetMaxParams(2);

			//set the function name that needs to be called when this Functoid is invoked. This means that
			//this Functoid assembly need to be present in GAC for its availability during Test..Map and Runtime.
			SetExternalFunctionName(GetType().Assembly.FullName, "Microsoft.Samples.BizTalk.CustomFunctoid.CustomStringConcatFunctoid", "ConCatStrings");

			//category for this functoid. This functoid goes under the String Functoid Tab in the
			//VS.Net toolbox for functoids.
			this.Category = FunctoidCategory.String;
			this.OutputConnectionType = ConnectionType.AllExceptRecord;

			AddInputConnectionType(ConnectionType.AllExceptRecord); //first input
			AddInputConnectionType(ConnectionType.AllExceptRecord); //second input
		}

		//this is the function that gets called when the Map is executed which has this functoid.
		public string ConCatStrings(string val1, string val2)
		{
			return val2+val1;
		}
	}

	/// <summary>
	/// The following Custom functoid does cumulative Multiplication. It takes two inputs and
	/// return the cumulative concatenated string. This Custom Functoid does provides inline code as
	/// well as ability to execute this functoid from this assembly.
	/// Cumulative Functoids need to expose three functions: 
	///			Init Function:	   This function is called for doing any initialization
	///			Cumulate Function: This function is called inside a loop to accumulate some result
	///			Get Function:      This function is called to get the result of accumulation.
	/// </summary>
	public class CumulativeMultiplyFunctoid : BaseFunctoid
	{
		private ArrayList myCumulativeArray = new ArrayList();

		public CumulativeMultiplyFunctoid() : base()
		{
			this.ID = 3002;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
			SetupResourceAssembly("Microsoft.Samples.BizTalk.CustomFunctoid.CustomFunctoidResources", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
			SetName("IDS_CUMULATIVEMULTIPLYFUNCTOID_NAME");
			SetTooltip("IDS_CUMULATIVEMULTIPLYFUNCTOID_TOOLTIP");
			SetDescription("IDS_CUMULATIVEMULTIPLYFUNCTOID_DESCRIPTION");
			SetBitmap("IDB_CUMULATIVEMULTIPLYFUNCTOID_BITMAP");

			//Set the functions that need to be called when this Functoid is invoked. This will
			//facilitate executing this functoid directly from this assembly.
			SetExternalFunctionName(GetType().Assembly.FullName, "Microsoft.Samples.BizTalk.CustomFunctoid.CumulativeMultiplyFunctoid", "InitCumulativeMultiply");
			SetExternalFunctionName2("AddToCumulativeMultiply");
			SetExternalFunctionName3("GetCumulativeMultiply");

			//Add support for C# inline code as well. This would mean that the Mapper at compile time can extract
			//the inline code for the Functoid, and embed it in the XSLT that is generated. 
			SetScriptBuffer(ScriptType.CSharp, GetCSharpInitBuffer(), 0);
			SetScriptBuffer(ScriptType.CSharp, GetCSharpAddToBuffer(), 1);
			SetScriptBuffer(ScriptType.CSharp, GetCSharpGetBuffer(), 2);

			//this is a nice way of declaring and maintaining global variables used during Map execution.
			SetScriptGlobalBuffer(ScriptType.CSharp, "public System.Collections.ArrayList myCumulativeArray = new System.Collections.ArrayList();\n");

			this.Category = FunctoidCategory.Cumulative;
			this.OutputConnectionType = ConnectionType.AllExceptRecord;

			AddInputConnectionType(ConnectionType.AllExceptRecord);

			this.RequiredGlobalHelperFunctions = InlineGlobalHelperFunction.IsNumeric;

			SetMinParams(1);	// 2nd param is optional
			SetMaxParams(2);	// 2nd param is optional

			//first input
			AddInputConnectionType((~ConnectionType.FunctoidCount)&(~ConnectionType.FunctoidIndex)&(~ConnectionType.FunctoidIteration)&(~ConnectionType.FunctoidCumulative)&(~ConnectionType.FunctoidLooping)&(~ConnectionType.Record));
			
			//second input
			AddInputConnectionType(ConnectionType.AllExceptRecord);
		}

		//Init Function
		public string InitCumulativeMultiply(int index)
		{
			if (index >= 0)
			{
				if (index >= myCumulativeArray.Count)
				{
					myCumulativeArray.Add(1.0);
				}
				else
				{
					myCumulativeArray[index] = 1.0;
				}
			}

			return "";
		}

		//Cumulate Function
		public string AddToCumulativeMultiply(int index, string val, string reserved)
		{
			if (index < 0 || index >= myCumulativeArray.Count)
			{
				return "";
			}

			if (IsNumeric(val))
			{
				double dval = Convert.ToDouble(val, CultureInfo.InvariantCulture);	// no need to exception check here because IsNumeric already did that
				myCumulativeArray[index] = (double)(myCumulativeArray[index]) * dval;
			}
			return myCumulativeArray[index].ToString();
		}

		//Get Function
		public string GetCumulativeMultiply(int index)
		{
			if (index < 0 || index >= myCumulativeArray.Count)
			{
				return "";
			}
			
			return myCumulativeArray[index].ToString();
		}

		//Init Function
		private string GetCSharpInitBuffer()
		{
			StringBuilder builder = new StringBuilder();
			
			builder.Append("public string InitCumulativeMultiply(int index)\n");
			builder.Append("{\n");
			builder.Append("	if (index >= 0)\n");
			builder.Append("	{\n");
			builder.Append("		if (index >= myCumulativeArray.Count)\n");
			builder.Append("		{\n");
			builder.Append("			myCumulativeArray.Add(1.0);\n");
			builder.Append("		}\n");
			builder.Append("		else\n");
			builder.Append("		{\n");
			builder.Append("			myCumulativeArray[index] = 1.0;\n");
			builder.Append("		}\n");
			builder.Append("	}\n");
			builder.Append("	return \"\";\n");
			builder.Append("}\n");

			return builder.ToString();
		}

		//Cumulute Function
		private string GetCSharpAddToBuffer()
		{
			StringBuilder builder = new StringBuilder();
			
			builder.Append("public string AddToCumulativeMultiply(int index, string val, string reserved)\n");
			builder.Append("{\n");
			builder.Append("	if (index < 0 || index >= myCumulativeArray.Count)\n");
			builder.Append("	{\n");
			builder.Append("		return \"\";\n");
			builder.Append("	}\n");
			builder.Append("	if (IsNumeric(val))\n");
			builder.Append("	{\n");
			builder.Append("		double dval = Convert.ToDouble(val, System.Globalization.CultureInfo.InvariantCulture);	// no need to exception check here because IsNumeric already did that\n");
			builder.Append("		myCumulativeArray[index] = (double)(myCumulativeArray[index]) * dval;\n");
			builder.Append("	}\n");
			builder.Append("	return myCumulativeArray[index].ToString();\n");
			builder.Append("}\n");

			return builder.ToString();
		}

		//Get Function
		private string GetCSharpGetBuffer()
		{
			StringBuilder builder = new StringBuilder();
			
			builder.Append("public string GetCumulativeMultiply(int index)\n");
			builder.Append("{\n");
			builder.Append("	if (index < 0 || index >= myCumulativeArray.Count)\n");
			builder.Append("	{\n");
			builder.Append("		return \"\";\n");
			builder.Append("	}\n");
			builder.Append("	return myCumulativeArray[index].ToString();\n");
			builder.Append("}\n");

			return builder.ToString();
		}
	}

	/// <summary>
	/// The following Custom functoid finds and returns the Longest string out of a given set of strings.
	/// cumulative Multiplication. This functoid is capable of handling variable number of inputs. The
	/// overidden function "GetInlineScriptBuffer" dynamically constructs inline code for the functoid
	/// based on the number of inputs specified.
	/// Since this functoid provides inline script, it will be embedded inside the compiled XSLT during
	/// compile time.
	/// </summary>
	public class LongestStringFunctoid : BaseFunctoid
	{
		public LongestStringFunctoid() : base()
		{
			this.ID = 3003;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
			SetupResourceAssembly("Microsoft.Samples.BizTalk.CustomFunctoid.CustomFunctoidResources", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
			SetName("IDS_LONGESTSTRINGFUNCTOID_NAME");
			SetTooltip("IDS_LONGESTSTRINGFUNCTOID_TOOLTIP");
			SetDescription("IDS_LONGESTSTRINGFUNCTOID_DESCRIPTION");
			SetBitmap("IDB_LONGESTSTRINGFUNCTOID_BITMAP");

			AddScriptTypeSupport(ScriptType.CSharp);

			this.Category = FunctoidCategory.String;
			this.OutputConnectionType = ConnectionType.AllExceptRecord;

			//this function can accept variable inputs. The overidden function "GetInlineScriptBuffer" will
			//provide inline script code for various number of inputs.
			this.HasVariableInputs = true;

			//Set the minimum number of parameters to be 1
			this.SetMinParams(1);

			AddInputConnectionType(ConnectionType.AllExceptRecord);
		}

		//this overidden function provides inline code for various number of inputs.
		protected override string GetInlineScriptBuffer(ScriptType scriptType, int numParams, int functionNumber)
		{
			if (ScriptType.CSharp == scriptType)
			{
				StringBuilder builder = new StringBuilder();

				builder.Append("public string GetLongestString(");

				for (int i=0; i<numParams; i++)
				{
					if (i > 0)
					{
						builder.Append(", ");
					}

					builder.Append("string param");
					builder.Append(i.ToString());
				}

				builder.Append(")\n");
				builder.Append("{\n");

				builder.Append("   System.Collections.ArrayList listStrings = new System.Collections.ArrayList();\n");

				for (int j=0; j<numParams; j++)
				{
					builder.Append("   listStrings.Add(param");
					builder.Append(j.ToString());
					builder.Append(");\n");
				}

				builder.Append("   string longestString = \"\";\n");
				builder.Append("   foreach (string s in listStrings)\n");
				builder.Append("   {\n");
				builder.Append("       if (s.Length > longestString.Length)\n");
				builder.Append("       {\n");
				builder.Append("           longestString = s;\n");
				builder.Append("       }\n");
				builder.Append("   }\n");
				builder.Append("   return longestString;\n");
				builder.Append("}\n");

				return builder.ToString();
			}
			else
			{
				return "";
			}
		}
	}

	/// <summary>
	/// The following Custom functoid 'BuildArray' builds an array of variable length based on the number of
	/// inputs specified. This functoid illustrates the usage of a global variable to store this array, which
	/// means that other functoids can access this global variable in their code. Global variables are typically
	/// used if you want to maintain state information in the map that is available to all the functoids.  This
	/// functoid is used in conjunction with the 'ExtractArray' custom functoid (listed in this sample), 
	/// which makes use of this global variable.
	/// The overidden function "GetInlineScriptBuffer" dynamically constructs inline code for the functoid
	/// based on the number of inputs specified.
	/// Since this functoid provides inline script, it will be embedded inside the compiled XSLT during
	/// compile time.
	/// </summary>
	public class BuildArray : BaseFunctoid
	{
		public BuildArray() : base()
		{
			this.ID = 3004;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
			SetupResourceAssembly("Microsoft.Samples.BizTalk.CustomFunctoid.CustomFunctoidResources", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
			SetName("IDS_BUILDARRAYFUNCTOID_NAME");
			SetTooltip("IDS_BUILDARRAYFUNCTOID_TOOLTIP");
			SetDescription("IDS_BUILDARRAYFUNCTOID_DESCRIPTION");
			SetBitmap("IDB_BUILDARRAYFUNCTOID_BITMAP");

			AddScriptTypeSupport(ScriptType.CSharp);

			//this is a nice way of declaring and maintaining global variables used during Map execution.
			SetScriptGlobalBuffer(ScriptType.CSharp, "public System.Collections.ArrayList _globalArray = new System.Collections.ArrayList();\n");

			this.Category = FunctoidCategory.String;
			this.OutputConnectionType = ConnectionType.AllExceptRecord;

			//This functoid can accept variable number of inputs.
			this.HasVariableInputs = true;

			//Set the minimum number of parameters to be 1
			this.SetMinParams(1);

			AddInputConnectionType(ConnectionType.AllExceptRecord);
		}

		//this overidden function provides inline code for various number of inputs.
		//The inline code builds the contents of the global array variable _globalArray using the parameters
		//specified to the function.
		protected override string GetInlineScriptBuffer(ScriptType scriptType, int numParams, int functionNumber)
		{
			if (ScriptType.CSharp == scriptType)
			{
				StringBuilder builder = new StringBuilder();
				
				builder.Append("public string BuildOutputArray(");

				for (int i=0; i<numParams; i++)
				{
					if (i > 0)
					{
						builder.Append(", ");
					}

					builder.Append("string param");
					builder.Append(i.ToString());
				}

				builder.Append(")\n");
				builder.Append("{\n");
				builder.Append("_globalArray.Clear();\n");
				for (int j=0; j<numParams; j++)
				{
					builder.Append("   _globalArray.Add(param");
					builder.Append(j.ToString());
					builder.Append(");\n");
				}

				builder.Append("   return _globalArray.Count.ToString();\n");
				builder.Append("}\n");

				return builder.ToString();
			}
			else
			{
				return "";
			}
		}
	}

	/// The following Custom functoid 'ExtractArray' is to be used in conjunction with the 'BuildArray' custom
	/// functoid. This functoid makes use of the global variable _globalArray declared in the 'BuildArray' functoid.
	/// This functoid accepts two inputs. The second parameter of this functoid is used as an index into the
	/// _globalArray array. It will then return the corresponding value of that array index.
	/// Since this functoid provides inline script, it will be embedded inside the compiled XSLT during
	/// compile time.
	/// </summary>
	public class ExtractArray : BaseFunctoid
	{
		public ExtractArray() : base()
		{
			this.ID = 3005;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
			SetupResourceAssembly("Microsoft.Samples.BizTalk.CustomFunctoid.CustomFunctoidResources", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
			SetName("IDS_EXTRACTARRAYFUNCTOID_NAME");
			SetTooltip("IDS_EXTRACTARRAYFUNCTOID_TOOLTIP");
			SetDescription("IDS_EXTRACTARRAYFUNCTOID_DESCRIPTION");
			SetBitmap("IDB_EXTRACTARRAYFUNCTOID_BITMAP");
			SetExternalFunctionName(GetType().Assembly.FullName, "Microsoft.Samples.BizTalk.CustomFunctoid.ExtractArray", "GetArrayValueByIndex");

			AddScriptTypeSupport(ScriptType.CSharp);
			
			this.Category = FunctoidCategory.String;
			this.OutputConnectionType = ConnectionType.AllExceptRecord;
			this.RequiredGlobalHelperFunctions = InlineGlobalHelperFunction.IsNumeric;
			
			//this functoid accepts exactly two parameters.
			this.SetMinParams(2);
			this.SetMaxParams(2);

			AddInputConnectionType(ConnectionType.AllExceptRecord);
		}

		//this overidden function provides inline code for various number of inputs.
		//The inline code has a function called GetArrayValueByIndex(...) that accepts two parameters. The
		//first parameter is ignored. The second parameter is used as an index into the global array variable
		//_globalArray. This function would return the value of that index location in the array.
		protected override string GetInlineScriptBuffer(ScriptType scriptType, int numParams, int functionNumber)
		{
			if (ScriptType.CSharp == scriptType)
			{
				StringBuilder builder = new StringBuilder();
			
				builder.Append("public string GetArrayValueByIndex(string ignoreParam, string val)\n");
				builder.Append("{\n");
				builder.Append("    int index = 0;\n");
				builder.Append("    if (IsNumeric(val))\n");
				builder.Append("	{\n");
				builder.Append("		index = Convert.ToInt32(val, System.Globalization.CultureInfo.InvariantCulture);	// no need to exception check here because IsNumeric already did that\n");
				builder.Append("	}\n");
				builder.Append("	if (index >= 0)\n");
				builder.Append("	{\n");
				builder.Append("		if (index >= _globalArray.Count)\n");
				builder.Append("		{\n");
				builder.Append("			return \"\";\n");
				builder.Append("		}\n");
				builder.Append("		else\n");
				builder.Append("		{\n");
				builder.Append("			return (string) _globalArray[index];\n");
				builder.Append("		}\n");
				builder.Append("	}\n");
				builder.Append("	return \"\";\n");
				builder.Append("}\n");

				return builder.ToString();
			}
			return "";
		}
	}
}
