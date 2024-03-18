using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Keep
{
    public class KeepRecord 
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public Decimal Count { get; set; }
        public int UnitsId { get; set; }
        public Decimal SubCount { get; set; }
        public int SubUnitsId { get; set; } 
        public DateTime? RecordDatetime { get; set; }
        public DateTime? RecordDate { get; set; }

        [NotMapped]
        public string _RecordDatetime 
        { get 
            {
                if (RecordDatetime.HasValue)
                {
                    return RecordDatetime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    return "";
                }
            } 
        }

        [NotMapped]
        public string _RecordDate
        {
            get
            {
                if (RecordDate.HasValue)
                {
                    return RecordDate.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    return "";
                }
            }
        }

        
        //public long UpdateDatetime { get; set; }

        //public DateTime U 
        //{
        //    get {

        //        return DateTimeOffset.FromUnixTimeMilliseconds(UpdateDatetime).DateTime;
        //    }
        //}


        public int Status { get; set; }
    } 
}
