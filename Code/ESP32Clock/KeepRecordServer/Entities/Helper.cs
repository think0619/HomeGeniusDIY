using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class ComHelper
    {
        /// <summary>
        /// Unix timestamp to Datetime
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;

            //DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
            //DateTime dateTime = dateTimeOffset.LocalDateTime;
        }
    }
}
