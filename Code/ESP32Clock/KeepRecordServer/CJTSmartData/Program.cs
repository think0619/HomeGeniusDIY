using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextVoiceServer
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:6000");
                })
            .ConfigureLogging(logging =>
                {
                    logging.AddLog4Net(log4NetConfigFile: "log4net.config");
                    logging.ClearProviders();
                    logging.AddConsole();//for Logging on Console 
                   // logging.AddLog4Net();//for DB Query Logging
                })
            ;   
    }
}
