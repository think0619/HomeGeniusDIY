 
using Entities;
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
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //private IConfiguration _config;
        private readonly IServiceScopeFactory _serviceScopeFactory;
      
        public LoginController(/*IConfiguration config,*/ IServiceScopeFactory serviceScopeFactory)
        {
            //_config = config;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel login)
        {
            string requestIP = "";
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            if (remoteIpAddress!=null) 
            {
                requestIP=remoteIpAddress.ToString();
            }
            LoginTipResult tipResult = new LoginTipResult()
            {
                Status = 0,
                Msg = "",
                Token = ""
            };  
            var user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                tipResult.Token = tokenString;
                tipResult.Status = 1;
                tipResult.Msg = "success"; 
            }
            else
            { 
                tipResult.Status = 0;
                tipResult.Msg = "用户名或密码错误。";
            } 
            return Ok(tipResult);
        }

        private string GenerateJSONWebToken(LoginUser userInfo)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, ConfigurationHelper.AppSetting["Jwt:ClaimName"]),
                new Claim(nameof(userInfo.username), userInfo.username),
                new Claim(nameof(userInfo.id), userInfo.id.ToString()),
                new Claim(nameof(userInfo.userinfo), userInfo.userinfo),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMinutes(60)).ToUnixTimeSeconds().ToString()),
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationHelper.AppSetting["Jwt:Key"]));
           
            string expireSeconds = ConfigurationHelper.AppSetting["Jwt:ExpireSeconds"];
          
            Int32.TryParse(expireSeconds, out int expire);

            var token = new JwtSecurityToken(
                ConfigurationHelper.AppSetting["Jwt:Issuer"],
                ConfigurationHelper.AppSetting["Jwt:Audience"],
                claims,
                null,
                DateTime.Now.AddSeconds(expire),
                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));  
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //Check user login info
        private LoginUser AuthenticateUser(UserModel loginuser)
        { 
            LoginUser user = null;
            if ((!String.IsNullOrWhiteSpace(loginuser.UserIDCode)))
            {
                user= SQLiteHelper.checkUserInfo(loginuser.UserIDCode); 
            } 
            return user;
        }
    }
}
