//---------------------------------------------------------------------
// File: Queue.h
// 
// Summary: Implementation of the LargeMessageQueue interop object
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

#include "mqlarge.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace LargeMessages
{
	// the unmanaged data that we need
	// in order to inplement the queue
	struct UnmanagedQueueData 
	{
		// The constructor intializes the data inside
		UnmanagedQueueData() 
		{
			queueHandle = NULL;
		}

		~UnmanagedQueueData() 
		{
			if (queueHandle != NULL) 
			{
				MQCloseQueue(queueHandle);
			}
		}

		QUEUEHANDLE queueHandle;
	};

	// Wrapps the Large messages api 
	// inside a class that can be reused from other .net languages
	// The Send method is thread-safe. 
	// Open or dispose are not thread-safe
	// If you want a thread-safe queue, add syncronization inside those methods
	public __gc class LargeMessageQueue : public IDisposable
	{
	public:
		// Creates a new large messages queue
		// formatName - the destination queue format name
		// useAuthentication - if true, messages are sent using authentication
		LargeMessageQueue(String* formatName, bool useAuthentication) 
		{
			Init(formatName, useAuthentication);
		}


		// Creates a new large messages queue
		// formatName - the destination queue format name
		// no authentication
		LargeMessageQueue(String* formatName) 
		{
			Init(formatName, false);
		}

		// Frees all the unmanaged resources
		~LargeMessageQueue() 
		{
			Dispose();
		}

		// Returns the queue format name
		__property String* get_FormatName() 
		{
			return formatName;
		}

		// Allows you to set the format name
		__property void set_FormatName(String* value) 
		{
			formatName = value;
		}

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

		// Opens the queue
		// Only send is supported
		void Open() 
		{
			if (IsOpen())
			{
				throw new InvalidOperationException("The queue is already opened.");
			}
			if (formatName == NULL) 
			{
				throw new ArgumentNullException("formatName", "The format name cannot be null.");
			}

			// we need to convert the queue name from .net
			// to a unicode string

			IntPtr convertedFormatName = NULL;
			try {
				convertedFormatName = Marshal::StringToBSTR(formatName);

				AllocUnmanagedQueueData();

				HRESULT hr = MQOpenQueue(
					(BSTR) convertedFormatName.ToPointer(),
					MQ_SEND_ACCESS,
					MQ_DENY_NONE,
					&unmanagedQueueData->queueHandle);

				if (FAILED(hr)) 
				{
					Marshal::ThrowExceptionForHR(hr);
				}
			} 
			__finally 
			{
				if (convertedFormatName != NULL) 
				{
					Marshal::FreeBSTR(convertedFormatName);
				}
			}
		}

		// Returns true if the queue is opened
		bool IsOpen() 
		{
			return 
				(unmanagedQueueData != NULL) && 
				(unmanagedQueueData->queueHandle != NULL);
		}

		// Sends a large message
		// Always sends transactional message, so the 
		// destination must be transactional
		// You cannot specify a transation
		void Send(LargeMessage* message) 
		{
			if (message == NULL) 
			{
				throw new ArgumentNullException("message", "The message cannot be null.");
			}

			UnmanagedMessageData __nogc* messageData = 
				message->Pack();

			HRESULT hr = MQSendLargeMessage(
				unmanagedQueueData->queueHandle, 
				&(messageData->message),
				MQ_SINGLE_MESSAGE,
				MQRTLARGE_USE_DEFAULT_PART_SIZE);

			if (FAILED(hr)) 
			{
				if (hr == MQ_ERROR_PROPERTY) 
				{
					throw new ApplicationException("One or more properties are invalid.");
				}
				Marshal::ThrowExceptionForHR(hr);
			}
		}

		// Closes the queue
		void Close() 
		{
			if (IsOpen()) 
			{
				MQCloseQueue(unmanagedQueueData->queueHandle);
			}
		}

		// Implementation of the IDisposable::Dispose
		virtual void Dispose() 
		{
			GC::SuppressFinalize(this);
			FreeUnmanagedQueueData();
		}

	private:
		// Initializes the data members
		void Init(String* formatName, bool useAuthentication) 
		{
			this->formatName = formatName;
			this->useAuthentication = useAuthentication;
			AllocUnmanagedQueueData();
		}

		// Allocates and returns the unmanaged data that 
		// we need to send messages
		void AllocUnmanagedQueueData() 
		{
			if (unmanagedQueueData == NULL) 
			{
				unmanagedQueueData = __nogc new UnmanagedQueueData;
			}
		}

		// releases the unmanaged queue data that we hold
		void FreeUnmanagedQueueData() 
		{
			if (unmanagedQueueData != NULL) 
			{
				delete unmanagedQueueData;
				unmanagedQueueData = NULL;
			}
		}


		String* formatName;
		bool useAuthentication;

		UnmanagedQueueData __nogc* unmanagedQueueData;
	};
}
