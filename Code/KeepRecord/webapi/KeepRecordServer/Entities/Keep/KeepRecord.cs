using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Keep
{
    public class KeepRecord 
    {
        public int RecID { get; set; }
        public string Type { get; set; }
        public Decimal Count { get; set; }
        public string Units { get; set; }
        public Decimal SubCount { get; set; }
        public string SubUnits { get; set; }
        public DateTime? RecordDatetime { get; set; }

    }


}
