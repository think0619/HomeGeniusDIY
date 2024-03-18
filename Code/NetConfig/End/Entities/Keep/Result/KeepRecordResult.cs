using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Keep.Result
{
    [Serializable]
    public class KeepRecordResult
    {
        /// <summary>
        /// 0 error
        /// 1 success
        /// </summary>
        public int Status { get; set; }
        public int Total { get; set; }
        public string Msg { get; set; }
        public KeepRecord Data  { get; set; } 
    }
}
