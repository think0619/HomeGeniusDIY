using System;
using System.Collections.Generic;

namespace Entities.Weather
{
    /// <summary>
    /// Caiyun API
    /// </summary>
    public class WeatherModel
    {
        public string status { get; set; }
        public string api_version { get; set; }
        public string api_status { get; set; }
        public string lang { get; set; }
        public string unit { get; set; }
        public int tzshift { get; set; }
        public string timezone { get; set; }
        public long server_time { get; set; }
        public double[] location { get; set; }
        public WeatherResult result { get; set; }
         
    }

    public class WeatherResult
    {
        public WeatherAlert alert { get; set; }
        public WeatherRealtime realtime { get; set; }
        public WeatherMin minutely { get; set; }
        public WeatherHourly hourly { get; set; }
        public WeatherDaily daily { get; set; }
        public object primary { get; set; }
        public string forecast_keypoint { get; set; }
    }


    public class WeatherAlert
    {
        public string status { get; set; }
        public List<WeatherAlertContent> content { get; set; }
        public List<WeatherAlertAdcode> adcodes { get; set; }
    }

    public class WeatherAlertContent
    {
        public string province { get; set; }
        public string status { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string regionId { get; set; }
        public string county { get; set; }
        public long pubtimestamp { get; set; }
        public double[] latlon { get; set; }
        public string city { get; set; }
        public string alertId { get; set; }
        public string title { get; set; }//标题，如"三明市气象台发布雷电黄色预警[Ⅲ 级/较重]",
        public string adcode { get; set; } 
        public string source { get; set; } //发布单位，如"国家预警信息发布中心",
        public string location { get; set; }
        public string request_status { get; set; }
    }
    public class WeatherAlertAdcode
    {
        public int adcode { get; set; }
        public string name { get; set; }
    }

    public class WeatherRealtime
    {
        public string status { get; set; }
        public float temperature { get; set; } //地表 2 米气温
        public float humidity { get; set; } //地表 2 米湿度相对湿度(%)
        public float cloudrate { get; set; } //总云量(0.0-1.0)
        public string skycon { get; set; } //天气现象
        public float visibility { get; set; } //地表水平能见度
        public float dswrf { get; set; } //向下短波辐射通量(W/M2)
        public WeatherRealtimeWind wind { get; set; }
        public float pressure { get; set; } //地面气压
        public float apparent_temperature { get; set; } //体感温度
        public WeatherRealtimePrecipitation precipitation { get; set; } //降水
        public WeatherRealtimeAirQuality air_quality { get; set; }
        public WeatherRealtimeLifeIndex life_index { get; set; } // 生活指数
    }

    public class WeatherRealtimeWind
    {
        public float speed { get; set; } //	地表 10 米风速
        public float direction { get; set; } //地表 10 米风向
    }

    public class WeatherRealtimePrecipitation
    {
        public WeatherRealtimePreLocal local { get; set; }
        public WeatherRealtimePreNearest nearest { get; set; }
    }

    public class WeatherRealtimePreLocal
    {
        public string status { get; set; }
        public string datasource { get; set; }
        public float intensity { get; set; }

    }

    public class WeatherRealtimePreNearest
    {
        public string status { get; set; }
        public float distance { get; set; }
        public float intensity { get; set; }

    }


    public class WeatherRealtimeAirQuality
    {
        public float pm25 { get; set; }
        public float pm10 { get; set; }
        public float o3 { get; set; }
        public float so2 { get; set; }
        public float no2 { get; set; }
        public float co { get; set; }
        public WeatherRealtimeAirQualityAqi aqi { get; set; }
        public WeatherRealtimeAirQualityDes description { get; set; }
    }

    public class WeatherRealtimeAirQualityAqi
    {
        public float chn { get; set; }
        public float usa { get; set; }
    }
    public class WeatherRealtimeAirQualityDes
    {
        public string chn { get; set; }
        public string usa { get; set; }
    }

    public class WeatherRealtimeLifeIndex
    {
        public WeatherRealtimeLifeIndexU ultraviolet { get; set; }
        public WeatherRealtimeLifeIndexC comfort { get; set; }
    }


    public class WeatherRealtimeLifeIndexU
    {
        public float index { get; set; }
        public string desc { get; set; }
    }
    public class WeatherRealtimeLifeIndexC
    {
        public float index { get; set; }
        public string desc { get; set; }
    }

    public class WeatherMin
    {
        public string status { get; set; }
        public string datasource { get; set; }
        public List<float> precipitation_2h { get; set; } // 表示未来2小时每一分钟的雷达降水强度 
        public List<float> precipitation { get; set; } // 表示未来1小时每一分钟的雷达降水强度 
        public List<float> probability { get; set; }
        public string description { get; set; }
    }

    public class WeatherHourly
    {
        public string status { get; set; }
        public string description { get; set; }
        public List<WeatherHourlyPrecipitation> precipitation { get; set; } //降水 
        public List<WeatherHourlyValue> temperature { get; set; }
        public List<WeatherHourlyValue> apparent_temperature { get; set; }
        public List<WeatherHourlyWind> wind { get; set; }
        public List<WeatherHourlyValue> humidity { get; set; }
        public List<WeatherHourlyValue> cloudrate { get; set; }
        public List<WeatherHourlyStringValue> skycon { get; set; } //	天气现象
        public List<WeatherHourlyValue> pressure { get; set; }
        public List<WeatherHourlyValue> visibility { get; set; }
        public List<WeatherHourlyValue> dswrf { get; set; }
        public WeatherHourlyAirQuality air_quality { get; set; }
    }

    public class WeatherHourlyPrecipitation
    {
        public DateTime datetime { get; set; }
        public float value { get; set; }
        public float probability { get; set; } //降水概率(%)
    }

    public class WeatherHourlyValue
    {
        public DateTime datetime { get; set; }
        public float value { get; set; }
    }
    public class WeatherHourlyStringValue
    {
        public DateTime datetime { get; set; }
        public string value { get; set; }
    }

    public class WeatherHourlyWind
    {
        public DateTime datetime { get; set; }
        public float speed { get; set; }
        public float direction { get; set; }
    }

    public class WeatherHourlyAirQuality
    {
        public List<WeatherHourlyAirQualityAqi> aqi { get; set; }
        public List<WeatherHourlyValue> pm25 { get; set; }
    }
    public class WeatherHourlyAirQualityAqi
    {
        public DateTime datetime { get; set; }
        public WeatherRealtimeAirQualityAqi value { get; set; }
    }

    public class WeatherDaily
    {
        public string status { get; set; }
        public List<WeatherDailyAstro> astro { get; set; }
        public List<WeatherDailyPrecipitation> precipitation_08h_20h { get; set; } //白天降水数据
        public List<WeatherDailyPrecipitation> precipitation_20h_32h { get; set; } //夜晚降水数据
        public List<WeatherDailyPrecipitation> precipitation { get; set; } //降水数据
        public List<WeatherDailyCommonData> temperature { get; set; } //全天地表 2 米气温
        public List<WeatherDailyCommonData> temperature_08h_20h { get; set; } //白天地表 2 米气温
        public List<WeatherDailyCommonData> temperature_20h_32h { get; set; } //夜晚地表 2 米气温
        public List<WeatherDailyWind> wind { get; set; } //全天地表 10 米风速
        public List<WeatherDailyWind> wind_08h_20h { get; set; } //白天地表 10 米风速
        public List<WeatherDailyWind> wind_20h_32h { get; set; } //夜晚地表 10 米风速
        public List<WeatherDailyCommonData> humidity { get; set; }
        public List<WeatherDailyCommonData> cloudrate { get; set; }
        public List<WeatherDailyCommonData> pressure { get; set; }
        public List<WeatherDailyCommonData> visibility { get; set; }
        public List<WeatherDailyCommonData> dswrf { get; set; }
        public WeatherDailyAirQuality air_quality { get; set; }
        public List<WeatherDailySkycon> skycon { get; set; } //全天主要 天气现象
        public List<WeatherDailySkycon> skycon_08h_20h { get; set; } //	白天主要 天气现象
        public List<WeatherDailySkycon> skycon_20h_32h { get; set; } //夜晚主要 天气现象
        public WeatherDailyLifeIndex life_index { get; set; }
    }

    public class WeatherDailyAstro
    {
        public DateTime date { get; set; }
        public WeatherDailyAstroSun sunrise { get; set; }
        public WeatherDailyAstroSun sunset { get; set; }
    }
    public class WeatherDailyAstroSun
    {
        public string time { get; set; }
    }

    public class WeatherDailyPrecipitation
    {
        public DateTime date { get; set; }
        public float max { get; set; }
        public float min { get; set; }
        public float avg { get; set; }
        public float probability { get; set; }
    }

    public class WeatherDailyWind
    {
        public DateTime date { get; set; }

        public WeatherDailyWindInfo max { get; set; }
        public WeatherDailyWindInfo min { get; set; }
        public WeatherDailyWindInfo avg { get; set; }
    }
    public class WeatherDailyWindInfo
    {
        public float speed { get; set; }
        public float direction { get; set; }
    }

    public class WeatherDailyCommonData
    {
        public DateTime date { get; set; }
        public float max { get; set; }
        public float min { get; set; }
        public float avg { get; set; }
    }

    public class WeatherDailyAirQuality
    {
        public List<WeatherDailyAirQualityAqi> aqi { get; set; }
        public List<WeatherDailyCommonData> pm25 { get; set; }
    }

    public class WeatherDailyAirQualityAqi
    {
        public DateTime date { get; set; }
        public WeatherDailyAirQualityAqiValue max { get; set; }
        public WeatherDailyAirQualityAqiValue min { get; set; }
        public WeatherDailyAirQualityAqiValue avg { get; set; }
    }


    public class WeatherDailyAirQualityAqiValue
    {
        public float chn { get; set; }
        public float usa { get; set; }
    }

    public class WeatherDailySkycon
    {
        public DateTime date { get; set; }
        public string value { get; set; }
    }

    public class WeatherDailyLifeIndex
    {
        public List<WeatherDailyLifeIndexValue> ultraviolet { get; set; }
        public List<WeatherDailyLifeIndexValue> carWashing { get; set; }
        public List<WeatherDailyLifeIndexValue> dressing { get; set; }
        public List<WeatherDailyLifeIndexValue> comfort { get; set; }
        public List<WeatherDailyLifeIndexValue> coldRisk { get; set; }
    }

    public class WeatherDailyLifeIndexValue
    {
        public DateTime date { get; set; }
        public string index { get; set; }
        public string desc { get; set; }
    }
}
