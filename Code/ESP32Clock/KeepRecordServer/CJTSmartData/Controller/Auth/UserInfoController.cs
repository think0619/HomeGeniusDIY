using ContentMgmtCore.Controller.CommonHandler;
using ContentMgmtCore.DBContext;
using ContentMgmtCore.Handlers;
using ContentMgmtCore.Model;
using Entities;
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

namespace ContentMgmtCore.Controller.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    { 
        private readonly IServiceScopeFactory _serviceScopeFactory;
      
        public UserInfoController(IServiceScopeFactory serviceScopeFactory)
        { 
            _serviceScopeFactory = serviceScopeFactory;
        }
         
        [HttpPost("ChangePwd")]
        public async Task<string> ChangePwd([FromForm] PwdModel password)
        {
            TipResult tipResult = new TipResult()
            {
                Status = 0,
                Msg = "" 
            };
            if ((!String.IsNullOrWhiteSpace(password.newpassword)) && (password.newpassword == password.repassword))
            {
                var loginUserID = HttpContext.Items[nameof(LoginUser.recid)];
                if (loginUserID == null)
                {
                    tipResult.Status = -1;
                    tipResult.Msg = "without auth";
                }
                else
                {
                    using (var dbcontext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>())
                    {
                        var userInfo = dbcontext.tb_loginuser.Where(x => loginUserID.Equals(x.recid) && x.Password.Equals(EncryptHelper.CreateMD5(password.oldpassword))).FirstOrDefault();
                        if (userInfo == null)
                        {
                            tipResult.Status = 0;
                            tipResult.Msg = "现在密码错误。";
                        }
                        else
                        {
                            userInfo.Password = EncryptHelper.CreateMD5(password.newpassword);
                            await dbcontext.SaveChangesAsync();
                            tipResult.Status = 1;
                            tipResult.Msg = "修改密码成功。";
                        }
                    }
                }
            }
            else 
            {
                tipResult.Status = 0;
                tipResult.Msg = "输入有误。";
            }
            
            return JsonHelper.SerializeObject(tipResult);
        } 
    }
}
