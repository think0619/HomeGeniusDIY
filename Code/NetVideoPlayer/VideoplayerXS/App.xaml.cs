using SqliteHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VideoplayerXS.Models;

namespace VideoplayerXS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string AuthInfo;

        public App()
        {
            AuthInfo = String.Empty;
            this.Startup += new StartupEventHandler(SQLiteInitial);
        }

        



        public void SQLiteInitial(object sender,StartupEventArgs e) 
        { 
            DatabaseClient sql = new DatabaseClient("app.db");

            ///check or create config table
            //check
            if (!sql.TableExists("tb_config")) 
            {
                List<Column> columns = new List<Column>();
                columns.Add(new Column("id", true, DataType.Integer, false));
                columns.Add(new Column("key", false, DataType.Text, true));
                columns.Add(new Column("value", false, DataType.Text, true));
                columns.Add(new Column("protocol", false, DataType.Text, true));
                columns.Add(new Column("status", false, DataType.Integer, true));
                //create 
                sql.CreateTable(ConstantValues.AutoConfig, columns);
            }

            //get config value
            //   string host = ConfigureHelper.ReadKey("ConnectionString:Host");
            SqliteHelper.Expression eRetrieve1 = new SqliteHelper.Expression("key", Operators.Equals, "current");
            DataTable selectResult1 = sql.Select(ConstantValues.AutoConfig, 0, null, null, eRetrieve1, null);
            if(selectResult1!=null&&selectResult1.Rows.Count>0) 
            {
                App.Current.Properties[ConstantValues.CurrentAuthInfo] = selectResult1.Rows[0]["value"].ToString();
            }

        }

        public void SetInitialConfig() 
        {
           
        }
    }

 
}
