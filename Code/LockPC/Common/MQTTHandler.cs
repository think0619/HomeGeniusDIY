using MQTTnet.Client;
using MQTTnet;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WindowsInput.Native;
using WindowsInput;

namespace VLCM3U8.Common
{
    public enum MonitorState
    {
        MonitorStateOn = -1,
        MonitorStateOff = 2,
        MonitorStateStandBy = 1
    }
    public class MQTTHandler
    {
        [DllImport("user32.dll")]
        public static extern bool LockWorkStation();

        [DllImport("user32.dll")]
        private static extern int SendMessage(int hWnd, int hMsg, int wParam, int lParam);

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
                              //  UpdateMQTTInfoDelegate
                                mainFrm.UpdateMQTTInfoDelegate("MQTT已断开");

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
                                mainFrm.UpdateMQTTInfoDelegate("MQTT已连接");
                                break;
                            }
                        }
                    });
                };

                try
                {
                    await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
                    await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

                    mainFrm.UpdateMQTTInfoDelegate("MQTT已连接");
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
                            case MQTTCommand.LockScreen: 
                                {
                                    LockWorkStation();
                                    break;
                                };
                            case MQTTCommand.BackDesktop: 
                                {
                                    var simu = new InputSimulator();
                                 
                                    simu.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_D);
                                    break;
                                };
                            case MQTTCommand.MonitorOff: 
                                {
                                    SetMonitorInState(MonitorState.MonitorStateOff);
                                    break;
                                }
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetMonitorInState(MonitorState.MonitorStateOff);
        }

        private void SetMonitorInState(MonitorState state)
        {
            SendMessage(0xFFFF, 0x112, 0xF170, (int)state);
        }

    }
}
