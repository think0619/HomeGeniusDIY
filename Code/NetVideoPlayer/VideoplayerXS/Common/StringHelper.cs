using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoplayerXS.Common
{
    public class StringHelper
    {
        /// <summary>
        /// convert string to Hex string(char by char)
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string Convert2HexString(string sourceString)
        {
            string output = string.Empty;
            if (!String.IsNullOrEmpty(sourceString))
            {
                StringBuilder convertSB = new StringBuilder();
                foreach (char c in sourceString)
                {
                    convertSB.Append("%");
                    convertSB.Append(Convert.ToInt32(c).ToString("X"));
                }
                output= convertSB.ToString();
            }
            return output;
        }
    }
}
