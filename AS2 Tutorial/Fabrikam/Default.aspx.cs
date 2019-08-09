using System;
using System.IO;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Win32;

public partial class _Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string Content = "";
        //Append headers to Content
        foreach (string key in Request.Headers.Keys)
        {
            Content += key + ": " + Request.Headers[key] + "\r\n";
        }
        Content += "\r\n";

        //Append body to Content
        StreamReader sr = new StreamReader(Request.InputStream);
        Content += sr.ReadToEnd();

        FileStream fp = new FileStream(getBizTalkInstallPath() + @"SDK\AS2 Tutorial\" + Request.QueryString["Destination"] + "\\" + Guid.NewGuid() + ".msg", FileMode.Create);
        fp.Write(Encoding.ASCII.GetBytes(Content), 0, Encoding.ASCII.GetByteCount(Content));
        fp.Flush();
        fp.Close();

        Response.End();
    }

    private string getBizTalkInstallPath()
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
