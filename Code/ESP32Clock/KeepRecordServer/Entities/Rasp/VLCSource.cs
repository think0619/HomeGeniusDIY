using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Rasp
{
    public class VLCSource
    {
        public int RecId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Remark { get; set; }
        public int? IsClockSrc { get; set; }
        public int Status { get; set; }
         
    }
}
