using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VLCM3U8
{
    public partial class MainFrm : Form
    {
        //MqttFactory mqttFactory = null;
        //MQtt服务端
        private static MqttServer mqttServer = null;

        private VideoView vvv;

        LibVLC libvlc;
        public MainFrm()
        {
            InitializeComponent();
            libvlc = new LibVLC();
            vvv=new VideoView();

            this.StartPosition= FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Core.Initialize();
            InitVideoPlayer();
            ////单个循环
            //var _media = new Media(libvlc, new Uri(videoFilePath[videoCurrentIndex]));
            //ThreadPool.QueueUserWorkItem(_ => this.videoView1.MediaPlayer.Play(_media));
            string v1 = "http://seb.sason.top/sc/dsxw_fhd.m3u8";
            string v2 = "https://streamak0138.akamaized.net/live0138lh-mbm9/_definst_/rti3/chunklist.m3u8";
            

            string url = v1;

            var media = new Media(libvlc,new Uri(url));
            this.vvv.MediaPlayer.Play(media);
            ThreadPool.QueueUserWorkItem(_ => this.vvv.MediaPlayer.Play(media));
        }

        public void InitVideoPlayer()
        {

            vvv.MediaPlayer = new MediaPlayer(libvlc);
            //vvv.Dock = DockStyle.Fill;
            vvv.MediaPlayer.EndReached += (s, e1) =>
            {
                ////单个循环时 超过设定时常 改为列表循环
                //int setSeconds = 120;
                //string _SingleVideoCycleMaxSecs = ConfigurationManager.AppSettings["SingleVideoCycleMaxSecs"].ToString();
                //Int32.TryParse(_SingleVideoCycleMaxSecs, out setSeconds);
                //if (videoPlayMode == VideoPlayMode.SingleCycle)
                //{
                //    var playDuration = (videoView1.MediaPlayer.Media.Duration / 1000) * videoCurrentPlayCount;
                //    if (setSeconds <= playDuration)
                //    {
                //        videoPlayMode = VideoPlayMode.ListCycle;
                //    }
                //}

                ////列表循环
                //if (videoPlayMode == VideoPlayMode.ListCycle)
                //{
                //    while (true)
                //    {
                //        videoCurrentIndex = (++videoCurrentIndex) % videoFilePath.Length;
                //        videoCurrentPlayCount = 1;
                //        if (File.Exists(videoFilePath[videoCurrentIndex]))
                //        {
                //            break;
                //        }
                //    }
                //}
                //else
                //{
                //    videoCurrentPlayCount++;
                //}

                ////单个循环
                //var _media = new Media(libvlc, new Uri(videoFilePath[videoCurrentIndex]));
                //ThreadPool.QueueUserWorkItem(_ => this.videoView1.MediaPlayer.Play(_media));
            };
        }

        private   void button1_Click(object sender, EventArgs e)
        {
              HandleWS(this);
        }


        public async void HandleWS(Form form) 
        {
             
            /*
            * This sample subscribes to a topic and processes the received message.
            */

            MqttFactory mqttFactory = new MqttFactory();

            var mqttClient = mqttFactory.CreateMqttClient();
            
                //var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("broker.hivemq.com").Build();
                var mqttClientOptions = new MqttClientOptionsBuilder().WithWebSocketServer("ws://127.0.0.1:8083/mqtt").Build();

                // Setup message handling before connecting so that queued messages
                // are also handled properly. When there is no event handler attached all
                // received messages get lost.
                mqttClient.ApplicationMessageReceivedAsync += e =>
                {
                   var msg= e.ApplicationMessage;
                    var m1= msg.Payload.ToString();
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
