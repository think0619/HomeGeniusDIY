using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.User
{
    public class LoginUser
    {
        public int Id { get; set; }
        public string Username { get; set; } 
        //MD5 from remark
        public string IDCode { get; set; } 
        public int Status { get; set; }
        public string UserInfo { get; set; }  
        public string Password { get; set; } //SHA1
        public string Remark { get; set; }   
        public string Salt { get; set; }
        public string Role { get; set; }
    }
}
 