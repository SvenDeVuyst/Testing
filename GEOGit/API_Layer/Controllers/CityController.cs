using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Layer.Samples;
using BusinessLaag.managers;
using BusinessLaag.models;
using DataLaag;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/Continent")]
    [ApiController]
    public class CityController : ControllerBase
    {
        public ContinentManager ContinentManager { get; set; }
        public CountryManager CountryManager { get; set; }
        public CityManager CityManager { get; set; }
        public CityController()
        {
            ContinentManager = new ContinentManager(new UnitOfWork(new DataContext()));
            CountryManager = new CountryManager(new UnitOfWork(new DataContext()));
            CityManager = new CityManager(new UnitOfWork(new DataContext()));
        }
        [HttpGet("{id}/Country/{countryId}/City/{cityId}")]
        public ActionResult<SampleCity> GetCity(int id, int countryId, int cityId)
        {
            try
            {
                var continent = ContinentManager.GetContinentById(id);
                var country = CountryManager.GetCountry(countryId);
                if (continent.Name == country.Continent.Name)
                {
                    var city = CityManager.GetCityById(cityId);
                    if (city == null)
                    {
                        return NotFound("City does not exist in this County!");
                    }
                    else
                    {
                        return new SampleCity { ID = city.ID, Name = city.Name, Capital = city.IsCapital, Country = $"http://localhost:5001/api/Continent/{id}/Country/{countryId}", Population = city.Population };
                    }
                }
                else
                {
                    return NotFound("Country not found in Continent!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR City Controller in the Get request (" + ex + ")");
            }
        }

        [HttpPost("{id}/Country/{countryId}/City")]
        public ActionResult PostCity(int id, int countryId, [FromBody] SampleCity city)
        {
            try
            {
                var continent = ContinentManager.GetContinentById(id);
                var country = CountryManager.GetCountry(countryId);
                if (continent.Name == country.Continent.Name)
                {
                    var tmpCity = new City(city.Name, city.Population, country, city.Capital);
                    CityManager.Add(tmpCity);
                    return CreatedAtAction(nameof(GetCity), new { id = continent.ID, countryId = country.ID, cityId = tmpCity.ID }, tmpCity);
                }
                else
                {
                    return BadRequest("Country not found in Continent!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR City Controller in the Post request (" + ex + ")");
            }
        }

        [HttpDelete("{id}/Country/{countryId}/City/{cityId}")]
        public ActionResult<SampleCity> DeleteCity(int id, int countryId, int cityId)
        {
            try
            {
                var continent = ContinentManager.GetContinentById(id);
                var country = CountryManager.GetCountry(countryId);
                if (continent.Name == country.Continent.Name)
                {
                    var city = CityManager.GetCityById(cityId);
                    if (city == null)
                    {
                        return NotFound("City does not exist in this Country!");
                    }
                    else
                    {
                        CityManager.RemoveCityById(cityId);
                        return NoContent();
                    }
                }
                else
                {
                    return NotFound("Country does not exist in this Continent!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR City Controller in the Delete request (" + ex + ")");
            }
        }
    }
}