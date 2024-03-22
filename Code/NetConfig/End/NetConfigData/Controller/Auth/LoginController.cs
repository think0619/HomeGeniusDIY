using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using TextVoiceServer;
using Entities.User;
using System.Threading.Tasks;
using TextVoiceServer.DBContext;
using TextVoiceServer.Model;
using MsgMiddleServer.ComHandler;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NetConfigServer.Controller.Auth
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
        [HttpPost("login")]
        public async Task<IActionResult> ManualLogin([FromBody] LoginInfo login)
        {
            var tipResult = new LoginTipResult()
            {
                Status = 0,
                Msg = "",
                Token = ""
            };

            var user = await AuthenticateUserByPassword(login);
            if (user != null)
            {
                tipResult.Token = GenerateJSONWebToken(user);
                tipResult.Status = 1;
                tipResult.Msg = "登录成功。";
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
                new Claim(nameof(userInfo.Username), userInfo.Username),
                new Claim(nameof(userInfo.Id), userInfo.Id.ToString()),
                new Claim(nameof(userInfo.UserInfo), userInfo.UserInfo),
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
        private async Task<LoginUser> AuthenticateUserByPassword(LoginInfo authuser)
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
                            if (hashedPassword == toLoginUser.Password)
                            {
                                user = toLoginUser;
                            }
                        }
                    }
                }
                catch (Exception)
                { 
                }
            }
            return user;
        }


        [AllowAnonymous]
        [HttpPost("newpwd")]
        public string GeneratePwd([FromBody] LoginInfo login)
        {
            var tipResult = new LoginTipResult()
            {
                Status = 0,
                Msg = "",
                Token = "" 
            };
            if (login?.Username != null)
            {
                string salt = PasswordHelper.GenerateSalt(16);  
                string hashedPassword = PasswordHelper.HashPassword(login.Password, salt);
                tipResult.Status = 1;
                tipResult.Msg = "新密码生成";
                tipResult.Token = $"Salt:{salt}  PwdHash:{hashedPassword}"; 
            } 
            return JsonHelper.SerializeObject(tipResult);
        }
    }
}
