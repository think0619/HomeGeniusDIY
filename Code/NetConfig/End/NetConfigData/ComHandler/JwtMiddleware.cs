 
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace TextVoiceServer
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next; 

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next; 
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
         
            if (token != null) 
            {
                validateAndAttachUserInfo(context, token);
            }  
            await _next(context);
        }

        private void validateAndAttachUserInfo(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
              //  var key = Encoding.UTF8.GetBytes(ConfigurationHelper.AppSetting["Jwt:Key"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationHelper.AppSetting["Jwt:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                context.Items[nameof(Entities.User.LoginUser.UserInfo)] = jwtToken.Claims.First(x => nameof(Entities.User.LoginUser.UserInfo).Equals(x.Type)).Value;
                context.Items[nameof(Entities.User.LoginUser.Username)] = jwtToken.Claims.First(x => nameof(Entities.User.LoginUser.Username).Equals(x.Type)).Value;
                context.Items[nameof(Entities.User.LoginUser.Id)] = jwtToken.Claims.First(x => nameof(Entities.User.LoginUser.Id).Equals(x.Type)).Value;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}