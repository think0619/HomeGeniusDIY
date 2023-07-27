using LibVLCSharp.Shared;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VideoplayerXS.Common;
using VideoplayerXS.Protocols;

namespace VideoplayerXS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LibVLC _libvlc;
        public MainWindow()
        {
            _libvlc = new LibVLC();
            InitializeComponent();
        }

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
        /// 
        /// </summary>
        private void LoadVLClib()
        {
             
            LibVLCSharp.Shared.MediaPlayer player = new LibVLCSharp.Shared.MediaPlayer(_libvlc);
            video_main.MediaPlayer = player;
            player.AspectRatio = "16:9";
            video_main.MediaPlayer.VolumeChanged += (a, b) =>
            {
                this.volumnlable.Dispatcher.Invoke(() => {
                    this.volumnlable.Content = video_main.MediaPlayer.Volume;
                });

            };
            //using (LibVLCSharp.Shared.Media media = new Media(_libvlc, new Uri(url)))
            //{
            //    video_main.MediaPlayer.Play(media);
            //}
        }

        private bool LoginSSH(SSHCon sshcon,out string sshConnectStringHex) 
        { 
            bool connected=false;
            using (var client = new SshClient(sshcon.Host, sshcon.Port, sshcon.Username, sshcon.Password))
            {
                try
                {
                    //Start the connection
                    client.Connect();
                    const string teststring = "test";
                    var output = client.RunCommand($"echo {teststring}");
                    if (output.Result.Equals($"{teststring}\n"))
                    {
                        connected = true;
                    }
                    client.Disconnect();
                }
                catch 
                {
                } 
            }
            sshConnectStringHex = sshcon.GetSSHConnectionString();
            return connected;
            //using (SftpClient sftp = new SftpClient(host, port, username, pwdhex))
            //{
            //    try
            //    {
            //        sftp.Connect(); 
            //        //sftp.Disconnect(); 
            //    }
            //    catch (Exception ex)
            //    { 
            //    }
            //} 
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadVLClib();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            video_main.MediaPlayer?.Stop();
            string host = ConfigureHelper.ReadKey("ConnectionString:Host");
            string port = ConfigureHelper.ReadKey("ConnectionString:Port");
            string username = ConfigureHelper.ReadKey("ConnectionString:UserName");
            string password = ConfigureHelper.ReadKey("ConnectionString:Password");
            if (!String.IsNullOrWhiteSpace(host))
            {
                Int32.TryParse(port, out int _port);
                SSHCon ssh = new SSHCon(host, username, password, _port);

                if (LoginSSH(ssh, out string sshconstring))
                {
                     string videoUrl = $"{sshconstring}/media/电影天堂dygod.org.犯罪都市3.2023.BD.1080P.韩语中字.mkv";
                    //  string videoUrl = $"{sshconstring}/media/1.mp4";
                    using (LibVLCSharp.Shared.Media media = new Media(_libvlc, new Uri(videoUrl)))
                    {
                        video_main.MediaPlayer?.Play(media);
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (video_main.MediaPlayer != null)
            {
                video_main.MediaPlayer.Volume = video_main.MediaPlayer.Volume + 1; 
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (video_main.MediaPlayer != null)
            {
                video_main.MediaPlayer.Volume = video_main.MediaPlayer.Volume - 1; 

            }
        }
    }
}
