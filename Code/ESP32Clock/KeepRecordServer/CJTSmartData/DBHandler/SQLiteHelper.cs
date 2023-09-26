using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using SqliteHelper;
using Newtonsoft.Json.Linq;
using System.Collections;
using Entities.User;
using Microsoft.AspNetCore.Components.Forms;
using Entities.Content;
using System.Security.Policy;

namespace TextVoiceServer.DBHandler
{
    public class SQLiteHelper
    {
        static readonly string dbFilename = "sysdata.db"; 

        public static void InitializeDatabase()
        {
            string dbFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB");
            if (!Directory.Exists(dbFolderPath))
            {
                Directory.CreateDirectory(dbFolderPath);
            }
            string dbpath = Path.Combine(dbFolderPath, dbFilename);
            if (!File.Exists(dbpath)) 
            {
                File.Create(dbpath).Dispose();
            }
            {
               

                using (var db = new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();
                    string tableCommand = "CREATE TABLE IF NOT " +
                        "EXISTS tb_configresp (" +
                        "id Integer NOT NULL PRIMARY KEY AUTOINCREMENT," +
                        "key Text COLLATE NOCASE," +
                        "value Text COLLATE NOCASE," +
                        "remark Text COLLATE NOCASE," +
                        "status Integer" +
                        ")"; 
                    var createTable = new SqliteCommand(tableCommand, db);
                    createTable.ExecuteReader();

                    string userTableCommand = "CREATE TABLE IF NOT " +
                        "EXISTS tb_user (" +
                        "id Integer NOT NULL PRIMARY KEY AUTOINCREMENT," +
                        "username Text COLLATE NOCASE," +
                        "idcode Text COLLATE NOCASE," +
                        "userinfo Text COLLATE NOCASE," +
                        "status Integer" +
                        ")";
                    var userCreateTable = new SqliteCommand(userTableCommand, db);
                    userCreateTable.ExecuteReader();

                    string apicfgTableCommand = "CREATE TABLE IF NOT " +
                        "EXISTS tb_apiconfig (" +
                        "username Text COLLATE NOCASE," +
                        "password Text COLLATE NOCASE," +
                        "wsurl Text COLLATE NOCASE," +
                        "flag Text COLLATE NOCASE," +
                        "remark Text COLLATE NOCASE" + 
                        ")";
                    var apicfgCreateTable = new SqliteCommand(apicfgTableCommand, db);
                    apicfgCreateTable.ExecuteReader();
                }
            }
        }

        /// <summary>
        /// select value from tb_configresp where key={key} and status=1
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        public static string getConfigValue(string _key)
        {
            string result = string.Empty;
            try
            {
                string dbpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB", dbFilename);
                if (File.Exists(dbpath) && !String.IsNullOrWhiteSpace(_key))
                {
                    DatabaseClient sql = new DatabaseClient(dbpath);
                    Expression eRetrieve1 = new Expression
                    {
                        LeftTerm = new Expression("key", Operators.Equals, _key),
                        Operator = Operators.And,
                        RightTerm = new Expression("status", Operators.Equals, 1)
                    };
                    DataTable selectResult1 = sql.Select("tb_configresp", 0, null, null, eRetrieve1, null);
                    var queryResult = selectResult1.Rows;
                    if (queryResult?.Count > 0)
                    {
                        var firstrow = queryResult[0];
                        result = firstrow["value"].ToString();
                    };
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static string setConfigValue(string _key, string _value)
        {
            string opsresult = "fail.";
            try
            {
                string dbpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB", dbFilename);

                if (File.Exists(dbpath) && !String.IsNullOrEmpty(_key))
                { 
                    DatabaseClient sql = new DatabaseClient(dbpath);
                    Expression expression1 = new Expression
                    {
                        LeftTerm = new Expression("key", Operators.Equals, _key),
                        Operator = Operators.And,
                        RightTerm = new Expression("status", Operators.Equals, 1)
                    };
                    DataTable selectResult1 = sql.Select("tb_configresp", 0, null, null, expression1, null);
                    var queryResult = selectResult1.Rows;
                    if (queryResult?.Count > 0)
                    {
                        Expression expression = new Expression("key", Operators.Equals, _key);
                        sql.Update("tb_configresp", (new Dictionary<string, Object>() { { "value", _value } }), expression);
                        opsresult = "success.";
                    }
                    else
                    {
                        opsresult = $"Fail.Key:{_key} doesn`t exits.";
                    }
                }
            }
            catch (Exception ex)
            {
                opsresult = ex.Message;
            }
            return opsresult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static string addConfigValue(string _key, string _value)
        {
            string opsresult = "fail.";
            try
            {
                string dbpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB", dbFilename);
                if (File.Exists(dbpath) && !String.IsNullOrEmpty(_key))
                {
                    DatabaseClient sql = new DatabaseClient(dbpath);
                    Expression expression1 = new Expression
                    {
                        LeftTerm = new Expression("key", Operators.Equals, _key),
                        Operator = Operators.And,
                        RightTerm = new Expression("status", Operators.Equals, 1)
                    };
                    DataTable selectResult1 = sql.Select("tb_configresp", 0, null, null, expression1, null);
                    var queryResult = selectResult1.Rows;
                    if (queryResult?.Count > 0)
                    {
                        opsresult = $"Fail.key:{_key} has exits";
                    }
                    else
                    {
                        Dictionary<string, object> d1 = new Dictionary<string, object>();
                        d1.Add("key", _key);
                        d1.Add("value", _value);
                        d1.Add("status", 1);
                        sql.Insert("tb_configresp", d1);
                        opsresult = "success.";
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return opsresult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string getConfigValueList() 
        {
            string opsresult = "";
            try
            {
                string dbpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB", dbFilename);

                if (File.Exists(dbpath))
                {
                    DatabaseClient sql = new DatabaseClient(dbpath);
                    Expression eRetrieve1 = new Expression("status", Operators.Equals, 1);
                    DataTable selectResult1 = sql.Select("tb_configresp", 0, null, null, eRetrieve1, null);
                    var queryResult = selectResult1.Rows;
                  
                    if (queryResult?.Count > 0)
                    {
                        Dictionary<string, string> resultdic = new Dictionary<string, string>();
                        for (var i = 0; i < queryResult.Count; i++) 
                        {
                            resultdic.Add(queryResult[i]["key"].ToString(), queryResult[i]["value"].ToString());
                        }
                        opsresult = string.Join(Environment.NewLine, resultdic);  
                    } 
                }     
            }
            catch (Exception ex)
            {
                opsresult = ex.Message;
            }
            return opsresult;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static LoginUser checkUserInfo(string usercode)
        {
            LoginUser user =null ;
            try
            {
                string dbpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB", dbFilename);

                if (File.Exists(dbpath) && !String.IsNullOrEmpty(usercode))
                { 
                    string usercodebase64= Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(usercode));
                    DatabaseClient sql = new DatabaseClient(dbpath);
                    Expression expression1 = new Expression("idcode", Operators.Equals, usercodebase64);
                    DataTable selectResult1 = sql.Select("tb_user", 0, null, null, expression1, null);
                    var queryResult = selectResult1.Rows;
                    if (queryResult?.Count > 0)
                    {
                        user = new LoginUser()
                        {
                            id =Convert.ToInt32(queryResult[0]["id"]),
                            username = queryResult[0]["username"].ToString(),
                            idcode = queryResult[0]["idcode"].ToString(),
                            //status = queryResult[0]["status"].ToString(),
                            userinfo = queryResult[0]["userinfo"].ToString(),
                        }; 
                    } 
                }
            }
            catch (Exception  )
            {
                
            }
            return user;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static APiConfig getAPIInfo(string flag)
        {
            APiConfig apiconfig = null;
            try
            {
                string dbpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB", dbFilename);

                if (File.Exists(dbpath) && !String.IsNullOrEmpty(flag))
                { 
                    DatabaseClient sql = new DatabaseClient(dbpath);
                    Expression expression1 = new Expression("flag", Operators.Equals, flag);
                    DataTable selectResult1 = sql.Select("tb_apiconfig", 0, null, null, expression1, null);
                    var queryResult = selectResult1.Rows;
                    if (queryResult?.Count > 0)
                    {
                        apiconfig = new APiConfig()
                        {
                            username = queryResult[0]["username"].ToString(),
                            password = queryResult[0]["password"].ToString(),
                            wsurl = queryResult[0]["wsurl"].ToString(),
                            flag = queryResult[0]["flag"].ToString(),
                            remark = queryResult[0]["remark"].ToString(),
                        };
                    }
                }
            }
            catch (Exception)
            { 
            }
            return apiconfig;
        }
    }
}
