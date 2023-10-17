using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Weather
{
    public class WeatherInfo
    {
        public DateTime ServerTime { get; set; }
        public string _ServerTime
        {
            get
            {
                if (ServerTime != null)
                {
                    return ServerTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    return "";
                }
            }
        }
        public string LatLong { get; set; }//经纬度
        public RealtimeInfo realtimeInfo { get; set; }
        public HourlyInfo hourlyInfo { get; set; }
        public DailyInfo dailyInfo { get; set; } 
        public string ForecastKeypoint { get; set; } 

        public WeatherInfo() { }
        public WeatherInfo(WeatherModel weathermodel)
        {
            if (weathermodel.server_time != 0)
            {
                ServerTime = ComHelper.UnixTimeStampToDateTime(weathermodel.server_time);
            }

            if (weathermodel.location.Length == 2)
            {
                LatLong = String.Format($"{weathermodel.location[0]},{weathermodel.location[1]}");
            }

            if (weathermodel.result.realtime != null)
            {
                var realtime = weathermodel.result.realtime;
                realtimeInfo = new RealtimeInfo();
                realtimeInfo.Temperature = realtime.temperature;
                realtimeInfo.ApparentTemperature = realtime.apparent_temperature;
                realtimeInfo.Humidity = realtime.humidity;
                realtimeInfo.CloudRate = realtime.cloudrate;
                realtimeInfo.Skycon = realtime.skycon;
                realtimeInfo.WindSpeed = realtime.wind.speed;
                realtimeInfo.Pressure = realtime.pressure;
                realtimeInfo.PrecipitationIntensityLocal = realtime.precipitation?.local.intensity;
                realtimeInfo.PrecipitationDistanceNearest = realtime.precipitation?.nearest.distance;
                realtimeInfo.AirPM25 = realtime.air_quality?.pm25;
                realtimeInfo.AirPM10 = realtime.air_quality?.pm10;
                realtimeInfo.AirO3 = realtime.air_quality?.o3;
                realtimeInfo.AirSO2 = realtime.air_quality?.so2;
                realtimeInfo.AirNO2 = realtime.air_quality?.no2;
                realtimeInfo.AirCO = realtime.air_quality?.co;
                realtimeInfo.AirAQI_CHN = String.Format($"{realtime.air_quality?.aqi?.chn},{realtime.air_quality?.description?.chn}");
                realtimeInfo.AirAQI_USA = String.Format($"{realtime.air_quality?.aqi?.usa},{realtime.air_quality?.description?.usa}");

                realtimeInfo.Life_Ultraviolet = String.Format($"紫外线：{realtime.life_index?.ultraviolet?.desc}");
                realtimeInfo.Life_Comfort = String.Format($"舒适度：{realtime.life_index?.comfort?.desc}");
            }

            if (weathermodel.result.hourly != null) 
            {
                var hourlydata=weathermodel.result.hourly;
                hourlyInfo = new HourlyInfo();
                hourlyInfo.Description = hourlydata.description;

                hourlyInfo.Precipitations = new List<PrecipitationInfo>();
                foreach (var pre in hourlydata.precipitation) 
                {
                    hourlyInfo.Precipitations.Add(new PrecipitationInfo()
                    {
                        DatetimeT = pre.datetime,
                        Value= pre.value,   
                        Probability= pre.probability,
                    });
                }

                hourlyInfo.Temperature = new List<TempInfo>();
                foreach (var temp in hourlydata.temperature)
                {
                    hourlyInfo.Temperature.Add(new TempInfo()
                    {
                        DatetimeT = temp.datetime,
                        Value = temp.value, 
                    });
                }

                hourlyInfo.ApparentTemperature = new List<TempInfo>();
                foreach (var temp in hourlydata.apparent_temperature)
                {
                    hourlyInfo.ApparentTemperature.Add(new TempInfo()
                    {
                        DatetimeT = temp.datetime,
                        Value = temp.value,
                    });
                }

                hourlyInfo.SkyconInfo=new List<SkyconInfo>();
                foreach (var sky in hourlydata.skycon)
                {
                    hourlyInfo.SkyconInfo.Add(new SkyconInfo()
                    {
                        DatetimeT = sky.datetime,
                        Value = sky.value,
                    });
                } 
            }

            if (weathermodel.result.daily != null)
            {
                var dailydata = weathermodel.result.daily;
                //dailyInfo = new DailyInfo()
                //{
                //    SunriseSunsetDesc = (dailydata.astro.Count > 0) ? String.Format($"Sunrise:{dailydata.astro[0].sunrise.time} Sunset:{dailydata.astro[0].sunset.time}") : "",
                //    PrecipitationDesc = (dailydata.precipitation.Count > 0) ? String.Format($"降水概率：{dailydata.precipitation[0].probability}") : "",
                //    TemperatureDesc = (dailydata.temperature.Count > 0) ? String.Format($"气温：{dailydata.temperature[0].min}-{dailydata.temperature[0].max}") : "",
                //    UltravioletDesc = (dailydata.life_index?.ultraviolet.Count > 0) ? $"紫外线：{dailydata.life_index.ultraviolet[0].desc}" : "",
                //    CarWashingDesc = (dailydata.life_index?.carWashing.Count > 0) ? $"洗车：{dailydata.life_index.carWashing[0].desc}" : "",
                //    DressingDesc = (dailydata.life_index?.dressing.Count > 0) ? $"穿衣指数：{dailydata.life_index.dressing[0].desc}" : "",
                //    ComfortDesc = (dailydata.life_index?.comfort.Count > 0) ? $"舒适度指数：{dailydata.life_index.comfort[0].desc}" : "",
                //    ColdRiskDesc = (dailydata.life_index?.coldRisk.Count > 0) ? $"感冒指数：{dailydata.life_index.coldRisk[0].desc}" : "",
                //};
                dailyInfo = new DailyInfo()
                {
                    SunriseSunsetDesc = (dailydata.astro.Count > 0) ? String.Format($"{dailydata.astro[0].sunrise.time}-{dailydata.astro[0].sunset.time}") : "",
                    PrecipitationDesc = (dailydata.precipitation.Count > 0) ? String.Format($"{dailydata.precipitation[0].probability}") : "",
                    TemperatureDesc = (dailydata.temperature.Count > 0) ? String.Format($"{dailydata.temperature[0].min}-{dailydata.temperature[0].max}") : "",
                    UltravioletDesc = (dailydata.life_index?.ultraviolet.Count > 0) ? $"{dailydata.life_index.ultraviolet[0].desc}" : "",
                    CarWashingDesc = (dailydata.life_index?.carWashing.Count > 0) ? $"{dailydata.life_index.carWashing[0].desc}" : "",
                    DressingDesc = (dailydata.life_index?.dressing.Count > 0) ? $"{dailydata.life_index.dressing[0].desc}" : "",
                    ComfortDesc = (dailydata.life_index?.comfort.Count > 0) ? $"{dailydata.life_index.comfort[0].desc}" : "",
                    ColdRiskDesc = (dailydata.life_index?.coldRisk.Count > 0) ? $"{dailydata.life_index.coldRisk[0].desc}" : "",
                };
            }

            ForecastKeypoint = weathermodel.result.forecast_keypoint;
        }
    } 

    public class RealtimeInfo
    {
        public float Temperature { get; set; }
        public float ApparentTemperature { get; set; }
        public float Humidity { get; set; }
        public float CloudRate { get; set; }
        public string Skycon { get; set; }
        public float WindSpeed { get; set; }
        public float Pressure { get; set; }
        public float? PrecipitationIntensityLocal { get; set; } //强度
        public float? PrecipitationDistanceNearest { get; set; }
        public float? AirPM25 { get; set; }
        public float? AirPM10 { get; set; }
        public float? AirO3 { get; set; }
        public float? AirSO2 { get; set; }
        public float? AirNO2 { get; set; }
        public float? AirCO { get; set; }
        public string AirAQI_CHN { get; set; }
        public string AirAQI_USA { get; set; }
        public string Life_Ultraviolet { get; set; } //紫外线
        public string Life_Comfort { get; set; } //舒适度指数 
    }

    public class HourlyInfo
    {
        public string Description { get; set; }
        public List<PrecipitationInfo> Precipitations { get; set; }
        public List<TempInfo> Temperature { get; set; }
        public List<TempInfo> ApparentTemperature { get; set; }
        public List<SkyconInfo> SkyconInfo { get; set; }

    }

    public class DailyInfo
    {
        public string SunriseSunsetDesc { get; set; }
        public string PrecipitationDesc { get; set; }
        public string TemperatureDesc { get; set; }
        public string UltravioletDesc { get; set; }
        public string CarWashingDesc { get; set; }
        public string DressingDesc { get; set; }
        public string ComfortDesc { get; set; }
        public string ColdRiskDesc { get; set; }
    }

    public class PrecipitationInfo
    {
        public DateTime DatetimeT { get; set; }
        public string Time 
        {
            get 
            {
                if (DatetimeT != null)
                {
                    return DatetimeT.ToString("HH:mm");
                }
                else 
                {
                    return "";
                }
            }
        }
        public float Value { get; set; }
        public float Probability { get; set; }
    }
    public class TempInfo
    {
        public DateTime DatetimeT { get; set; }
        public string Time
        {
            get
            {
                if (DatetimeT != null)
                {
                    return DatetimeT.ToString("HH:mm");
                }
                else
                {
                    return "";
                }
            }
        }
        public float Value { get; set; }
    }
    public class SkyconInfo
    {
        public DateTime DatetimeT { get; set; }
        public string Time
        {
            get
            {
                if (DatetimeT != null)
                {
                    return DatetimeT.ToString("HH:mm");
                }
                else
                {
                    return "";
                }
            }
        }
        public string Value { get; set; }
        public string _Value { get; set; }
    }
     

}
