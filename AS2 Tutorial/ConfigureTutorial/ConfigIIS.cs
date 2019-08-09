using System;
using System.DirectoryServices;

namespace Microsoft.BizTalk.Edi.Tutorial
{
    public class ConfigIIS
    {
        public static void CreateAppPool(string AppPoolName, string AppPoolIdentity, string AppPoolPassword)
        {
            DirectoryEntry apppools = new DirectoryEntry("IIS://localhost/W3SVC/AppPools");
            DirectoryEntry apppool = null;
            try
            {
                apppool = apppools.Children.Add(AppPoolName, "IIsApplicationPool");
            }
            catch
            {
                apppool = new DirectoryEntry("IIS://localhost/W3SVC/AppPools/" + AppPoolName);
            }

            apppool.Properties["RapidFailProtectionMaxCrashes"].Value = 1000;
            apppool.Properties["AppPoolIdentityType"].Value = 3;
            apppool.Properties["WAMUserName"].Value = AppPoolIdentity;
            apppool.Properties["WAMUserPass"].Value = AppPoolPassword;
            apppool.Properties["IdleTimeout"].Value = 0;
            apppool.Properties["PeriodicRestartTime"].Value = 0;
            apppool.Properties["AppPoolAutoStart"].Value = true;

            apppool.CommitChanges();
        }

        public static void DeleteAppPool(string AppPoolName)
        {
            DirectoryEntry apppools = new DirectoryEntry("IIS://localhost/W3SVC/AppPools");
            apppools.Children.Remove(new DirectoryEntry("IIS://localhost/W3SVC/AppPools/" + AppPoolName));

            apppools.CommitChanges();
        }


        public static void CreateIisVirtualDirectory(string VdName, string AppPoolName, string Path)
        {
            try
            {
                DirectoryEntry Parent = new DirectoryEntry(@"IIS://localhost/W3SVC/1/Root");
                DirectoryEntry NewVirtualDir;
                NewVirtualDir = Parent.Children.Add(VdName, "IIsWebVirtualDir");
                NewVirtualDir.Properties["Path"][0] = Path;
                NewVirtualDir.Properties["AuthFlags"][0] = 5;
                NewVirtualDir.Properties["AppFriendlyName"][0] = VdName;
                try
                {
                    NewVirtualDir.Properties["AppPoolId"][0] = AppPoolName;
                }
                catch { }
                NewVirtualDir.Properties["AccessFlags"][0] = 517;
                NewVirtualDir.Properties["AppRoot"][0] = "/LM/W3SVC/1/Root/" + VdName;

                NewVirtualDir.CommitChanges();
            }
            catch { }
        }

        /// <summary>
        /// Deletes the IIS virtual directory
        /// </summary>
        /// <param name="VdName"></param>
        public static void DeleteIisVirtualDirectory(string VdName)
        {
            try
            {
                DirectoryEntry Parent = new DirectoryEntry(@"IIS://localhost/W3SVC/1/Root");
                Object[] Parameters = { "IIsWebVirtualDir", VdName };
                Parent.Invoke("Delete", Parameters);
            }
            catch { }
        }

    }
}
