

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 6.00.0366 */
/* Compiler settings for ssoadmin.idl:
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

#ifndef __ssoadmin_h__
#define __ssoadmin_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __ISSOAdmin_FWD_DEFINED__
#define __ISSOAdmin_FWD_DEFINED__
typedef interface ISSOAdmin ISSOAdmin;
#endif 	/* __ISSOAdmin_FWD_DEFINED__ */


#ifndef __ISSOAdmin2_FWD_DEFINED__
#define __ISSOAdmin2_FWD_DEFINED__
typedef interface ISSOAdmin2 ISSOAdmin2;
#endif 	/* __ISSOAdmin2_FWD_DEFINED__ */


#ifndef __SSOAdmin_FWD_DEFINED__
#define __SSOAdmin_FWD_DEFINED__

#ifdef __cplusplus
typedef class SSOAdmin SSOAdmin;
#else
typedef struct SSOAdmin SSOAdmin;
#endif /* __cplusplus */

#endif 	/* __SSOAdmin_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 

void * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void * ); 

/* interface __MIDL_itf_ssoadmin_0000 */
/* [local] */ 


/***************************************************************************
         Copyright (c) Microsoft Corporation, All rights reserved.          
***************************************************************************/



extern RPC_IF_HANDLE __MIDL_itf_ssoadmin_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_ssoadmin_0000_v0_0_s_ifspec;

#ifndef __ISSOAdmin_INTERFACE_DEFINED__
#define __ISSOAdmin_INTERFACE_DEFINED__

/* interface ISSOAdmin */
/* [oleautomation][unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_ISSOAdmin;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("C177306C-A7A0-49E1-901E-D2BDFCFF4532")
    ISSOAdmin : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetGlobalInfo( 
            /* [out] */ LONG *plFlags,
            /* [out] */ LONG *plAuditAppDeleteMax,
            /* [out] */ LONG *plAuditMappingDeleteMax,
            /* [out] */ LONG *plAuditNtpLookupMax,
            /* [out] */ LONG *plAuditXpLookupMax,
            /* [out] */ LONG *plTicketTimeout,
            /* [out] */ LONG *plCredCacheTimeout,
            /* [out] */ BSTR *pbstrSecretServer,
            /* [out] */ BSTR *pbstrSSOAdminGroup,
            /* [out] */ BSTR *pbstrAffiliateAppMgrGroup) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE UpdateGlobalInfo( 
            /* [in] */ LONG lFlags,
            /* [in] */ LONG lFlagMask,
            /* [in] */ LONG *plAuditAppDeleteMax,
            /* [in] */ LONG *plAuditMappingDeleteMax,
            /* [in] */ LONG *plAuditNtpLookupMax,
            /* [in] */ LONG *plAuditXpLookupMax,
            /* [in] */ LONG *plTicketTimeout,
            /* [in] */ LONG *plCredCacheTimeout,
            /* [in] */ BSTR bstrSecretServer,
            /* [in] */ BSTR bstrSSOAdminGroup,
            /* [in] */ BSTR bstrAffiliateAppMgrGroup) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE CreateApplication( 
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrDescription,
            /* [in] */ BSTR bstrContactInfo,
            /* [in] */ BSTR bstrUserGroupName,
            /* [in] */ BSTR bstrAdminGroupName,
            /* [in] */ LONG lFlags,
            /* [in] */ LONG lNumFields) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE DeleteApplication( 
            /* [in] */ BSTR bstrApplicationName) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetApplicationInfo( 
            /* [in] */ BSTR bstrApplicationName,
            /* [out] */ BSTR *pbstrDescription,
            /* [out] */ BSTR *pbstrContactInfo,
            /* [out] */ BSTR *pbstrUserGroupName,
            /* [out] */ BSTR *pbstrAdminGroupName,
            /* [out] */ LONG *plFlags,
            /* [out] */ LONG *plNumFields) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE UpdateApplication( 
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrDescription,
            /* [in] */ BSTR bstrContactInfo,
            /* [in] */ BSTR bstrUserGroupName,
            /* [in] */ BSTR bstrAdminGroupName,
            /* [in] */ LONG lFlags,
            /* [in] */ LONG lFlagMask) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE PurgeCacheForApplication( 
            /* [in] */ BSTR bstrApplicationName) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE CreateFieldInfo( 
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrLabel,
            /* [in] */ LONG lFlags) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISSOAdminVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISSOAdmin * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISSOAdmin * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISSOAdmin * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ISSOAdmin * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ISSOAdmin * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ISSOAdmin * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ISSOAdmin * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetGlobalInfo )( 
            ISSOAdmin * This,
            /* [out] */ LONG *plFlags,
            /* [out] */ LONG *plAuditAppDeleteMax,
            /* [out] */ LONG *plAuditMappingDeleteMax,
            /* [out] */ LONG *plAuditNtpLookupMax,
            /* [out] */ LONG *plAuditXpLookupMax,
            /* [out] */ LONG *plTicketTimeout,
            /* [out] */ LONG *plCredCacheTimeout,
            /* [out] */ BSTR *pbstrSecretServer,
            /* [out] */ BSTR *pbstrSSOAdminGroup,
            /* [out] */ BSTR *pbstrAffiliateAppMgrGroup);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *UpdateGlobalInfo )( 
            ISSOAdmin * This,
            /* [in] */ LONG lFlags,
            /* [in] */ LONG lFlagMask,
            /* [in] */ LONG *plAuditAppDeleteMax,
            /* [in] */ LONG *plAuditMappingDeleteMax,
            /* [in] */ LONG *plAuditNtpLookupMax,
            /* [in] */ LONG *plAuditXpLookupMax,
            /* [in] */ LONG *plTicketTimeout,
            /* [in] */ LONG *plCredCacheTimeout,
            /* [in] */ BSTR bstrSecretServer,
            /* [in] */ BSTR bstrSSOAdminGroup,
            /* [in] */ BSTR bstrAffiliateAppMgrGroup);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *CreateApplication )( 
            ISSOAdmin * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrDescription,
            /* [in] */ BSTR bstrContactInfo,
            /* [in] */ BSTR bstrUserGroupName,
            /* [in] */ BSTR bstrAdminGroupName,
            /* [in] */ LONG lFlags,
            /* [in] */ LONG lNumFields);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *DeleteApplication )( 
            ISSOAdmin * This,
            /* [in] */ BSTR bstrApplicationName);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetApplicationInfo )( 
            ISSOAdmin * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [out] */ BSTR *pbstrDescription,
            /* [out] */ BSTR *pbstrContactInfo,
            /* [out] */ BSTR *pbstrUserGroupName,
            /* [out] */ BSTR *pbstrAdminGroupName,
            /* [out] */ LONG *plFlags,
            /* [out] */ LONG *plNumFields);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *UpdateApplication )( 
            ISSOAdmin * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrDescription,
            /* [in] */ BSTR bstrContactInfo,
            /* [in] */ BSTR bstrUserGroupName,
            /* [in] */ BSTR bstrAdminGroupName,
            /* [in] */ LONG lFlags,
            /* [in] */ LONG lFlagMask);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *PurgeCacheForApplication )( 
            ISSOAdmin * This,
            /* [in] */ BSTR bstrApplicationName);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *CreateFieldInfo )( 
            ISSOAdmin * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrLabel,
            /* [in] */ LONG lFlags);
        
        END_INTERFACE
    } ISSOAdminVtbl;

    interface ISSOAdmin
    {
        CONST_VTBL struct ISSOAdminVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISSOAdmin_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ISSOAdmin_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ISSOAdmin_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ISSOAdmin_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define ISSOAdmin_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define ISSOAdmin_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define ISSOAdmin_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define ISSOAdmin_GetGlobalInfo(This,plFlags,plAuditAppDeleteMax,plAuditMappingDeleteMax,plAuditNtpLookupMax,plAuditXpLookupMax,plTicketTimeout,plCredCacheTimeout,pbstrSecretServer,pbstrSSOAdminGroup,pbstrAffiliateAppMgrGroup)	\
    (This)->lpVtbl -> GetGlobalInfo(This,plFlags,plAuditAppDeleteMax,plAuditMappingDeleteMax,plAuditNtpLookupMax,plAuditXpLookupMax,plTicketTimeout,plCredCacheTimeout,pbstrSecretServer,pbstrSSOAdminGroup,pbstrAffiliateAppMgrGroup)

#define ISSOAdmin_UpdateGlobalInfo(This,lFlags,lFlagMask,plAuditAppDeleteMax,plAuditMappingDeleteMax,plAuditNtpLookupMax,plAuditXpLookupMax,plTicketTimeout,plCredCacheTimeout,bstrSecretServer,bstrSSOAdminGroup,bstrAffiliateAppMgrGroup)	\
    (This)->lpVtbl -> UpdateGlobalInfo(This,lFlags,lFlagMask,plAuditAppDeleteMax,plAuditMappingDeleteMax,plAuditNtpLookupMax,plAuditXpLookupMax,plTicketTimeout,plCredCacheTimeout,bstrSecretServer,bstrSSOAdminGroup,bstrAffiliateAppMgrGroup)

#define ISSOAdmin_CreateApplication(This,bstrApplicationName,bstrDescription,bstrContactInfo,bstrUserGroupName,bstrAdminGroupName,lFlags,lNumFields)	\
    (This)->lpVtbl -> CreateApplication(This,bstrApplicationName,bstrDescription,bstrContactInfo,bstrUserGroupName,bstrAdminGroupName,lFlags,lNumFields)

#define ISSOAdmin_DeleteApplication(This,bstrApplicationName)	\
    (This)->lpVtbl -> DeleteApplication(This,bstrApplicationName)

#define ISSOAdmin_GetApplicationInfo(This,bstrApplicationName,pbstrDescription,pbstrContactInfo,pbstrUserGroupName,pbstrAdminGroupName,plFlags,plNumFields)	\
    (This)->lpVtbl -> GetApplicationInfo(This,bstrApplicationName,pbstrDescription,pbstrContactInfo,pbstrUserGroupName,pbstrAdminGroupName,plFlags,plNumFields)

#define ISSOAdmin_UpdateApplication(This,bstrApplicationName,bstrDescription,bstrContactInfo,bstrUserGroupName,bstrAdminGroupName,lFlags,lFlagMask)	\
    (This)->lpVtbl -> UpdateApplication(This,bstrApplicationName,bstrDescription,bstrContactInfo,bstrUserGroupName,bstrAdminGroupName,lFlags,lFlagMask)

#define ISSOAdmin_PurgeCacheForApplication(This,bstrApplicationName)	\
    (This)->lpVtbl -> PurgeCacheForApplication(This,bstrApplicationName)

#define ISSOAdmin_CreateFieldInfo(This,bstrApplicationName,bstrLabel,lFlags)	\
    (This)->lpVtbl -> CreateFieldInfo(This,bstrApplicationName,bstrLabel,lFlags)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOAdmin_GetGlobalInfo_Proxy( 
    ISSOAdmin * This,
    /* [out] */ LONG *plFlags,
    /* [out] */ LONG *plAuditAppDeleteMax,
    /* [out] */ LONG *plAuditMappingDeleteMax,
    /* [out] */ LONG *plAuditNtpLookupMax,
    /* [out] */ LONG *plAuditXpLookupMax,
    /* [out] */ LONG *plTicketTimeout,
    /* [out] */ LONG *plCredCacheTimeout,
    /* [out] */ BSTR *pbstrSecretServer,
    /* [out] */ BSTR *pbstrSSOAdminGroup,
    /* [out] */ BSTR *pbstrAffiliateAppMgrGroup);


void __RPC_STUB ISSOAdmin_GetGlobalInfo_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOAdmin_UpdateGlobalInfo_Proxy( 
    ISSOAdmin * This,
    /* [in] */ LONG lFlags,
    /* [in] */ LONG lFlagMask,
    /* [in] */ LONG *plAuditAppDeleteMax,
    /* [in] */ LONG *plAuditMappingDeleteMax,
    /* [in] */ LONG *plAuditNtpLookupMax,
    /* [in] */ LONG *plAuditXpLookupMax,
    /* [in] */ LONG *plTicketTimeout,
    /* [in] */ LONG *plCredCacheTimeout,
    /* [in] */ BSTR bstrSecretServer,
    /* [in] */ BSTR bstrSSOAdminGroup,
    /* [in] */ BSTR bstrAffiliateAppMgrGroup);


void __RPC_STUB ISSOAdmin_UpdateGlobalInfo_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOAdmin_CreateApplication_Proxy( 
    ISSOAdmin * This,
    /* [in] */ BSTR bstrApplicationName,
    /* [in] */ BSTR bstrDescription,
    /* [in] */ BSTR bstrContactInfo,
    /* [in] */ BSTR bstrUserGroupName,
    /* [in] */ BSTR bstrAdminGroupName,
    /* [in] */ LONG lFlags,
    /* [in] */ LONG lNumFields);


void __RPC_STUB ISSOAdmin_CreateApplication_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOAdmin_DeleteApplication_Proxy( 
    ISSOAdmin * This,
    /* [in] */ BSTR bstrApplicationName);


void __RPC_STUB ISSOAdmin_DeleteApplication_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOAdmin_GetApplicationInfo_Proxy( 
    ISSOAdmin * This,
    /* [in] */ BSTR bstrApplicationName,
    /* [out] */ BSTR *pbstrDescription,
    /* [out] */ BSTR *pbstrContactInfo,
    /* [out] */ BSTR *pbstrUserGroupName,
    /* [out] */ BSTR *pbstrAdminGroupName,
    /* [out] */ LONG *plFlags,
    /* [out] */ LONG *plNumFields);


void __RPC_STUB ISSOAdmin_GetApplicationInfo_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOAdmin_UpdateApplication_Proxy( 
    ISSOAdmin * This,
    /* [in] */ BSTR bstrApplicationName,
    /* [in] */ BSTR bstrDescription,
    /* [in] */ BSTR bstrContactInfo,
    /* [in] */ BSTR bstrUserGroupName,
    /* [in] */ BSTR bstrAdminGroupName,
    /* [in] */ LONG lFlags,
    /* [in] */ LONG lFlagMask);


void __RPC_STUB ISSOAdmin_UpdateApplication_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOAdmin_PurgeCacheForApplication_Proxy( 
    ISSOAdmin * This,
    /* [in] */ BSTR bstrApplicationName);


void __RPC_STUB ISSOAdmin_PurgeCacheForApplication_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOAdmin_CreateFieldInfo_Proxy( 
    ISSOAdmin * This,
    /* [in] */ BSTR bstrApplicationName,
    /* [in] */ BSTR bstrLabel,
    /* [in] */ LONG lFlags);


void __RPC_STUB ISSOAdmin_CreateFieldInfo_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ISSOAdmin_INTERFACE_DEFINED__ */


#ifndef __ISSOAdmin2_INTERFACE_DEFINED__
#define __ISSOAdmin2_INTERFACE_DEFINED__

/* interface ISSOAdmin2 */
/* [oleautomation][unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_ISSOAdmin2;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("CB878AF5-9BE9-4451-9492-9D228A3B65E7")
    ISSOAdmin2 : public ISSOAdmin
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetApplicationInfo2( 
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ IPropertyBag *pAppInfoProps) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE UpdateApplication2( 
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ IPropertyBag *pAppInfoProps) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISSOAdmin2Vtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISSOAdmin2 * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISSOAdmin2 * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISSOAdmin2 * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ISSOAdmin2 * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ISSOAdmin2 * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ISSOAdmin2 * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ISSOAdmin2 * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetGlobalInfo )( 
            ISSOAdmin2 * This,
            /* [out] */ LONG *plFlags,
            /* [out] */ LONG *plAuditAppDeleteMax,
            /* [out] */ LONG *plAuditMappingDeleteMax,
            /* [out] */ LONG *plAuditNtpLookupMax,
            /* [out] */ LONG *plAuditXpLookupMax,
            /* [out] */ LONG *plTicketTimeout,
            /* [out] */ LONG *plCredCacheTimeout,
            /* [out] */ BSTR *pbstrSecretServer,
            /* [out] */ BSTR *pbstrSSOAdminGroup,
            /* [out] */ BSTR *pbstrAffiliateAppMgrGroup);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *UpdateGlobalInfo )( 
            ISSOAdmin2 * This,
            /* [in] */ LONG lFlags,
            /* [in] */ LONG lFlagMask,
            /* [in] */ LONG *plAuditAppDeleteMax,
            /* [in] */ LONG *plAuditMappingDeleteMax,
            /* [in] */ LONG *plAuditNtpLookupMax,
            /* [in] */ LONG *plAuditXpLookupMax,
            /* [in] */ LONG *plTicketTimeout,
            /* [in] */ LONG *plCredCacheTimeout,
            /* [in] */ BSTR bstrSecretServer,
            /* [in] */ BSTR bstrSSOAdminGroup,
            /* [in] */ BSTR bstrAffiliateAppMgrGroup);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *CreateApplication )( 
            ISSOAdmin2 * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrDescription,
            /* [in] */ BSTR bstrContactInfo,
            /* [in] */ BSTR bstrUserGroupName,
            /* [in] */ BSTR bstrAdminGroupName,
            /* [in] */ LONG lFlags,
            /* [in] */ LONG lNumFields);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *DeleteApplication )( 
            ISSOAdmin2 * This,
            /* [in] */ BSTR bstrApplicationName);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetApplicationInfo )( 
            ISSOAdmin2 * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [out] */ BSTR *pbstrDescription,
            /* [out] */ BSTR *pbstrContactInfo,
            /* [out] */ BSTR *pbstrUserGroupName,
            /* [out] */ BSTR *pbstrAdminGroupName,
            /* [out] */ LONG *plFlags,
            /* [out] */ LONG *plNumFields);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *UpdateApplication )( 
            ISSOAdmin2 * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrDescription,
            /* [in] */ BSTR bstrContactInfo,
            /* [in] */ BSTR bstrUserGroupName,
            /* [in] */ BSTR bstrAdminGroupName,
            /* [in] */ LONG lFlags,
            /* [in] */ LONG lFlagMask);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *PurgeCacheForApplication )( 
            ISSOAdmin2 * This,
            /* [in] */ BSTR bstrApplicationName);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *CreateFieldInfo )( 
            ISSOAdmin2 * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ BSTR bstrLabel,
            /* [in] */ LONG lFlags);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetApplicationInfo2 )( 
            ISSOAdmin2 * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ IPropertyBag *pAppInfoProps);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *UpdateApplication2 )( 
            ISSOAdmin2 * This,
            /* [in] */ BSTR bstrApplicationName,
            /* [in] */ IPropertyBag *pAppInfoProps);
        
        END_INTERFACE
    } ISSOAdmin2Vtbl;

    interface ISSOAdmin2
    {
        CONST_VTBL struct ISSOAdmin2Vtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISSOAdmin2_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ISSOAdmin2_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ISSOAdmin2_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ISSOAdmin2_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define ISSOAdmin2_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define ISSOAdmin2_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define ISSOAdmin2_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define ISSOAdmin2_GetGlobalInfo(This,plFlags,plAuditAppDeleteMax,plAuditMappingDeleteMax,plAuditNtpLookupMax,plAuditXpLookupMax,plTicketTimeout,plCredCacheTimeout,pbstrSecretServer,pbstrSSOAdminGroup,pbstrAffiliateAppMgrGroup)	\
    (This)->lpVtbl -> GetGlobalInfo(This,plFlags,plAuditAppDeleteMax,plAuditMappingDeleteMax,plAuditNtpLookupMax,plAuditXpLookupMax,plTicketTimeout,plCredCacheTimeout,pbstrSecretServer,pbstrSSOAdminGroup,pbstrAffiliateAppMgrGroup)

#define ISSOAdmin2_UpdateGlobalInfo(This,lFlags,lFlagMask,plAuditAppDeleteMax,plAuditMappingDeleteMax,plAuditNtpLookupMax,plAuditXpLookupMax,plTicketTimeout,plCredCacheTimeout,bstrSecretServer,bstrSSOAdminGroup,bstrAffiliateAppMgrGroup)	\
    (This)->lpVtbl -> UpdateGlobalInfo(This,lFlags,lFlagMask,plAuditAppDeleteMax,plAuditMappingDeleteMax,plAuditNtpLookupMax,plAuditXpLookupMax,plTicketTimeout,plCredCacheTimeout,bstrSecretServer,bstrSSOAdminGroup,bstrAffiliateAppMgrGroup)

#define ISSOAdmin2_CreateApplication(This,bstrApplicationName,bstrDescription,bstrContactInfo,bstrUserGroupName,bstrAdminGroupName,lFlags,lNumFields)	\
    (This)->lpVtbl -> CreateApplication(This,bstrApplicationName,bstrDescription,bstrContactInfo,bstrUserGroupName,bstrAdminGroupName,lFlags,lNumFields)

#define ISSOAdmin2_DeleteApplication(This,bstrApplicationName)	\
    (This)->lpVtbl -> DeleteApplication(This,bstrApplicationName)

#define ISSOAdmin2_GetApplicationInfo(This,bstrApplicationName,pbstrDescription,pbstrContactInfo,pbstrUserGroupName,pbstrAdminGroupName,plFlags,plNumFields)	\
    (This)->lpVtbl -> GetApplicationInfo(This,bstrApplicationName,pbstrDescription,pbstrContactInfo,pbstrUserGroupName,pbstrAdminGroupName,plFlags,plNumFields)

#define ISSOAdmin2_UpdateApplication(This,bstrApplicationName,bstrDescription,bstrContactInfo,bstrUserGroupName,bstrAdminGroupName,lFlags,lFlagMask)	\
    (This)->lpVtbl -> UpdateApplication(This,bstrApplicationName,bstrDescription,bstrContactInfo,bstrUserGroupName,bstrAdminGroupName,lFlags,lFlagMask)

#define ISSOAdmin2_PurgeCacheForApplication(This,bstrApplicationName)	\
    (This)->lpVtbl -> PurgeCacheForApplication(This,bstrApplicationName)

#define ISSOAdmin2_CreateFieldInfo(This,bstrApplicationName,bstrLabel,lFlags)	\
    (This)->lpVtbl -> CreateFieldInfo(This,bstrApplicationName,bstrLabel,lFlags)


#define ISSOAdmin2_GetApplicationInfo2(This,bstrApplicationName,pAppInfoProps)	\
    (This)->lpVtbl -> GetApplicationInfo2(This,bstrApplicationName,pAppInfoProps)

#define ISSOAdmin2_UpdateApplication2(This,bstrApplicationName,pAppInfoProps)	\
    (This)->lpVtbl -> UpdateApplication2(This,bstrApplicationName,pAppInfoProps)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOAdmin2_GetApplicationInfo2_Proxy( 
    ISSOAdmin2 * This,
    /* [in] */ BSTR bstrApplicationName,
    /* [in] */ IPropertyBag *pAppInfoProps);


void __RPC_STUB ISSOAdmin2_GetApplicationInfo2_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ISSOAdmin2_UpdateApplication2_Proxy( 
    ISSOAdmin2 * This,
    /* [in] */ BSTR bstrApplicationName,
    /* [in] */ IPropertyBag *pAppInfoProps);


void __RPC_STUB ISSOAdmin2_UpdateApplication2_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ISSOAdmin2_INTERFACE_DEFINED__ */



#ifndef __SSOAdminLib_LIBRARY_DEFINED__
#define __SSOAdminLib_LIBRARY_DEFINED__

/* library SSOAdminLib */
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

EXTERN_C const IID LIBID_SSOAdminLib;

EXTERN_C const CLSID CLSID_SSOAdmin;

#ifdef __cplusplus

class DECLSPEC_UUID("C0F73BB9-A69D-434F-9D9C-7D5EC49E4F12")
SSOAdmin;
#endif
#endif /* __SSOAdminLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long *, BSTR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


