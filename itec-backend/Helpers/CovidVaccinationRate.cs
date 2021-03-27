using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using itec_backend.Models;
using Newtonsoft.Json;

namespace itec_backend.Helpers
{
    public static class CovidVaccinationRate
    {
        public static float GetCovidVaccinationRatePerCountry(string countryName)
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("https://raw.githubusercontent.com/owid/covid-19-data/master/public/data/vaccinations/vaccinations.json");
                var countryData = new CountryData();
                var movie1 = JsonConvert.DeserializeObject<List<CovidVaccinRate>>(json);
                var covidVaccinRate=movie1.FirstOrDefault(t => String.Equals(t.country, countryName, StringComparison.CurrentCultureIgnoreCase));
                if(covidVaccinRate!=null)
                    countryData= covidVaccinRate.data
                        .FindLast(t => t.date != String.Empty );
                return countryData.people_vaccinated_per_hundred;
            }
        }

    }
    
    
}