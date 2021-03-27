using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using itec_backend.Data;
using itec_backend.Entities;
using itec_backend.Helpers;
using itec_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace itec_backend.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UnauthorizedResult), 401)]
    public class CountryController : Controller
    {
        private readonly IRepository<CountryEntity> _countryEntity;

        public CountryController(ApplicationDbContext context)
        {
            _countryEntity = context.GetRepository<CountryEntity>();
        }


        [HttpGet("GetCountryInfo")]
        [ProducesResponseType(typeof(IEnumerable<CountryEntity>), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetCountryInfo(string countryName, string lat, string lng)
        {
            var countryEntity = _countryEntity.Queryable.FirstOrDefault(t =>
                String.Equals(t.Name, countryName, StringComparison.CurrentCultureIgnoreCase));
            var covidVaccinationRate = (float)0;
            if (countryEntity == null)
            {
                var newCountryEntity = new CountryEntity(countryName,CovidVaccinationRate.GetCovidVaccinationRatePerCountry(countryName),DateTime.Today, 
                    false,Guid.NewGuid().ToString());
                await _countryEntity.AddAsync(newCountryEntity);
            }else 
            if (countryEntity.CovidVaccinationDate!=DateTime.Today )
            {
                countryEntity.CovidVaccinationRate = CovidVaccinationRate.GetCovidVaccinationRatePerCountry(countryName);
                countryEntity.CovidVaccinationDate= DateTime.Today;
                await  _countryEntity.UpdateAsync(countryEntity);

            } 
            else
            {
                covidVaccinationRate = countryEntity.CovidVaccinationRate;

            }

            var response = new CountryModel
            {
                Name = countryName,
                covidVaccinesRate = covidVaccinationRate,
                weather = WeatherHelper.GetWeather(lat, lng)
            };
            return Ok(response);
        }
        
   
    };
}