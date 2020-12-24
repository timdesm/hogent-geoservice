using BusinessLayer.Managers;
using BusinessLayer.Models;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/continent")]
    [ApiController]
    public class ContinentController : ControllerBase
    {
        private ContinentManager ContinentManager { get; set; }

        public ContinentController()
        {
            this.ContinentManager = new ContinentManager(new UnitOfWork(new DataContext()));
        }

        #region GET
        /// <summary>
        /// Get all Continents
        /// GET: /api/continent
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<TContinent> Get()
        {
            try
            {
                return this.ContinentManager.GetAll().Select(x => new TContinent(x)).ToList<TContinent>();
            } catch
            {
                Response.StatusCode = 400;
                return null;
            }
        }

        /// <summary>
        /// Get Continent by Id
        /// GET: /api/continenet/{continent}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public ActionResult<TContinent> GetContinent(int id)
        {
            try
            {
                Continent continent = this.ContinentManager.Get(id);
                if (continent != null)
                {
                    return new TContinent(continent);
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
        /// Add Continent
        /// POST: /api/continent
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<TContinent> Post([FromBody] TContinent c)
        {
            try
            {
                Continent continent = new Continent(c.Name);
                ContinentManager.Add(continent);
                return CreatedAtAction(nameof(GetContinent), new { id = continent.Id }, continent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
          
        #region DELETE
        /// <summary>
        /// Delete Continent
        /// DELETE: /api/continenet/{continent}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                Continent continent = ContinentManager.Get(id);
                if (continent != null)
                {
                    if(continent.Countries.Count == 0)
                    {
                        ContinentManager.Delete(continent);
                        return Ok("Continent succesfully deleted");
                    }
                    return BadRequest("Conflic with a country");
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
        /// Update Continent
        /// PUT: /api/continenet/{continent}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public ActionResult<TContinent> Put(int id, [FromBody] TContinent c)
        {
            try
            {
                Continent continent = ContinentManager.Get(id);
                if (continent != null)
                {
                    continent.SetName(c.Name);
                    return Ok(new TContinent(continent));
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
