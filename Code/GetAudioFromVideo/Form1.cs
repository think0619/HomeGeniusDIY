using LoadingIndicator.WinForms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Threading;


namespace GetAudioFromVideo
{

    public partial class Form1 : Form
    {
        string videoFilePath = String.Empty;
        string audioFilePath = String.Empty;

        private LongOperation _longOperation;

        string outputFilePath = String.Empty;
        string srcFolderPath = String.Empty;
        string filetype = String.Empty;

        WaitWnd.WaitWndFun waitForm = new WaitWnd.WaitWndFun();
        public Form1()
        {
            InitializeComponent();
            _longOperation = new LongOperation(this);
            this.aacRadioBtn.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if ((!String.IsNullOrWhiteSpace(this.srcFileTextBox.Text)) && (!String.IsNullOrWhiteSpace(this.outputFilePath)))
            {
                string outputinfo = "";
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "ffmpeg.exe";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    //[non-reencoding]    ffmpeg -i input-video.avi -vn -acodec copy output-audio.aac
                    //[re-encoding] ffmpeg -i sample.avi -q:a 0 -map a sample.mp3 
                    DirectoryInfo selectFolder = new DirectoryInfo(srcFolderPath);
                    FileInfo[] Files = selectFolder.GetFiles($"*.{filetype}"); //Getting Text files

                    waitForm.Show(this);
                    if (Files?.Length > 0)
                    {
                        for (int i = 0; i < Files.Length; i++)
                        {
                            var file = Files[i];
                            // file.Name 
                            string outputFileName = $"{file.Name.Replace(file.Extension, "")}";
                            string outputFullName = Path.Combine(outputFilePath, outputFileName);
                            string argu = "";
                            if (this.aacRadioBtn.Checked)
                            {
                                argu = String.Format($"-autoexit -y -i \"{file.FullName}\" -vn -acodec copy \"{outputFullName}.aac\"");
                                outputinfo = $"{outputinfo}{Environment.NewLine}{outputFileName}.aac";
                            }
                            else
                            {
                                argu = String.Format($" -autoexit -y -i \"{file.FullName}\"  -q:a 0 -map a  \"{outputFullName}.mp3\"");
                                outputinfo = $"{outputinfo}{Environment.NewLine}{outputFileName}.mp3";
                            }
                            process.StartInfo.Arguments = argu;
                            process.StartInfo.RedirectStandardOutput = true;

                            this.processingFileLabel.Invoke((MethodInvoker)delegate
                            {
                                this.processingFileLabel.Text = file.Name;
                            });

                            this.outoutFileTextbox.Invoke((MethodInvoker)delegate
                            {
                                this.outoutFileTextbox.Text = outputinfo;
                            });
                            this.processIndexLabel.Invoke((MethodInvoker)delegate
                            {
                                this.processIndexLabel.Text = $"{i + 1}/{Files.Length}";
                            });
                            //System.Threading.Thread.Sleep(3000);
                            process.Start();
                            process.WaitForExit();
                        }
                    }
                    waitForm.Close();
                }
                //videoFilePath = String.Empty;
                //audioFilePath = String.Empty;
                this.outoutFileTextbox.Text = outputinfo;
                MessageBox.Show("Done.", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            srcFolderPath = "";
            CommonOpenFileDialog dialog = new CommonOpenFileDialog("请选择源文件夹");
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                srcFolderPath = dialog.FileName;
                filetype = this.filetypeTextbox.Text;
                DirectoryInfo selectFolder = new DirectoryInfo(dialog.FileName);
                FileInfo[] Files = selectFolder.GetFiles($"*.{filetype}"); //Getting Text files
                string str = "";

                int index = 0;
                foreach (FileInfo file in Files)
                {
                    str = $"{str}{Environment.NewLine}NO{(++index).ToString()}. {file.Name}";
                }
                this.srcFileTextBox.Text = str;
                this.outoutFileTextbox.Text = "";
                this.processingFileLabel.Text = "";
                this.processIndexLabel.Text = $"0/{Files.Length}";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            outputFilePath = "";
            CommonOpenFileDialog dialog = new CommonOpenFileDialog("请选择输出文件夹");
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                outputFilePath = dialog.FileName;
                this.outputPathTextbox.Text = outputFilePath;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // opens the folder in explorer
            if (Directory.Exists(outputFilePath))
            {
                Process.Start(outputFilePath);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            waitForm.Show(this);
            Thread.Sleep(5000);
            waitForm.Close();
        }
    }
}
