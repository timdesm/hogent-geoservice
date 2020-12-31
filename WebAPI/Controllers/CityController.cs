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
    [Route("api/continent/{continentId:int}/country/{countryId:int}/city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private ContinentManager ContinentManager { get; set; }
        private CountryManager CountryManager { get; set; }
        private CityManager CityManager { get; set; }

        public CityController()
        {
            this.ContinentManager = new ContinentManager(new UnitOfWork(new DataContext()));
            this.CountryManager = new CountryManager(new UnitOfWork(new DataContext()));
            this.CityManager = new CityManager(new UnitOfWork(new DataContext()));
        }

        #region GET
        /// <summary>
        /// Get all country Cities
        /// GET: /api/continent/{continent}/country/{country}/city
        /// </summary>
        /// <param name="continentId"></param>
        /// <param name="countryId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<TCity>> Get(int continentId, int countryId)
        {
            try
            {
                Continent continent = ContinentManager.Get(continentId);
                if (continent != null)
                {
                    Country country = CountryManager.Get(countryId);
                    if(country != null)
                    {
                        return country.Cities.Select(x => new TCity(x)).ToList<TCity>();
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

        /// <summary>
        /// Get City by Id
        /// GET: /api/continent/{continent}/country/{country}/city/{city}
        /// </summary>
        /// <param name="continentId"></param>
        /// <param name="countryId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public ActionResult<TCity> GetCity(int continentId, int countryId, int id)
        {
            try
            {
                Continent continent = ContinentManager.Get(continentId);
                if (continent != null)
                {
                    Country country = CountryManager.Get(countryId);
                    if (country != null)
                    {
                        City city = CityManager.Get(id);
                        if(city != null)
                        {
                            return new TCity(city);
                        }
                        return NotFound("City not found");
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
        /// <param name="countryId"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public ActionResult<TCity> Post(int continentId, int countryId, [FromBody] TCity c)
        {
            try
            {
                Continent continent = ContinentManager.Get(continentId);
                if (continent != null)
                {
                    Country country = CountryManager.Get(countryId);
                    if (country != null)
                    {
                        City city = new City(c.Name, country, c.Population);
                        city.Country.AddCity(city);
                        CityManager.Add(city);
                        return CreatedAtAction(nameof(GetCity), new { id = city.Id }, city);
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

        #region DELETE
        /// <summary>
        /// Delete City
        /// DELETE: /api/continenet/{continent}/country/{country}/city/{city}
        /// </summary>
        /// <param name="continentId"></param>
        /// <param name="countryId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int continentId, int countryId, int id)
        {
            try
            {
                Continent continent = ContinentManager.Get(continentId);
                if (continent != null)
                {
                    Country country = CountryManager.Get(countryId);
                    if (country != null)
                    {
                        City city = CityManager.Get(id);
                        if (city != null)
                        {
                            CityManager.Delete(city);
                            return Ok("City succesfully deleted");
                        }
                        return NotFound("City not found");
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
        /// Update City
        /// PUT: /api/continenet/{continent}/country/{country}/city/{city}
        /// </summary>
        /// <param name="continentId"></param>
        /// <param name="countryId"></param>
        /// <param name="id"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public ActionResult<TCity> Put(int continentId, int countryId, int id, [FromBody] TCity c)
        {
            try
            {
                Continent continent = ContinentManager.Get(continentId);
                if (continent != null)
                {
                    Country country = CountryManager.Get(countryId);
                    if (country != null)
                    {
                        City city = country.Cities.Where(x => x.Id == id).Single();
                        if (city != null)
                        {
                            // Update city
                            city.SetName(c.Name);
                            city.SetPopulation(c.Population);

                            // Update city country
                            if (!string.IsNullOrWhiteSpace(c.Country) && int.TryParse(c.Country, out int newCountryId))
                            {
                                Country newCountry = CountryManager.Get(newCountryId);
                                if (newCountry != null)
                                {
                                    country.RemoveCity(city);
                                    city.SetCountry(newCountry);
                                    newCountry.AddCity(city);
                                    CountryManager.Update(country);
                                    CountryManager.Update(newCountry);
                                }  
                                else 
                                    return NotFound("New country not found");
                            }
                            else
                                CountryManager.Update(country);
                            return Ok(new TCity(city));
                        }
                        return NotFound("City not found");
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