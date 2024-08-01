using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using MQTTnet.Client;
using MQTTnet;
using System.Threading;
using System.Net.NetworkInformation;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System.Diagnostics;
using RestSharp;
using System.IO;
using System.Timers;
using log4net;

namespace OfficeControl
{
    public partial class Form1 : Form
    {
        private static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        MqttFactory mqttFactory;
        IMqttClient _mqttClient;
        const string MQTTTopic = "OtherEquip";
        System.Timers.Timer timer;

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MQTTStatusLabel.Text = "未连接.";
            mqttFactory = new MqttFactory();
            _mqttClient = mqttFactory.CreateMqttClient();
            ConnectMQTT();
            InitTimer(); 
        }
 

        public async void ConnectMQTT()
        {
            string _mqttbroker = ConfigurationManager.AppSettings["mqttbroker"].ToString();
            string _mqttuser = ConfigurationManager.AppSettings["mqttuser"].ToString();
            string _mqttpwd = ConfigurationManager.AppSettings["mqttpwd"].ToString();
            string clientID = ConfigurationManager.AppSettings["PCClientID"].ToString();
            clientID = $"{clientID}_{GetMacAddress().ToString()}";
            var mqttClientOptions = new MqttClientOptionsBuilder()
                                            .WithClientId(clientID)
                                            .WithTcpServer(_mqttbroker, 1883)
                                            .WithCredentials(_mqttuser, _mqttpwd)
                                            .Build();

            //_mqttClient.ApplicationMessageReceivedAsync += arg =>
            //{
            //    var topic = arg?.ApplicationMessage?.Topic;
            //    if (MQTTTopic.Equals(topic))
            //    {
            //        var payloadText = Encoding.UTF8.GetString(arg?.ApplicationMessage?.Payload ?? Array.Empty<byte>());
            //        if ($"LOCKPC_{clientID}" == payloadText)
            //        {
            //            LockWorkStation();
            //        }
            //        else if (payloadText.IndexOf($"Msg_{clientID}") == 0)
            //        {
            //            string msgbody = payloadText.Replace($"Msg_{clientID}_", "");
            //            if (!String.IsNullOrWhiteSpace(msgbody))
            //            {
            //                this.BeginInvoke(new Action(() => { this.TopMost = true; this.Opacity = 1; this.Show(); }));
            //                this.msgtextbox.BeginInvoke(new Action(() => { this.msgtextbox.Text = msgbody; }));
            //                Task.Delay(5000).ContinueWith(a =>
            //                {
            //                    this.BeginInvoke(new Action(() => { this.Opacity = 0; }));
            //                });
            //            }
            //        }
            //    }
            //    return Task.CompletedTask;
            //};

            _mqttClient.DisconnectedAsync += async e =>
            {
                if (e.ClientWasConnected)
                {
                    _logger.Error("mqtt断开");
                    this.MQTTStatusLabel.BeginInvoke(new Action(() => {
                        this.MQTTStatusLabel.Text = "已断开.";
                        SoftBlink(this.MQTTStatusLabel, Color.FromArgb(30, 30, 30), Color.Red, 2000, true); 
                    }));
                    await _mqttClient.ConnectAsync(_mqttClient.Options);
                    this.MQTTStatusLabel.BeginInvoke(new Action(() => { this.MQTTStatusLabel.Text = "已连接.";
                        SoftBlink(this.MQTTStatusLabel, Color.FromArgb(30, 30, 30), Color.Red, 2000, true); 
                    }));
                }
            };
            try
            {
                await _mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
                _logger.Info("mqtt已连接");
                this.MQTTStatusLabel.BeginInvoke(new Action(() => { 
                    this.MQTTStatusLabel.Text = "已连接."; 
                    SoftBlink(this.MQTTStatusLabel, Color.FromArgb(30, 30, 30), Color.Green, 2000, true);
                }));

                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                    .WithTopicFilter(f => { f.WithTopic(MQTTTopic); }) 
                    .Build();

                await _mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

            }
            catch (Exception ex) { }
        }

        public async void SendMsg(string msg) 
        {
            if (_mqttClient != null && _mqttClient.IsConnected)
            {
                var message = new MqttApplicationMessageBuilder()
                          .WithTopic(MQTTTopic)
                          .WithPayload(msg)
                          .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                          .WithRetainFlag()
                          .Build();
                _logger.Info($"mqtt msg:{msg}");
                await _mqttClient.PublishAsync(message);
            }
        } 

        public static PhysicalAddress GetMacAddress()
        {
            var myInterfaceAddress = NetworkInterface.GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .OrderByDescending(n => n.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                .Select(n => n.GetPhysicalAddress())
                .FirstOrDefault();

            return myInterfaceAddress;
        }
        private async void SoftBlink(Control ctrl, Color c1, Color c2, short CycleTime_ms, bool BkClr)
        {
            var sw = new Stopwatch(); sw.Start();
            short halfCycle = (short)Math.Round(CycleTime_ms * 0.5);
            while (true)
            {
                await Task.Delay(1);
                var n = sw.ElapsedMilliseconds % CycleTime_ms;
                var per = (double)Math.Abs(n - halfCycle) / halfCycle;
                var red = (short)Math.Round((c2.R - c1.R) * per) + c1.R;
                var grn = (short)Math.Round((c2.G - c1.G) * per) + c1.G;
                var blw = (short)Math.Round((c2.B - c1.B) * per) + c1.B;
                var clr = Color.FromArgb(red, grn, blw);
                if (BkClr) ctrl.BackColor = clr; else ctrl.ForeColor = clr;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendMsg("officedooropen");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SendMsg("aircondition");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_mqttClient!=null&&(!_mqttClient.IsConnected))
            {
                ConnectMQTT();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
        }

        public async void SendNetMsg() 
        {
            var weatherClient = new RestClient("https://hw.hellolinux.cn:8888");

            var request = new RestRequest($"/api/espservice/exe")
                .AddParameter("flag", "OtherEquip")
                .AddParameter("cmd", "aircondition");
            request.Timeout = TimeSpan.FromSeconds(5);
            try
            {
                var response = await weatherClient.GetAsync<string>(request); 
            }
            catch (Exception ex)
            { 
            }
        }

        private void InitTimer()
        {
            //设置定时间隔(毫秒为单位)
            int interval = 1000;
            timer = new System.Timers.Timer(interval);
            //设置执行一次（false）还是一直执行(true)
            timer.AutoReset = true;
            //设置是否执行System.Timers.Timer.Elapsed事件
            timer.Enabled = true;
            //绑定Elapsed事件
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
         
            int intHour = e.SignalTime.Hour;
            int intMinute = e.SignalTime.Minute;
            int intSecond = e.SignalTime.Second; 
            int iHour =6;
            int iMinute = 0;
            int iSecond = 0;
            // 设置 每天的00：00：00开始执行程序
            if (intHour == iHour && intMinute == iMinute && iSecond == intSecond)
            {
                if (timeStartCheckBox.Checked)
                {
                    _logger.Info($"Automatically send message: 'aircondition'");
                    SendMsg("aircondition");
                }
            }
        } 

    }
}
