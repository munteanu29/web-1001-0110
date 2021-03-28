using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
           
            var decodedCountry=CountryTranslator.Translate(countryName);
            var countryEntity = _countryEntity.Queryable.FirstOrDefault(t =>
                String.Equals(t.Name, decodedCountry, StringComparison.CurrentCultureIgnoreCase));
            var covidVaccinationRate = (float)0;
            if (countryEntity == null)
            {
                covidVaccinationRate = CovidVaccinationRate.GetCovidVaccinationRatePerCountry(decodedCountry);
                var newCountryEntity = new CountryEntity(decodedCountry,covidVaccinationRate,DateTime.Today, 
                    false,Guid.NewGuid().ToString());
                await _countryEntity.AddAsync(newCountryEntity);
            }else 
            if (countryEntity.CovidVaccinationDate!=DateTime.Today )
            {
                covidVaccinationRate = CovidVaccinationRate.GetCovidVaccinationRatePerCountry(decodedCountry);
                countryEntity.CovidVaccinationRate =covidVaccinationRate;
                countryEntity.CovidVaccinationDate= DateTime.Today;
                await  _countryEntity.UpdateAsync(countryEntity);

            } 
            else
            {
                covidVaccinationRate = countryEntity.CovidVaccinationRate;

            }

            var response = new CountryModel
            {
                Name = decodedCountry,
                covidVaccinesRate = covidVaccinationRate,
                weather = WeatherHelper.GetWeather(lat, lng)
            };
            return Ok(response);
        }

     
    };
}