
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

namespace TextVoiceServer.ContentMgmt
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandleCertController : ControllerBase
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public HandleCertController(IServiceScopeFactory serviceScopeFactory )
        {
            _serviceScopeFactory = serviceScopeFactory; 
        }

        [HttpGet]
        public string Get()
        {
            return "hah";
        }

        [HttpGet("GetHH")]
        public void GetHH() 
        { 
            
        }


 
    }
}
