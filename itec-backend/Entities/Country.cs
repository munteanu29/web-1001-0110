using System;
using System.Collections.Generic;

namespace itec_backend.Entities
{
    public class CountryEntity: Entity
    {
        public CountryEntity(string name, float covidVaccinationRate,DateTime covidVaccinationDate, bool deleted, string id):   base(deleted, id)
        {
            Name = name;
            CovidVaccinationRate = covidVaccinationRate;
            LocationEntities = new List<LocationEntity>();
            CovidVaccinationDate = DateTime.Today;
            CovidVaccinationDate = covidVaccinationDate;

        }
        public string Name { get; set; }
        public float CovidVaccinationRate { get; set; }
        public DateTime CovidVaccinationDate { get; set; }
        public List<LocationEntity> LocationEntities { get; set; }
    }
}