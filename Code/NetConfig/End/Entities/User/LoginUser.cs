using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.User
{
    public class LoginUser
    {
        public int Id { get; set; }
        public string Username { get; set; }


        public int Status { get; set; }
        public string UserInfo { get; set; }

        [JsonIgnore]
        public string Password { get; set; } //SHA1

        [JsonIgnore]
        public string Salt { get; set; }


        //MD5 from remark
        public string Remark { get; set; }
        public string Role { get; set; }

       
        

      
    }
}
