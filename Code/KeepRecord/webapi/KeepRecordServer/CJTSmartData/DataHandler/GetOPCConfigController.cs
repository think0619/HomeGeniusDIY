 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Websocket.Client;
using Entities;
using System.Threading.Tasks;
using CJTSmartData.DBContext;
using Entities.Sensor;
using StackExchange.Redis;

namespace CJTSmartData
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetOPCConfigController : ControllerBase
    {
        private readonly IWebsocketClient _websocket;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConnectionMultiplexer _redis;
        // private List<SensorValueCfg> _sensorValueCfgs; 

        public GetOPCConfigController(IWebsocketClient websocket, IServiceScopeFactory serviceScopeFactory, IConnectionMultiplexer redis)
        {
            _websocket = websocket;
            _serviceScopeFactory = serviceScopeFactory;
            _redis = redis;
        }

        //public  void Test() 
        //{
        //    try
        //    {
        //        using (var dbcontext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>())
        //        {

        //            var datalist = dbcontext.view_senvaluedbcfg.ToList();
        //            string x = "";

        //        }

        //    }
        //    catch (Exception ex) { }
        //}


        /// <summary>
        /// read from database and 
        /// </summary>
        /// <returns></returns>
        [HttpGet("CalcLastDaySum")]
        public string CalcLastDaySum()
        {
            var redisdb = _redis.GetDatabase();
            var sensorValueCfgStr = redisdb.StringGet(ConstValue.RedisSensorCfg).ToString();

            using (var dbcontext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>())
            {
                if (!String.IsNullOrWhiteSpace(sensorValueCfgStr))
                {
                    try
                    {
                        List<SensorValueCfg> sensorValueCfgList = JsonHelper.DeserializeJsonToList<SensorValueCfg>(sensorValueCfgStr);
                        sensorValueCfgList = sensorValueCfgList.Where(svf => svf.NeedRecord).ToList();
                        if (sensorValueCfgList?.Count > 0)
                        {
                            /////Configurations GUIDs
                            //List<string> valueCfgGUIDs = (from svc in sensorValueCfgList select svc.ValueCfgGUID).ToList();
                            foreach (SensorValueCfg sensorValueCfg in sensorValueCfgList)
                            {
                                SensorCumulant insertData = new SensorCumulant();
                                insertData.ValueCfgGUID = sensorValueCfg.ValueCfgGUID;
                                insertData.SumDate = DateTime.Now.AddDays(-1).Date;

                                SensorData lastdayLatestSensorValue = dbcontext.tb_sensorvalue.Where(sen => sen.ValueCfgGUID.Equals(sensorValueCfg.ValueCfgGUID)
                                                                                                         && sen.Datetime.Date.Equals(DateTime.Now.AddDays(-1).Date)
                                                                                                     ).OrderByDescending(o => o.Datetime).FirstOrDefault();
                                if (lastdayLatestSensorValue != null)
                                {
                                    insertData.SumValue = lastdayLatestSensorValue.Value;
                                }
                                else
                                {
                                    //get data of the day before yesterday
                                    SensorCumulant xxx = dbcontext.tb_sensorcumulant.Where(sen => sen.ValueCfgGUID.Equals(sensorValueCfg.ValueCfgGUID, StringComparison.OrdinalIgnoreCase)
                                                                                               && sen.SumDate.Date.Equals(DateTime.Now.AddDays(-2).Date)).FirstOrDefault();
                                    if (xxx != null)
                                    {
                                        insertData.SumValue = xxx.SumValue;
                                    }
                                    else
                                    {
                                        /////
                                        //有异常，只存0 
                                    }
                                }
                                var deleteElements = dbcontext.tb_sensorcumulant.Where(x => x.ValueCfgGUID.Equals(insertData.ValueCfgGUID) && x.SumDate.Equals(insertData.SumDate.Date));
                                if (deleteElements?.Count()>0) 
                                {
                                    dbcontext.tb_sensorcumulant.RemoveRange(deleteElements);
                                } 
                                dbcontext.tb_sensorcumulant.AddAsync(insertData); 
                            }
                            dbcontext.SaveChanges(); 
                        }
                    }
                    catch (Exception ex) 
                    { }
                    }
            }

            return "ss";
        }


        [HttpGet("GetSingle")]
        public string GetSingle()
        {
            var allStateCmd = new
            {
                infotype = "damreadall"
            };
            _websocket.Send(JsonHelper.SerializeObject(allStateCmd));
            //AuthMsg sendmsg = new AuthMsg()
            //{
            //    token = "API_0011",
            //    infotype = "register",
            //    info = ""
            //};
            //Task.Run(() => _websocket.Send(JsonHelper.SerializeObject(sendmsg)));

            ////4-101-电表
            //var allStateCmd = new
            //{
            //    infotype = "damread",
            //    info = new
            //    {
            //        unid = "JY912028250F7FUQ-96"
            //    }
            //};
            //_websocket.Send(JsonHelper.SerializeObject(allStateCmd));

            return "ss";
        }

        [HttpGet("OpenSwitchDemo")]
        [HttpPost("OpenSwitchDemo")]
        public string OpenSwitchDemo()
        {
            var allStateCmd = new
            {
                token = "API_20220104_ND",
                infotype = "damopr",
                info = new
                {
                    opr = "open",
                    unid = "JY912028254h7e6q_28",
                    io = 1
                }
            };

            _websocket.Send(JsonHelper.SerializeObject(allStateCmd));

            return "ss";
        }

        [HttpGet("CloseSwitchDemo")]
        [HttpPost("CloseSwitchDemo")]
        public string CloseSwitchDemo()
        {
            var allStateCmd = new
            {
                token = "API_20220104_ND",
                infotype = "damopr",
                info = new
                {
                    opr = "close",
                    unid = "JY912028254h7e6q_28",
                    io = 1
                }
            };

            _websocket.Send(JsonHelper.SerializeObject(allStateCmd));

            return "ss";
        }
    }
}
