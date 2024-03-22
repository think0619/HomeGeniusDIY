namespace TextVoiceServer.Model
{
    public class UserModel
    {
        public string UserIDCode { get; set; }
    }

    public class AuthUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
    } 

    public class LoginInfo 
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }  /** 验证码 */ 
    }

    public class UpdatePwdInfo
    {
        public int Id { get; set; }
        public string CurrentPwd { get; set; }
        public string NewPwd { get; set; }  /** 验证码 */
    }
}
