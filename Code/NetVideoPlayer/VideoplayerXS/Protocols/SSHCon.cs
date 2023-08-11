using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VideoplayerXS.Common;

namespace VideoplayerXS.Protocols
{
    public class SSHCon
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public SSHCon() { }

        public SSHCon(string _host,string _username,string password,int _port= 22)
        {
            Host = _host;
            Port = _port;
            Username= _username;
            Password = password; 
        }

        public SSHCon(string authstring)
        {
            Host = "";
            Port = -1;
            Username = "";
            Password = "";
            if (!String.IsNullOrWhiteSpace(authstring))
            {
                try
                {
                    SSHCon ssh= JsonHelper.DeserializeJsonToObject<SSHCon>(authstring);
                    Host= ssh.Host;
                    Port= ssh.Port;
                    Username= ssh.Username;
                    Password= ssh.Password;
                }
                catch { } 
            } 
        }

        public string GetSSHConnectionString() 
        {
            //[sftp://][user[:password]@]ip_address[:port_number/][/Filepath]
            string hexPwd= StringHelper.Convert2HexString(Password);
            string convertPwd = string.IsNullOrEmpty(Password) ? "" : $":{hexPwd}";
            return String.Format($@"sftp://{Username}{convertPwd}@{Host}:{Port}/");
        }
    }
}
