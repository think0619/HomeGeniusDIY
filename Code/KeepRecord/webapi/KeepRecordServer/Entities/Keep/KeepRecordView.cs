using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Keep
{
    public class KeepRecordView 
    {
        public int RecID { get; set; }
        public string TypeName { get; set; }
        public Decimal Count { get; set; }
        public string Units { get; set; } 
        public int UnitsId { get; set; }
        public Decimal SubCount { get; set; }
        public string SubUnits { get; set; }
        public int SubUnitsId { get; set; }
        public DateTime? RecordDatetime { get; set; }
        public DateTime? RecordDate { get; set; }
    }


}
