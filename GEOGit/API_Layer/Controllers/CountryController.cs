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
using Microsoft.Extensions.Logging;

namespace API_Layer.Controllers
{
    [Route("api/Continent")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        public CountryManager CountryManager { get; set; }
        public ContinentManager ContinentManager { get; set; }
        public CountryController()
        {
            CountryManager = new CountryManager(new UnitOfWork(new DataContext()));
            ContinentManager = new ContinentManager(new UnitOfWork(new DataContext()));
        }

        [HttpGet("{id}/Country")]
        public ActionResult<List<SampleCountry>> GetAllCountriesInCont(int id)
        {
            try
            {
                var continent = ContinentManager.GetContinentById(id);
                return CountryManager.GetContinentWithName(continent).Select(x => new SampleCountry
                {
                    ID = x.ID,
                    Name = x.Name,
                    Population = x.Population,
                    Continent = $"http://localhost:3000/api/Continent/" + continent.ID,
                    Surface = x.Surface,
                    Cities = x.Cities.Select(k => $"http://localhost:3000/api/Continent/{continent.ID}/Country/{x.ID}/City/" + k.ID.ToString()).ToList()
                }).ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/Country/{countryID}")]
        public ActionResult<SampleCountry> getCountry(int id, int countryID)
        {
            try
            {
                var continent = ContinentManager.GetContinentById(id);
                var country = CountryManager.GetCountry(countryID);
                if (continent.Name == country.Continent.Name)
                {
                    return new SampleCountry
                    {
                        ID = country.ID,
                        Name = country.Name,
                        Continent = $"http://localhost:3000/api/Continent/" + country.Continent.ID,
                        Population = country.Population,
                        Surface = country.Surface,
                        Cities = country.Cities.Select(k => $"http://localhost:3000/api/Continent/{country.Continent.ID}/Country/{country.ID}/City/" + k.ID.ToString()).ToList()
                    };
                }
                else
                {
                    return NotFound("Country not found in continent");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/Country/{countryID}")]
        public ActionResult<SampleCountry> Put(int id, int CountryID, [FromBody] SampleCountry sampleCountry)
        {
            try
            {
                var continent = ContinentManager.GetContinentById(id);
                var country = CountryManager.GetCountry(CountryID);

                if (sampleCountry.Continent == null || sampleCountry.Continent == "")
                {
                    sampleCountry.Continent = id.ToString();
                }

                if (continent.Name == country.Continent.Name)
                {
                    CountryManager.Update(country, sampleCountry.Name, sampleCountry.Population, sampleCountry.Surface, ContinentManager.GetContinentById(Int32.Parse(sampleCountry.Continent)));
                    return Ok();
                }
                else
                {
                    return NotFound("Country Not found in Continent!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/Country")]
        public ActionResult<SampleCountry> Post(int id, [FromBody] SampleCountry sampleCountry)
        {
            try
            {
                CountryManager = new CountryManager(new UnitOfWork(new DataContext()));
                var country = new Country(sampleCountry.Name, ContinentManager.GetContinentById(id), sampleCountry.Population, sampleCountry.Surface);
                CountryManager.Add(country);
                return CreatedAtAction(nameof(getCountry), new { id = country.Continent.ID, countryID = country.ID }, country);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}/Country/{countryID}")]
        public ActionResult Delete(int id, int countryID)
        {
            try
            {
                var continent = ContinentManager.GetContinentById(id);
                var country = CountryManager.GetCountry(countryID);

                if (continent.Name == country.Continent.Name)
                {
                    if (country.Cities.Count == 0 || country.Cities == null)
                    {
                        CountryManager.RemoveCountry(countryID);
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest("County has cities! delete them first!");
                    }
                }
                else
                {
                    return NotFound("Country not found in continent");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}