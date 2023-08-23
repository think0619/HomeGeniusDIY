using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TextVoiceServer
{
    static class ConfigurationHelper
    {
        public static IConfiguration AppSetting { get; }
        static ConfigurationHelper()
        {
            AppSetting = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        }
    }

    public class WebSocketClient
    {
        public string IP { get; set; }
    }

    public class DBServer
    {
        public string IP { get; set; }

        public int Port { get; set; }
        public string UserId { get; set; }
        public string UserPwd { get; set; }
    }

    public class ZeroMQ
    {  
        public String Url { get; set; } 
    }
}