using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VideoplayerXS
{
    public partial class MainWindow : Window
    {

        private void video_main_Loaded(object sender, RoutedEventArgs e)
        {
            // LibVLC _libvlc = new LibVLC();
            // LibVLCSharp.Shared.MediaPlayer player = new LibVLCSharp.Shared.MediaPlayer(_libvlc);
            // //video_main.Width = this.Width;
            // //video_main.Height = this.Height;

            // video_main.MediaPlayer = player;
            // //通过设置宽高比为窗体宽高可达到视频铺满全屏的效果
            //// player.AspectRatio = this.Width + ":" + this.Height;
            // string url = @"E:\\Movies\\海边的曼彻斯特\\海边的曼彻斯特.mp4";
            // //using (LibVLCSharp.Shared.Media media = new Media(_libvlc, new Uri(url)))
            // //{
            // //    video_main.MediaPlayer.Play(media);
            // //}

            // //var connectionInfo = new ConnectionInfo("hw.hellolinux.cn",
            // //                            "root",
            // //                            new PasswordAuthenticationMethod("root", @"@Xiongsen1994!+1qaz@WSX"),
            // //                            new PrivateKeyAuthenticationMethod("rsa.key"));
            // //using (var client = new SftpClient(connectionInfo))
            // //{
            // //    client.Connect();
            // //}

            // string host = @"hw.hellolinux.cn";
            // string username = "root"; 
            // string password = @"@Xiongsen1994!+1qaz@WSX";

            // string pwd_hex=StringHelper.Convert2HexString(password);

            // SSH ssh = new SSH(host, username, password)  ;

            // string remoteDirectory = "/";

            // using (SftpClient sftp = new SftpClient(host, username, pwd_hex))
            // {
            //     try
            //     {
            //         string videoUrl = $"{ssh.GetSSHConnectionString()}/media/电影天堂dygod.org.犯罪都市3.2023.BD.1080P.韩语中字.mkv";
            //        // sftp.Connect();
            //         //sftp://user@ip_address:port_number//mnt/some_dir/
            //         string surl = $"sftp://{username}:{pwd_hex}@{host}:22//media/电影天堂dygod.org.犯罪都市3.2023.BD.1080P.韩语中字.mkv";
            //         //var files = sftp.ListDirectory(remoteDirectory);

            //         //foreach (var file in files)
            //         //{
            //         //    Console.WriteLine(file.Name);
            //         //}

            //         //sftp.Disconnect();
            //         using (LibVLCSharp.Shared.Media media = new Media(_libvlc, new Uri(videoUrl)))
            //         {
            //             video_main.MediaPlayer.Play(media);
            //         }
            //     }
            //     catch (Exception ex)
            //     {
            //         Console.WriteLine("An exception has been caught " + e.ToString());
            //     }
            // } 
        }



        /// <summary>
        /// List a remote directory in the console.
        /// </summary>
        private void listFiles()
        {
            string host = @"hw.hellolinux.cn";
            string username = "root";
            string password = @"@Xiongsen1994!+1qaz@WSX";

            string remoteDirectory = "/";

            using (SftpClient sftp = new SftpClient(host, username, password))
            {
                try
                {
                    sftp.Connect();

                    var files = sftp.ListDirectory(remoteDirectory);

                    foreach (var file in files)
                    {
                        Console.WriteLine(file.Name);
                    }

                    sftp.Disconnect();
                }
                catch (Exception e)
                {
                    Console.WriteLine("An exception has been caught " + e.ToString());
                }
            }
        }


    }
}
