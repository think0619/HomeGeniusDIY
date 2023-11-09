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
using NetMQ.Sockets;
using NetMQ;

namespace TextVoiceServer.Serivices
{
    /// <summary>
    /// update Sensor Value config  data
    /// </summary>
    public class HandleMQPublishService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        /// 转发消息 key
        /// </summary>
        public static string MidMsgString = "ControlMsg";

        /// <summary>
        /// 测试连通性
        /// </summary>
        public static string TestConnectionString = "TestConnection";
        public HandleMQPublishService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            PublishCommandMQ();
            await Task.Delay(TimeSpan.FromMilliseconds(1));
        }

        public void PublishCommandMQ()
        {
            int mqport = 61234;
            string _mqport = ConfigurationHelper.AppSetting["SystemCfg:MQPort"].ToString();
            Int32.TryParse(_mqport, out mqport);

            Task.Factory.StartNew(() =>
             {
                using (var publisher = new PublisherSocket())
                {
                    publisher.Bind($"tcp://*:{mqport}");

                    Task.Factory.StartNew(() =>
                    {
                        System.Timers.Timer sendConnectionTimer = new System.Timers.Timer();
                        sendConnectionTimer.Enabled = true;
                        sendConnectionTimer.Interval = 10 * 1000; //执行间隔时间,单位为毫秒; 
                        sendConnectionTimer.Start();
                        sendConnectionTimer.Elapsed += (e, v) =>
                        {
                            publisher.SendFrame($"{MidMsgString}@{TestConnectionString}");
                        };
                    });

                    while (true)
                    {
                         // 移除并获取列表的第一个元素
                         string topmsg = RedisHelper.LPop(MidMsgString);
                         if (topmsg != null)
                         {
                             publisher.SendFrame($"{MidMsgString}@{topmsg}");
                         }
                         System.Threading.Thread.Sleep(5);
                    }
                }
            });
        }
    }
}
