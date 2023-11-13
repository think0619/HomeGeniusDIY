
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

namespace TextVoiceServer.DataApi
{
    [Route("api/configservice")]
    [ApiController]
    public class ConfigServiceController : ControllerBase
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ConfigServiceController(IServiceScopeFactory serviceScopeFactory )
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
        public string GetConfigValue([FromQuery]string key) 
        {
            string result = "";
            if(!String.IsNullOrEmpty(key)) 
            {
                result = SQLiteHelper.getConfigValue(key); 
            } 
            return result;
        }

        [HttpGet("set")]
        public string SetConfigValue([FromQuery] string key,string value)
        {
            string result = "";
            if (!String.IsNullOrEmpty(key))
            {
                result = SQLiteHelper.setConfigValue(key, value);
            }
            return result;
        }

        [HttpGet("add")]
        public string AddConfigValue([FromQuery] string key, string value)
        {
            string result = "";
            if (!String.IsNullOrEmpty(key))
            {
                result = SQLiteHelper.addConfigValue(key, value);
            }
            return result;
        }

        [HttpGet("getlist")]
        public string GetConfigValue()
        { 
            return SQLiteHelper.getConfigValueList();  
        } 
    }
}
