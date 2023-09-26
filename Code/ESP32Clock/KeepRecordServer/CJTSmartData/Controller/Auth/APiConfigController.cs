 
using Entities;
using Entities.Content;
using Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TextVoiceServer.DBHandler;
using TextVoiceServer.Model;

namespace TextVoiceServer.Controller.Auth
{
    [Route("api/apiconfig")]
    [ApiController]
    public class APiConfigController : ControllerBase
    {
        //private IConfiguration _config;
        private readonly IServiceScopeFactory _serviceScopeFactory;
      
        public APiConfigController(/*IConfiguration config,*/ IServiceScopeFactory serviceScopeFactory)
        {
            //_config = config;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [HttpGet]
        public string Get()
        {
            return "hah";
        }


        [HttpPost("get")]
        public IActionResult GetAPiConfig([FromQuery]string flag)
        {
            TipResult tipResult = new TipResult()
            {
                Status = 0,
                Msg = "",
               Data = new APiConfig()
            };

           
            var loginusername = HttpContext.Items[nameof(LoginUser.username)];
            if (!String.IsNullOrWhiteSpace(flag) && loginusername != null && !String.IsNullOrWhiteSpace(loginusername.ToString()))
            {
                tipResult.Data = SQLiteHelper.getAPIInfo(flag);
                tipResult.Status = 1;
                tipResult.Msg = "Success.";
            }
            else 
            {
                tipResult.Status =0;
                tipResult.Msg = "Fail.";
            }
            return Ok(tipResult);
        } 
    }
}
