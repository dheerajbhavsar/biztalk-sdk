//---------------------------------------------------------------------
// File: Resources.cs
// 
// Sample: ExplorerOM
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




//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version:2.0.40607.16
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace CBR.Properties
{
	using System;
	using System.IO;
	using System.Resources;

	/// <summary>
	///    A strongly-typed resource class, for looking up localized strings, etc.
	/// </summary>
	// This class was auto-generated by the Strongly Typed Resource Builder
	// class via a tool like ResGen or Visual Studio.NET.
	// To add or remove a member, edit your .ResX file then rerun ResGen
	// with the /str option, or rebuild your VS project.
	class Resources
	{

		private static System.Resources.ResourceManager _resMgr;

		private static System.Globalization.CultureInfo _resCulture;

		/*FamANDAssem*/
		internal Resources()
		{
		}

		/// <summary>
		///    Returns the cached ResourceManager instance used by this class.
		/// </summary>
		[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
		public static System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if ((_resMgr == null))
				{
					System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Resources", typeof(Resources).Assembly);
					_resMgr = temp;
				}
				return _resMgr;
			}
		}

		/// <summary>
		///    Overrides the current thread's CurrentUICulture property for all
		///    resource lookups using this strongly typed resource class.
		/// </summary>
		[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
		public static System.Globalization.CultureInfo Culture
		{
			get
			{
				return _resCulture;
			}
			set
			{
				_resCulture = value;
			}
		}
	}
}
