//---------------------------------------------------------------------
// File: Message.h
// 
// Summary: Implementation of the LargeMessage interop object
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

#pragma once

#include <windows.h>
#include <mq.h>
#include <Transact.h>

using namespace System;
using namespace System::Diagnostics;
using namespace System::IO;
using namespace System::Runtime::InteropServices;

namespace LargeMessages
{
	// we only support label, authentication and body
	// but we leave space for other properties
	const int MAX_MESSAGE_PROPERTIES = 15;

	// the unmanaged data that we need in order to 
	// construct and send a message
	struct UnmanagedMessageData
	{
		UnmanagedMessageData() 
		{
			message.aPropID = propertyId;
			message.aPropVar = propertyValue; 
			message.aStatus = status;
			message.cProp = 0;
		}

		MQMSGPROPS		message;
	private:
		// you should only work with the message
		MSGPROPID		propertyId[MAX_MESSAGE_PROPERTIES];
		MQPROPVARIANT	propertyValue[MAX_MESSAGE_PROPERTIES];
		HRESULT			status[MAX_MESSAGE_PROPERTIES];
	};

	// Class that exposes a message with 3 properties:
	// Label, body and authentication
	// The class is not thread safe. You should use 
	// different messages on different threads
	public __gc class LargeMessage : public IDisposable
	{
	public:
		// Creates a new large messages with the given label and body stream
		LargeMessage(String* label, Stream* bodyStream) 
		{
			Init(label, bodyStream);
		}

		// Creates a new large messages with the given label
		// You can set the body stream later
		LargeMessage(String* label) 
		{
			Init(label, NULL);
		}

		// Creates a new large messages with the given body stream
		// You can set the label later
		LargeMessage(Stream* bodyStream) 
		{
			Init(NULL, bodyStream);
		}

		// Creates a new large messages
		// You can set the label and the body stream later
		LargeMessage() 
		{
			Init(NULL, NULL);
		}

		// Frees all the unmanaged resources
		~LargeMessage() 
		{
			Dispose();
		}

		// Returns the message label
		__property String* get_Label() 
		{
			return label;
		}

		// Allows you to set the label
		__property void set_Label(String* value) 
		{
			label = value;
		}

		// Returns the body stream
		__property Stream* get_BodyStream()
		{
			// Keep consistent with the System.Messaging
			// implementation of a message
			if (bodyStream == NULL) 
			{
				// alloc a new stream, 
				// the user might write into it
				bodyStream = new MemoryStream();
			}

			return bodyStream;
		}

		// Allows you to set the body stream
		__property void set_BodyStream(Stream* value)
		{
			bodyStream = value;
		}


		// Implementation of the IDisposable::Dispose
		virtual void Dispose() 
		{
			GC::SuppressFinalize(this);
			FreeUnmanagedData();
		}

	// private public is the managed C++ way of 
	// saying "internal" - visible to all the classes
	// inside this dll
	private public:

		// Because of security reasons, we don't allow 
		// setting the authenticated flag per message
		// You can only set it when you create a LargeMessageQueue

		// If true, the messages will be authenticated
		__property bool get_UseAuthentication() 
		{
			return useAuthentication;
		}

		// Sets the new authentication level
		__property void set_UseAuthentication(bool value)
		{
			useAuthentication = value;
		}

		// Build the actual message
		UnmanagedMessageData __nogc * Pack() 
		{
			// free the existing properties
			FreeMessageProperties();

			int index = 0;

			if ((bodyStream != NULL) && (bodyStream->Length > 0)) 
			{
				Debug::Assert(index < MAX_MESSAGE_PROPERTIES);

				PutBody(unmanagedMessageData->message.aPropVar[index]);
				unmanagedMessageData->message.aPropID[index] = PROPID_M_BODY;
				unmanagedMessageData->message.aStatus[index] = S_OK;

				index++;
			}

			if (label != NULL) 
			{
				Debug::Assert(index < MAX_MESSAGE_PROPERTIES);	

				PutLabel(unmanagedMessageData->message.aPropVar[index]);
				unmanagedMessageData->message.aPropID[index] = PROPID_M_LABEL;
				unmanagedMessageData->message.aStatus[index] = S_OK;

				index++;
			}

			if (useAuthentication) 
			{
				Debug::Assert(index < MAX_MESSAGE_PROPERTIES);	

				PutAuthentication(unmanagedMessageData->message.aPropVar[index]);
				unmanagedMessageData->message.aPropID[index] = PROPID_M_AUTH_LEVEL;
				unmanagedMessageData->message.aStatus[index] = S_OK;

				index++;
			}

			unmanagedMessageData->message.cProp = index;
			return unmanagedMessageData;
		}
	private:
		// Initialize the relevant data members
		void Init(String* label, Stream* bodyStream) 
		{
			this->label = label;
			this->bodyStream = bodyStream;

			this->useAuthentication = false;
			AllocUnmanagedData();
		}

		// Allocates the unmanaged data for the message
		void AllocUnmanagedData() 
		{
			if (unmanagedMessageData == NULL) 
			{
				unmanagedMessageData = __nogc new UnmanagedMessageData();
			}
		}

		// Loads the body from the stream and
		// puts it into a MQPROPVARIANT
		void PutBody(MQPROPVARIANT& propVariant) 
		{
			Debug::Assert(bodyStream != NULL);

			if (bodyStream->Length > UInt32::MaxValue) 
			{
				throw new InvalidOperationException("The message cannot be bigger than 4Gb.");
			}

			// mqrtlarge message limit is 4Gb
			unsigned int streamLength = (unsigned int) bodyStream->Length;
			bodyStream->Seek(0, SeekOrigin::Begin);

			BYTE __nogc * body = __nogc new BYTE[streamLength];
			propVariant.vt = VT_VECTOR | VT_UI1;
			propVariant.caub.cElems = streamLength;
			propVariant.caub.pElems = body;
			
			// leave the read optimizations to the stream class
			for (unsigned int i=0; i < streamLength; i++) 
			{
				BYTE b = (BYTE) bodyStream->ReadByte();
				body[i] = b;
			}
		}

		// Loads the body from the stream and
		// puts it into a MQPROPVARIANT
		void PutLabel(MQPROPVARIANT& propVariant) 
		{
			Debug::Assert(label != NULL);

			BSTR bstrLabel = (BSTR) Marshal::StringToBSTR(label).ToPointer();
			propVariant.vt = VT_LPWSTR;
			propVariant.pwszVal = bstrLabel;
		}

		// Loads the body from the stream and
		// puts it into a MQPROPVARIANT
		void PutAuthentication(MQPROPVARIANT& propVariant) 
		{
			propVariant.vt = VT_UI4;
			if (useAuthentication) 
			{
				propVariant.ulVal = MQMSG_AUTH_LEVEL_ALWAYS;
			} 
			else 
			{
				propVariant.ulVal = MQMSG_AUTH_LEVEL_NONE;
			}
		}

		// Frees the properties of the message
		void FreeMessageProperties() 
		{
			if (unmanagedMessageData != NULL) 
			{
				for (unsigned int i=0; i < unmanagedMessageData->message.cProp; i++) 
				{
					switch (unmanagedMessageData->message.aPropID[i])
					{
					case PROPID_M_BODY:
						delete[] unmanagedMessageData->message.aPropVar[i].caub.pElems;
						break;
					case PROPID_M_LABEL:
						Marshal::FreeBSTR(IntPtr(unmanagedMessageData->message.aPropVar[i].pwszVal));
						break;
					}
				}
				unmanagedMessageData->message.cProp = 0;
			}
		}

		// Frees the unmanaged data for the message
		void FreeUnmanagedData() 
		{
			if (unmanagedMessageData != NULL) 
			{
				FreeMessageProperties();
				delete unmanagedMessageData;
				unmanagedMessageData = NULL;
			}
		}

		String* label;
		bool useAuthentication;
		Stream* bodyStream;

		UnmanagedMessageData __nogc* unmanagedMessageData;
	};
}
