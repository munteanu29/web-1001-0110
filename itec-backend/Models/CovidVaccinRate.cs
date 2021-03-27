using System.Collections.Generic;

namespace itec_backend.Models
{
    public class CovidVaccinRate
    {
        public string country { get; set; }
        public List<CountryData> data { get; set; }
    }

    public class CountryData
    {
        public string  date { get; set; }
        public float people_vaccinated_per_hundred { get; set; }
        
    }
}