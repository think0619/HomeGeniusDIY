using MQTTnet.Client;
using MQTTnet;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using System.Diagnostics;

namespace VLCM3U8.Common
{
    public class MQTTHandler
    {
        String mqttUrl = "";
        String mediaTopic = "";
        String sysTopic = "";
        MainFrm mainFrm = null;

        string machineID = "";

        public MQTTHandler()
        {
            //check
            mqttUrl = ConfigureHelper.ReadKey("MQTT:FullUrl");
            mediaTopic = ConfigureHelper.ReadKey("MQTT:TopicMedia");
            sysTopic = ConfigureHelper.ReadKey("MQTT:TopicSystem");
            machineID = ConfigureHelper.ReadKey("DeviceInfo:MachineID");
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
                    _ = Task.Run(async () =>
                    {
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
            string[] payloadArray = payload.Split('$');

            if (payloadArray.Length == 2)
            {
                if (machineID.Equals(payloadArray[0]))
                {
                    var topic = msg.Topic;
                    if (mediaTopic.Equals(topic))
                    {
                        mainFrm.UpdateVLCSourceDelegate(payloadArray[1]);
                    }
                    else if (sysTopic.Equals(topic))
                    {
                        switch (payloadArray[1])
                        {
                            case MQTTCommand.VolumnPlus:
                                {
                                    this.mainFrm.Invoke(new MethodInvoker(delegate ()
                                    {
                                        AudioManager.SetMasterVolumeMute(false);
                                        AudioManager.StepMasterVolume(3);
                                    }));
                                    break;
                                };
                            case MQTTCommand.VolumnSub:
                                {
                                    this.mainFrm.Invoke(new MethodInvoker(delegate ()
                                    {
                                        AudioManager.SetMasterVolumeMute(false);
                                        AudioManager.StepMasterVolume(-3);
                                    }));
                                    break;
                                };
                            case MQTTCommand.Mute:
                                {
                                    this.mainFrm.Invoke(new MethodInvoker(delegate ()
                                    {
                                        AudioManager.SetMasterVolumeMute(true);
                                    }));
                                    break;
                                };
                            case MQTTCommand.Unmute:
                                {
                                    this.mainFrm.Invoke(new MethodInvoker(delegate ()
                                    {
                                        AudioManager.SetMasterVolumeMute(false);
                                    }));
                                    break;
                                };
                            case MQTTCommand.Shutdown:
                                {
                                    this.mainFrm.Invoke(new MethodInvoker(delegate ()
                                    {
                                        CommandHandler.ExecuteCommand(@"shutdown /s /f /t 10 ");
                                    }));
                                    break;
                                };
                            case MQTTCommand.CloseAPP:
                                {
                                    this.mainFrm.Invoke(new MethodInvoker(delegate ()
                                    {
                                        Process.GetCurrentProcess().Kill();
                                        Application.Exit();
                                    }));
                                    break;
                                };
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
