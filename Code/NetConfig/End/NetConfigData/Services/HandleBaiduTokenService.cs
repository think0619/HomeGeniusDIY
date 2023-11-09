using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using System.Collections.Generic; 
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using TextVoiceServer.DBContext;  
using System.Net.Http; 
using System.Net.Http.Headers;

namespace TextVoiceServer.Serivices
{
    /// <summary>
    /// update Sensor Value config  data
    /// </summary>
    public class HandleBaiduTokenService : BackgroundService
    {  
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public HandleBaiduTokenService(  IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory; 
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
          
            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        
    }
}
