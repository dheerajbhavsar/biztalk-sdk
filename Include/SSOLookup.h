

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 6.00.0366 */
/* Compiler settings for ssolookup.idl:
    Oicf, W1, Zp8, env=Win32 (32b run)
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
//@@MIDL_FILE_HEADING(  )

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __ssolookup_h__
#define __ssolookup_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __ISSOLookup1_FWD_DEFINED__
#define __ISSOLookup1_FWD_DEFINED__
typedef interface ISSOLookup1 ISSOLookup1;
#endif 	/* __ISSOLookup1_FWD_DEFINED__ */


#ifndef __ISSOLookup2_FWD_DEFINED__
#define __ISSOLookup2_FWD_DEFINED__
typedef interface ISSOLookup2 ISSOLookup2;
#endif 	/* __ISSOLookup2_FWD_DEFINED__ */


#ifndef __ISSOTicket_FWD_DEFINED__
#define __ISSOTicket_FWD_DEFINED__
typedef interface ISSOTicket ISSOTicket;
#endif 	/* __ISSOTicket_FWD_DEFINED__ */


#ifndef __SSOLookup_FWD_DEFINED__
#define __SSOLookup_FWD_DEFINED__

#ifdef __cplusplus
typedef class SSOLookup SSOLookup;
#else
typedef struct SSOLookup SSOLookup;
#endif /* __cplusplus */

#endif 	/* __SSOLookup_FWD_DEFINED__ */


#ifndef __SSOTicket_FWD_DEFINED__
#define __SSOTicket_FWD_DEFINED__

#ifdef __cplusplus
typedef class SSOTicket SSOTicket;
#else
typedef struct SSOTicket SSOTicket;
#endif /* __cplusplus */

#endif 	/* __SSOTicket_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 

void * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void * ); 

/* interface __MIDL_itf_ssolookup_0000 */
/* [local] */ 


/***************************************************************************
         Copyright (c) Microsoft Corporation, All rights reserved.          
***************************************************************************/



extern RPC_IF_HANDLE __MIDL_itf_ssolookup_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_ssolookup_0000_v0_0_s_ifspec;

#ifndef __ISSOLookup1_INTERFACE_DEFINED__
#define __ISSOLookup1_INTERFACE_DEFINED__

/* interface ISSOLookup1 */
/* [oleautomation][unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_ISSOLookup1;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("66E51591-90D4-4E75-A47A-6CC0B20B0C04")
    ISSOLookup1 : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetCredentials( 
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ LONG lFlags,
            /* [out] */ BSTR *pbstrExternalUserName,
            /* [retval][out] */ SAFEARRAY * *ppsaExternalCredentials) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISSOLookup1Vtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISSOLookup1 * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISSOLookup1 * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISSOLookup1 * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ISSOLookup1 * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ISSOLookup1 * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ISSOLookup1 * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ISSOLookup1 * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetCredentials )( 
            ISSOLookup1 * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ LONG lFlags,
            /* [out] */ BSTR *pbstrExternalUserName,
            /* [retval][out] */ SAFEARRAY * *ppsaExternalCredentials);
        
        END_INTERFACE
    } ISSOLookup1Vtbl;

    interface ISSOLookup1
    {
        CONST_VTBL struct ISSOLookup1Vtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISSOLookup1_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ISSOLookup1_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ISSOLookup1_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ISSOLookup1_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define ISSOLookup1_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define ISSOLookup1_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define ISSOLookup1_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define ISSOLookup1_GetCredentials(This,bstrApplicationName,lFlags,pbstrExternalUserName,ppsaExternalCredentials)	\
    (This)->lpVtbl -> GetCredentials(This,bstrApplicationName,lFlags,pbstrExternalUserName,ppsaExternalCredentials)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOLookup1_GetCredentials_Proxy( 
    ISSOLookup1 * This,
    /* [in] */ BSTR bstrApplicationName,
    /* [in] */ LONG lFlags,
    /* [out] */ BSTR *pbstrExternalUserName,
    /* [retval][out] */ SAFEARRAY * *ppsaExternalCredentials);


void __RPC_STUB ISSOLookup1_GetCredentials_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ISSOLookup1_INTERFACE_DEFINED__ */


#ifndef __ISSOLookup2_INTERFACE_DEFINED__
#define __ISSOLookup2_INTERFACE_DEFINED__

/* interface ISSOLookup2 */
/* [oleautomation][unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_ISSOLookup2;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("DBD1E8D2-4743-4d22-9B74-7929E0BDC3E1")
    ISSOLookup2 : public ISSOLookup1
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE LogonExternalUser( 
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrUserName,
            /* [in] */ LONG lFlags,
            /* [in] */ SAFEARRAY * *ppsaCredentials,
            /* [retval][out] */ ULONGLONG *pullAccessToken) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISSOLookup2Vtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISSOLookup2 * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISSOLookup2 * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISSOLookup2 * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ISSOLookup2 * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ISSOLookup2 * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ISSOLookup2 * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ISSOLookup2 * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetCredentials )( 
            ISSOLookup2 * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ LONG lFlags,
            /* [out] */ BSTR *pbstrExternalUserName,
            /* [retval][out] */ SAFEARRAY * *ppsaExternalCredentials);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *LogonExternalUser )( 
            ISSOLookup2 * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrUserName,
            /* [in] */ LONG lFlags,
            /* [in] */ SAFEARRAY * *ppsaCredentials,
            /* [retval][out] */ ULONGLONG *pullAccessToken);
        
        END_INTERFACE
    } ISSOLookup2Vtbl;

    interface ISSOLookup2
    {
        CONST_VTBL struct ISSOLookup2Vtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISSOLookup2_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ISSOLookup2_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ISSOLookup2_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ISSOLookup2_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define ISSOLookup2_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define ISSOLookup2_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define ISSOLookup2_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define ISSOLookup2_GetCredentials(This,bstrApplicationName,lFlags,pbstrExternalUserName,ppsaExternalCredentials)	\
    (This)->lpVtbl -> GetCredentials(This,bstrApplicationName,lFlags,pbstrExternalUserName,ppsaExternalCredentials)


#define ISSOLookup2_LogonExternalUser(This,bstrApplicationName,bstrUserName,lFlags,ppsaCredentials,pullAccessToken)	\
    (This)->lpVtbl -> LogonExternalUser(This,bstrApplicationName,bstrUserName,lFlags,ppsaCredentials,pullAccessToken)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOLookup2_LogonExternalUser_Proxy( 
    ISSOLookup2 * This,
    /* [in] */ BSTR bstrApplicationName,
    /* [in] */ BSTR bstrUserName,
    /* [in] */ LONG lFlags,
    /* [in] */ SAFEARRAY * *ppsaCredentials,
    /* [retval][out] */ ULONGLONG *pullAccessToken);


void __RPC_STUB ISSOLookup2_LogonExternalUser_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ISSOLookup2_INTERFACE_DEFINED__ */


#ifndef __ISSOTicket_INTERFACE_DEFINED__
#define __ISSOTicket_INTERFACE_DEFINED__

/* interface ISSOTicket */
/* [oleautomation][unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_ISSOTicket;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("77437865-7472-46A8-9269-431F143E6A27")
    ISSOTicket : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IssueTicket( 
            /* [in] */ LONG lFlags,
            /* [retval][out] */ BSTR *pbstrTicket) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE RedeemTicket( 
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrSender,
            /* [in] */ BSTR bstrTicket,
            /* [in] */ LONG lFlags,
            /* [out] */ BSTR *pbstrExternalUserName,
            /* [retval][out] */ SAFEARRAY * *ppsaExternalCredentials) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISSOTicketVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISSOTicket * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISSOTicket * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISSOTicket * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ISSOTicket * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ISSOTicket * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ISSOTicket * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ISSOTicket * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *IssueTicket )( 
            ISSOTicket * This,
            /* [in] */ LONG lFlags,
            /* [retval][out] */ BSTR *pbstrTicket);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *RedeemTicket )( 
            ISSOTicket * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrSender,
            /* [in] */ BSTR bstrTicket,
            /* [in] */ LONG lFlags,
            /* [out] */ BSTR *pbstrExternalUserName,
            /* [retval][out] */ SAFEARRAY * *ppsaExternalCredentials);
        
        END_INTERFACE
    } ISSOTicketVtbl;

    interface ISSOTicket
    {
        CONST_VTBL struct ISSOTicketVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISSOTicket_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ISSOTicket_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ISSOTicket_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ISSOTicket_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define ISSOTicket_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define ISSOTicket_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define ISSOTicket_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define ISSOTicket_IssueTicket(This,lFlags,pbstrTicket)	\
    (This)->lpVtbl -> IssueTicket(This,lFlags,pbstrTicket)

#define ISSOTicket_RedeemTicket(This,bstrApplicationName,bstrSender,bstrTicket,lFlags,pbstrExternalUserName,ppsaExternalCredentials)	\
    (This)->lpVtbl -> RedeemTicket(This,bstrApplicationName,bstrSender,bstrTicket,lFlags,pbstrExternalUserName,ppsaExternalCredentials)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOTicket_IssueTicket_Proxy( 
    ISSOTicket * This,
    /* [in] */ LONG lFlags,
    /* [retval][out] */ BSTR *pbstrTicket);


void __RPC_STUB ISSOTicket_IssueTicket_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOTicket_RedeemTicket_Proxy( 
    ISSOTicket * This,
    /* [in] */ BSTR bstrApplicationName,
    /* [in] */ BSTR bstrSender,
    /* [in] */ BSTR bstrTicket,
    /* [in] */ LONG lFlags,
    /* [out] */ BSTR *pbstrExternalUserName,
    /* [retval][out] */ SAFEARRAY * *ppsaExternalCredentials);


void __RPC_STUB ISSOTicket_RedeemTicket_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ISSOTicket_INTERFACE_DEFINED__ */



#ifndef __SSOLookupLib_LIBRARY_DEFINED__
#define __SSOLookupLib_LIBRARY_DEFINED__

/* library SSOLookupLib */
/* [helpstring][version][uuid] */ 

#ifndef _SSO_FLAG
#define _SSO_FLAG
typedef /* [helpstring][uuid] */  DECLSPEC_UUID("DE580262-EE6C-4f9a-8BE2-81D2CE331270") 
enum SSO_FLAG
    {	SSO_FLAG_NONE	= 0,
	SSO_FLAG_REFRESH	= 0x1,
	SSO_FLAG_ENABLED	= 0x2,
	SSO_FLAG_SSO_WINDOWS_TO_EXTERNAL	= 0x4,
	SSO_FLAG_RUNTIME	= 0x4,
	SSO_FLAG_SSO_EXTERNAL_TO_WINDOWS	= 0x8,
	SSO_FLAG_SSO_VERIFY_EXTERNAL_CREDS	= 0x10,
	SSO_FLAG_ALLOW_TICKETS	= 0x20,
	SSO_FLAG_VALIDATE_TICKETS	= 0x40,
	SSO_FLAG_ADMIN_ENABLED	= 0x80,
	SSO_FLAG_READ_MODIFY_WRITE	= 0x100,
	SSO_FLAG_REPLAY	= 0x100,
	SSO_FLAG_PARTIAL_SYNC_FROM_WINDOWS_TO_DB	= 0x100,
	SSO_FLAG_PARTIAL_SYNC_FROM_EXTERNAL_TO_DB	= 0x200,
	SSO_FLAG_FULL_SYNC_FROM_WINDOWS_TO_EXTERNAL	= 0x400,
	SSO_FLAG_FULL_SYNC_FROM_EXTERNAL_TO_WINDOWS	= 0x800,
	SSO_FLAG_SYNC_VERIFY_EXTERNAL_CREDS	= 0x1000,
	SSO_FLAG_SYNC_PROVIDE_OLD_EXTERNAL_CREDS	= 0x2000,
	SSO_FLAG_SYNC_ALLOW_MAPPING_CONFLICTS	= 0x4000,
	SSO_FLAG_APP_USES_GROUP_MAPPING	= 0x10000,
	SSO_FLAG_APP_GROUP	= 0x10000,
	SSO_FLAG_APP_EXTERNAL_NAME_SAME	= 0x20000,
	SSO_FLAG_APP_ALLOW_LOCAL	= 0x40000,
	SSO_FLAG_APP_ADMIN_SAME	= 0x80000,
	SSO_FLAG_APP_CONFIG_STORE	= 0x100000,
	SSO_FLAG_APP_TICKET_TIMEOUT	= 0x200000,
	SSO_FLAG_APP_ADAPTER	= 0x400000,
	SSO_FLAG_APP_FILTER_BY_TYPE	= 0x1,
	SSO_FLAG_MAPPING_REQUIRES_WINDOWS_PASSWORD	= 0x1000000,
	SSO_FLAG_MAPPING_REQUIRES_EXTERNAL_CREDS	= 0x2000000,
	SSO_FLAG_MAPPING_ENABLE_AUDIT	= 0x4000000,
	SSO_FLAG_MAPPING_CONFIG_STORE	= 0x8000000,
	SSO_FLAG_MAPPING_ADMIN	= 0x10000000,
	SSO_FLAG_MAPPING_HOSTGROUP	= 0x20000000,
	SSO_FLAG_MAPPING_GROUP	= 0x40000000,
	SSO_FLAG_MAPPING_HIDE	= 0x80000000,
	SSO_FLAG_FIELD_INFO_MASK	= 0x10000000,
	SSO_FLAG_FIELD_INFO_SYNC	= 0x20000000,
	SSO_FLAG_SSO_DISABLE_CRED_CACHE	= 0x40000000
    } 	SSO_FLAG;

typedef /* [helpstring][uuid] */  DECLSPEC_UUID("0DF09E8B-B957-4ff6-8B29-B57918D61442") 
enum SSO_APP_TYPE
    {	SSO_APP_TYPE_NONE	= 0,
	SSO_APP_TYPE_INDIVIDUAL	= 0x1,
	SSO_APP_TYPE_GROUP	= 0x2,
	SSO_APP_TYPE_CONFIG_STORE	= 0x4,
	SSO_APP_TYPE_HOST_GROUP	= 0x8,
	SSO_APP_TYPE_PS_ADAPTER	= 0x10,
	SSO_APP_TYPE_PS_GROUP_ADAPTER	= 0x20
    } 	SSO_APP_TYPE;

#define	GLOBAL_FLAG_MASK	( SSO_FLAG_ENABLED | SSO_FLAG_SSO_WINDOWS_TO_EXTERNAL | SSO_FLAG_SSO_EXTERNAL_TO_WINDOWS | SSO_FLAG_SSO_VERIFY_EXTERNAL_CREDS | SSO_FLAG_ALLOW_TICKETS | SSO_FLAG_VALIDATE_TICKETS | SSO_FLAG_PARTIAL_SYNC_FROM_EXTERNAL_TO_DB | SSO_FLAG_FULL_SYNC_FROM_WINDOWS_TO_EXTERNAL | SSO_FLAG_FULL_SYNC_FROM_EXTERNAL_TO_WINDOWS | SSO_FLAG_SYNC_VERIFY_EXTERNAL_CREDS )

#define	SYNC_FLAG_MASK	( SSO_FLAG_PARTIAL_SYNC_FROM_EXTERNAL_TO_DB | SSO_FLAG_FULL_SYNC_FROM_WINDOWS_TO_EXTERNAL | SSO_FLAG_FULL_SYNC_FROM_EXTERNAL_TO_WINDOWS | SSO_FLAG_SYNC_VERIFY_EXTERNAL_CREDS | SSO_FLAG_SYNC_PROVIDE_OLD_EXTERNAL_CREDS | SSO_FLAG_SYNC_ALLOW_MAPPING_CONFLICTS )

#define	APP_FLAG_MASK	( SSO_FLAG_ENABLED | SSO_FLAG_SSO_WINDOWS_TO_EXTERNAL | SSO_FLAG_SSO_EXTERNAL_TO_WINDOWS | SSO_FLAG_ALLOW_TICKETS | SSO_FLAG_VALIDATE_TICKETS | SSO_FLAG_SSO_VERIFY_EXTERNAL_CREDS | SSO_FLAG_APP_USES_GROUP_MAPPING | SSO_FLAG_APP_EXTERNAL_NAME_SAME | SSO_FLAG_APP_ALLOW_LOCAL | SSO_FLAG_APP_ADMIN_SAME | SSO_FLAG_APP_CONFIG_STORE | SSO_FLAG_APP_TICKET_TIMEOUT | SSO_FLAG_APP_ADAPTER | SSO_FLAG_SSO_DISABLE_CRED_CACHE | SYNC_FLAG_MASK )

#define	DEFAULT_APP_FLAGS	( SSO_FLAG_SSO_WINDOWS_TO_EXTERNAL | SSO_FLAG_VALIDATE_TICKETS | SSO_FLAG_APP_TICKET_TIMEOUT )

#define	MAPPING_FLAG_MASK_IN	( SSO_FLAG_SSO_WINDOWS_TO_EXTERNAL | SSO_FLAG_SSO_EXTERNAL_TO_WINDOWS | SSO_FLAG_MAPPING_ENABLE_AUDIT )

#define	MAPPING_FLAG_MASK_OUT	( SSO_FLAG_ENABLED | SSO_FLAG_SSO_WINDOWS_TO_EXTERNAL | SSO_FLAG_SSO_EXTERNAL_TO_WINDOWS | SSO_FLAG_SSO_VERIFY_EXTERNAL_CREDS | SSO_FLAG_MAPPING_REQUIRES_WINDOWS_PASSWORD | SSO_FLAG_MAPPING_REQUIRES_EXTERNAL_CREDS | SSO_FLAG_MAPPING_ENABLE_AUDIT )

#define	FIELD_FLAG_MASK	( SSO_FLAG_FIELD_INFO_MASK | SSO_FLAG_FIELD_INFO_SYNC )

#endif _SSO_FLAG

EXTERN_C const IID LIBID_SSOLookupLib;

EXTERN_C const CLSID CLSID_SSOLookup;

#ifdef __cplusplus

class DECLSPEC_UUID("EB638249-7D3A-4AC9-91A2-492F2ED5F7DB")
SSOLookup;
#endif

EXTERN_C const CLSID CLSID_SSOTicket;

#ifdef __cplusplus

class DECLSPEC_UUID("DB8A01BE-D296-4756-909D-D473E0944FB5")
SSOTicket;
#endif
#endif /* __SSOLookupLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long *, BSTR * ); 

unsigned long             __RPC_USER  LPSAFEARRAY_UserSize(     unsigned long *, unsigned long            , LPSAFEARRAY * ); 
unsigned char * __RPC_USER  LPSAFEARRAY_UserMarshal(  unsigned long *, unsigned char *, LPSAFEARRAY * ); 
unsigned char * __RPC_USER  LPSAFEARRAY_UserUnmarshal(unsigned long *, unsigned char *, LPSAFEARRAY * ); 
void                      __RPC_USER  LPSAFEARRAY_UserFree(     unsigned long *, LPSAFEARRAY * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


