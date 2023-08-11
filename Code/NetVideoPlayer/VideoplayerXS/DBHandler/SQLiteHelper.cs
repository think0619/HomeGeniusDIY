using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;  

namespace VideoplayerXS.DBHandler
{
    public class SQLiteHelper
    {
        public static void InitializeDatabase()
        {
            string dbpath=Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"DB", "sqliteSample.db");
            if (!File.Exists(dbpath)) 
            {
               File.Create(dbpath );
            }
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
                    "Text_Entry NVARCHAR(2048) NULL)";

                var createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
            //await ApplicationData.Current.LocalFolder
            //        .CreateFileAsync("sqliteSample.db", CreationCollisionOption.OpenIfExists);
            //string dbpath =System.IO.Path.Combine(ApplicationData.Current.LocalFolder.Path,
            //                             "sqliteSample.db");
            //using (var db = new SqliteConnection($"Filename={dbpath}"))
            //{
            //    db.Open();

            //    string tableCommand = "CREATE TABLE IF NOT " +
            //        "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
            //        "Text_Entry NVARCHAR(2048) NULL)";

            //    var createTable = new SqliteCommand(tableCommand, db);

            //    createTable.ExecuteReader();
            //}
        }

    }
}
