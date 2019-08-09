using System;
using System.Net;
using System.Threading;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace Ms.Test.BizTalk.Edi.As2
{
    public class RequestState
    {
        // Client  socket.
        public HttpWebRequest Request;
    }

    public class HttpSender
    {
        #region Private Memebers
        // URL for connection
        private String m_sUrl = "";

        // Authentication Information
        private string m_sUsername = "";
        private string m_sPassword = "";
        private string m_sDomain = "";

        // Array of additional headers
        private string[] m_headers;
        private string[] m_headerValues;

        // Request Data
        private Stream m_sRequest = null;

        private string m_partyName = "";

        private int _timeout = 0;
        #endregion

        #region Properties

        public Stream Request
        {
            get
            {
                return m_sRequest;
            }
            set
            {
                m_sRequest = value;
            }
        }

        public string[] Headers
        {
            get
            {
                return m_headers;
            }
            set
            {
                m_headers = value;
            }
        }

        public string[] HeaderValues
        {
            get
            {
                return m_headerValues;
            }
            set
            {
                m_headerValues = value;
            }
        }
        #endregion

        #region Methods
        static void Main(string[] args)
        {
            HttpSender TestSender = new HttpSender("http://localhost/Contoso/BTSHttpReceive.dll");

            //Request Synchronous MDN
            //Stream sr = new FileStream(getBizTalkInstallPath() + @"SDK\AS2 Tutorial\X12_00401_864-Sync.edi", FileMode.Open, FileAccess.Read);

            //Request Asynchronous MDN            
            Stream sr = new FileStream(getBizTalkInstallPath() + @"SDK\AS2 Tutorial\X12_00401_864.edi", FileMode.Open, FileAccess.Read);

            TestSender.Request = sr;
            Stream SyncResponse = TestSender.Send();
            if (SyncResponse != null)
            {
                StreamReader sreader = new StreamReader(SyncResponse, true);
                Console.WriteLine(sreader.ReadToEnd());
            }
            else
            {
                Console.WriteLine("AS2 message successfully sent.");
            }
        }

        public HttpSender(string URL)
        {
            m_sUrl = URL;
        }

        public HttpSender(string URL, string PartyName)
        {
            m_sUrl = URL;
            m_partyName = PartyName;
        }

        public HttpSender(string URL, int timeout)
            : this(URL)
        {
            if (timeout >= 0)
                _timeout = timeout;
            else
                throw new ApplicationException("Http Sender timeout cannot be negative");
        }

        public HttpSender(string URL, string PartyName, int timeout)
            : this(URL, PartyName)
        {
            if (timeout >= 0)
                _timeout = timeout;
            else
                throw new ApplicationException("Http Sender timeout cannot be negative");
        }


        public Stream Send()
        {
            //Local copies of headers
            string[] l_headers = m_headers;
            string[] l_headerValues = m_headerValues;

            m_sRequest.Seek(0, SeekOrigin.Begin);

            HttpWebRequest hrqWebRequest = (HttpWebRequest)WebRequest.Create(m_sUrl);
            if (_timeout != 0) hrqWebRequest.Timeout = _timeout;

            RequestState rqsRequestState = new RequestState();
            rqsRequestState.Request = hrqWebRequest;

            if (m_sPassword != string.Empty)
            {
                hrqWebRequest.PreAuthenticate = true;
                hrqWebRequest.Credentials = new NetworkCredential(m_sUsername, m_sPassword, m_sDomain);
            }
            hrqWebRequest.Method = "POST";

            Stream outStream = hrqWebRequest.GetRequestStream();

            //Read the headers from the request stream
            if (l_headers == null)
            {
                StreamReader sr = new StreamReader(m_sRequest);

                //Counts the size of the headers
                int byteCounter = 0;
                string line;
                string headers = "";
                while (string.Empty != (line = sr.ReadLine()))
                {
                    byteCounter += line.Length + Environment.NewLine.Length;
                    line = line.Replace(": ", ":");
                    if (headers != string.Empty)
                    {
                        if (line[0] == ' ')
                            headers += "\r\n" + line;
                        else
                            headers += "^" + line;
                    }
                    else
                        headers = line;
                }
                l_headers = headers.Split("^".ToCharArray());

                //Skip the headers
                byteCounter += 2;
                m_sRequest.Seek(byteCounter, SeekOrigin.Begin);
            }

            if (l_headers != null)
            {
                //Get the headers from the hashtable
                foreach (string line in l_headers)
                {
                    string sLine = line;
                    if (l_headerValues != null)
                    {
                        for (int i = 0; i < l_headerValues.Length; i++)
                        {
                            sLine = sLine.Replace("{" + i.ToString().Trim() + "}", l_headerValues[i]);
                        }
                    }

                    string[] pair = sLine.Split(":".ToCharArray());
                    //reassemble if more than 1 : is in the header (e.g., http://someURL)
                    string value = "";
                    if (pair.Length > 2)
                    {
                        value = pair[1];
                        for (int i = 2; i < pair.Length; i++)
                            value += ":" + pair[i];
                    }
                    else
                        value = pair[1];


                    switch (pair[0].ToUpper())
                    {
                        case "CONTENT-TYPE":
                            hrqWebRequest.ContentType = value;
                            break;
                        case "CONNECTION":
                        case "HOST":
                        case "USER-AGENT":
                        case "DATE":
                        case "EXPECT":
                        case "CONTENT-LENGTH":
                            break;
                        case "AS2-FROM":
                            if (m_partyName != "")
                                hrqWebRequest.Headers.Add("AS2-From", m_partyName);
                            else
                                hrqWebRequest.Headers.Add("AS2-From", value);
                            break;
                        default:
                            hrqWebRequest.Headers.Add(pair[0], value);
                            break;
                    }
                }
            }

            //m_sRequest.Seek(2, SeekOrigin.Current);            
            byte[] buffer = new byte[4096];
            int count = 0;
            while (0 != (count = m_sRequest.Read(buffer, 0, buffer.Length)))
            {
                outStream.Write(buffer, 0, count);
            }
            outStream.Flush();
            outStream.Close();

            HttpWebResponse hrpWebResponse = (HttpWebResponse)hrqWebRequest.GetResponse();
            MemoryStream memoryStream = new MemoryStream();

            if (hrpWebResponse.ContentLength > 0)
            {
                //Prepend headers
                foreach (string header in hrpWebResponse.Headers.Keys)
                {
                    memoryStream.Write(Encoding.ASCII.GetBytes(header + ": " + hrpWebResponse.Headers[header] + "\r\n"), 0, Encoding.ASCII.GetByteCount(header + ": " + hrpWebResponse.Headers[header] + "\r\n"));
                }
                //skip the empty line
                memoryStream.Write(Encoding.ASCII.GetBytes("\r\n\r\n"), 0, Encoding.ASCII.GetByteCount("\r\n\r\n"));

                count = 0;
                while (0 != (count = hrpWebResponse.GetResponseStream().Read(buffer, 0, buffer.Length)))
                {
                    memoryStream.Write(buffer, 0, count);
                }
                memoryStream.Seek(0, SeekOrigin.Begin);
            }
            else
            {
                memoryStream = null;
            }

            hrpWebResponse.Close();
            return memoryStream;
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
        #endregion
    }
}

