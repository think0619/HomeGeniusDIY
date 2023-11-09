using System;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Websocket.Client;

namespace WSTest
{
    internal class Program
    {
        static   void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string wsurl = String.Format($"ws://openapi.iot02.com:60092/ws");
            var exitEvent = new ManualResetEvent(false);
            var url = new Uri(wsurl);

            using (var client = new WebsocketClient(url))
            {
                client.ReconnectTimeout = TimeSpan.FromSeconds(30);
                client.ReconnectionHappened.Subscribe(info =>
                   Console.WriteLine($"Reconnection happened, type: {info.Type}"));

                string EquipState = "{\"infotype\":\"EquipState\"";

                client.MessageReceived
                    .Where(msg => 
                    msg.ToString().Contains(EquipState))
                    .ObserveOn(TaskPoolScheduler.Default)
                    .Subscribe(obj =>
                    {
                        Console.WriteLine("EquipState");

                    });

                client.MessageReceived
                    .Where(msg =>
                    !msg.ToString().Contains(EquipState))
                   .ObserveOn(TaskPoolScheduler.Default)
                   .Subscribe(async obj =>
                   {
                        Console.WriteLine(obj.ToString());
                        await File.AppendAllTextAsync("E:\\juyinresult.txt", obj.ToString());
                   });



                //client.MessageReceived.Subscribe(async msg => { 
                //    await File.AppendAllTextAsync("E:\\juyinresult.txt",msg.ToString());
                //}  );
                client.Start();

                SendMsg sendmsg = new SendMsg()
                {
                    token = "API_0011",
                    infotype = "register",
                    info = ""
                };


                Task.Run(() => client.Send(JsonHelper.SerializeObject(sendmsg)));

                exitEvent.WaitOne();
            }


        }
    }

    public class SendMsg
    {
        public string token { get; set; }
        public string infotype { get; set; }
        public string info { get; set; }
    }
}
