
//---------------------------------------------------------------------
// File: AssemblyInfo.cs
// 
// Summary: Implementation of an adapter framework sample adapter. 
//
// Sample: Adapter framework transactional adapter.
//
//---------------------------------------------------------------------
// This file is part of the Microsoft BizTalk Server SDK
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// This source code is intended only as a supplement to Microsoft BizTalk
// Server release and/or on-line documentation. See these other
// materials for detailed information regarding Microsoft code samples.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
// PURPOSE.
//---------------------------------------------------------------------

using System;
using System.Reflection;
using System.Security.Permissions;
using System.Runtime.CompilerServices;

// Most interfaces are Compliant, thos that are not, are attributed accordingly
[assembly: CLSCompliant(true)]

///
/// This assembly does not need to be visible to COM since the objects 
/// contained within it are only created via managed interop assemblies used
/// by BizTalk.
/// 
[assembly: System.Runtime.InteropServices.ComVisible(false)]

///
/// Assembly level description and version information is pulled from the
/// global BizTalk ProductInfo assembly.
/// 

[assembly: AssemblyTitle("Microsoft BizTalk Transactional Transport Admin")]
[assembly: AssemblyDescription("Microsoft BizTalk Transactional Transport Admin")]
[assembly: AssemblyCulture("")]