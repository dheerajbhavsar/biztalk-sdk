using System;
using System.Reflection;
using System.Globalization;
using System.Diagnostics;
using System.Security.Principal;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Solutions.Btarn.CertWizard
{
    /// <summary>
    /// Certificates class
    /// </summary>
    public sealed class PrivateKeyManager
    {
        private PrivateKeyManager()
        {
        }

        #region Imports
        /// <summary>
        /// Logins specified user
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1414:MarkBooleanPInvokeArgumentsWithMarshalAs"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "1"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "2"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "0"), DllImport("advapi32.dll")]
        static extern bool LogonUser(String username, String domain, String password, int logonType,
            int logonProvider, out int token);

        /// <summary>
        /// Opens certificate store
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "4"), DllImport("crypt32.dll")]
        static extern IntPtr CertOpenStore(IntPtr storeProvider, int encodingType, IntPtr cryptoProvider, int flags,
            string storeName);

        /// <summary>
        /// Closes certificate store
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1414:MarkBooleanPInvokeArgumentsWithMarshalAs"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("crypt32.dll")]
        static extern bool CertCloseStore(IntPtr storeHandle, int flags);

        /// <summary>
        /// Finds certificate in store
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("crypt32.dll")]
        static extern IntPtr CertFindCertificateInStore(IntPtr storeHandle, int encoding, int flags, int findType,
            CRYPT_DATA_BLOB cryptBlob, IntPtr previousContext);

        /// <summary>
        /// Enumerates certificates in store
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("crypt32.dll")]
        static extern IntPtr CertEnumCertificatesInStore(IntPtr storeHandle, IntPtr previousContext);

        /// <summary>
        /// Deletes certificate from store
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1414:MarkBooleanPInvokeArgumentsWithMarshalAs"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("crypt32.dll")]
        static extern bool CertDeleteCertificateFromStore(IntPtr certificateContext);

        /// <summary>
        /// Adds certificate context to store
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1414:MarkBooleanPInvokeArgumentsWithMarshalAs"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("crypt32.dll")]
        static extern bool CertAddCertificateContextToStore(IntPtr storeHandle, IntPtr certificateContext, int flags, IntPtr disp);

        /// <summary>
        /// Imports certificate store from the PFX file
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("crypt32.dll")]
        static extern IntPtr PFXImportCertStore(CRYPT_DATA_BLOB buffer, [MarshalAs(UnmanagedType.LPWStr)]string password, int flags);

        /// <summary>
        /// Exports certificate store to the PFX file
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1414:MarkBooleanPInvokeArgumentsWithMarshalAs"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("crypt32.dll")]
        static extern bool PFXExportCertStore(IntPtr storeHandle, CRYPT_DATA_BLOB buffer, [MarshalAs(UnmanagedType.LPWStr)]string password, int flags);
        #endregion

        #region Data structures, constants and enumerations
        /// <summary>
        /// CryptoAPI parameters
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "KEYEXCHANGE")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        internal const int AT_KEYEXCHANGE = 0x00000001;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LOCATION")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SYSTEM")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STORE")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "MASK")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CERT")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        internal const int CERT_SYSTEM_STORE_LOCATION_MASK = 0x00FF0000;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SYSTEM")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STORE")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "MACHINE")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LOCAL")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CERT")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        internal const int CERT_SYSTEM_STORE_LOCAL_MACHINE = 0x00020000;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "EXPORTABLE")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CRYPT")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        internal const int CRYPT_EXPORTABLE = 0x00000001;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SCHANNEL")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RSA")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PROV")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        internal const int PROV_RSA_SCHANNEL = 0x0000000C;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ISSUED")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DISP")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        internal const int CR_DISP_ISSUED = 0x00000003;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "BASE")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        internal const int CR_IN_BASE64 = 0x00000001;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PKCS")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        internal const int CR_IN_PKCS10 = 0x00000100;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "OUT")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "BASE")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        internal const int CR_OUT_BASE64 = 0x00000001;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "USER")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SYSTEM")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STORE")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CURRENT")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CERT")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        internal const int CERT_SYSTEM_STORE_CURRENT_USER = 0x00010000;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "FLAG")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STORE")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "READONLY")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "FLAG")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CERT")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "READONLY")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        internal const int CERT_STORE_READONLY_FLAG = 0x00008000;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "FLAG")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STORE")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "OPEN")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "FLAG")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "EXISTING")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CERT")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        internal const int CERT_STORE_OPEN_EXISTING_FLAG = 0x00004000;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SUBJECT")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STR")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "FIND")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CERT")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        internal const int CERT_FIND_SUBJECT_STR = 0x00080007;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ENCODING")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ASN")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        internal const int X509_ASN_ENCODING = 0x00000001;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PKCS")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ENCODING")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ASN")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        internal const int PKCS_7_ASN_ENCODING = 0x00010000;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RSA")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PUBLICKEYBLOB")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CSP")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        internal const int RSA_CSP_PUBLICKEYBLOB = 19;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TYPE")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ENCODING")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        internal const int ENCODING_TYPE = PKCS_7_ASN_ENCODING | X509_ASN_ENCODING;

        /// <summary>
        /// CERT_CONTEXT structure
        /// </summary>
        /*[StructLayout(LayoutKind.Sequential)]
            struct CERT_CONTEXT 
        {
            public IntPtr dwCertEncodingType;  
            public int pbCertEncoded;  
            public IntPtr cbCertEncoded;  
            PCERT_INFO pCertInfo;  
            public IntPtr hCertStore;
        } */

        /// <summary>
        /// CRYPT_DATA_BLOB structure
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), StructLayout(LayoutKind.Sequential)]
        class CRYPT_DATA_BLOB
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "cb")]
            IntPtr cbData;

            public IntPtr CbData
            {
                get { return cbData; }
                set { cbData = value; }
            }
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "pb")]
            int pbData;

            public int PbData
            {
                get { return pbData; }
                set { pbData = value; }
            }
        }

        ///// <summary>
        ///// CERT_ENHKEY_USAGE structure
        ///// </summary>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), StructLayout(LayoutKind.Sequential)]
        //class CERT_ENHKEY_USAGE
        //{
        //    IntPtr cUsageIdentifier = IntPtr.Zero;
        //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "rgpsz")]
        //    [MarshalAs(UnmanagedType.LPWStr)]
        //    string rgpszUsageIdentifier = null;
        //}
        ///// <summary>
        ///// CERT_USAGE_MATCH structure
        ///// </summary>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), StructLayout(LayoutKind.Sequential)]
        //class CERT_USAGE_MATCH
        //{
        //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "dw")]
        //    IntPtr dwType = IntPtr.Zero;
        //    CERT_ENHKEY_USAGE Usage = null;
        //}

        /// <summary>
        /// Certificate store types
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible"), Flags]
        public enum CertificateStores
        {
            None = 0,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "USER")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SYSTEM")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STORE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CURRENT")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CERT")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            CERT_SYSTEM_STORE_CURRENT_USER = 0x00010000,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SYSTEM")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STORE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "MACHINE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LOCAL")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CERT")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            CERT_SYSTEM_STORE_LOCAL_MACHINE = 0x00020000
        }

        /// <summary>
        /// Keyset import locations
        /// </summary>
        [Flags]
        internal enum KeySetLocations
        {
            None = 0,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "EXPORTABLE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CRYPT")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            CRYPT_EXPORTABLE = 0x00000001,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "MACHINE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "KEYSET")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CRYPT")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "KEYSET")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            CRYPT_MACHINE_KEYSET = 0x00000020,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "USER")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "KEYSET")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CRYPT")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "KEYSET")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            CRYPT_USER_KEYSET = 0x00001000
        }

        /// <summary>
        /// Encoding types
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1717:OnlyFlagsEnumsShouldHavePluralNames")]
        internal enum Encodings
        {
            None = 0,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ENCODING")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ASN")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            X509_ASN_ENCODING = 0x00000001
        }

        /// <summary>
        /// Certificate finding types
        /// </summary>
        [Flags]
        internal enum FindTypes
        {
            None = 0,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SHA")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HASH")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "FIND")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CERT")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            CERT_FIND_SHA1_HASH = 0x00010000
        }

        /// <summary>
        /// Store providers
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1717:OnlyFlagsEnumsShouldHavePluralNames")]
        internal enum StoreProviders
        {
            None = 0,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STORE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PROV")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "MEMORY")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CERT")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            CERT_STORE_PROV_MEMORY = 0x00000002,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PROV")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STORE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "FILENAME")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CERT")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "FILENAME")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            CERT_STORE_PROV_FILENAME = 0x00000007,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SYSTEM")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STORE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PROV")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CERT")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            CERT_STORE_PROV_SYSTEM = 0x00000009
        }

        /// <summary>
        /// Store opening flags
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flags"), Flags]
        internal enum OpenFlags
        {
            None = 0,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "FLAG")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STORE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NEW")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "FLAG")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CREATE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CERT")]
            CERT_STORE_CREATE_NEW_FLAG = 0x00002000,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "FLAG")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STORE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "OPEN")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "FLAG")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "EXISTING")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CERT")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            CERT_STORE_OPEN_EXISTING_FLAG = 0x00004000,
        }

        /// <summary>
        /// Certificate adding flags
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flags"), Flags]
        internal enum AddFlags
        {
            None = 0,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STORE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CERT")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ALWAYS")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ADD")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            CERT_STORE_ADD_ALWAYS = 0x00000004
        }

        /// <summary>
        /// Certificate export flags
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Falgs")]
        [Flags]
        internal enum ExportFalgs
        {
            None = 0,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PRIVATE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "KEYS")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "EXPORT")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            EXPORT_PRIVATE_KEYS = 0x00000004
        }

        /// <summary>
        /// Store closing flags
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flags"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1717:OnlyFlagsEnumsShouldHavePluralNames")]
        internal enum CloseFlags
        {
            None = 0,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "FLAG")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STORE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "FORCE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "FLAG")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CLOSE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CERT")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            CERT_CLOSE_STORE_FORCE_FLAG = 0x00000001,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "FLAG")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "STORE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "FLAG")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CLOSE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CHECK")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CERT")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            CERT_CLOSE_STORE_CHECK_FLAG = 0x00000002
        }

        /// <summary>
        /// Logon types
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1717:OnlyFlagsEnumsShouldHavePluralNames"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Logon")]
        internal enum LogonTypes
        {
            None = 0,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LOGON")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "INTERACTIVE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LOGON")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            LOGON32_LOGON_INTERACTIVE = 0x00000002,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NETWORK")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LOGON")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LOGON")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            LOGON32_LOGON_NETWORK = 0x00000003,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LOGON")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "BATCH")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LOGON")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            LOGON32_LOGON_BATCH = 0x00000004,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SERVICE")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LOGON")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LOGON")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            LOGON32_LOGON_SERVICE = 0x00000005,
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "UNLOCK")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LOGON")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LOGON")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            LOGON32_LOGON_UNLOCK = 0x00000007
        }

        /// <summary>
        /// Logon providers
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1717:OnlyFlagsEnumsShouldHavePluralNames"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Logon")]
        internal enum LogonProviders
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PROVIDER")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LOGON")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DEFAULT")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LOGON")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            LOGON32_PROVIDER_DEFAULT = 0x000000000
        }
        #endregion

        #region Data members
        /// <summary>
        /// User token
        /// </summary>
        static int UserToken = 0;

        /// <summary>
        /// Impersonation context
        /// </summary>
        static WindowsImpersonationContext ImpersonationContext = null;
        #endregion

        #region Function members

        /// <summary>
        /// Impersonates specified user
        /// </summary>
        /// <param name="user">User name with domain prefix</param>
        /// <param name="password">User password</param>
        public static void ImpersonateUser(string user, string password)
        {
            // Parse user name
            int slash = user.IndexOf('\\');
            string domain = null;
            if (slash == -1)
            {
                domain = "redmond";
            }
            else
            {
                domain = user.Substring(0, slash);
                user = user.Substring(slash + 1);
            }

            // Check if impersonation is needed
            if (string.Compare(SystemInformation.UserName, user, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return;
            }

            // Logon user
            LogonUser(user, domain, password, (int)LogonTypes.LOGON32_LOGON_NETWORK, (int)LogonProviders.LOGON32_PROVIDER_DEFAULT, out UserToken);

            // Impersonate user
            IntPtr tokenPtr = new IntPtr(UserToken);

            WindowsIdentity identity = new WindowsIdentity(tokenPtr);
            ImpersonationContext = identity.Impersonate();
        }

        /// <summary>
        /// Undo user impersonation
        /// </summary>
        public static void UndoImpersonation()
        {
            // Undo user impersonation
            if (ImpersonationContext != null)
            {
                ImpersonationContext.Undo();
            }
        }

        /// <summary>
        /// Imports certificate from file to specified store
        /// </summary>
        /// <param name="filePath">Path to certificate file</param>
        /// <param name="password">Password for certificate file</param>
        /// <param name="thumbprint">Certificate thumbprint</param>
        /// <param name="store">Store location</param>
        /// <param name="storeName">Store name</param>
        /// <param name="keySetLocation">Key set location</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public static void Import(string filePath, string password, string thumbprint, CertificateStores store, string storeName, bool isExportable)
        {
            if (string.IsNullOrEmpty(password)) password = null;
            // Open stream to read PFX data
            System.IO.FileStream pfxStream = (System.IO.FileStream)System.IO.File.OpenRead(filePath);

            // Read PFX data to temporal buffer
            byte[] pfxBytes = new byte[(int)pfxStream.Length];
            pfxStream.Read(pfxBytes, 0, Convert.ToInt32(pfxStream.Length));

            // Convert and store PFX data in buffer
            CRYPT_DATA_BLOB pfxBlob = new CRYPT_DATA_BLOB();
            pfxBlob.CbData = (IntPtr)(pfxBytes.Length);
            pfxBlob.PbData = (int)Marshal.AllocHGlobal(pfxBytes.Length);
            Marshal.Copy(pfxBytes, 0, (IntPtr)pfxBlob.PbData, pfxBytes.Length);

            IntPtr uExportable = IntPtr.Zero;

            if (isExportable) uExportable = (IntPtr)KeySetLocations.CRYPT_EXPORTABLE;

            // Import PFX data into PFX store
            IntPtr pfxStore = PFXImportCertStore(pfxBlob, password,
               (int)KeySetLocations.CRYPT_MACHINE_KEYSET | (int)uExportable);

            if (pfxStore == IntPtr.Zero) throw new Exception("FileOpenError");

            // Open system store
            IntPtr systemStore = CertOpenStore((IntPtr)StoreProviders.CERT_STORE_PROV_SYSTEM, 0, IntPtr.Zero,
                ((int)OpenFlags.CERT_STORE_OPEN_EXISTING_FLAG | (int)store), storeName);

            if (thumbprint != null)
            {
                // Convert thumbprint string into bytes blob
                byte[] thumbprintBytes = new byte[thumbprint.Length / 2];
                for (int i = 0; i < thumbprint.Length; i += 2)
                {
                    thumbprintBytes[i / 2] = Byte.Parse(thumbprint.Substring(i, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                }

                // Marshal and store bytes blob in crypto blob
                CRYPT_DATA_BLOB thumbprintBlob = new CRYPT_DATA_BLOB();
                thumbprintBlob.CbData = (IntPtr)(thumbprintBytes.Length);
                thumbprintBlob.PbData = (int)Marshal.AllocHGlobal(thumbprintBytes.Length);
                Marshal.Copy(thumbprintBytes, 0, (IntPtr)thumbprintBlob.PbData, thumbprintBytes.Length);

                // Get cert context in PFX store
                IntPtr certContext = CertFindCertificateInStore(pfxStore, (int)Encodings.X509_ASN_ENCODING,
                    0, (int)FindTypes.CERT_FIND_SHA1_HASH, thumbprintBlob, IntPtr.Zero);

                // Copy cert to the system store
                CertAddCertificateContextToStore(systemStore, certContext, (int)AddFlags.CERT_STORE_ADD_ALWAYS, IntPtr.Zero);

                // Free thumbprint blob
                Marshal.FreeHGlobal((IntPtr)thumbprintBlob.PbData);
            }
            else
            {
                // Import all certificates from file
                IntPtr certContext = IntPtr.Zero;

                while ((certContext = CertEnumCertificatesInStore(pfxStore, certContext)) != IntPtr.Zero)
                {
                    // Copy cert to the system store
                    CertAddCertificateContextToStore(systemStore, certContext, (int)AddFlags.CERT_STORE_ADD_ALWAYS, IntPtr.Zero);
                }
            }

            // Close PFX store
            // OLD: CertCloseStore(pfxStore, (IntPtr)CloseFlags.CERT_CLOSE_STORE_FORCE_FLAG);
            // Change by SohailZ, Bug# 94208
            CertCloseStore(pfxStore, 0);

            // Close system store
            // OLD: CertCloseStore(systemStore, (IntPtr)CloseFlags.CERT_CLOSE_STORE_FORCE_FLAG);
            // Change by SohailZ, Bug# 94208
            CertCloseStore(systemStore, 0);

            // Free pfx data
            Marshal.FreeHGlobal((IntPtr)pfxBlob.PbData);
        }



        /// <summary>
        /// Imports certificate from file to specified store
        /// </summary>
        /// <param name="filePath">Path to certificate file</param>
        /// <param name="password">Password for certificate file</param>
        /// <param name="thumbprint">Certificate thumbprint</param>
        /// <param name="store">Store location</param>
        /// <param name="storeName">Store name</param>
        /// <param name="keySetLocation">Key set location</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public static void ImportPublicCert(string filePath, string thumbprint, CertificateStores store, string storeName)
        {
            // Open system store
            IntPtr systemStore = CertOpenStore((IntPtr)StoreProviders.CERT_STORE_PROV_SYSTEM, 0, IntPtr.Zero,
                (int)OpenFlags.CERT_STORE_OPEN_EXISTING_FLAG | (int)store, storeName);

            // Open file store containing the public key
            IntPtr hCertStore = CertOpenStore((IntPtr)StoreProviders.CERT_STORE_PROV_FILENAME, 0, IntPtr.Zero,
               (int)OpenFlags.CERT_STORE_OPEN_EXISTING_FLAG | (int)CERT_STORE_READONLY_FLAG, filePath);

            if (thumbprint != null)
            {
                // Convert thumbprint string into bytes blob
                byte[] thumbprintBytes = new byte[thumbprint.Length / 2];
                for (int i = 0; i < thumbprint.Length; i += 2)
                {
                    thumbprintBytes[i / 2] = Byte.Parse(thumbprint.Substring(i, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                }

                // Marshal and store bytes blob in crypto blob
                CRYPT_DATA_BLOB thumbprintBlob = new CRYPT_DATA_BLOB();
                thumbprintBlob.CbData = (IntPtr)(thumbprintBytes.Length);
                thumbprintBlob.PbData = (int)Marshal.AllocHGlobal(thumbprintBytes.Length);
                Marshal.Copy(thumbprintBytes, 0, (IntPtr)thumbprintBlob.PbData, thumbprintBytes.Length);

                // Get cert context in PFX store
                IntPtr certContext = CertFindCertificateInStore(hCertStore, (int)Encodings.X509_ASN_ENCODING,
                    0, (int)FindTypes.CERT_FIND_SHA1_HASH, thumbprintBlob, IntPtr.Zero);

                // Copy cert to the system store
                CertAddCertificateContextToStore(systemStore, certContext, (int)AddFlags.CERT_STORE_ADD_ALWAYS, IntPtr.Zero);

                // Free thumbprint blob
                Marshal.FreeHGlobal((IntPtr)thumbprintBlob.PbData);
            }
            else
            {
                // Import all certificates from file
                IntPtr certContext = IntPtr.Zero;

                while ((certContext = CertEnumCertificatesInStore(hCertStore, certContext)) != IntPtr.Zero)
                {
                    // Copy cert to the system store
                    CertAddCertificateContextToStore(systemStore, certContext, (int)AddFlags.CERT_STORE_ADD_ALWAYS, IntPtr.Zero);
                }
            }

            // Close PFX store
            // OLD: CertCloseStore(hCertStore, (IntPtr)CloseFlags.CERT_CLOSE_STORE_FORCE_FLAG);
            // Change by SohailZ, Bug# 94208
            CertCloseStore(hCertStore, 0);

            // Close system store
            // OLD: CertCloseStore(systemStore, (IntPtr)CloseFlags.CERT_CLOSE_STORE_FORCE_FLAG);
            // Change by SohailZ, Bug# 94208
            CertCloseStore(systemStore, 0);

        }


        /// <summary>
        /// Exports certificate from the specified store to the file
        /// </summary>
        /// <param name="filePath">Path to certificate file</param>
        /// <param name="password">Password for certificate file</param>
        /// <param name="thumbprint">Certificate thumbprint</param>
        /// <param name="store">Store location</param>
        /// <param name="storeName">Store name</param>
        /// <param name="keySetLocation">Key set location</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public static void Export(string filePath, string password, string thumbprint, CertificateStores store, string storeName)
        {
            // Open system store
            IntPtr systemStore = CertOpenStore((IntPtr)StoreProviders.CERT_STORE_PROV_SYSTEM, 0, IntPtr.Zero,
               (int)OpenFlags.CERT_STORE_OPEN_EXISTING_FLAG | (int)store, storeName);

            // Create memory store
            IntPtr pfxStore = CertOpenStore((IntPtr)StoreProviders.CERT_STORE_PROV_MEMORY, 0, IntPtr.Zero,
                (int)OpenFlags.CERT_STORE_CREATE_NEW_FLAG | (int)store, "Temp PFX Store");

            if (thumbprint != null)
            {
                // Convert thumbprint string into bytes blob
                byte[] thumbprintBytes = new byte[thumbprint.Length / 2];
                for (int i = 0; i < thumbprint.Length; i += 2)
                {
                    thumbprintBytes[i / 2] = Byte.Parse(thumbprint.Substring(i, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                }

                // Marshal and store bytes blob in crypto blob
                CRYPT_DATA_BLOB thumbprintBlob = new CRYPT_DATA_BLOB();
                thumbprintBlob.CbData = (IntPtr)(thumbprintBytes.Length);
                thumbprintBlob.PbData = (int)Marshal.AllocHGlobal(thumbprintBytes.Length);
                Marshal.Copy(thumbprintBytes, 0, (IntPtr)thumbprintBlob.PbData, thumbprintBytes.Length);

                // Get cert context in PFX store
                IntPtr certContext = CertFindCertificateInStore(systemStore, (int)Encodings.X509_ASN_ENCODING,
                    0, (int)FindTypes.CERT_FIND_SHA1_HASH, thumbprintBlob, IntPtr.Zero);

                // Copy cert to the pfx store
                CertAddCertificateContextToStore(pfxStore, certContext, (int)AddFlags.CERT_STORE_ADD_ALWAYS, IntPtr.Zero);

                // Free thumbprint blob
                Marshal.FreeHGlobal((IntPtr)thumbprintBlob.PbData);
            }
            else
            {
                // Export all certificates
                IntPtr certContext = IntPtr.Zero;

                while ((certContext = CertEnumCertificatesInStore(systemStore, certContext)) != IntPtr.Zero)
                {
                    // Copy cert to the pfx store
                    CertAddCertificateContextToStore(pfxStore, certContext, (int)AddFlags.CERT_STORE_ADD_ALWAYS, IntPtr.Zero);
                }
            }

            // Close system store
            // OLD: CertCloseStore(systemStore, (IntPtr)CloseFlags.CERT_CLOSE_STORE_FORCE_FLAG);
            // Change by SohailZ, Bug# 94208
            CertCloseStore(systemStore, 0);


            // Allocate pfx blob
            CRYPT_DATA_BLOB pfxBlob = new CRYPT_DATA_BLOB();
            pfxBlob.CbData = IntPtr.Zero;
            pfxBlob.PbData = 0;

            // Get pfx blob size
            PFXExportCertStore(pfxStore, pfxBlob, password, (int)ExportFalgs.EXPORT_PRIVATE_KEYS);

            // Allocate data blob and get the pfx blob
            pfxBlob.PbData = (int)Marshal.AllocHGlobal((int)pfxBlob.CbData);

            // Get pfx blob
            PFXExportCertStore(pfxStore, pfxBlob, password, (int)ExportFalgs.EXPORT_PRIVATE_KEYS);

            // Get pfx bytes
            byte[] pfxBytes = new byte[(int)pfxBlob.CbData];
            Marshal.Copy((IntPtr)pfxBlob.PbData, pfxBytes, 0, (int)pfxBlob.CbData);

            // Write pfx blob to file
            System.IO.FileStream pfxStream = (System.IO.FileStream)System.IO.File.OpenWrite(filePath);
            pfxStream.Write(pfxBytes, 0, pfxBytes.Length);
            pfxStream.Close();

            // Free pfx blob
            Marshal.FreeHGlobal((IntPtr)pfxBlob.PbData);

            // Close pfx store
            // OLD: CertCloseStore(pfxStore, (IntPtr)CloseFlags.CERT_CLOSE_STORE_FORCE_FLAG);
            // Change by SohailZ, Bug# 94208
            CertCloseStore(pfxStore, 0);

        }

        /// <summary>
        /// Enumerates thumbprints and names of all certificate from the specified PFX file
        /// </summary>
        /// <param name="filePath">Path to certificate file</param>
        /// <param name="password">Password for certificate file</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Certs")]
        public static Hashtable EnumerateCerts(string filePath, string password)
        {
            Hashtable hCertificates = new Hashtable();

            if (string.IsNullOrEmpty(password)) password = null;
            // Open stream to read PFX data
            System.IO.FileStream pfxStream = (System.IO.FileStream)System.IO.File.OpenRead(filePath);

            // Read PFX data to temporal buffer
            byte[] pfxBytes = new byte[(int)pfxStream.Length];
            pfxStream.Read(pfxBytes, 0, Convert.ToInt32(pfxStream.Length));

            // Convert and store PFX data in buffer
            CRYPT_DATA_BLOB pfxBlob = new CRYPT_DATA_BLOB();
            pfxBlob.CbData = (IntPtr)(pfxBytes.Length);
            pfxBlob.PbData = (int)Marshal.AllocHGlobal(pfxBytes.Length);
            Marshal.Copy(pfxBytes, 0, (IntPtr)pfxBlob.PbData, pfxBytes.Length);

            // Import PFX data into PFX store
            IntPtr pfxStore = PFXImportCertStore(pfxBlob, password,
                (int)KeySetLocations.CRYPT_MACHINE_KEYSET | (int)KeySetLocations.CRYPT_EXPORTABLE);

            if (pfxStore == IntPtr.Zero) throw new Exception("FileOpenError");
            // Import all certificates from file
            IntPtr certContext = IntPtr.Zero;

            while ((certContext = CertEnumCertificatesInStore(pfxStore, certContext)) != IntPtr.Zero)
            {
                X509Certificate x509 = new X509Certificate((IntPtr)certContext);
                hCertificates.Add(x509.GetCertHashString(), x509.Subject);
            }

            // Close PFX store
            // OLD: CertCloseStore(pfxStore, (IntPtr)CloseFlags.CERT_CLOSE_STORE_FORCE_FLAG);
            // Change by SohailZ, Bug# 94208
            CertCloseStore(pfxStore, 0);


            // Free pfx data
            Marshal.FreeHGlobal((IntPtr)pfxBlob.PbData);
            return hCertificates;

        }


        /// <summary>
        /// Enumerates thumbprints and names of all certificate from the specified CER file
        /// </summary>
        /// <param name="filePath">Path to certificate file</param>
        /// <param name="password">Password for certificate file</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Certs")]
        public static Hashtable EnumeratePublicCerts(string filePath)
        {
            Hashtable hCertificates = new Hashtable();

            // Open file store containing the public key
            IntPtr hCertStore = CertOpenStore((IntPtr)StoreProviders.CERT_STORE_PROV_FILENAME, 0, IntPtr.Zero,
                (int)OpenFlags.CERT_STORE_OPEN_EXISTING_FLAG | (int)CERT_STORE_READONLY_FLAG, filePath);

            // Import all certificates from file
            IntPtr certContext = IntPtr.Zero;

            while ((certContext = CertEnumCertificatesInStore(hCertStore, certContext)) != IntPtr.Zero)
            {
                X509Certificate x509 = new X509Certificate((IntPtr)certContext);
                hCertificates.Add(x509.GetCertHashString(), x509.Subject);
            }

            // Close cert store
            // OLD: CertCloseStore(hCertStore, (IntPtr)CloseFlags.CERT_CLOSE_STORE_FORCE_FLAG);
            // Change by SohailZ, Bug# 94208
            CertCloseStore(hCertStore, 0);

            return hCertificates;
        }

        /// <summary>
        /// Deletes the certificate from specified store
        /// </summary>
        /// <param name="thumbprint"></param>
        /// <param name="store"></param>
        /// <param name="storeName"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public static void Delete(string thumbprint, CertificateStores store, string storeName)
        {
            // Convert thumbprint string into bytes blob
            byte[] thumbprintBytes = new Byte[thumbprint.Length / 2];
            for (int i = 0; i < thumbprint.Length; i += 2)
            {
                thumbprintBytes[i / 2] = Byte.Parse(thumbprint.Substring(i, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            // Marshal and store bytes blob in crypto blob
            CRYPT_DATA_BLOB thumbprintBlob = new CRYPT_DATA_BLOB();
            thumbprintBlob.CbData = (IntPtr)(thumbprintBytes.Length);
            thumbprintBlob.PbData = (int)Marshal.AllocHGlobal(thumbprintBytes.Length);
            Marshal.Copy(thumbprintBytes, 0, (IntPtr)thumbprintBlob.PbData, thumbprintBytes.Length);

            // Open system store
            IntPtr systemStore = CertOpenStore((IntPtr)StoreProviders.CERT_STORE_PROV_SYSTEM, 0, IntPtr.Zero,
                (int)OpenFlags.CERT_STORE_OPEN_EXISTING_FLAG | (int)store, storeName);

            // Get cert context in this store
            IntPtr certContext = CertFindCertificateInStore(systemStore, (int)Encodings.X509_ASN_ENCODING,
                0, (int)FindTypes.CERT_FIND_SHA1_HASH, thumbprintBlob, IntPtr.Zero);

            // Delete certificate from store
            CertDeleteCertificateFromStore(certContext);

            // Close system store
            // OLD: CertCloseStore(systemStore, (IntPtr)CloseFlags.CERT_CLOSE_STORE_FORCE_FLAG);
            // Change by SohailZ, Bug# 94208
            CertCloseStore(systemStore, 0);
        }

        ///// <summary>
        ///// Retrieves the list of available enterprise CAs from the Active Directory
        ///// </summary>
        ///// <returns>List of the CAs</returns>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "CAs")]
        //internal static string[] GetAvailableCAs()
        //{
        //    Type type = Type.GetTypeFromProgID("CertificateAuthority.Config.1");
        //    object certConfig = Activator.CreateInstance(type);

        //    // Reset enumeration
        //    int index = 0;
        //    object[] args = new object[] { index };
        //    int count = (int)type.InvokeMember("Reset", BindingFlags.InvokeMethod, null, certConfig, args, CultureInfo.InvariantCulture);

        //    // Check number of available CAs
        //    if (count == 0)
        //    {
        //        throw new ApplicationException("There's no any Enterprise Certification Authority available");
        //    }

        //    string[] CAs = new string[count];
        //    for (int i = 0; index != -1; i++)
        //    {
        //        // Get CA qualified name
        //        args = new object[] { "Config" };
        //        CAs[i] = (string)type.InvokeMember("GetField", BindingFlags.InvokeMethod, null, certConfig, args, CultureInfo.InvariantCulture);

        //        // Move to next entry
        //        index = (int)type.InvokeMember("Next", BindingFlags.InvokeMethod, null, certConfig, null, CultureInfo.InvariantCulture);
        //    }

        //    return CAs;
        //}

        /*/// <summary>
        /// Requests the certificate from the CA and stores it in LOCAL_MACHINE "MY" store
        /// </summary>
        /// <param name="CA">CA name</param>
        /// <returns>Thumbprint of the certificate stored in the LOCAL_MACHINE "MY" store</returns>
        public static string RequestCertificate(string CA)
        {
            // Set enroll flags
            CEnrollClass enroll=new CEnrollClass();

            enroll.MyStoreFlags=enroll.MyStoreFlags & ~CERT_SYSTEM_STORE_LOCATION_MASK | CERT_SYSTEM_STORE_LOCAL_MACHINE;
            enroll.GenKeyFlags=enroll.GenKeyFlags | CRYPT_EXPORTABLE;
            enroll.KeySpec=AT_KEYEXCHANGE;
            enroll.ProviderType=PROV_RSA_SCHANNEL;
            enroll.DeleteRequestCert=1;

            // Prepare PKCS10 request
            string distinguishedName=string.Format("CN={0},OU=Office,O=Microsoft,L=Redmond,S=WA,C=US", SystemInformation.ComputerName);
            string request=enroll.createPKCS10(distinguishedName, "1.3.6.1.5.5.7.3.1");
					
            Type type=Type.GetTypeFromProgID("CertificateAuthority.Request.1");
            object certRequest=Activator.CreateInstance(type);

            // Submit request
            object[] args=new object[] {CR_IN_BASE64 | CR_IN_PKCS10, request, "CertificateTemplate:WebServer", CA};
            int disposition=0;
					
            disposition=(int)type.InvokeMember("Submit", BindingFlags.InvokeMethod, null, certRequest, args);

            // Check CA reply
            if(disposition!=CR_DISP_ISSUED)
            {
                // Request was denined by the CA
                throw new ApplicationException("Request was denied by the CA");
            }

            // Retrieve certificate
            args=new object[] {CR_OUT_BASE64};
            string certificateBlob=(string)type.InvokeMember("GetCertificate", BindingFlags.InvokeMethod, null, certRequest, args);

            // Accept cerrtificate
            enroll.acceptPKCS7(certificateBlob);

            // Attach certificate class to blob
            CertificateClass certificate=new CertificateClass();
            certificate.Import(certificateBlob);

            // Return SHA1 hash
            return certificate.Thumbprint;
        }*/
        /*
                /// <summary>
                /// Requests the certificate from all available CAs until gets issued one
                /// </summary>
                /// <returns>Thumbprint of the certificate stored in the LOCAL_MACHINE "MY" store</returns>
                public static string RequestCertificateFromAnyCA()
                {
                    // Look if we can use existing certificate
                    StoreClass store=new StoreClass();
                    store.Open(CAPICOM_STORE_LOCATION.CAPICOM_LOCAL_MACHINE_STORE, Constants.CAPICOM_MY_STORE,
                        CAPICOM_STORE_OPEN_MODE.CAPICOM_STORE_OPEN_MAXIMUM_ALLOWED);

                    foreach(Certificate certificate in store.Certificates)
                    {
                        // Check certificate usage flags
                        KeyUsage usage=certificate.KeyUsage();
                        if(usage.IsKeyEnciphermentEnabled && usage.IsDigitalSignatureEnabled)
                        {
                            return certificate.Thumbprint;
                        }
                    }

                    string[] CAs=GetAvailableCAs();
			
                    foreach(string CA in CAs)
                    {
                        try
                        {
                            string thumbprint=RequestCertificate(CA);
                            if(thumbprint!=null)
                            {
                                return thumbprint;
                            }
                        }
                        catch(Exception e)
                        {
                            Trace.WriteLine("Request submission failed: "+e);
                            continue;
                        }
                    }

                    throw new ApplicationException("Failed to obtain server certificate");
                }*/
        #endregion
    }
}

