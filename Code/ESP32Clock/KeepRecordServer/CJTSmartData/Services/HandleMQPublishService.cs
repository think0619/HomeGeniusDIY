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
using MQTTnet;
using MQTTnet.Client;

namespace TextVoiceServer.Serivices
{
    /// <summary>
    /// update Sensor Value config  data
    /// </summary>
    public class HandleMQPublishService : BackgroundService
    {
        private MqttFactory mqttFactory;
        private IMqttClient mqttClient;
         
        public HandleMQPublishService( )
        { 
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ConnectMQTTServer( );
            await Task.Delay(TimeSpan.FromMilliseconds(1));
        }

        public async void ConnectMQTTServer()
        {
            mqttFactory = new MqttFactory();
            mqttClient = mqttFactory.CreateMqttClient();

            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer("hw.hellolinux.cn", 1883) 
                .WithCredentials("homediskrelay", "@Xiongsen1994!+")
                .Build();

            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None); 
        }

        public async void PublicMsg(string topic, string msg) 
        {
            var applicationMessage = new MqttApplicationMessageBuilder()
                      .WithTopic(topic)
                      .WithPayload(msg)
                      .Build();
            await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
        }



        public async void PublishCommandMQ()
        { 
            var mqttFactory = new MqttFactory();

            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer("hw.hellolinux.cn", 1883)
                    
                    .WithCredentials("homediskrelay", "@Xiongsen1994!+")
                    .Build();

                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                var applicationMessage = new MqttApplicationMessageBuilder()
                    .WithTopic("ShowClockTime")
                    .WithPayload("你好啊")
                    .Build(); 
                await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

                await mqttClient.DisconnectAsync();
            }
        }
    }
}