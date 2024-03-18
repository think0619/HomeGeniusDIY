using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Entities.Keep
{
    public class KeepRecordView 
    {
        //[JsonIgnore]
        //[IgnoreDataMember]
        public int Id { get; set; }

        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public Decimal Count { get; set; }
        public string Units { get; set; } 
        public int UnitsId { get; set; }
        public Decimal SubCount { get; set; }
        public string SubUnits { get; set; }
        public int SubUnitsId { get; set; }

        [IgnoreDataMember]
        public DateTime? RecordDatetime { get; set; }

        [IgnoreDataMember]
        public DateTime? RecordDate { get; set; }

        [NotMapped]
        public string _RecordDatetime
        {
            get
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

        [NotMapped]
        public string DescInfo 
        {
            get 
            {
                return String.Format($"{TypeName}:{Count}{Units} / {SubCount}{SubUnits}");
            }
        }

<<<<<<< Updated upstream
        //[NotMapped]
=======
        
>>>>>>> Stashed changes
        //public long UpdateDatetime { get; set; }

        //[NotMapped]
        //public DateTime U
        //{
        //    get
        //    { 
        //        return DateTimeOffset.FromUnixTimeMilliseconds(UpdateDatetime).DateTime;
        //    }
        //}
    } 
}
