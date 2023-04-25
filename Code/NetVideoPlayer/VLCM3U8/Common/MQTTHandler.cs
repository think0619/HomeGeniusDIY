using MQTTnet.Client;
using MQTTnet;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VLCM3U8.Common
{
    public class MQTTHandler
    {
        String mqttUrl = "";
        String mediaTopic = "";
        String sysTopic = "";
        MainFrm mainFrm = null;

        public MQTTHandler()
        {
            //check
            mqttUrl = ConfigureHelper.ReadKey("MQTT:FullUrl");
            mediaTopic = ConfigureHelper.ReadKey("MQTT:TopicMedia");
            sysTopic = ConfigureHelper.ReadKey("MQTT:TopicSystem");
        }

        public async void HandleMQTT(MainFrm _mainFrm)
        { 
            this.mainFrm = _mainFrm;
            if (!String.IsNullOrEmpty(mqttUrl))
            {
                var mqttClientOptions = new MqttClientOptionsBuilder().WithCleanSession(false).WithWebSocketServer(mqttUrl).Build();
                MqttFactory mqttFactory = new MqttFactory();
                var mqttSubscribeOptions = 
                       mqttFactory.CreateSubscribeOptionsBuilder()
                       .WithTopicFilter(f => { f.WithTopic(sysTopic); })
                       .WithTopicFilter(f => { f.WithTopic(mediaTopic); })
                       .Build();

                IMqttClient mqttClient = mqttFactory.CreateMqttClient();
                mqttClient.ApplicationMessageReceivedAsync += ReceiveMsgHandler;  
                mqttClient.DisconnectedAsync += async (e) =>
                {
                    _ = Task.Run(async () => {
                       while (true)
                       {
                           if (!mqttClient.IsConnected)
                           {
                                try
                                {
                                    await mqttClient.ReconnectAsync();
                                    await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
                                }
                                catch (Exception)
                                {
                                }
                                finally
                                {
                                    await Task.Delay(TimeSpan.FromSeconds(5));
                                }
                            }
                           else
                           {
                               break;
                           } 
                       }
                   }); 
                };

                try
                {
                    await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None); 
                    await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
                }
                catch (Exception ex) { } 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public Task ReceiveMsgHandler(MqttApplicationMessageReceivedEventArgs e) 
        { 
            var msg = e.ApplicationMessage;
            string payload = Encoding.UTF8.GetString(msg.Payload); 

            var topic = msg.Topic;
            if (mediaTopic.Equals(topic))
            {
                mainFrm.UpdateVLCSourceDelegate(payload);
            }
            else if (sysTopic.Equals(topic))
            {
            }
            return Task.CompletedTask; 
        } 
    }
}
