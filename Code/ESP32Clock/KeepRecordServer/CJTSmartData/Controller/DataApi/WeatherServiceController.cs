
using TextVoiceServer.DBContext;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TextVoiceServer.DBHandler;
using TextVoiceServer.Serivices;
using RestSharp;
using Entities.User; 
using Entities.Weather;
using Microsoft.AspNetCore.Http.HttpResults;
using Org.BouncyCastle.Asn1.Ocsp;

namespace TextVoiceServer.DataApi
{
    [Route("api/weather")]
    [ApiController]
    public class WeatherServiceController : ControllerBase
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
         
        public WeatherServiceController(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        
        [HttpPost("getall/{type}")]
        public async Task<IActionResult> GetWeather( string type ) 
        {
            //[FromBody]string type
            TipResult tipResult = new TipResult()
            {
                Status = 0,
                Msg = "", 
            }; 
            var loginusername = HttpContext.Items[nameof(LoginUser.Username)];
            if ( loginusername != null && !String.IsNullOrWhiteSpace(loginusername.ToString()))
            {
                string latlongInfo = "118.700859,32.112609"; //pukou,nanjing
                var weatherClient = new RestClient("https://api.caiyunapp.com");
                
                var request = new RestRequest($"/v2.6/dEGbvLSvTzLrOCNl/{latlongInfo}/weather")
                    .AddParameter("alert", "true")
                    .AddParameter("dailysteps", "1")
                    .AddParameter("hourlysteps", "24")
                    .AddParameter("alert", "true");
                request.Timeout = 5000;
                try
                {
                    var response = await weatherClient.GetAsync<WeatherModel>(request);
                    tipResult.Status = 1;
                    tipResult.Msg = "Success."; 
                    if ("filter".Equals(type))
                    {
                        WeatherInfo weatherInfo = new WeatherInfo(response);
                        string weathernameFilePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Data", "weatherphenomena.json");
                        string resultMsg = System.IO.File.ReadAllText(weathernameFilePath);
                        if(resultMsg != null)
                        {
                            List<PhenomenaCode> codelist = JsonHelper.DeserializeJsonToList<PhenomenaCode>(resultMsg);
                            weatherInfo.hourlyInfo.SkyconInfo.ForEach(sky =>
                            {
                                var sameCode = codelist.Where(c => c.Code.Equals(sky.Value, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                                sky._Value = sameCode?.Phenomena;
                            });

                            var sameCode2 = codelist.Where(c => c.Code.Equals(weatherInfo.realtimeInfo.Skycon, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                            weatherInfo.realtimeInfo._Skycon=sameCode2?.Phenomena;
                        } 
                        tipResult.Data = weatherInfo;
                    }
                    else
                    {
                        tipResult.Data = response;
                    }
                }
                catch(Exception ex)
                {
                    tipResult.Status = 0;
                    tipResult.Msg = "Fail."; 
                    tipResult.Data = ex;
                } 
            }
            else
            {
                tipResult.Status = 0;
                tipResult.Msg = "Unauthorized.";
            }
            return Ok(tipResult);
        }

        [HttpPost("hefengw")]
        public async Task<IActionResult> GetHefengWeather()
        {
            //[FromBody]string type
            TipResult tipResult = new TipResult()
            {
                Status = 0,
                Msg = "",
            };
            
           var loginusername = HttpContext.Items[nameof(LoginUser.Username)];
            if (loginusername != null && !String.IsNullOrWhiteSpace(loginusername.ToString()))
            {
                string location = "101190107"; //pukou,nanjing
                string key = ConfigurationHelper.AppSetting["HefengApi:Key"].ToString(); 
                var weatherClient = new RestClient("https://devapi.qweather.com/v7/weather/");

                //实时
                string nowRequestString = $"now?key={key}&location={location}";
                //24h预报
                string hourlyRequestString = $"24h?key={key}&location={location}";
                //7天
                string dailyRequestString = $"7d?key={key}&location={location}";
                 
               
                try
                {
                    HefengWeatherResponse weatherResponse = new HefengWeatherResponse();
                    //nowRequest
                    var nowRequest = new RestRequest(nowRequestString);
                    nowRequest.Timeout = 5000;
                    var nowResponse = await weatherClient.GetAsync<HefengWeatherResponse>(nowRequest);
                    if(nowResponse != null&& "200".Equals(nowResponse.code))
                    {
                        weatherResponse.now = nowResponse.now;
                    }

                    //nowRequest
                    var hourlyRequest = new RestRequest(hourlyRequestString);
                    hourlyRequest.Timeout = 5000;
                    var hourlyResponse = await weatherClient.GetAsync<HefengWeatherResponse>(hourlyRequest);
                    if (hourlyResponse != null && "200".Equals(hourlyResponse.code))
                    {
                        weatherResponse.hourly = hourlyResponse.hourly;
                    }

                    //dailyRequest
                    var dailyRequest = new RestRequest(dailyRequestString);
                    dailyRequest.Timeout = 5000;
                    var dailyResponse = await weatherClient.GetAsync<HefengWeatherResponse>(dailyRequest);
                    if (dailyResponse != null && "200".Equals(dailyResponse.code))
                    {
                        weatherResponse.daily = dailyResponse.daily;
                    }
                    tipResult.Data = weatherResponse;
                    tipResult.Status = 1;
                    tipResult.Msg = "Success."; 
                }
                catch (Exception ex)
                {
                    tipResult.Status = 0;
                    tipResult.Msg = "Fail.";
                    tipResult.Data = ex;
                }
            }
            else
            {
                tipResult.Status = 0;
                tipResult.Msg = "Unauthorized.";
            }
            return Ok(tipResult);
        }




    }
}
