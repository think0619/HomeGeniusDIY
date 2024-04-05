using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Weather
{
    public class HefengWeatherResponse
    {
        public string code { get; set; }

        public DateTime updateTime { get; set; }
        public string fxLink { get; set; }
         public HefengWeatherNow  now { get; set; }
        public List<HefengWeatherHourly> hourly { get; set; }
        public List<HefengWeatherDaily> daily { get; set; }
    }

    public class HefengWeatherNow
    {
        /// <summary>
        /// 数据观测时间
        /// </summary>
        public DateTime obsTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string obsTimeFmt
        { 
            get
            {
                string fmtResult = "";
                if (obsTime != null)
                {
                    fmtResult = obsTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                return fmtResult;
            }
        }

        /// <summary>
        /// 温度，默认单位：摄氏度
        /// </summary>
        public string temp { get; set; }

        /// <summary>
        /// 体感温度，默认单位：摄氏度
        /// </summary>
        public string feelsLike { get; set; } //

        /// <summary>
        /// 天气状况的图标代码
        /// </summary>
        public string icon { get; set; } // 

        /// <summary>
        /// 天气状况的文字描述，包括阴晴雨雪等天气状态的描述
        /// </summary>
        public string text { get; set; } // 

        /// <summary>
        /// 风向360角度
        /// </summary>
        public string wind360 { get; set; } //

        /// <summary>
        /// 风向
        /// </summary>
        public string windDir { get; set; } //

        /// <summary>
        /// 风力等级
        /// </summary>
        public string windScale { get; set; }

        /// <summary>
        /// 相对湿度，百分比数值
        /// </summary>
        public string windSpeed { get; set; }

        /// <summary>
        /// 相对湿度，百分比数值
        /// </summary>
        public string humidity { get; set; }

        /// <summary>
        /// 当前小时累计降水量，默认单位：毫米
        /// </summary>
        public string precip { get; set; }

        /// <summary>
        /// 大气压强，默认单位：百帕
        /// </summary>
        public string pressure { get; set; }

        /// <summary>
        ///  能见度，默认单位：公里
        /// </summary>
        public string vis { get; set; }

        /// <summary>
        /// 云量，百分比数值。可能为空
        /// </summary>
        public string cloud { get; set; }

        /// <summary>
        /// 露点温度。可能为空
        /// </summary>
        public string dew { get; set; }
    }

    public class HefengWeatherHourly
    {
        /// <summary>
        /// 预报时间
        /// </summary>
        public DateTime fxTime { get; set; }
        public string fxTimeFmt
        {
            get
            {
                string fmtResult = "";
                if (fxTime != null)
                {
                    fmtResult = fxTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                return fmtResult;
            }
        }
        public string fxTimeTimeFmt
        {
            get
            {
                string fmtResult = "";
                if (fxTime != null)
                {
                    fmtResult = fxTime.ToString("HH");
                }
                return fmtResult;
            }
        }


        /// <summary>
        ///  温度，默认单位：摄氏度
        /// </summary>
        public string temp { get; set; }

        /// <summary>
        /// 天气状况的图标代码
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 天气状况的文字描述，包括阴晴雨雪等天气状态的描述
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 风向360角度
        /// </summary>
        public string wind360 { get; set; }

        /// <summary>
        /// 风向
        /// </summary>
        public string windDir { get; set; }

        /// <summary>
        /// 风力等级
        /// </summary>
        public string windScale { get; set; }

        /// <summary>
        /// 风速，公里/小时
        /// </summary>
        public string windSpeed { get; set; }

        /// <summary>
        /// 相对湿度，百分比数值
        /// </summary>
        public string humidity { get; set; }

        /// <summary>
        /// 逐小时预报降水概率，百分比数值，可能为空
        /// </summary>
        public string pop { get; set; }

        /// <summary>
        ///  当前小时累计降水量，默认单位：毫米
        /// </summary>
        public string precip { get; set; }

        /// <summary>
        /// 大气压强，默认单位：百帕
        /// </summary>
        public string pressure { get; set; }

        /// <summary>
        /// 云量，百分比数值。可能为空
        /// </summary>
        public string cloud { get; set; }

        /// <summary>
        /// 露点温度。可能为空
        /// </summary>
        public string dew { get; set; }
    }

    public class HefengWeatherDaily
    {
        /// <summary>
        /// 预报日期
        /// </summary>
        public DateTime fxDate { get; set; }

        public string fxTimeTimeFmt
        {
            get
            {
                string fmtResult = "";
                if (fxDate != null)
                {
                    fmtResult = fxDate.ToString("yyyy-MM-dd");
                }
                return fmtResult;
            }
        }

        /// <summary>
        /// 日出时间，在高纬度地区可能为空
        /// </summary>
        public string sunrise { get; set; }

        /// <summary>
        /// 日落时间，在高纬度地区可能为空
        /// </summary>
        public string sunset { get; set; }

        /// <summary>
        /// 当天月升时间，可能为空
        /// </summary>
        public string moonrise { get; set; }

        /// <summary>
        /// 当天月落时间，可能为空
        /// </summary>
        public string moonset { get; set; }

        /// <summary>
        /// 月相名称
        /// </summary>
        public string moonPhase { get; set; }

        /// <summary>
        /// 月相图标代码
        /// </summary>
        public string moonPhaseIcon { get; set; }

        /// <summary>
        /// 预报当天最高温度
        /// </summary>
        public string tempMax { get; set; }

        /// <summary>
        /// 预报当天最低温度
        /// </summary>
        public string tempMin { get; set; }

        /// <summary>
        /// 预报白天天气状况的图标代码
        /// </summary>
        public string iconDay { get; set; }

        /// <summary>
        ///  预报白天天气状况文字描述，包括阴晴雨雪等天气状态的描述
        /// </summary>
        public string textDay { get; set; }

        /// <summary>
        /// 预报夜间天气状况的图标代码
        /// </summary>
        public string iconNight { get; set; }

        /// <summary>
        ///  预报晚间天气状况文字描述，包括阴晴雨雪等天气状态的描述
        /// </summary>
        public string textNight { get; set; }

        /// <summary>
        /// 预报白天风向360角度
        /// </summary>
        public string wind360Day { get; set; }

        /// <summary>
        /// 预报白天风向
        /// </summary>
        public string windDirDay { get; set; }

        /// <summary>
        ///  预报白天风力等级
        /// </summary>
        public string windScaleDay { get; set; }

        /// <summary>
        /// 预报白天风速，公里/小时
        /// </summary>
        public string windSpeedDay { get; set; }

        /// <summary>
        /// 预报夜间风向360角度
        /// </summary>
        public string wind360Night { get; set; }

        /// <summary>
        /// 预报夜间当天风向
        /// </summary>
        public string windDirNight { get; set; }

        /// <summary>
        /// 预报夜间风力等级
        /// </summary>
        public string windScaleNight { get; set; }

        /// <summary>
        /// 预报夜间风速，公里/小时
        /// </summary>
        public string windSpeedNight { get; set; }

        /// <summary>
        /// 相对湿度，百分比数值
        /// </summary>
        public string humidity { get; set; }

        /// <summary>
        /// 预报当天总降水量，默认单位：毫米
        /// </summary>
        public string precip { get; set; }

        /// <summary>
        /// 大气压强，默认单位：百帕
        /// </summary>
        public string pressure { get; set; }

        /// <summary>
        ///  能见度，默认单位：公里
        /// </summary>
        public string vis { get; set; }

        /// <summary>
        ///  云量，百分比数值。可能为空
        /// </summary>
        public string cloud { get; set; }

        /// <summary>
        /// 紫外线强度指数
        /// </summary>
        public string uvIndex { get; set; }
    }
}
