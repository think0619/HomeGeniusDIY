using Aspose.Cells;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetFileList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            Aspose.Cells.License license = new Aspose.Cells.License();
            try
            {
                license.SetLicense("Aspose.Total.lic");
                Console.WriteLine("License set successfully.");
            }
            catch (Exception e) { }
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            CommonOpenFileDialog ofd = new CommonOpenFileDialog();
            ofd.IsFolderPicker = true;   //设置为选择文件夹
            if (ofd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                textBox1.Text = ofd.FileName; 
            } 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                string[] files = Directory.GetFiles(this.textBox1.Text); // 获取指定目录下的所有文件

                // initiate an instance of Workbook
                var book = new Aspose.Cells.Workbook();
                // access first (default) worksheet
                var sheet = book.Worksheets[0];
                // access CellsCollection of first worksheet
                var cells = sheet.Cells;
                cells["A1"].Value = "File Name";
                Cell cell = sheet.Cells["A1"];
                Style style = cell.GetStyle();
                style.Font.Name = "Times New Roman";
                style.Font.Color = Color.White;
                style.ForegroundColor = Color.Blue;
                style.Pattern = BackgroundType.Solid;
                cell.SetStyle(style);
              
                 

                int i = 2;
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file); // 从文件路径中获取文件名
                    listBox1.Items.Add(fileName); // 将文件名添加到ListBox中或者您想要展示的控件中
                    cells[$"A{i++}"].Value = fileName;
                }
               
                // write HelloWorld to cells A1
                
              
                // save spreadsheet to disc
                book.Save("output.xlsx", SaveFormat.Xlsx);
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生错误: " + ex.Message);
            }
        }
    }
}
