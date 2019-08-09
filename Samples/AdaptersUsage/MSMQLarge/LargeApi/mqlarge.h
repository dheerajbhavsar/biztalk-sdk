//---------------------------------------------------------------------
// File:	mqlarge.h
// 
// Summary: 	Defines MSMQ APIs for sending and receiving large (>4MB) messages 
//
// Utility: 	mqrtlarge.dll
//
//---------------------------------------------------------------------
// This file is part of the Microsoft BizTalk Server 2009 SDK
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// This source code is intended only as a supplement to Microsoft BizTalk
// Server 2006 release and/or on-line documentation. See these other
// materials for detailed information regarding Microsoft code samples.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
// PURPOSE.
//---------------------------------------------------------------------

#ifndef __MQLARGE_H__
#define __MQLARGE_H__

//
// Denote that the sender would like to use the system default part size when sending a 
// message. (Used in MQSendLargeMessage)
//

#define MQRTLARGE_USE_DEFAULT_PART_SIZE 0

#ifdef __cplusplus
extern "C"
{
#endif

HRESULT
APIENTRY
MQSendLargeMessage(
    IN	QUEUEHANDLE  hQueue,
    IN	MQMSGPROPS*  pmp,
    IN	ITransaction *pTransaction,
	IN	DWORD		 dwRequestedPartSize
);


HRESULT
APIENTRY
MQReceiveLargeMessage(
	IN	QUEUEHANDLE			hQueue,                   
	IN	DWORD				dwTimeout,                       
	IN	DWORD				dwAction,                        
	OUT	MQMSGPROPS			*pMessageProps,              
	IN	LPOVERLAPPED		lpOverlapped,             
	IN	PMQRECEIVECALLBACK	fnReceiveCallback,  
	IN	HANDLE				hCursor,                        
	IN	ITransaction		*pTransaction
);

#ifdef __cplusplus
}
#endif

#endif // __MQLARGE_H__