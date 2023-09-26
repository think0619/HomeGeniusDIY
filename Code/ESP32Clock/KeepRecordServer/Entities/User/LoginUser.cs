using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.User
{
    public class LoginUser
    {
        public int id { get; set; }
        public string username { get; set; } 
        //MD5
        public string idcode { get; set; } 
        public int status { get; set; }
        public string userinfo { get; set; } 
    }
}
 