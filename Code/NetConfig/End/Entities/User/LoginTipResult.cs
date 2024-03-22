using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.User
{
    [Serializable]
    public class LoginTipResult
    {
        /// <summary>
        /// 0 error
        /// 1 success
        /// </summary>
        public int Status { get; set; }
        public string Msg { get; set; }
        public string Token { get; set; } 

        public class LoginTipData
        {
            public string token { get; set; }
        }
    }
}
