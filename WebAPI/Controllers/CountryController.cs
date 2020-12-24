using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Managers;
using BusinessLayer.Models;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/continent/{continentId:int}/country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private ContinentManager ContinentManager { get; set; }
        private CountryManager CountryManager { get; set; }

        public CountryController()
        {
            this.ContinentManager = new ContinentManager(new UnitOfWork(new DataContext()));
            this.CountryManager = new CountryManager(new UnitOfWork(new DataContext()));
        }

        #region GET
        /// <summary>
        /// Get all continent Countries 
        /// GET: /api/continent/{continent}/country
        /// </summary>
        /// <param name="continentId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<TCountry>> Get(int continentId)
        {
            try
            {
                Continent continent = ContinentManager.Get(continentId);
                if(continent != null)
                {
                    return continent.Countries.Select(x => new TCountry(x)).ToList<TCountry>();
                }
                return NotFound("Continent not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get Country by Id
        /// GET: /api/continent/{continent}/country/{country}
        /// </summary>
        /// <param name="continentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public ActionResult<TCountry> GetCountry(int continentId, int id)
        {
            try
            {
                Continent continent = ContinentManager.Get(continentId);
                if (continent != null)
                {
                    Country country = CountryManager.Get(id);
                    if(country != null)
                    {
                        return new TCountry(country);
                    }
                    return NotFound("Country not found");
                }
                return NotFound("Continent not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region POST
        /// <summary>
        /// 
        /// </summary>
        /// <param name="continentId"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public ActionResult<TCountry> Post(int continentId, [FromBody] TCountry c)
        {
            try
            {
                Continent continent = ContinentManager.Get(continentId);
                if (continent != null)
                {
                    Country country = new Country(c.Name, c.Population, c.Surface, continent);
                    continent.AddCountry(country);
                    country = CountryManager.Add(country);
                    return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, country);
                }
                return NotFound("Continent not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Delete Country
        /// DELETE: /api/continenet/{continent}/country/{country}
        /// </summary>
        /// <param name="continentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int continentId, int id)
        {
            try
            {
                Continent continent = ContinentManager.Get(continentId);
                if (continent != null)
                {
                    Country country = CountryManager.Get(id);
                    if (country != null)
                    {
                        if (country.Cities.Count == 0)
                        {
                            CountryManager.Delete(country);
                            return Ok("Country succesfully deleted");
                        }
                        return BadRequest("Conflic with a city");
                    }
                    return NotFound("Country not found");
                }
                return NotFound("Continent not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region PUT
        /// <summary>
        /// Update Country
        /// PUT: /api/continenet/{continent}/country/{country}
        /// </summary>
        /// <param name="continentId"></param>
        /// <param name="id"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public ActionResult<TCountry> Put(int continentId, int id, [FromBody] TCountry c)
        {
            try
            {
                Continent continent = ContinentManager.Get(continentId);
                if (continent != null)
                {
                    Country country = CountryManager.Get(id);
                    if (country != null)
                    {
                        country.SetName(c.Name);
                        country.SetPopulation(c.Population);
                        country.SetSurface(c.Surface);
                        CountryManager.Update(country);
                        return Ok(new TCountry(country));
                    }
                    return NotFound("Country not found");
                }
                return NotFound("Continent not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}