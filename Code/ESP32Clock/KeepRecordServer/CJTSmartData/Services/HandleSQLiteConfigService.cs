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
using TextVoiceServer.DBHandler;

namespace TextVoiceServer.Serivices
{
    /// <summary>
    /// update Sensor Value config  data
    /// </summary>
    public class HandleSQLiteConfigService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public HandleSQLiteConfigService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SQLiteHelper.InitializeDatabase();
            await Task.Delay(TimeSpan.FromSeconds(1));
            //"后台服务启动……"
            //await Task.Factory.StartNew(async () => {
            //    while (!stoppingToken.IsCancellationRequested)
            //    { 
            //        try
            //        {
            //            await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            //        }
            //        catch (OperationCanceledException) { }
            //    }
            //}, stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            //logger.LogInformation("后台服务停止……");
            await Task.CompletedTask;
        }

    }
}
