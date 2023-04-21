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
        IMqttClient mqttClient; 

        public async void HandleWS(Form form)
        { 
            MqttFactory mqttFactory = new MqttFactory();

              mqttClient = mqttFactory.CreateMqttClient();

            //var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("broker.hivemq.com").Build();
            var mqttClientOptions = new MqttClientOptionsBuilder().WithWebSocketServer("ws://127.0.0.1:8083/mqtt").Build();

            // Setup message handling before connecting so that queued messages
            // are also handled properly. When there is no event handler attached all
            // received messages get lost.
            mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                var msg = e.ApplicationMessage;
                var m1 = msg.Payload.ToString();
                string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                Console.WriteLine(payload);


                return Task.CompletedTask;
            };

            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(
                    f =>
                    {
                        f.WithTopic("presence");
                    })
                .Build();

            await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

            Console.WriteLine("MQTT client subscribed to topic.");

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();

        }
    }
}
