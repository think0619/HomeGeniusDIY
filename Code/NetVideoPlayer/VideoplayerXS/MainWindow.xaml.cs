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
using VideoplayerXS.DBHandler;
using VideoplayerXS.Models;
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
         
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadVLClib();
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
                this.volumnlable.Dispatcher.Invoke(() =>
                {
                    this.volumnlable.Content = video_main.MediaPlayer.Volume;
                });
            }; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sshcon"></param>
        /// <param name="sshConnectStringHex"></param>
        /// <returns></returns>

        private bool LoginSSH(SSHCon sshcon, out string sshConnectStringHex)
        {
            bool connected = false;
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            video_main.MediaPlayer?.Stop();

            var CurrentAuthInfo = App.Current.Properties[ConstantValues.CurrentAuthInfo]?.ToString();
            if (!String.IsNullOrEmpty(CurrentAuthInfo))
            {
                SSHCon sshcon = new SSHCon(CurrentAuthInfo);
                if (LoginSSH(sshcon, out string sshconstring))
                {
                    string videoUrl = $"{sshconstring}/media/电影天堂dygod.org.犯罪都市3.2023.BD.1080P.韩语中字.mkv";
                    // string videoUrl = $"{sshconstring}/home/11露天矿生态修复技术全息沙盘展示.mp4";
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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            video_main.MediaPlayer?.Pause();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

            video_main.MediaPlayer?.Play();
            var x = video_main.MediaPlayer;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //SQLiteHelper.InitializeDatabase();
        }
    }
}
