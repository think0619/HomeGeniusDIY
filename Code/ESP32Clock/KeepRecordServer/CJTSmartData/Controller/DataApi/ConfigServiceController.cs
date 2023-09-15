
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
