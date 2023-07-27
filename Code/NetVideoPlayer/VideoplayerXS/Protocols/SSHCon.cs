using System;
using System.Collections.Generic;
using System.Linq;
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

        public SSHCon(string _host,string _username,string password,int _port= 22)
        {
            Host = _host;
            Port = _port;
            Username= _username;
            Password = password; 
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
