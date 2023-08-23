 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection; 
using Entities;
using System.Threading.Tasks;
using TextVoiceServer.DBContext;  
using System.Net.Http;

namespace TextVoiceServer
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    { 
        private readonly IServiceScopeFactory _serviceScopeFactory;
       
        // private List<SensorValueCfg> _sensorValueCfgs; 

        public RegisterController(  IServiceScopeFactory serviceScopeFactory )
        { 
            _serviceScopeFactory = serviceScopeFactory; 
        }

        
         
    }
}
