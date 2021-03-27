using System.Net;
using itec_backend.Models;
using Newtonsoft.Json;

namespace itec_backend.Helpers
{
    public static class WeatherHelper
    {
        public static WeatherModel GetWeather(string lat, string lng)
        {
            var iconLink = "http://openweathermap.org/img/wn/";

            using (WebClient wc = new WebClient())
            {
                var url = "https://api.openweathermap.org/data/2.5/weather?lat=" + lat
                                                                                 + "&lon=" + lng
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

                    return weatherData;
                }
                catch
                {

                    return null;
                }

            }
        }
    }
}