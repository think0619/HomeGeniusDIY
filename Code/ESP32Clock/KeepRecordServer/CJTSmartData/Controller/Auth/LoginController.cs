 
using Entities;
using Entities.User;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MsgMiddleServer.ComHandler;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TextVoiceServer.DBContext;
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
        public async Task<IActionResult> Login([FromBody] UserModel login)
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
            var user =await AuthenticateUserByCode(login); 

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

        [AllowAnonymous]
        [HttpPost("manual")]
        public async Task<IActionResult> ManualLogin([FromBody] AuthUser login)
        { 
            LoginTipResult tipResult = new LoginTipResult()
            {
                Status = 0,
                Msg = "",
                Token = ""
            }; 

            var user = await AuthenticateUserByPassword(login);
            if (user != null)
            {
                tipResult.Token = GenerateJSONWebToken(user);
                //  tipResult.Token = GenerateJwtToken(user.Username); 
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
                new Claim("user.Username", userInfo.Username),
                new Claim("user.Id", userInfo.Id.ToString()),
                new Claim("user.UserInfo", userInfo.UserInfo!=null?userInfo.UserInfo:""),
                new Claim("user.role", userInfo.Role!=null?userInfo.Role:""),
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

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationHelper.AppSetting["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(
                ConfigurationHelper.AppSetting["Jwt:Issuer"],
                ConfigurationHelper.AppSetting["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        //Check user login info
        private async Task<LoginUser> AuthenticateUserByPassword( AuthUser authuser)
        { 
            LoginUser user = null;
            if (authuser?.Username != null)
            {
                //string salt = PasswordHelper.GenerateSalt(16); // 生成16字节的随机盐值
                //string hashedPassword = PasswordHelper.HashPassword(authuser.Password, salt); 
                try
                {
                    using (var dbcontext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>())
                    {
                        var toLoginUser = await dbcontext.tb_user.Where(t => t.Status == 1 && t.Username == authuser.Username).FirstOrDefaultAsync();
                        if (toLoginUser != null)
                        {
                            //check password
                            string hashedPassword = PasswordHelper.HashPassword(authuser.Password, toLoginUser.Salt);
                            if(hashedPassword == toLoginUser.Password)
                            {
                                user = toLoginUser;
                            } 
                        } 
                    }
                }
                catch (Exception  )
                {
                   
                } 
            }  
            return user;
        }

        private async Task<LoginUser> AuthenticateUserByCode(UserModel loginuser)
        {
            LoginUser user = null;
            if ((!String.IsNullOrWhiteSpace(loginuser.UserIDCode)))
            {
                try
                {
                    using (var dbcontext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>())
                    {
                        var usercodeBase64= EncryptHelper.Base64Encode(loginuser.UserIDCode);
                        var _user = await dbcontext.tb_user.Where(t => t.Status == 1&& usercodeBase64.Equals(t.IDCode)).FirstOrDefaultAsync();
                        if (_user != null) 
                        {
                            user = _user;
                        } 
                    }
                }
                catch (Exception ex)
                {
                   
                }
            }
            return user;
        }
    }
}
