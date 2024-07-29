
namespace GetAudioFromVideo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.mp3RadioBtn = new System.Windows.Forms.RadioButton();
            this.aacRadioBtn = new System.Windows.Forms.RadioButton();
            this.filetypeTextbox = new System.Windows.Forms.TextBox();
            this.outputPathTextbox = new System.Windows.Forms.TextBox();
            this.srcFileTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.processIndexLabel = new System.Windows.Forms.Label();
            this.processingFileLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.outoutFileTextbox = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(81)))), ((int)(((byte)(219)))));
            this.button1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(5, 378);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(897, 54);
            this.button1.TabIndex = 0;
            this.button1.Text = "Convert";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(151, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Select Source Folder";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 252);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(151, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Select Output Folder";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.filetypeTextbox);
            this.groupBox1.Controls.Add(this.outputPathTextbox);
            this.groupBox1.Controls.Add(this.srcFileTextBox);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(896, 350);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.mp3RadioBtn);
            this.groupBox3.Controls.Add(this.aacRadioBtn);
            this.groupBox3.Location = new System.Drawing.Point(771, 240);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(119, 35);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Output Type";
            // 
            // mp3RadioBtn
            // 
            this.mp3RadioBtn.AutoSize = true;
            this.mp3RadioBtn.Location = new System.Drawing.Point(53, 12);
            this.mp3RadioBtn.Name = "mp3RadioBtn";
            this.mp3RadioBtn.Size = new System.Drawing.Size(41, 16);
            this.mp3RadioBtn.TabIndex = 7;
            this.mp3RadioBtn.TabStop = true;
            this.mp3RadioBtn.Text = "mp3";
            this.mp3RadioBtn.UseVisualStyleBackColor = true;
            // 
            // aacRadioBtn
            // 
            this.aacRadioBtn.AutoSize = true;
            this.aacRadioBtn.Location = new System.Drawing.Point(6, 13);
            this.aacRadioBtn.Name = "aacRadioBtn";
            this.aacRadioBtn.Size = new System.Drawing.Size(41, 16);
            this.aacRadioBtn.TabIndex = 6;
            this.aacRadioBtn.TabStop = true;
            this.aacRadioBtn.Text = "aac";
            this.aacRadioBtn.UseVisualStyleBackColor = true;
            // 
            // filetypeTextbox
            // 
            this.filetypeTextbox.Location = new System.Drawing.Point(784, 20);
            this.filetypeTextbox.Name = "filetypeTextbox";
            this.filetypeTextbox.Size = new System.Drawing.Size(100, 21);
            this.filetypeTextbox.TabIndex = 5;
            this.filetypeTextbox.Text = "mp4";
            // 
            // outputPathTextbox
            // 
            this.outputPathTextbox.Location = new System.Drawing.Point(6, 281);
            this.outputPathTextbox.Multiline = true;
            this.outputPathTextbox.Name = "outputPathTextbox";
            this.outputPathTextbox.Size = new System.Drawing.Size(884, 60);
            this.outputPathTextbox.TabIndex = 4;
            // 
            // srcFileTextBox
            // 
            this.srcFileTextBox.Location = new System.Drawing.Point(6, 49);
            this.srcFileTextBox.Multiline = true;
            this.srcFileTextBox.Name = "srcFileTextBox";
            this.srcFileTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.srcFileTextBox.Size = new System.Drawing.Size(884, 185);
            this.srcFileTextBox.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.processIndexLabel);
            this.groupBox2.Controls.Add(this.processingFileLabel);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.outoutFileTextbox);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Location = new System.Drawing.Point(5, 457);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(897, 182);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output";
            // 
            // processIndexLabel
            // 
            this.processIndexLabel.AutoSize = true;
            this.processIndexLabel.Location = new System.Drawing.Point(831, 28);
            this.processIndexLabel.Name = "processIndexLabel";
            this.processIndexLabel.Size = new System.Drawing.Size(0, 12);
            this.processIndexLabel.TabIndex = 4;
            // 
            // processingFileLabel
            // 
            this.processingFileLabel.AutoSize = true;
            this.processingFileLabel.Location = new System.Drawing.Point(84, 28);
            this.processingFileLabel.Name = "processingFileLabel";
            this.processingFileLabel.Size = new System.Drawing.Size(17, 12);
            this.processingFileLabel.TabIndex = 3;
            this.processingFileLabel.Text = "  ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Processing:";
            // 
            // outoutFileTextbox
            // 
            this.outoutFileTextbox.Location = new System.Drawing.Point(6, 59);
            this.outoutFileTextbox.Multiline = true;
            this.outoutFileTextbox.Name = "outoutFileTextbox";
            this.outoutFileTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outoutFileTextbox.Size = new System.Drawing.Size(885, 73);
            this.outoutFileTextbox.TabIndex = 1;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 138);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(116, 23);
            this.button4.TabIndex = 0;
            this.button4.Text = "Output Folder";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 651);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Get Audio from Video";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox outoutFileTextbox;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox outputPathTextbox;
        private System.Windows.Forms.TextBox srcFileTextBox;
        private System.Windows.Forms.TextBox filetypeTextbox;
        private System.Windows.Forms.RadioButton mp3RadioBtn;
        private System.Windows.Forms.RadioButton aacRadioBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label processingFileLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label processIndexLabel;
    }
}

