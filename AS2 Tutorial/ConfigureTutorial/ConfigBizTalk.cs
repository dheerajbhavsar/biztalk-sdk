using System;
using System.Management;

namespace Microsoft.BizTalk.Edi.Tutorial
{
    public class ConfigBizTalk
    {
        public static string GetBizTalkHostServiceAccount()
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
                    if (oHost.GetPropertyValue("IsDefault").ToString().ToUpper() == "TRUE")
                    {
                        return oHost.GetPropertyValue("LastUsedLogon").ToString();
                    }
                }
                catch
                { }
            }
            return null;
        }

        public static void SetSigningCertificate(string sSigningThumbprint)
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
                    Console.WriteLine("Completed setting up public signing certificate for BizTalk Server.");
                }
                catch
                {
                    Console.WriteLine("Failed setting up signing certificate for the BizTalk Server.");
                }
            }
        }

        public static void SetDecryptionCertificate(string sDecryptionThumbprint, string sIdentityUser)
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
                    if (sIdentityUser == null || oHost.GetPropertyValue("LastUsedLogon").ToString().ToUpper() == sIdentityUser.ToUpper())
                    {
                        oHost.SetPropertyValue("DecryptCertThumbprint", sDecryptionThumbprint);
                        oHost.Put();
                        Console.WriteLine("Completed setting up decryption certificate for host " + oHost.GetPropertyValue("Name").ToString() + ".");
                    }
                }
                catch
                {
                    Console.WriteLine("Failed setting up decryption certificate for host: " + oHost.GetPropertyValue("Name").ToString());
                }
            }
        }
    }
}
