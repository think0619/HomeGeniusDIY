
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
using Entities.Voice;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NetMQ;
using NetConfigServer.Controller.Message;

namespace TextVoiceServer.Message
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandleMsgController : ControllerBase
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;  
        public HandleMsgController(IServiceScopeFactory serviceScopeFactory )
        {
            _serviceScopeFactory = serviceScopeFactory; 
        }

        [HttpGet]
        public string Get()
        {
           DateTime dt= NTPHelper.GetNetworkTime();
            return "hah";
        }

        /// <summary>
        /// receive msg from user
        /// insert msg into Redis
        /// </summary>
        /// <returns></returns>
       // [HttpPost("ReceiveMsgFromUser")]
        public async Task<string> ReceiveMsgFromUser([FromBody] ReceiveMsg receiveMsg) 
        {
            TipResult tipResult = new TipResult()
            {
                Status = 1,
                Msg = "success"
            };

            var handleTask=Task.Run(() =>
            {
                string userauth_string = RedisHelper.Get("UserAuth");
               // string userauth_string = ConfigurationHelper.AppSetting["SystemCfg:UserAuth"].ToString();
                if (userauth_string.Equals(receiveMsg.Sender))
                {
                    try
                    {
                        if (receiveMsg.Msg!=null) 
                        {
                            long len1 = RedisHelper.LPush("ControlMsg", receiveMsg.Msg);
                            tipResult.Msg = String.Format($"success:{len1}");
                        } 
                    }
                    catch (Exception ex)
                    {
                        tipResult.Status = 0;
                        tipResult.Msg = ex.Message;
                    }
                }
                else
                {
                    tipResult.Status = 0;
                    tipResult.Msg = "unauthorized";
                } 
            });
            await Task.WhenAll(handleTask); 
            return JsonHelper.SerializeObject(tipResult); 
        } 


       // [HttpGet("RedisTest")]
        //[HttpPost("RedisTest")]
        public string RedisTest()
        { 
            // 将一个或多个值插入到列表头部
            string[] lpush1 = new string[] { "003", "004" };
            long len1 = RedisHelper.LPush("listtest", "000");
            long len2 = RedisHelper.LPush("listtest", "001", "002");
            long len3 = RedisHelper.LPush("listtest", lpush1);
            return "RedisTest";
        }

        //[HttpGet("RedisPush")]
       // [HttpPost("RedisPush")]
        public string RedisPush()
        {
            long len4 = RedisHelper.RPush("listtest", "010");
            return "RedisTest";
        }

        //[HttpGet("RedisGet")]
       // [HttpPost("RedisGet")]
        public string RedisGet()
        {
            // 移除并获取列表的第一个元素
            string val1 = RedisHelper.LPop("listtest");
            
            return val1;
        }

       // [HttpGet("GetQueryResult")]
       // [HttpPost("GetQueryResult")]
        public string GetQueryResult(/*[FromBody] CertQuertParam certQuertParam */)
        { 
            return ""; 
        }
    }
}
