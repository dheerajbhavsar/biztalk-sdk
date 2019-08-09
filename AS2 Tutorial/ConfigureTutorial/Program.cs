using System;
using Microsoft.Win32;

namespace Microsoft.BizTalk.Edi.Tutorial
{
    class Configure
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || (args[0].ToUpper() == "/I" && args.Length != 2) || (args[0].ToUpper() == "/U" && args.Length != 1) || (args[0].ToUpper() == "/S" && args.Length != 2))
            {
                Console.WriteLine(@"Microsoft (R) BizTalk Server AS2 Tutorial Configuration Tool.
Copyright (C) Microsoft Corporation.  All rights reserved.

Usage: 
ConfigureTutorial.exe /I <password for user {0}>
    To install tutorial.

ConfigureTutorial.exe /U
    To uninstall tutorial.

ConfigureTutorial.exe /S <thumbprint>
    To setup BizTalk signing and decryption certificate.
", ConfigBizTalk.GetBizTalkHostServiceAccount());
            }
            else if (args[0].ToUpper() == "/I")
                Setup(args[1]);
            else if (args[0].ToUpper() == "/U")
                Uninstall();
            else
            {
                ConfigureCertificates(args);
            }
            Environment.Exit(0);
        }

        private static void ConfigureCertificates(string[] args)
        {
            try
            {
                ConfigBizTalk.SetDecryptionCertificate(args[1], ConfigBizTalk.GetBizTalkHostServiceAccount());
                ConfigBizTalk.SetSigningCertificate(args[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Certificate Configuration Failed: " + e.Message);
            }
        }

        private static void Setup(string password)
        {
            try
            {
                ConfigIIS.CreateAppPool("AS2Tutorial", ConfigBizTalk.GetBizTalkHostServiceAccount(), password);
                ConfigIIS.CreateIisVirtualDirectory("Fabrikam", "AS2Tutorial", getBizTalkInstallPath() + @"SDK\AS2 Tutorial\Fabrikam");
                ConfigIIS.CreateIisVirtualDirectory("Contoso", "AS2Tutorial", getBizTalkInstallPath() + @"HttpReceive");
            }
            catch (Exception e)
            {
                Console.WriteLine("Installation Failed: " + e.Message);
            }
        }

        private static void Uninstall()
        {
            try
            {
                ConfigIIS.DeleteIisVirtualDirectory("Fabrikam");
                ConfigIIS.DeleteIisVirtualDirectory("Contoso");
                ConfigIIS.DeleteAppPool("AS2Tutorial");
            }
            catch (Exception e)
            {
                Console.WriteLine("Uninstallation Failed: " + e.Message);
            }
        }

        private static string getBizTalkInstallPath()
        {
            string installPath = null;
            RegistryKey rKeyBizTalkServer = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\BizTalk Server\\3.0");
            if (rKeyBizTalkServer != null)
            {
                installPath = rKeyBizTalkServer.GetValue("InstallPath").ToString();
            }
            return installPath;
        }
    }
}