using System;
using System.Collections.Generic;
using itec_backend.Entities;

namespace itec_backend.Models
{
    public class CountryModel
    {
        public string Name { get; set; }
        public float covidVaccinesRate { get; set; }
        public  WeatherModel weather {
            get;
            set;
        }

    }
}