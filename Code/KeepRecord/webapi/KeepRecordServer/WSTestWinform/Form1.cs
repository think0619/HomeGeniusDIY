using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Websocket.Client;

namespace WSTestWinform
{
    public partial class Form1 : Form
    {

        //自动回传
        static string AutoResoneEquipStateResponse = "{\"infotype\":\"EquipState\"";

        //请求返回单个
        static string SingleEquipStateResponse = "{\"infotype\":\"damread\"";

        //请求返回所有
        static string AllEquipStateResponse = "{\"infotype\":\"damreadall\"";

        //登录状态信息
        static string AuthStateResponse = "{\"infotype\":\"register\"";

        string wsurl = String.Format($"ws://openapi.iot02.com:60092/ws");

        WebsocketClient client = null;

        public Form1()
        {
            InitializeComponent();
            ManualResetEvent exitEvent = new ManualResetEvent(false);
            var url = new Uri(wsurl);
            client = new WebsocketClient(url);
            SendMsg sendmsg = new SendMsg()
            {
                token = "API_0011",
                infotype = "register",
                info = ""
            };

            {
                client.ReconnectTimeout = TimeSpan.FromSeconds(60);
                client.ReconnectionHappened.Subscribe(info =>
                {
                    //client.Send(JsonHelper.SerializeObject(sendmsg));
                    //  Console.WriteLine($"Reconnection happened, type: {info.Type}");
                });
                 

                //client.MessageReceived
                //    .Where(msg =>
                //    msg.ToString().Contains(AutoResoneEquipStateResponse))
                //    .ObserveOn(TaskPoolScheduler.Default)
                //    .Subscribe(async obj =>
                //    {
                //        await File.AppendAllTextAsync("E:\\juyinTest\\autoresponse.txt", obj.ToString());
                //        var xx = JsonHelper.DeserializeJsonToObject<Entities.MachineSingleResponseMsg>(obj.ToString());

                //    });

                //client.MessageReceived
                //    .Where(msg =>
                //    msg.ToString().Contains(SingleEquipStateResponse))
                //    .ObserveOn(TaskPoolScheduler.Default)
                //    .Subscribe(async obj =>
                //    {
                //        await File.AppendAllTextAsync("E:\\juyinTest\\singleInforesponse.txt", obj.ToString());

                //    });

                //client.MessageReceived
                //    .Where(msg =>
                //    msg.ToString().Contains(AllEquipStateResponse))
                //    .ObserveOn(TaskPoolScheduler.Default)
                //    .Subscribe(async obj =>
                //    {
                //        await
                //        File.AppendAllTextAsync("E:\\juyinTest\\allInforesponse.txt", obj.ToString());
                //        var xx = JsonHelper.DeserializeJsonToObject<Entities.MachineRAllResponseMsg>(obj.ToString());
                //    });

                client.MessageReceived
                    .Where(msg =>
                    msg.ToString().Contains(AuthStateResponse))
                    .ObserveOn(TaskPoolScheduler.Default)
                    .Subscribe(async obj =>
                    {
                        await File.AppendAllTextAsync("E:\\juyinTest\\12345.txt", obj.ToString());
                        var xx = JsonHelper.DeserializeJsonToObject<Entities.AuthMsg>(obj.ToString());

                    });


                client.Start();
                //API_20220104_ND
                //API_0011 
                Task.Run(() => client.Send(JsonHelper.SerializeObject(sendmsg)));

                //exitEvent.WaitOne();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var allStateCmd = new
            {
                infotype = "damreadall"
            };
            client.Send(JsonHelper.SerializeObject(allStateCmd));
        }
    }


    public class SendMsg
    {
        public string token { get; set; }
        public string infotype { get; set; }
        public string info { get; set; }
    }
}
