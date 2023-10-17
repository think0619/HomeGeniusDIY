
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
            var loginusername = HttpContext.Items[nameof(LoginUser.username)];
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



        
    }
}
