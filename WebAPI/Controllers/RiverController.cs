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
    [Route("api/river")]
    [ApiController]
    public class RiverController : ControllerBase
    {
        private RiverManager RiverManager { get; set; }
        private CountryManager CountryManager { get; set; }

        public RiverController()
        {
            this.RiverManager = new RiverManager(new UnitOfWork(new DataContext()));
            this.CountryManager = new CountryManager(new UnitOfWork(new DataContext()));
        }

        #region GET
        /// <summary>
        /// Get all Rivers
        /// GET: /api/river
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<TRiver> Get()
        {
            try
            {
                return this.RiverManager.GetAll().Select(x => new TRiver(x)).ToList<TRiver>();
            }
            catch
            {
                Response.StatusCode = 400;
                return null;
            }
        }

        /// <summary>
        /// Get River by Id
        /// GET: /api/river/{river}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public ActionResult<TRiver> GetRiver(int id)
        {
            try
            {
                River river = this.RiverManager.Get(id);
                if (river != null)
                {
                    return new TRiver(river);
                }
                return NotFound("River not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region POST
        /// <summary>
        /// Add River
        /// POST: /api/river
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<TRiver> Post([FromBody] TRiver r)
        {
            try
            {
                List<Country> countries = new List<Country>();
                foreach(string strCountry in r.Countries)
                {
                    if(int.TryParse(strCountry, out int countryId))
                    {
                        Country country = CountryManager.Get(countryId);
                        if (country != null)
                            countries.Add(country);
                        else
                            return BadRequest(string.Format("Provided country {0} does not exist", countryId));
                    }
                    else
                        return BadRequest("Invalid country id format in list");
                }
                River river = new River(r.Name, r.Length, countries);
                foreach (Country country in river.Countries)
                {
                    country.AddRiver(river);
                }
                    

                river = RiverManager.Add(river);
                return CreatedAtAction(nameof(GetRiver), new { id = river.Id }, river);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}