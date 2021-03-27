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
        
        
        [HttpGet("GetCountry")]
        [ProducesResponseType(typeof(IEnumerable<CountryEntity>), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetCountry(string countryName)
        {
            var countryEntity =  _countryEntity.Queryable.Where(t=>String.Equals(t.Name, countryName, StringComparison.CurrentCultureIgnoreCase))
                .Include((entity=>entity.LocationEntities)).FirstOrDefault();
            if (countryEntity!=null )
            {
                countryEntity.CovidVaccinationRate = CovidVaccinationRate.GetCovidVaccinationRatePerCountry(countryEntity.Name);
                countryEntity.CovidVaccinationDate= DateTime.Now;
                await  _countryEntity.UpdateAsync(countryEntity);
            }
            return Ok(countryEntity);
        }
        
        [HttpPost("AddCountry")]
        public async Task<IActionResult> AddCountry([FromBody] CountryModel country)
        {
          
            var countryEntity =  _countryEntity.Queryable.FirstOrDefault(t => t.Name == country.Name);
            if (countryEntity != null &&  countryEntity.CovidVaccinationDate != DateTime.Now)
            {
                country.LocationEntities.ForEach(t=> countryEntity.LocationEntities
                    .Add(new LocationEntity(t.Name, t.Price, t.XCoordonate, t.YCoordonate, false,Guid.NewGuid().ToString() )));
                await  _countryEntity.UpdateAsync(countryEntity);
                return Ok();
            }

            var newCountryEntity = new CountryEntity(country.Name,CovidVaccinationRate.GetCovidVaccinationRatePerCountry(country.Name),DateTime.Now, 
                false,Guid.NewGuid().ToString());
            country.LocationEntities.ForEach(t=> newCountryEntity.LocationEntities
                .Add(new LocationEntity(t.Name, t.Price, t.XCoordonate, t.YCoordonate, false,Guid.NewGuid().ToString() )));
           
            await _countryEntity.AddAsync(newCountryEntity);
            return Ok();
        }
        
    }
}