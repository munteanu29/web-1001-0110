using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using itec_backend.Entities;
using itec_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace itec_backend.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UnauthorizedResult), 401)]
    public class Weather : Controller
    {

        
        [HttpGet("GetWeather")]
        [ProducesResponseType(typeof(IEnumerable<CountryEntity>), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetWeather(string x, string y)
        {
            var iconLink = "http://openweathermap.org/img/wn/";

            using (WebClient wc = new WebClient())
            {
                var url = "https://api.openweathermap.org/data/2.5/weather?lat=" + x
                                                                                 + "&lon=" + y
                                                                                 + "&units=metric&appid=" +
                                                                                 "53e81050fa084937ff89f0b6eaf85cd7";
                try
                {
                    var json = wc.DownloadString(url);
                    var weatherData = JsonConvert.DeserializeObject<WeatherModel>(json);
                    foreach (var item in weatherData.weather)
                    {
                        item.icon = iconLink + item.icon + "@2x.png";
                    }

                    return Ok(weatherData);
                }
                catch
                {
                    return BadRequest("location error");
                }
            }
        }
        
      
    }
}