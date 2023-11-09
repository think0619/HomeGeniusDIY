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
using Microsoft.Extensions.Caching.Memory;

namespace TextVoiceServer.Serivices
{
    /// <summary>
    /// update Sensor Value config  data
    /// </summary>
    public class HandleSystemCfgService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public HandleSystemCfgService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetUserAuth();
            while (!stoppingToken.IsCancellationRequested)
            {
                SetUserAuth();
                await Task.Delay(60 * 60 * 1000, stoppingToken);
            }
        }

        public void SetUserAuth()
        {
            string userAuthString = ConfigurationHelper.AppSetting["SystemCfg:UserAuth"].ToString();
            RedisHelper.Set("UserAuth", userAuthString);
        } 
    }
}
