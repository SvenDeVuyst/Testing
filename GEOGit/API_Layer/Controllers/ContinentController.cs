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
    public class ContinentController : ControllerBase
    {
        public ContinentManager ContinentManager { get; set; }
        public CountryManager CountryManager { get; set; }

        public ContinentController()
        {
            ContinentManager = new ContinentManager(new UnitOfWork(new DataContext()));
            CountryManager = new CountryManager(new UnitOfWork(new DataContext()));
        }

        [HttpGet]
        public ActionResult<List<SampleContinent>> GetAllContinents()
        {
            try
            {
                return ContinentManager.GetAllContinents().Select(x => new SampleContinent
                {
                    ID = $"http://localhost:3000/api/Continent/{x.ID}",
                    Name = x.Name,
                    Population = x.Population,
                    Countries = CountryManager.GetContinentWithName(x).Select(y => $"http://localhost:3000/api/Continent/{x.ID}/Country/" + y.ID.ToString()).ToList()
                }).ToList();
            }
            catch
            {
                Response.StatusCode = 400;
                return null;
            }
        }

        [HttpPost]
        public ActionResult<SampleContinent> AddContinent([FromBody] SampleContinent sampleContinent)
        {
            try
            {
                Continent continent = new Continent(sampleContinent.Name);
                if (ContinentManager.ContinentExists(continent))
                {
                    ContinentManager.Add(continent);
                    return CreatedAtAction(nameof(GetContinent), new { id = continent.ID }, continent);
                }
                else
                {
                    return BadRequest("Continent already exists!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("ERROR : " + ex);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<SampleContinent> GetContinent(int id)
        {
            try
            {
                var continent = ContinentManager.GetContinentById(id);
                var countryList = CountryManager.GetContinentWithName(continent).Select(x => $"http://localhost:3000/api/Continent/{continent.ID}/Country/" + x.ID.ToString()).ToList();
                return new SampleContinent { ID = continent.ID.ToString(), Name = continent.Name, Population = continent.Population, Countries = countryList };
            }
            catch (Exception ex)
            {
                return NotFound("Continent not found!");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<SampleContinent> Put(int id, [FromBody] SampleContinent sampleContinent)
        {
            try
            {
                var continent = ContinentManager.GetContinentById(id);

                if (ContinentManager.ContinentExists(new Continent(continent.Name)))
                {
                    continent.SetName(continent.Name);
                    ContinentManager.UpdateContinent(continent);
                    return CreatedAtAction(nameof(GetContinent), new { id = continent.ID }, continent);
                }
                else
                {
                    return BadRequest("Continent error");
                }
            }
            catch (Exception ex)
            {
                return NotFound("Continent not found!");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var continent = ContinentManager.GetContinentById(id);
                if (continent != null)
                {
                    if (CountryManager.GetContinentWithName(continent).Count == 0)
                    {
                        ContinentManager.RemoveContinent(id);
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest("Continent still has countries!");
                    }
                }
                else
                {
                    return NotFound("Continent not found!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}