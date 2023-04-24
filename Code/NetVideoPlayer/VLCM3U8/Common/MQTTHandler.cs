using MQTTnet.Client;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VLCM3U8.Common
{
    public class MQTTHandler
    { 
        public async void HandleWS(MainFrm mainFrm)
        {
            //check
            String mqttUrl = ConfigureHelper.ReadKey("MQTT:FullUrl");
            String mediaTopic = ConfigureHelper.ReadKey("MQTT:TopicMedia");
            String sysTopic = ConfigureHelper.ReadKey("MQTT:TopicSystem");

            if (!String.IsNullOrEmpty(mqttUrl) )
            {
                MqttFactory mqttFactory = new MqttFactory();
                var mqttClientOptions = new MqttClientOptionsBuilder().WithWebSocketServer(mqttUrl).Build();
                IMqttClient mqttClient = mqttFactory.CreateMqttClient();
                 
                mqttClient.ApplicationMessageReceivedAsync += e =>
                {
                    var msg = e.ApplicationMessage;
                    string payload = Encoding.UTF8.GetString(msg.Payload);
                    var topic=msg.Topic;
                    if (mediaTopic.Equals(topic))
                    {
                        mainFrm.UpdateVLCSourceDelegate(payload);
                    }
                    else if (sysTopic.Equals(topic))
                    {
                    }   
                    return Task.CompletedTask;
                }; 
                try
                {
                    await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
                    var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                        .WithTopicFilter(f => { f.WithTopic(sysTopic); })
                        .WithTopicFilter(f => { f.WithTopic(mediaTopic); })
                        .Build();
                    await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
                }
                catch(Exception ex) { }
              
            } 
        }
    }
}
