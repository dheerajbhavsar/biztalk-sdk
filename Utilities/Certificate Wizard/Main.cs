using System;
using System.IO;
using System.Text;
using System.Management;
using System.Collections;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using Microsoft.BizTalk.ExplorerOM;
using System.Reflection;

using System.Security.Cryptography.X509Certificates;
using System.Globalization;

namespace Microsoft.Solutions.Btarn.CertWizard
{
    /// <summary>
    /// Summary description for Main.
    /// </summary>
    public sealed class CertificateWizard
    {
        private CertificateWizard()
        {
        }

        #region enum types
        enum KeyUsage
        {
            NULL,
            SIGN,
            DECRYPT,
            BOTH,
            NONE,
            SSL
        }
        enum KeyType
        {
            PRIVATE,
            PUBLIC,
            ROOT,
            INTER
        }

        enum ErrorType
        {
            NO_ERRORS,
            MISSING_FILENAME,
            MISSING_PARAMETER,
            MISSING_VALUE,
            FILE_NOTEXIST,
            UNKOWN_PARAMETER,
            BAD_FILEPASSWORD,
            INVALID_USAGE,
            INVALID_VALUE,
            DUPLICATE_PARAMETER
        }
        #endregion

        #region members
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1504:ReviewMisleadingFieldNames")]
        static private string m_sCertFile = null;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1504:ReviewMisleadingFieldNames")]
        static private string m_sIdentityUser = null;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1504:ReviewMisleadingFieldNames")]
        static private string m_sIdentityPassword = null;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1504:ReviewMisleadingFieldNames")]
        static private string m_sThumb = null;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1504:ReviewMisleadingFieldNames")]
        static private string m_sFilePassword = null;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1504:ReviewMisleadingFieldNames")]
        static private bool m_bIsExportable = false;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1504:ReviewMisleadingFieldNames")]
        static private KeyType m_KeyType;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1504:ReviewMisleadingFieldNames")]
        static private KeyUsage m_KeyUsage = KeyUsage.NULL;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1504:ReviewMisleadingFieldNames")]
        static private Queue m_ErrorBag = new Queue();
        #endregion

        #region methods
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), STAThread]
        static void Main(string[] argv)
        {
            Hashtable sParameters = new Hashtable();

            //check that every parameter name has a value
            if (argv.Length >= 1 && (argv[0].ToUpper(CultureInfo.InvariantCulture) == "/H" || argv[0].ToUpper(CultureInfo.InvariantCulture) == "/?" || argv[0].ToUpper(CultureInfo.InvariantCulture) == "/HELP"))
            {
                DisplayUsage();
                Environment.Exit(0);
            }
            else if (argv.Length % 2 == 1 || argv.Length == 0)
                m_ErrorBag.Enqueue(ErrorType.MISSING_VALUE);
            else
                for (int i = 0; i < argv.Length; i++)
                {
                    if (!argv[i].StartsWith("/", StringComparison.OrdinalIgnoreCase) || argv[i + 1].StartsWith("/", StringComparison.OrdinalIgnoreCase))
                    {
                        m_ErrorBag.Enqueue(ErrorType.MISSING_VALUE);
                        break;
                    }
                    try
                    {
                        sParameters.Add(argv[i++], argv[i]);
                    }
                    catch (ArgumentException)
                    {
                        m_ErrorBag.Enqueue(ErrorType.DUPLICATE_PARAMETER);
                    }
                }

            //check for validity of parameters
            if (m_ErrorBag.Count == 0)
            {
                foreach (string sParameter in sParameters.Keys)
                {
                    switch (sParameter.ToUpper(CultureInfo.InvariantCulture).Substring(0, 5))
                    {
                        case "/PRIV":
                            //PRIVATEKEY
                            m_KeyType = KeyType.PRIVATE;
                            if (!File.Exists(sParameters[sParameter].ToString()))
                                m_ErrorBag.Enqueue(ErrorType.FILE_NOTEXIST);
                            else
                                m_sCertFile = sParameters[sParameter].ToString();
                            break;

                        case "/PUBL":
                            //PUBLICKEY
                            m_KeyType = KeyType.PUBLIC;
                            if (!File.Exists(sParameters[sParameter].ToString()))
                                m_ErrorBag.Enqueue(ErrorType.FILE_NOTEXIST);
                            else
                                m_sCertFile = sParameters[sParameter].ToString();
                            break;
                        case "/INTE":
                            m_KeyType = KeyType.INTER;
                            if (!File.Exists(sParameters[sParameter].ToString()))
                                m_ErrorBag.Enqueue(ErrorType.FILE_NOTEXIST);
                            else
                                m_sCertFile = sParameters[sParameter].ToString();
                            break;


                        case "/ROOT":
                            //ROOTKEY
                            m_KeyType = KeyType.ROOT;
                            if (!File.Exists(sParameters[sParameter].ToString()))
                                m_ErrorBag.Enqueue(ErrorType.FILE_NOTEXIST);
                            else
                                m_sCertFile = sParameters[sParameter].ToString();
                            break;

                        case "/FILE":
                            //FILEPASSWORD
                            m_sFilePassword = sParameters[sParameter].ToString();
                            break;

                        case "/EXPO":
                            //EXPORT
                            if (sParameters[sParameter].ToString().ToUpper(CultureInfo.InvariantCulture) == "TRUE")
                                m_bIsExportable = true;
                            else if (sParameters[sParameter].ToString().ToUpper(CultureInfo.InvariantCulture) == "FALSE")
                                m_bIsExportable = false;
                            else
                                m_ErrorBag.Enqueue(ErrorType.INVALID_VALUE);
                            break;

                        case "/USER":
                            //USERIDENTITY
                            m_sIdentityUser = sParameters[sParameter].ToString();
                            break;

                        case "/PASS":
                            //PASSWORD
                            m_sIdentityPassword = sParameters[sParameter].ToString();
                            break;

                        case "/THUM":
                            //KEYTHUMBPRINT
                            m_sThumb = sParameters[sParameter].ToString();
                            break;

                        case "/USAG":
                            switch (sParameters[sParameter].ToString().ToUpper(CultureInfo.InvariantCulture).Substring(0, 3))
                            {
                                case "DEC":
                                    m_KeyUsage = KeyUsage.DECRYPT;
                                    break;
                                case "SIG":
                                    m_KeyUsage = KeyUsage.SIGN;
                                    break;
                                case "NON":
                                    m_KeyUsage = KeyUsage.NONE;
                                    break;
                                case "BOT":
                                    m_KeyUsage = KeyUsage.BOTH;
                                    break;
                                case "SSL":
                                    m_KeyUsage = KeyUsage.SSL;
                                    break;
                                default:
                                    m_ErrorBag.Enqueue(ErrorType.INVALID_USAGE);
                                    break;
                            }
                            break;
                        default:
                            m_ErrorBag.Enqueue(ErrorType.UNKOWN_PARAMETER);
                            break;
                    }
                }
            }

            if (m_ErrorBag.Count == 0)
            {
                try
                {
                    if (m_KeyType == KeyType.PRIVATE && m_sFilePassword == null)
                    {
                        Console.Write("Please enter the password for the certificate file: ");
                        m_sFilePassword = PwdConsole.ReadLine(); //Console.ReadLine();
                        Console.WriteLine();
                    }

                    if (m_sThumb == null) m_sThumb = PromptForCertificate(m_sCertFile, m_sFilePassword, m_KeyType);

                    switch (m_KeyType)
                    {
                        case KeyType.PUBLIC:
                            ImportPublicCert(m_sCertFile, m_sThumb);
                            break;
                        case KeyType.INTER:
                            ImportIntermediateCert(m_sCertFile, m_sThumb);
                            break;
                        case KeyType.PRIVATE:
                            if (m_KeyUsage == KeyUsage.NULL)
                            {
                                string sChoice = "5";
                                Console.Write("\r\nThis home certificate will be used for (default 5):\r\n[1] Signing  [2]Decryption  [3] Signing & Decryption  [4] IIS SSL  [5] None\r\nEnter your option here: ");
                                sChoice = Console.ReadLine();
                                if (string.IsNullOrEmpty(sChoice.Trim())) sChoice = "5";

                                switch (sChoice.Substring(0, 1))
                                {
                                    case "1":
                                        m_KeyUsage = KeyUsage.SIGN;
                                        break;
                                    case "2":
                                        m_KeyUsage = KeyUsage.DECRYPT;
                                        break;
                                    case "3":
                                        m_KeyUsage = KeyUsage.BOTH;
                                        break;
                                    case "4":
                                        m_KeyUsage = KeyUsage.SSL;
                                        break;
                                    default:
                                        m_KeyUsage = KeyUsage.NONE;
                                        break;
                                }
                            }

                            if (m_KeyUsage == KeyUsage.SSL)
                            {
                                PrivateKeyManager.Import(m_sCertFile, m_sFilePassword, m_sThumb, PrivateKeyManager.CertificateStores.CERT_SYSTEM_STORE_LOCAL_MACHINE, "MY", m_bIsExportable);
                                SetSslCertificate(m_sThumb);
                                Console.WriteLine("\r\nCompleted importing and configuring SSL certificate on IIS.");
                            }
                            else if (m_KeyUsage == KeyUsage.DECRYPT || m_KeyUsage == KeyUsage.SIGN || m_KeyUsage == KeyUsage.BOTH || m_KeyUsage == KeyUsage.NONE)
                            {
                                ImportPrivateCert(m_sCertFile, m_sFilePassword, m_sThumb, m_bIsExportable);

                                Console.WriteLine();

                                if (m_KeyUsage == KeyUsage.DECRYPT || m_KeyUsage == KeyUsage.BOTH)
                                    SetDecryptionCertificate(m_sThumb, m_sIdentityUser);

                                if (m_KeyUsage == KeyUsage.SIGN || m_KeyUsage == KeyUsage.BOTH)
                                    SetSigningCertificate(m_sThumb);
                            }

                            break;
                        case KeyType.ROOT:
                            ImportPublicRootCert(m_sCertFile);
                            break;
                    }
                }
                catch (Exception e)
                {
                    if (e.Message == "FileOpenError")
                        m_ErrorBag.Enqueue(ErrorType.BAD_FILEPASSWORD);
                    else
                        m_ErrorBag.Enqueue(ErrorType.UNKOWN_PARAMETER);
                }
            }

            bool bErrors = (m_ErrorBag.Count > 0);
            while (m_ErrorBag.Count > 0)
            {

                ErrorType error = (ErrorType)m_ErrorBag.Dequeue();
                switch (error)
                {
                    case ErrorType.FILE_NOTEXIST:
                        Console.WriteLine("File does not exists. Please check the path.");
                        break;
                    case ErrorType.MISSING_FILENAME:
                        Console.WriteLine("Certificate file not specified.");
                        break;
                    case ErrorType.MISSING_PARAMETER:
                        Console.WriteLine("Missing parameter.");
                        break;
                    case ErrorType.MISSING_VALUE:
                        Console.WriteLine("One or more parameter values are missing.");
                        break;
                    case ErrorType.UNKOWN_PARAMETER:
                        Console.WriteLine("One or more unknown parameters are supplied.");
                        break;
                    case ErrorType.BAD_FILEPASSWORD:
                        Console.WriteLine("Could not open certificate file. Check password.");
                        break;
                    case ErrorType.INVALID_USAGE:
                        Console.WriteLine("Invalid usage was specified.");
                        break;
                    case ErrorType.INVALID_VALUE:
                        Console.WriteLine("One or more parameters have invalid values.");
                        break;
                    case ErrorType.DUPLICATE_PARAMETER:
                        Console.WriteLine("A parameter was repeated more than once.");
                        break;
                }
            }

            if (bErrors)
            {
                DisplayUsage();
                Environment.Exit(1);
            }
            else
                Console.WriteLine("\r\nImport successfully completed!");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static string PromptForCertificate(string m_sCertFile, string m_sFilePassword, KeyType m_KeyType)
        {
            Hashtable hCertificates = null;
            Hashtable hEnumHash = new Hashtable();

            if (m_KeyType == KeyType.PRIVATE)
            {
                hCertificates = PrivateKeyManager.EnumerateCerts(m_sCertFile, m_sFilePassword);
            }
            else if (m_KeyType == KeyType.ROOT || m_KeyType == KeyType.PUBLIC || m_KeyType == KeyType.INTER)
            {
                hCertificates = PrivateKeyManager.EnumeratePublicCerts(m_sCertFile);
            }

            if (hCertificates.Keys.Count == 1)
                Console.WriteLine("The available certificate is:\r\n");
            else
                Console.WriteLine("Select the certificate to import from the list below:\r\n");

            int iCounter = 1;
            foreach (string sHashID in hCertificates.Keys)
            {
                Console.WriteLine(iCounter + "- " + hCertificates[sHashID].ToString() + "\r\nThumbprint: " + sHashID + "\r\n");
                hEnumHash.Add(iCounter++, sHashID);
            }

            int iChoice = 1;

            if (hCertificates.Keys.Count > 1)
            {
                Console.Write("\r\nEnter certificate number 1-" + (iCounter - 1) + " (default 1): ");

                string sChoice = Console.ReadLine();
                if (string.IsNullOrEmpty(sChoice)) sChoice = "1";

                try
                {
                    iChoice = System.Convert.ToInt16(sChoice, CultureInfo.InvariantCulture);
                }
                catch
                {
                    iChoice = 1;
                }
                if (iChoice < 1 || iChoice > iCounter - 1) iChoice = 1;
            }
            return hEnumHash[iChoice].ToString();
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static void ImportPublicRootCert(string sPrivateCertPath)
        {
            try
            {
                PrivateKeyManager.ImportPublicCert(sPrivateCertPath,
                    null,
                    PrivateKeyManager.CertificateStores.CERT_SYSTEM_STORE_LOCAL_MACHINE, "Root");
            }
            catch
            {
                Console.WriteLine("Failed importing root public key to the Trusted Root Certification Authorities certificate store");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static void ImportIntermediateCert(string privateCertificatePath, string thumbprint) 
        {
            try
            {
                PrivateKeyManager.ImportPublicCert(privateCertificatePath,
                    thumbprint,
                    PrivateKeyManager.CertificateStores.CERT_SYSTEM_STORE_LOCAL_MACHINE, "CA");
            }
            catch
            {
                Console.WriteLine("Failed importing public key to the Other People certificate store");
            }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static void ImportPublicCert(string sPrivateCertPath, string m_sThumb)
        {
            try
            {
                PrivateKeyManager.ImportPublicCert(sPrivateCertPath,
                    m_sThumb,
                    PrivateKeyManager.CertificateStores.CERT_SYSTEM_STORE_LOCAL_MACHINE, "AddressBook");
            }
            catch
            {
                Console.WriteLine("Failed importing public key to the Other People certificate store");
            }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static void ImportPrivateCert(string sPrivateCertPath, string sFilePassword, string m_sThumb, bool m_bIsExportable)
        {
            Hashtable hs = new Hashtable();

            if (m_sIdentityUser == null)
            {
                ManagementObjectSearcher m_mgMISearcher = new ManagementObjectSearcher(new WqlObjectQuery("SELECT * FROM MSBTS_HostSetting"));
                m_mgMISearcher.Scope = new ManagementScope("root\\MicrosoftBizTalkServer");
                m_mgMISearcher.Options.ReturnImmediately = true;

                ManagementBaseObject[] m_moHostInstances = new ManagementBaseObject[m_mgMISearcher.Get().Count];

                m_mgMISearcher.Get().CopyTo(m_moHostInstances, 0);

                foreach (ManagementObject oHost in m_moHostInstances)
                {
                    try
                    {
                        hs.Add(oHost.GetPropertyValue("LastUsedLogon").ToString().ToUpper(CultureInfo.InvariantCulture), null);
                    }
                    catch { }
                }
            }
            else
            {
                hs.Add(m_sIdentityUser, m_sIdentityPassword);
            }

            foreach (string sUser in hs.Keys)
            {
                string sPassword = null;

                if (hs[sUser] == null)
                {
                    Console.Write("\r\nEnter password for identity " + sUser + ": ");
                    sPassword = PwdConsole.ReadLine(); //Console.ReadLine();
                }
                else
                    sPassword = hs[sUser].ToString();
                try
                {
                    PrivateKeyManager.ImpersonateUser(sUser, sPassword);
                    PrivateKeyManager.Import(sPrivateCertPath,
                        sFilePassword,
                        m_sThumb,
                        PrivateKeyManager.CertificateStores.CERT_SYSTEM_STORE_CURRENT_USER, "MY", m_bIsExportable);
                    Console.WriteLine("Completed importing private certificate to user identity " + sUser + ".");

                    PrivateKeyManager.UndoImpersonation();
                }
                catch
                {
                    Console.WriteLine("Failed importing private key to user [" + sUser + "].\n\rCheck that you have entered a valid password.");
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static void SetDecryptionCertificate(string sDecryptionThumbprint, string sIdentityUser)
        {
            ManagementObjectSearcher m_mgMISearcher = new ManagementObjectSearcher(new WqlObjectQuery("SELECT * FROM MSBTS_HostSetting"));
            m_mgMISearcher.Scope = new ManagementScope("root\\MicrosoftBizTalkServer");
            m_mgMISearcher.Options.ReturnImmediately = true;

            ManagementBaseObject[] m_moHostInstances = new ManagementBaseObject[m_mgMISearcher.Get().Count];

            m_mgMISearcher.Get().CopyTo(m_moHostInstances, 0);

            Console.WriteLine();
            foreach (ManagementObject oHost in m_moHostInstances)
            {
                try
                {
                    if (sIdentityUser == null || oHost.GetPropertyValue("LastUsedLogon").ToString().ToUpper(CultureInfo.InvariantCulture) == sIdentityUser.ToUpper(CultureInfo.InvariantCulture))
                    {
                        oHost.SetPropertyValue("DecryptCertThumbprint", sDecryptionThumbprint);
                        oHost.Put();
                        Console.WriteLine("Completed setting decryption certificate on host " + oHost.GetPropertyValue("Name").ToString() + ".");
                    }
                }
                catch
                {
                    Console.WriteLine("Failed setting decryption thumbprint for host: " + oHost.GetPropertyValue("Name").ToString());
                }
            }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static void SetSigningCertificate(string sSigningThumbprint)
        {
            ManagementObjectSearcher m_mgMISearcher = new ManagementObjectSearcher(new WqlObjectQuery("SELECT * FROM MSBTS_GroupSetting"));
            m_mgMISearcher.Scope = new ManagementScope("root\\MicrosoftBizTalkServer");
            m_mgMISearcher.Options.ReturnImmediately = true;

            ManagementBaseObject[] m_moBTSInstances = new ManagementBaseObject[m_mgMISearcher.Get().Count];

            m_mgMISearcher.Get().CopyTo(m_moBTSInstances, 0);

            foreach (ManagementObject oBTSServer in m_moBTSInstances)
            {
                try
                {
                    oBTSServer.SetPropertyValue("SignCertThumbprint", sSigningThumbprint);
                    oBTSServer.Put();
                    Console.WriteLine("Completed setting public signing certificate for BizTalk server.");
                }
                catch
                {
                    Console.WriteLine("Failed setting signing thumbprint for the BizTalk Server.");
                }
            }
        }

        private static void SetSslCertificate(string sSslThumbprint)
        {
            DirectoryEntry WebSite = new DirectoryEntry(@"IIS://localhost/W3SVC/1");

            WebSite.NativeObject.GetType().InvokeMember("SSLCertHash", BindingFlags.SetProperty,
                null, WebSite.NativeObject, new object[] { HexToBinary(sSslThumbprint) }, CultureInfo.InvariantCulture);

            WebSite.Properties["SSLStoreName"].Value = "MY";
            WebSite.Properties["SecureBindings"].Value = ":443:";
            WebSite.CommitChanges();
        }

        private static byte[] HexToBinary(string thumbprint)
        {
            thumbprint = thumbprint.Replace(" ", "").Trim();
            byte[] newvalue = new byte[thumbprint.Length / 2];
            int i = 0;
            while (i < thumbprint.Length)
            {
                newvalue[i / 2] = Convert.ToByte(thumbprint.Substring(i, 2), 16);
                i = i + 2;
            }
            return newvalue;
        }

        private static void DisplayUsage()
        {
            Console.WriteLine("\r\nMicrosoft (R) BizTalk Server Import Certificates Utility." +
                "\r\nCopyright (C) Microsoft Corporation. All rights reserved." +
                "\r\n\r\nUsage:" +
                "\r\n" +
                "\r\nCertWizard /Privatekey filename.pfx [/Filepassword filepassword]" +
                "\r\n          [/Useridentity useridentity] [/Password password]" +
                "\r\n          [/Thumbprint thumbprint] [/Usage sign|decrypt|both|ssl|none]" +
                "\r\n          [/Exportable true|false]" +
                "\r\nCertWizard /Publickey filename.cer [/Thumbprint thumbprint]" +
                "\r\nCertWizard /Rootkey filename.cer [/Thumbprint thumbprint]" +
                "\r\n" +
                "\r\nWhere:" +
                "\r\n- Filename.pfx(.cer) is the full path for the PFX/CER file." +
                "\r\n- Filepassword is the password to unlock the PFX file." +
                "\r\n- Useridentity is a service identity used by one or more BizTalk hosts. \r\n  If not supplied, it will be detected automatically." +
                "\r\n- Password is the password for the service identity user." +
                "\r\n- Usage indicates the intended usage of the imported private certificate:\r\n  Sign: used for signing outbound messages\r\n  Decrypt: used for decrypting inbound encrypted messages\r\n  Both: used for both outbound signing and inbound decryption of messages\r\n  SSL: used for configuring the IIS web server" +
                "\r\n- If exportable is set then the private key can be re-exported from the certificate store." +
                "\r\n- Thumbprint is the thumprint of a specific certificate" +
                "\r\n  in case the certificate file contains more than one certificates."
                );
        }

        #endregion
    }

    internal static class PwdConsole
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("kernel32", SetLastError = true)]
        static extern IntPtr GetStdHandle(int whichHandle);
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1414:MarkBooleanPInvokeArgumentsWithMarshalAs"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("kernel32", SetLastError = true)]
        static extern bool GetConsoleMode(IntPtr handle, out uint mode);
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1414:MarkBooleanPInvokeArgumentsWithMarshalAs"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), DllImport("kernel32", SetLastError = true)]
        static extern bool SetConsoleMode(IntPtr handle, uint mode);

        static readonly IntPtr STD_INPUT_HANDLE = new IntPtr(-10);
        const int ENABLE_LINE_INPUT = 2;
        const uint ENABLE_ECHO_INPUT = 4;
        const int ENABLE_PROCESSED_INPUT = 1;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public static string ReadLine()
        {
            // turn off console echo
            IntPtr hConsole = GetStdHandle((int)STD_INPUT_HANDLE);
            uint oldMode;
            if (!GetConsoleMode(hConsole, out oldMode))
            {
                throw new Exception("GetConsoleMode failed");
            }
            uint newMode = oldMode & ~(ENABLE_LINE_INPUT | ENABLE_ECHO_INPUT);
            if (!SetConsoleMode(hConsole, newMode))
            {
                throw new Exception("SetConsoleMode failed");
            }
            int i;
            StringBuilder secret = new StringBuilder();
            while (true)
            {
                i = Console.Read();
                if (i == 13 || i == 10)     // break when 
                    break;

                if (i == 8 && secret.Length >= 0) //backspace
                {
                    if (secret.Length > 0)
                    {
                        secret.Remove(secret.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                    continue;
                }

                secret.Append((char)i);
                Console.Write("*");
            }

            Console.WriteLine();
            // restore console echo and line input mode
            if (!SetConsoleMode(hConsole, oldMode))
            {
                throw new Exception("SetConsoleMode failed");
            }
            return secret.ToString();
        }
    }
}