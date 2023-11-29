
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TextVoiceServer.DataApi
{
    [Route("api/raspsrc")]
    [ApiController]
    public class RaspSourceServiceController : ControllerBase
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public RaspSourceServiceController(IServiceScopeFactory serviceScopeFactory )
        {
            _serviceScopeFactory = serviceScopeFactory; 
        }

        [HttpGet]
        public string Get()
        {
            return "hah";
        } 

        [HttpGet("getlist")]
        [HttpPost("getlist")]
        public async Task<IActionResult> GetConfigValue()
        {
            TipResult tip = new TipResult()
            {
                Status = 0,
                Msg = "",
            };
            try
            {
                using (var dbcontext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>())
                {
                    tip.Data = await dbcontext.tb_vlcsrc.Where(t => t.Status == 1).ToListAsync();
                    tip.Status = 1;
                    tip.Msg = "Success";
                }
            }
            catch (Exception ex)
            {
                tip.Msg = ex.Message;
            }
            return Ok(tip);
        }
    }
}
