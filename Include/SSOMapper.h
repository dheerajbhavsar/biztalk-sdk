

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 6.00.0366 */
/* Compiler settings for ssomapper.idl:
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

#ifndef __ssomapper_h__
#define __ssomapper_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __ISSOMapper_FWD_DEFINED__
#define __ISSOMapper_FWD_DEFINED__
typedef interface ISSOMapper ISSOMapper;
#endif 	/* __ISSOMapper_FWD_DEFINED__ */


#ifndef __ISSOMapper2_FWD_DEFINED__
#define __ISSOMapper2_FWD_DEFINED__
typedef interface ISSOMapper2 ISSOMapper2;
#endif 	/* __ISSOMapper2_FWD_DEFINED__ */


#ifndef __ISSOMapping_FWD_DEFINED__
#define __ISSOMapping_FWD_DEFINED__
typedef interface ISSOMapping ISSOMapping;
#endif 	/* __ISSOMapping_FWD_DEFINED__ */


#ifndef __SSOMapper_FWD_DEFINED__
#define __SSOMapper_FWD_DEFINED__

#ifdef __cplusplus
typedef class SSOMapper SSOMapper;
#else
typedef struct SSOMapper SSOMapper;
#endif /* __cplusplus */

#endif 	/* __SSOMapper_FWD_DEFINED__ */


#ifndef __SSOMapping_FWD_DEFINED__
#define __SSOMapping_FWD_DEFINED__

#ifdef __cplusplus
typedef class SSOMapping SSOMapping;
#else
typedef struct SSOMapping SSOMapping;
#endif /* __cplusplus */

#endif 	/* __SSOMapping_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 

void * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void * ); 

/* interface __MIDL_itf_ssomapper_0000 */
/* [local] */ 


/***************************************************************************
         Copyright (c) Microsoft Corporation, All rights reserved.          
***************************************************************************/



extern RPC_IF_HANDLE __MIDL_itf_ssomapper_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_ssomapper_0000_v0_0_s_ifspec;

#ifndef __ISSOMapper_INTERFACE_DEFINED__
#define __ISSOMapper_INTERFACE_DEFINED__

/* interface ISSOMapper */
/* [oleautomation][unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_ISSOMapper;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("A146E1CC-043D-4D33-9B82-16392AD36E7D")
    ISSOMapper : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetMappingsForWindowsUser( 
            /* [in] */ BSTR bstrWindowsDomainName,
            /* [in] */ BSTR bstrWindowsUserName,
            /* [in] */ BSTR bstrApplicationName,
            /* [retval][out] */ SAFEARRAY * *ppsaMappings) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetMappingsForExternalUser( 
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrExternalUserName,
            /* [retval][out] */ SAFEARRAY * *ppsaMappings) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SetWindowsPassword( 
            /* [in] */ BSTR bstrWindowsPassword) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SetExternalCredentials( 
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrExternalUserName,
            /* [in] */ SAFEARRAY * *ppsaExternalCredentials) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetApplications( 
            /* [out] */ SAFEARRAY * *ppsaApplications,
            /* [out] */ SAFEARRAY * *ppsaDescriptions,
            /* [out] */ SAFEARRAY * *ppsaContactInfo) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetFieldInfo( 
            /* [in] */ BSTR bstrApplicationName,
            /* [out] */ SAFEARRAY * *ppsaLabels,
            /* [out] */ SAFEARRAY * *ppsaFlags) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISSOMapperVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISSOMapper * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISSOMapper * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISSOMapper * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ISSOMapper * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ISSOMapper * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ISSOMapper * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ISSOMapper * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetMappingsForWindowsUser )( 
            ISSOMapper * This,
            /* [in] */ BSTR bstrWindowsDomainName,
            /* [in] */ BSTR bstrWindowsUserName,
            /* [in] */ BSTR bstrApplicationName,
            /* [retval][out] */ SAFEARRAY * *ppsaMappings);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetMappingsForExternalUser )( 
            ISSOMapper * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrExternalUserName,
            /* [retval][out] */ SAFEARRAY * *ppsaMappings);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SetWindowsPassword )( 
            ISSOMapper * This,
            /* [in] */ BSTR bstrWindowsPassword);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SetExternalCredentials )( 
            ISSOMapper * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrExternalUserName,
            /* [in] */ SAFEARRAY * *ppsaExternalCredentials);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetApplications )( 
            ISSOMapper * This,
            /* [out] */ SAFEARRAY * *ppsaApplications,
            /* [out] */ SAFEARRAY * *ppsaDescriptions,
            /* [out] */ SAFEARRAY * *ppsaContactInfo);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetFieldInfo )( 
            ISSOMapper * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [out] */ SAFEARRAY * *ppsaLabels,
            /* [out] */ SAFEARRAY * *ppsaFlags);
        
        END_INTERFACE
    } ISSOMapperVtbl;

    interface ISSOMapper
    {
        CONST_VTBL struct ISSOMapperVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISSOMapper_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ISSOMapper_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ISSOMapper_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ISSOMapper_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define ISSOMapper_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define ISSOMapper_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define ISSOMapper_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define ISSOMapper_GetMappingsForWindowsUser(This,bstrWindowsDomainName,bstrWindowsUserName,bstrApplicationName,ppsaMappings)	\
    (This)->lpVtbl -> GetMappingsForWindowsUser(This,bstrWindowsDomainName,bstrWindowsUserName,bstrApplicationName,ppsaMappings)

#define ISSOMapper_GetMappingsForExternalUser(This,bstrApplicationName,bstrExternalUserName,ppsaMappings)	\
    (This)->lpVtbl -> GetMappingsForExternalUser(This,bstrApplicationName,bstrExternalUserName,ppsaMappings)

#define ISSOMapper_SetWindowsPassword(This,bstrWindowsPassword)	\
    (This)->lpVtbl -> SetWindowsPassword(This,bstrWindowsPassword)

#define ISSOMapper_SetExternalCredentials(This,bstrApplicationName,bstrExternalUserName,ppsaExternalCredentials)	\
    (This)->lpVtbl -> SetExternalCredentials(This,bstrApplicationName,bstrExternalUserName,ppsaExternalCredentials)

#define ISSOMapper_GetApplications(This,ppsaApplications,ppsaDescriptions,ppsaContactInfo)	\
    (This)->lpVtbl -> GetApplications(This,ppsaApplications,ppsaDescriptions,ppsaContactInfo)

#define ISSOMapper_GetFieldInfo(This,bstrApplicationName,ppsaLabels,ppsaFlags)	\
    (This)->lpVtbl -> GetFieldInfo(This,bstrApplicationName,ppsaLabels,ppsaFlags)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOMapper_GetMappingsForWindowsUser_Proxy( 
    ISSOMapper * This,
    /* [in] */ BSTR bstrWindowsDomainName,
    /* [in] */ BSTR bstrWindowsUserName,
    /* [in] */ BSTR bstrApplicationName,
    /* [retval][out] */ SAFEARRAY * *ppsaMappings);


void __RPC_STUB ISSOMapper_GetMappingsForWindowsUser_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOMapper_GetMappingsForExternalUser_Proxy( 
    ISSOMapper * This,
    /* [in] */ BSTR bstrApplicationName,
    /* [in] */ BSTR bstrExternalUserName,
    /* [retval][out] */ SAFEARRAY * *ppsaMappings);


void __RPC_STUB ISSOMapper_GetMappingsForExternalUser_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOMapper_SetWindowsPassword_Proxy( 
    ISSOMapper * This,
    /* [in] */ BSTR bstrWindowsPassword);


void __RPC_STUB ISSOMapper_SetWindowsPassword_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOMapper_SetExternalCredentials_Proxy( 
    ISSOMapper * This,
    /* [in] */ BSTR bstrApplicationName,
    /* [in] */ BSTR bstrExternalUserName,
    /* [in] */ SAFEARRAY * *ppsaExternalCredentials);


void __RPC_STUB ISSOMapper_SetExternalCredentials_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOMapper_GetApplications_Proxy( 
    ISSOMapper * This,
    /* [out] */ SAFEARRAY * *ppsaApplications,
    /* [out] */ SAFEARRAY * *ppsaDescriptions,
    /* [out] */ SAFEARRAY * *ppsaContactInfo);


void __RPC_STUB ISSOMapper_GetApplications_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOMapper_GetFieldInfo_Proxy( 
    ISSOMapper * This,
    /* [in] */ BSTR bstrApplicationName,
    /* [out] */ SAFEARRAY * *ppsaLabels,
    /* [out] */ SAFEARRAY * *ppsaFlags);


void __RPC_STUB ISSOMapper_GetFieldInfo_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ISSOMapper_INTERFACE_DEFINED__ */


#ifndef __ISSOMapper2_INTERFACE_DEFINED__
#define __ISSOMapper2_INTERFACE_DEFINED__

/* interface ISSOMapper2 */
/* [oleautomation][unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_ISSOMapper2;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("2D56DCAB-FA6C-435b-835A-B135177DE8B0")
    ISSOMapper2 : public ISSOMapper
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetApplications2( 
            /* [out] */ SAFEARRAY * *ppsaApplications,
            /* [out] */ SAFEARRAY * *ppsaDescriptions,
            /* [out] */ SAFEARRAY * *ppsaContactInfo,
            /* [out] */ SAFEARRAY * *ppsaUserAccounts,
            /* [out] */ SAFEARRAY * *ppsaAdminAccounts,
            /* [out] */ SAFEARRAY * *ppsaFlags) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISSOMapper2Vtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISSOMapper2 * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISSOMapper2 * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISSOMapper2 * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ISSOMapper2 * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ISSOMapper2 * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ISSOMapper2 * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ISSOMapper2 * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetMappingsForWindowsUser )( 
            ISSOMapper2 * This,
            /* [in] */ BSTR bstrWindowsDomainName,
            /* [in] */ BSTR bstrWindowsUserName,
            /* [in] */ BSTR bstrApplicationName,
            /* [retval][out] */ SAFEARRAY * *ppsaMappings);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetMappingsForExternalUser )( 
            ISSOMapper2 * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrExternalUserName,
            /* [retval][out] */ SAFEARRAY * *ppsaMappings);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SetWindowsPassword )( 
            ISSOMapper2 * This,
            /* [in] */ BSTR bstrWindowsPassword);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SetExternalCredentials )( 
            ISSOMapper2 * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrExternalUserName,
            /* [in] */ SAFEARRAY * *ppsaExternalCredentials);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetApplications )( 
            ISSOMapper2 * This,
            /* [out] */ SAFEARRAY * *ppsaApplications,
            /* [out] */ SAFEARRAY * *ppsaDescriptions,
            /* [out] */ SAFEARRAY * *ppsaContactInfo);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetFieldInfo )( 
            ISSOMapper2 * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [out] */ SAFEARRAY * *ppsaLabels,
            /* [out] */ SAFEARRAY * *ppsaFlags);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetApplications2 )( 
            ISSOMapper2 * This,
            /* [out] */ SAFEARRAY * *ppsaApplications,
            /* [out] */ SAFEARRAY * *ppsaDescriptions,
            /* [out] */ SAFEARRAY * *ppsaContactInfo,
            /* [out] */ SAFEARRAY * *ppsaUserAccounts,
            /* [out] */ SAFEARRAY * *ppsaAdminAccounts,
            /* [out] */ SAFEARRAY * *ppsaFlags);
        
        END_INTERFACE
    } ISSOMapper2Vtbl;

    interface ISSOMapper2
    {
        CONST_VTBL struct ISSOMapper2Vtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISSOMapper2_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ISSOMapper2_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ISSOMapper2_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ISSOMapper2_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define ISSOMapper2_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define ISSOMapper2_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define ISSOMapper2_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define ISSOMapper2_GetMappingsForWindowsUser(This,bstrWindowsDomainName,bstrWindowsUserName,bstrApplicationName,ppsaMappings)	\
    (This)->lpVtbl -> GetMappingsForWindowsUser(This,bstrWindowsDomainName,bstrWindowsUserName,bstrApplicationName,ppsaMappings)

#define ISSOMapper2_GetMappingsForExternalUser(This,bstrApplicationName,bstrExternalUserName,ppsaMappings)	\
    (This)->lpVtbl -> GetMappingsForExternalUser(This,bstrApplicationName,bstrExternalUserName,ppsaMappings)

#define ISSOMapper2_SetWindowsPassword(This,bstrWindowsPassword)	\
    (This)->lpVtbl -> SetWindowsPassword(This,bstrWindowsPassword)

#define ISSOMapper2_SetExternalCredentials(This,bstrApplicationName,bstrExternalUserName,ppsaExternalCredentials)	\
    (This)->lpVtbl -> SetExternalCredentials(This,bstrApplicationName,bstrExternalUserName,ppsaExternalCredentials)

#define ISSOMapper2_GetApplications(This,ppsaApplications,ppsaDescriptions,ppsaContactInfo)	\
    (This)->lpVtbl -> GetApplications(This,ppsaApplications,ppsaDescriptions,ppsaContactInfo)

#define ISSOMapper2_GetFieldInfo(This,bstrApplicationName,ppsaLabels,ppsaFlags)	\
    (This)->lpVtbl -> GetFieldInfo(This,bstrApplicationName,ppsaLabels,ppsaFlags)


#define ISSOMapper2_GetApplications2(This,ppsaApplications,ppsaDescriptions,ppsaContactInfo,ppsaUserAccounts,ppsaAdminAccounts,ppsaFlags)	\
    (This)->lpVtbl -> GetApplications2(This,ppsaApplications,ppsaDescriptions,ppsaContactInfo,ppsaUserAccounts,ppsaAdminAccounts,ppsaFlags)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOMapper2_GetApplications2_Proxy( 
    ISSOMapper2 * This,
    /* [out] */ SAFEARRAY * *ppsaApplications,
    /* [out] */ SAFEARRAY * *ppsaDescriptions,
    /* [out] */ SAFEARRAY * *ppsaContactInfo,
    /* [out] */ SAFEARRAY * *ppsaUserAccounts,
    /* [out] */ SAFEARRAY * *ppsaAdminAccounts,
    /* [out] */ SAFEARRAY * *ppsaFlags);


void __RPC_STUB ISSOMapper2_GetApplications2_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ISSOMapper2_INTERFACE_DEFINED__ */


#ifndef __ISSOMapping_INTERFACE_DEFINED__
#define __ISSOMapping_INTERFACE_DEFINED__

/* interface ISSOMapping */
/* [oleautomation][unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_ISSOMapping;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("CB6B05A8-B806-43CC-B441-9E5B5EFB881C")
    ISSOMapping : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Create( 
            /* [in] */ LONG lFlags) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Delete( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Enable( 
            /* [in] */ LONG lFlags) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Disable( void) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_WindowsDomainName( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_WindowsDomainName( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_WindowsUserName( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_WindowsUserName( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ApplicationName( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_ApplicationName( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ExternalUserName( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_ExternalUserName( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Flags( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Flags( 
            /* [in] */ LONG newVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISSOMappingVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISSOMapping * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISSOMapping * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISSOMapping * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ISSOMapping * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ISSOMapping * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ISSOMapping * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ISSOMapping * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Create )( 
            ISSOMapping * This,
            /* [in] */ LONG lFlags);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Delete )( 
            ISSOMapping * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Enable )( 
            ISSOMapping * This,
            /* [in] */ LONG lFlags);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Disable )( 
            ISSOMapping * This);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_WindowsDomainName )( 
            ISSOMapping * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_WindowsDomainName )( 
            ISSOMapping * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_WindowsUserName )( 
            ISSOMapping * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_WindowsUserName )( 
            ISSOMapping * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ApplicationName )( 
            ISSOMapping * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ApplicationName )( 
            ISSOMapping * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ExternalUserName )( 
            ISSOMapping * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ExternalUserName )( 
            ISSOMapping * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Flags )( 
            ISSOMapping * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Flags )( 
            ISSOMapping * This,
            /* [in] */ LONG newVal);
        
        END_INTERFACE
    } ISSOMappingVtbl;

    interface ISSOMapping
    {
        CONST_VTBL struct ISSOMappingVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISSOMapping_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ISSOMapping_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ISSOMapping_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ISSOMapping_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define ISSOMapping_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define ISSOMapping_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define ISSOMapping_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define ISSOMapping_Create(This,lFlags)	\
    (This)->lpVtbl -> Create(This,lFlags)

#define ISSOMapping_Delete(This)	\
    (This)->lpVtbl -> Delete(This)

#define ISSOMapping_Enable(This,lFlags)	\
    (This)->lpVtbl -> Enable(This,lFlags)

#define ISSOMapping_Disable(This)	\
    (This)->lpVtbl -> Disable(This)

#define ISSOMapping_get_WindowsDomainName(This,pVal)	\
    (This)->lpVtbl -> get_WindowsDomainName(This,pVal)

#define ISSOMapping_put_WindowsDomainName(This,newVal)	\
    (This)->lpVtbl -> put_WindowsDomainName(This,newVal)

#define ISSOMapping_get_WindowsUserName(This,pVal)	\
    (This)->lpVtbl -> get_WindowsUserName(This,pVal)

#define ISSOMapping_put_WindowsUserName(This,newVal)	\
    (This)->lpVtbl -> put_WindowsUserName(This,newVal)

#define ISSOMapping_get_ApplicationName(This,pVal)	\
    (This)->lpVtbl -> get_ApplicationName(This,pVal)

#define ISSOMapping_put_ApplicationName(This,newVal)	\
    (This)->lpVtbl -> put_ApplicationName(This,newVal)

#define ISSOMapping_get_ExternalUserName(This,pVal)	\
    (This)->lpVtbl -> get_ExternalUserName(This,pVal)

#define ISSOMapping_put_ExternalUserName(This,newVal)	\
    (This)->lpVtbl -> put_ExternalUserName(This,newVal)

#define ISSOMapping_get_Flags(This,pVal)	\
    (This)->lpVtbl -> get_Flags(This,pVal)

#define ISSOMapping_put_Flags(This,newVal)	\
    (This)->lpVtbl -> put_Flags(This,newVal)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOMapping_Create_Proxy( 
    ISSOMapping * This,
    /* [in] */ LONG lFlags);


void __RPC_STUB ISSOMapping_Create_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOMapping_Delete_Proxy( 
    ISSOMapping * This);


void __RPC_STUB ISSOMapping_Delete_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOMapping_Enable_Proxy( 
    ISSOMapping * This,
    /* [in] */ LONG lFlags);


void __RPC_STUB ISSOMapping_Enable_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOMapping_Disable_Proxy( 
    ISSOMapping * This);


void __RPC_STUB ISSOMapping_Disable_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE ISSOMapping_get_WindowsDomainName_Proxy( 
    ISSOMapping * This,
    /* [retval][out] */ BSTR *pVal);


void __RPC_STUB ISSOMapping_get_WindowsDomainName_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE ISSOMapping_put_WindowsDomainName_Proxy( 
    ISSOMapping * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB ISSOMapping_put_WindowsDomainName_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE ISSOMapping_get_WindowsUserName_Proxy( 
    ISSOMapping * This,
    /* [retval][out] */ BSTR *pVal);


void __RPC_STUB ISSOMapping_get_WindowsUserName_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE ISSOMapping_put_WindowsUserName_Proxy( 
    ISSOMapping * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB ISSOMapping_put_WindowsUserName_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE ISSOMapping_get_ApplicationName_Proxy( 
    ISSOMapping * This,
    /* [retval][out] */ BSTR *pVal);


void __RPC_STUB ISSOMapping_get_ApplicationName_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE ISSOMapping_put_ApplicationName_Proxy( 
    ISSOMapping * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB ISSOMapping_put_ApplicationName_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE ISSOMapping_get_ExternalUserName_Proxy( 
    ISSOMapping * This,
    /* [retval][out] */ BSTR *pVal);


void __RPC_STUB ISSOMapping_get_ExternalUserName_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE ISSOMapping_put_ExternalUserName_Proxy( 
    ISSOMapping * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB ISSOMapping_put_ExternalUserName_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE ISSOMapping_get_Flags_Proxy( 
    ISSOMapping * This,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB ISSOMapping_get_Flags_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE ISSOMapping_put_Flags_Proxy( 
    ISSOMapping * This,
    /* [in] */ LONG newVal);


void __RPC_STUB ISSOMapping_put_Flags_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ISSOMapping_INTERFACE_DEFINED__ */



#ifndef __SSOMapperLib_LIBRARY_DEFINED__
#define __SSOMapperLib_LIBRARY_DEFINED__

/* library SSOMapperLib */
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

EXTERN_C const IID LIBID_SSOMapperLib;

EXTERN_C const CLSID CLSID_SSOMapper;

#ifdef __cplusplus

class DECLSPEC_UUID("EFB65A89-B1D6-485A-92EE-1E3CC591D56C")
SSOMapper;
#endif

EXTERN_C const CLSID CLSID_SSOMapping;

#ifdef __cplusplus

class DECLSPEC_UUID("78CC010D-67A0-4A54-A3E9-2905F0D80B75")
SSOMapping;
#endif
#endif /* __SSOMapperLib_LIBRARY_DEFINED__ */

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


