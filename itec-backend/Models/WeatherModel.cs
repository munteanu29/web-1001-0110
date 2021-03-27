namespace itec_backend.Models
{
    public class WeatherModel
    {
        public Weather[] weather { get; set; }
        public Main main { get; set; }
    }

    public class Weather
    {
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public float temp { get; set; }
    }
    
   
}
