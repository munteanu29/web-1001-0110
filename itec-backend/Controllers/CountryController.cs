using System.Collections.Generic;
using System.Threading.Tasks;
using itec_backend.Data;
using itec_backend.Entities;
using itec_backend.Helpers;
using itec_backend.Models;
using Microsoft.AspNetCore.Mvc;

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
            var response = new CountryModel
            {
                Name = countryName,
                covidVaccinesRate = CovidVaccinationRate.GetCovidVaccinationRatePerCountry(countryName),
                weather = WeatherHelper.GetWeather(lat, lng)
            };
            return Ok(response);
        }
    }
}