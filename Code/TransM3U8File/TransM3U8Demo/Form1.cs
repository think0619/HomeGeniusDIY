namespace TransM3U8Demo
{
    public partial class Form1 : Form
    {
        public const string EXTINF="#EXTINF";
        public const string groupName = "groupName";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<SourceItem> list = new List<SourceItem>();
            string filePath = @"C:\Users\xs\Downloads\M3U8-master\M3U8-master\all.m3u";
            List<string> fileText = (from line in File.ReadLines(filePath) select line).ToList();
           
            SourceItem item = new SourceItem();

            foreach (string _line in fileText)
            {
                string line = _line.Trim();
                if (line.IndexOf(EXTINF) == 0) 
                {
                    item = new SourceItem();
                    //#EXTINF:-1 group-title="groupName=¹úÄÚ",CCTV-1×ÛºÏ
                    string[] infos= line.Split(',');
                    if (infos.Length > 1) 
                    { 
                        item.SrcName= infos[1];

                        int nameIndex = infos[0].IndexOf(groupName);
                        if(nameIndex != -1) 
                        {
                            string gropuInfo= infos[0].Substring(nameIndex, infos[0].Length-nameIndex);
                            gropuInfo = gropuInfo.Replace(groupName,"").Replace("=", "").Replace("\"", "").Replace("'", "");
                            item.GroupName= gropuInfo; 
                        } 
                    }
                }
                else if (Uri.IsWellFormedUriString(_line, UriKind.RelativeOrAbsolute))
                {
                    item.SrcUrl = _line;
                    list.Add(item);
                }
            }
        }
    }
}