
using TextVoiceServer.DBContext;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TextVoiceServer.DBHandler;
using Microsoft.AspNetCore.Authorization;
using MsgMiddleServer.ComHandler;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySqlX.XDevAPI.Common; 

namespace TextVoiceServer.DataApi
{
    [Route("api/configservice")]
    [ApiController]
    public class ESPConfigServiceController : ControllerBase
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ESPConfigServiceController(IServiceScopeFactory serviceScopeFactory )
        {
            _serviceScopeFactory = serviceScopeFactory; 
        }

        [HttpGet]
        public string Get()
        {
            return "hah";
        }

        [HttpGet("timenow")]
        public string GetTime([FromQuery]int zonehour)
        {
            //(year, month, day[, hour[, minute[, second[, microsecond[ ]]]]])
            DateTime dt=DateTime.Now;
            try
            {
                dt = NTPHelper.GetNetworkTime();
            }
            catch(Exception ex) 
            {
            } 
            int year=dt.Year;
            int month=dt.Month;
            int day=dt.Day;
            int hour=dt.Hour+ zonehour;
            int minute=dt.Minute;
            int second=dt.Second;
            int microsecond=dt.Millisecond; 
            int[] times = { year, month, day, hour, minute, second, microsecond, (int)dt.DayOfWeek }; 
            return string.Join(',', times);
        }


        [HttpGet("get")]
        public async Task<string> GetConfigValue([FromQuery] string key)
        {
            string result = "";
            if (!String.IsNullOrEmpty(key))
            {
                key = key.Trim();
                try
                {
                    // result = SQLiteHelper.getConfigValue(key); 
                    using (var dbcontext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>())
                    {
                        var desRecord = await dbcontext.tb_espconfig.Where(t => t.status == 1 && t.key == key).FirstOrDefaultAsync();
                        if (desRecord != null)
                        {
                            result = desRecord.value;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }
            return result;
        }

        [HttpGet("set")]
        public async Task<string> SetConfigValue([FromQuery] string key,string value)
        {
            string result = "";
            if (!String.IsNullOrEmpty(key))
            {
                key = key.Trim();
                // result = SQLiteHelper.setConfigValue(key, value);
                try
                {
                    using (var dbcontext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>())
                    {
                        var desRecord = await dbcontext.tb_espconfig.Where(t => t.status == 1 && t.key == key).FirstOrDefaultAsync();
                        if (desRecord != null)
                        {
                            desRecord.value = value;
                            await dbcontext.SaveChangesAsync();
                            result = "success.";
                        }
                        else 
                        {
                            result = $"Fail.Key:{key} doesn`t exits.";
                        } 
                    }
                }
                catch(Exception ex)
                {
                    result =ex.Message;
                } 
            }
            return result;
        }

        [HttpGet("add")]
        public async Task<string> AddConfigValue([FromQuery] string key, string value)
        {
            string result = "";
            if (!String.IsNullOrEmpty(key))
            {
                key = key.Trim();
                // result = SQLiteHelper.addConfigValue(key, value);
                try
                {
                    using (var dbcontext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>())
                    {
                        await dbcontext.tb_espconfig.AddAsync(new Entities.Content.ESPConfig()
                        {
                            key = key,
                            value = value,
                            status = 1,
                        });
                        await dbcontext.SaveChangesAsync();
                        result = "success.";
                    }
                }
                catch (Exception ex)
                {
                    result = $"Faile.{ex.Message}";
                }
            }
            return result;
        }

        [HttpGet("getlist")]
        public async Task<string> GetConfigValue()
        {
            // return SQLiteHelper.getConfigValueList();
            string result = "";
            try
            {
                using (var dbcontext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>())
                {
                    var records = await dbcontext.tb_espconfig.Where(t => t.status == 1).ToListAsync();
                    List<dynamic> anonymousList = new List<dynamic>();
                    foreach (var rec in records)
                    {
                        anonymousList.Add(new
                        {
                            RecId = rec.recid,
                            Key = rec.key,
                            Value = rec.value
                        });
                    } 
                    result = JsonHelper.SerializeObject(records);
                }
            }
            catch (Exception ex)
            {
                result = $"Fail.{ex.Message}";
            }
            return result;
        } 
    }
}
