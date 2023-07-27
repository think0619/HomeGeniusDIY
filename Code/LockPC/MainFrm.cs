
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
     
        public MainFrm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
         
            MQTTHandler _MQTTHandler = new MQTTHandler();
            _MQTTHandler.HandleMQTT(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            #region add notifyIcon
            notifyIcon1.Icon = new System.Drawing.Icon(@"tv.ico");
            notifyIcon1.Visible = true;
            notifyIcon1.Text = "远程控制";
            notifyIcon1.Visible = true;
            this.notifyIcon1.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.notifyIcon1.DoubleClick += (obj, ev) =>
            {
                this.ShowInTaskbar = true;
                this.Show();
            };
            this.notifyIcon1.ContextMenuStrip.Items.Add("Dashboard", null, (obj, ej) =>
            {
                this.ShowInTaskbar = true;
                this.Show();
            });
            this.notifyIcon1.ContextMenuStrip.Items.Add("Exit", null, (obj, ej) =>
            { 
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                System.Windows.Forms.Application.Exit();
            });
            #endregion 







            Task.Factory.StartNew(() =>
            { 
                Task.Delay(3 * 1000).ContinueWith(t =>
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        this.Hide();
                    }));
                });
            });
        }

        /// <summary>
        /// init vlcplayer
        /// </summary>
        public void InitVideoPlayer()
        { 
        
        }

        /// <summary>
        /// update vlc player source
        /// </summary>
        /// <param name="newsrc"></param>
        public void UpdateMQTTInfoDelegate(string info)
        {
            this.label1.Invoke(new MethodInvoker(delegate ()
            {
                this.label1.Text = String.Format(info);
            }));
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
           e.Cancel = true;
        }

        //public void UpdateInfo(string  src) 
        //{
        //    this.label1.Invoke(new MethodInvoker(delegate ()
        //    { 
        //        this.label1.Text= String.Format($"当前播放Url:{src}");
        //    }));
        //} 
    }
}
