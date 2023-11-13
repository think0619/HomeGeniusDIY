using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Net
{
    public  class NetInfo
    {
        public int RecId { get; set; }
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public string ServerName { get; set; }
        public string InnerIP { get; set; }
        public string OuterIP { get; set; }
        public string Username { get; set; }
        public string Userpassword { get; set; }
        public string Token { get; set; }
        public string Remark { get; set; }
        public string Remark2 { get; set; }
        public string Remark3 { get; set; }
        public string TextRecord { get; set; }
        public int Status { get; set; }

    } 
}
