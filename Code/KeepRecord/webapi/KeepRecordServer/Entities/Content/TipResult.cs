using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Serializable]
    public class TipResult
    {
        /// <summary>
        /// 0 error
        /// 1 success
        /// </summary>
        public int Status { get; set; }

        public string Msg { get; set; }
    }
}
