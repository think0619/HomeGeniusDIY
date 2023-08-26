using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Keep.Result
{
    [Serializable]
    public class KeepRecordViewTip
    {
        /// <summary>
        /// 0 error
        /// 1 success
        /// </summary>
        public int Status { get; set; }
        public string Msg { get; set; }
        public List<KeepRecordView> Data  { get; set; } 
    }
}
