using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLCM3U8 
{
    public class ConfigureHelper
    {

        public static string ReadKey(string key) 
        {
            string result = "";
            if (!string.IsNullOrEmpty(key)) 
            {
                //s1
                string[] keys = new string[2];
                string[] getKeys = key.Split(':');
                if (getKeys.Length > 0)
                {
                    keys[0] = getKeys[0];
                    if (getKeys.Length > 0)
                    {
                        keys[1] = getKeys[1];
                    }
                }

                //s2
                string settingFile = Path.Combine(Environment.CurrentDirectory, "Data", "appsettings.json");
                if (File.Exists(settingFile))
                {
                    string settings = File.ReadAllText(settingFile);
                    try
                    {
                        //s3
                        if (!String.IsNullOrEmpty(keys[0]))
                        {
                            var firstValue = JObject.Parse(settings).GetValue(keys[0]).ToString();
                            if (!String.IsNullOrWhiteSpace(keys[1]))
                            {
                                var secondValue = JObject.Parse(firstValue).GetValue(keys[1]).ToString();
                                result = secondValue.Trim();
                            }
                            else
                            {
                                result = firstValue.Trim();
                            }
                        } 
                    }
                    catch
                    {
                    }
                } 
            }
            return result; 
        }
    }
}
