using System;
using System.Collections.Generic;
using System.Text;

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
