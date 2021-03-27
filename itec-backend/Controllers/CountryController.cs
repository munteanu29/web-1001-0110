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
            var decodedCountry=RemoveDiacritics(countryName).ToLower();
            var countryEntity = _countryEntity.Queryable.FirstOrDefault(t =>
                String.Equals(t.Name, decodedCountry, StringComparison.CurrentCultureIgnoreCase));
            var covidVaccinationRate = (float)0;
            if (countryEntity == null)
            {
                var newCountryEntity = new CountryEntity(decodedCountry,CovidVaccinationRate.GetCovidVaccinationRatePerCountry(decodedCountry),DateTime.Today, 
                    false,Guid.NewGuid().ToString());
                await _countryEntity.AddAsync(newCountryEntity);
            }else 
            if (countryEntity.CovidVaccinationDate!=DateTime.Today )
            {
                countryEntity.CovidVaccinationRate = CovidVaccinationRate.GetCovidVaccinationRatePerCountry(decodedCountry);
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

        static Dictionary<string, string> foreign_characters = new Dictionary<string, string>
    {
        { "äæǽ", "ae" },
        { "öœ", "oe" },
        { "ü", "ue" },
        { "Ä", "Ae" },
        { "Ü", "Ue" },
        { "Ö", "Oe" },
        { "ÀÁÂÃÄÅǺĀĂĄǍΑΆẢẠẦẪẨẬẰẮẴẲẶА", "A" },
        { "àáâãåǻāăąǎªαάảạầấẫẩậằắẵẳặа", "a" },
        { "Б", "B" },
        { "б", "b" },
        { "ÇĆĈĊČ", "C" },
        { "çćĉċč", "c" },
        { "Д", "D" },
        { "д", "d" },
        { "ÐĎĐΔ", "Dj" },
        { "ðďđδ", "dj" },
        { "ÈÉÊËĒĔĖĘĚΕΈẼẺẸỀẾỄỂỆЕЭ", "E" },
        { "èéêëēĕėęěέεẽẻẹềếễểệеэ", "e" },
        { "Ф", "F" },
        { "ф", "f" },
        { "ĜĞĠĢΓГҐ", "G" },
        { "ĝğġģγгґ", "g" },
        { "ĤĦ", "H" },
        { "ĥħ", "h" },
        { "ÌÍÎÏĨĪĬǏĮİΗΉΊΙΪỈỊИЫ", "I" },
        { "ìíîïĩīĭǐįıηήίιϊỉịиыї", "i" },
        { "Ĵ", "J" },
        { "ĵ", "j" },
        { "ĶΚК", "K" },
        { "ķκк", "k" },
        { "ĹĻĽĿŁΛЛ", "L" },
        { "ĺļľŀłλл", "l" },
        { "М", "M" },
        { "м", "m" },
        { "ÑŃŅŇΝН", "N" },
        { "ñńņňŉνн", "n" },
        { "ÒÓÔÕŌŎǑŐƠØǾΟΌΩΏỎỌỒỐỖỔỘỜỚỠỞỢО", "O" },
        { "òóôõōŏǒőơøǿºοόωώỏọồốỗổộờớỡởợо", "o" },
        { "П", "P" },
        { "п", "p" },
        { "ŔŖŘΡР", "R" },
        { "ŕŗřρр", "r" },
        { "ŚŜŞȘŠΣС", "S" },
        { "śŝşșšſσςс", "s" },
        { "ȚŢŤŦτТ", "T" },
        { "țţťŧт", "t" },
        { "ÙÚÛŨŪŬŮŰŲƯǓǕǗǙǛŨỦỤỪỨỮỬỰУ", "U" },
        { "ùúûũūŭůűųưǔǖǘǚǜυύϋủụừứữửựу", "u" },
        { "ÝŸŶΥΎΫỲỸỶỴЙ", "Y" },
        { "ýÿŷỳỹỷỵй", "y" },
        { "В", "V" },
        { "в", "v" },
        { "Ŵ", "W" },
        { "ŵ", "w" },
        { "ŹŻŽΖЗ", "Z" },
        { "źżžζз", "z" },
        { "ÆǼ", "AE" },
        { "ß", "ss" },
        { "Ĳ", "IJ" },
        { "ĳ", "ij" },
        { "Œ", "OE" },
        { "ƒ", "f" },
        { "ξ", "ks" },
        { "π", "p" },
        { "β", "v" },
        { "μ", "m" },
        { "ψ", "ps" },
        { "Ё", "Yo" },
        { "ё", "yo" },
        { "Є", "Ye" },
        { "є", "ye" },
        { "Ї", "Yi" },
        { "Ж", "Zh" },
        { "ж", "zh" },
        { "Х", "Kh" },
        { "х", "kh" },
        { "Ц", "Ts" },
        { "ц", "ts" },
        { "Ч", "Ch" },
        { "ч", "ch" },
        { "Ш", "Sh" },
        { "ш", "sh" },
        { "Щ", "Shch" },
        { "щ", "shch" },
        { "ЪъЬь", "" },
        { "Ю", "Yu" },
        { "ю", "yu" },
        { "Я", "Ya" },
        { "я", "ya" },
    };
        public static string RemoveDiacritics( string s) 
        {
            //StringBuilder sb = new StringBuilder ();
            string text = "";


            foreach (char c in s)
            {
                int len = text.Length;

                foreach(KeyValuePair<string, string> entry in foreign_characters)
                {
                    if(entry.Key.IndexOf (c) != -1)
                    {
                        text += entry.Value;
                        break;
                    }
                }

                if (len == text.Length) {
                    text += c;  
                }
            }
            return text;
        }
    };
}