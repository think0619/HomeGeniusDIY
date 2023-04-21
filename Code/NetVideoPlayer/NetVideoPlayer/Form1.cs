using LibVLCSharp.Shared; 
namespace NetVideoPlayer
{
    public partial class Form1 : Form
    {
        LibVLC libvlc;
        public Form1()
        {
            InitializeComponent();
            libvlc = new LibVLC();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Core.Initialize();
            InitVideoPlayer();
            string v1 = "http://seb.sason.top/sc/dsxw_fhd.m3u8";
            string v2 = "https://streamak0138.akamaized.net/live0138lh-mbm9/_definst_/rti3/chunklist.m3u8";
            string v3 = "http://cloud-play.hhalloy.com/live/1aab6cf57296bdefc6f4bea94702782a.flv";
             

            var media = new Media(libvlc, new Uri(v3));
            this.videoView1.MediaPlayer?.Play(media);
            ThreadPool.QueueUserWorkItem(_ => this.videoView1.MediaPlayer?.Play(media));
        }

        public void InitVideoPlayer()
        { 
            this.videoView1.MediaPlayer = new MediaPlayer(libvlc);
            this.videoView1.MediaPlayer.EndReached += (s, e1) =>
            { 
                ////µ¥¸öÑ­»·
                //var _media = new Media(libvlc, new Uri(videoFilePath[videoCurrentIndex]));
                //ThreadPool.QueueUserWorkItem(_ => this.videoView1.MediaPlayer.Play(_media));
            };
        }
    }
}