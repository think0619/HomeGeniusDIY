using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class ConstValue
    {
        /// <summary>
        /// CJTSensorValueCfg
        /// </summary>
        public static string RedisSensorCfg = "CJTSensorValueCfg";

        public static string RedisSensorUserCfg = "CJTSensorValueCfgByUser";

        public const string JuYinToken = "JuYinToken";

        /// <summary>
        /// 请检查参数完整性。
        /// </summary>
        public static string CheckParamsFull = "请检查参数完整性。";

        /// <summary>
        /// 该钉钉用户未录入。
        /// </summary>
        public static string DingAccountError = "该钉钉用户未录入。";

        /// <summary>
        /// 未发现对应开关设备。
        /// </summary>
        public static string UserSensorError = "未发现对应开关设备。";  

        /// <summary>
        /// open switch
        /// </summary>
        public const string SwtichOpenCmd = "open";

        /// <summary>
        /// close switch
        /// </summary>
        public const string SwtichCloaseCmd = "close";

        /// <summary>
        /// 命令成功
        /// </summary>
        public const string CmdSentSuccess = "命令成功";

        /// <summary>
        /// 开关指令错误。'open' or 'close
        /// </summary>
        public const string SwitchCmdError = "开关指令错误。'open' or 'close'";

        /// <summary>
        /// 系统发生异常。
        /// </summary>
        public static string SystemExceptionMsg= "系统发生异常。";

        /// <summary>
        /// SignalR Connection Key Pre for Redis
        /// SignalRConnect
        /// </summary>
        public static string SignalRConnectPre = "SignalRConnect";

        public static string SignalRAdmin = "SignalRAdmin";

    }
}
