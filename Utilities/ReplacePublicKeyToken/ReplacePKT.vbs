'--------------------------------------------------------------------------
' File: ReplacePKT.vbs
'
' Summary: This file is used to replace occurrences of old public key token 
' in the specified file with a new one.
'
'--------------------------------------------------------------------------
' This file is part of the Microsoft BizTalk Server 2009 SDK
'
' Copyright (c) Microsoft Corporation. All rights reserved.
'
' This source code is intended only as a supplement to Microsoft BizTalk
' Server 2009 release and/or on-line documentation. See these other
' materials for detailed information regarding Microsoft code samples.
'
' THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
' KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
' IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
' PURPOSE.
'--------------------------------------------------------------------------

Option Explicit

Function DetectCharset (file)
	Dim stream 
   	Set stream = CreateObject("ADODB.Stream")
	stream.Open
	stream.Charset = "x-ansi"
	stream.LoadFromFile file
	Dim bytes
	bytes = stream.ReadText (3)
	stream.Close

	' Try to detect appropriate charset
	charset = EncodingASCII
        If (Len(bytes) >= 2) Then
	  Dim byte1, byte2, byte3
	  byte1 = Asc(Mid(bytes, 1, 1))
	  byte2 = Asc(Mid(bytes, 2, 1))
          If (byte1 = 255) AND (byte2 = 254) Then
             charset = EncodingUnicode
          ElseIf (Len(bytes) = 3) Then
	     byte3 = Asc(Mid(bytes, 3, 1))
             If (byte1 = 239) AND (byte2 = 187) AND (byte3 = 191) Then
                charset = EncodingUTF8
             End If
          End If
	End If

	DetectCharset = charset
End Function

If WScript.Arguments.Length <> 3 Then
	Wscript.Echo "Usage: ReplacePKT.wsf <Public Key token file> <old public key token> <file to replace>"
end if

Dim newPKToken
newPKToken = RetrievePublicKeyToken(WScript.Arguments.Item(0))

Dim fileToReplace
fileToReplace = WScript.Arguments.Item(2)

Dim charset
charset = DetectCharset (fileToReplace)

ReplacePKTokenInFile fileToReplace, charset, WScript.Arguments.Item(1), newPKToken 

