
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
using TextVoiceServer.Serivices;

namespace TextVoiceServer.DataApi
{
    [Route("api/espservice")]
    [ApiController]
    public class ESPServiceController : ControllerBase
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly HandleMQPublishService _mqttService;
         
        public ESPServiceController(IServiceScopeFactory serviceScopeFactory, HandleMQPublishService  mqttService)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mqttService = mqttService;
        }
        //autoget.html?flag=clock&cmd=on

        [HttpGet("exe")]
        public string GetConfigValue([FromQuery]string flag, [FromQuery] string cmd) 
        {
            string result = "";
            if (!String.IsNullOrEmpty(flag) && !String.IsNullOrEmpty(cmd))
            {
                _mqttService.PublicMsg(flag,cmd);
                result = "Success.";
            }
            else 
            {
                result = "Invalid params.";
            }
            return result;
        }
         
    }
}
