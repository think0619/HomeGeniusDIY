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
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VLCM3U8.Common;

namespace VLCM3U8
{
    public partial class MainFrm : Form
    { 
        LibVLC libvlc;
        public MainFrm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            MQTTHandler _MQTTHandler = new MQTTHandler();
            _MQTTHandler.HandleWS(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            libvlc = new LibVLC();
            Core.Initialize();
            InitVideoPlayer();

            String initUrl = ConfigureHelper.ReadKey("VlC:InitUrl");
            if (Uri.IsWellFormedUriString(initUrl, UriKind.RelativeOrAbsolute))
            {
                var media = new Media(libvlc, new Uri(initUrl));
                this.videoView1.MediaPlayer.Play(media);
                UpdateInfo(initUrl);
                //ThreadPool.QueueUserWorkItem(_ => this.videoView1.MediaPlayer.Play(media));
            }
        }

        /// <summary>
        /// init vlcplayer
        /// </summary>
        public void InitVideoPlayer()
        { 
            this.videoView1.MediaPlayer = new MediaPlayer(libvlc);
            this.videoView1.Dock = DockStyle.Fill;
            this.videoView1.MediaPlayer.EndReached += (s, e1) =>{  };
        }

        /// <summary>
        /// update vlc player source
        /// </summary>
        /// <param name="newsrc"></param>
        public void UpdateVLCSourceDelegate(string newsrc)
        {
            this.videoView1.Invoke(new MethodInvoker(delegate ()
              {
                  if (Uri.IsWellFormedUriString(newsrc, UriKind.RelativeOrAbsolute))
                  {
                      var media = new Media(libvlc, new Uri(newsrc));
                      this.videoView1.MediaPlayer.Play(media);
                      UpdateInfo(newsrc);
                  }
              }));
        }

        public void UpdateInfo(string  src) 
        {
            this.label1.Invoke(new MethodInvoker(delegate ()
            { 
                this.label1.Text= String.Format($"当前播放Url:{src}");
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("x");
        }
    }
}
