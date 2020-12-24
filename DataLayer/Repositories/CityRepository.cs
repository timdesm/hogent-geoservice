using BusinessLayer.Models;
using BusinessLayer.Repositories;
using DataLayer.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositories
{
    public class CityRepository : ICityRepository
    {
        protected DataContext context;

        /// <summary> 
        /// Create City Repository with DataContext
        /// </summary>
        public CityRepository(DataContext context)
        {
            this.context = context;
        }

        /// <summary> 
        /// Add a new City 
        /// </summary>
        public City Add(City city)
        {
            try
            {
                this.context.Cities.Add(city);
                return city;
            }
            catch (Exception) { throw new InsertException(); }
        }

        /// <summary> 
        /// Delete City
        /// </summary>
        public void Delete(City city)
        {
            try
            {
                this.context.Cities.Remove(city);
            }
            catch (Exception) { throw new DeleteException(); }
        }

        /// <summary>
        /// Delete all Cities
        /// </summary>
        public void DeleteAll()
        {
            try
            {
                this.context.Cities.RemoveRange(context.Cities);
            }
            catch (Exception) { throw new DeleteException(); }
        }

        /// <summary> 
        /// Check if City exist
        /// </summary>
        public bool Exist(City city)
        {
            try
            {
                return this.context.Cities.Contains(city);
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Get list of all Cities 
        /// </summary>
        public List<City> GetAll()
        {
            try
            {
                return this.context.Cities
                    .Include(city => city.Country)
                        .ThenInclude(country => country.Continent)
                    .Include(city => city.Country)
                        .ThenInclude(country => country.Cities)
                    .Include(city => city.Country)
                        .ThenInclude(country => country.Capitals)
                    .Include(city => city.Country)
                        .ThenInclude(country => country.Rivers)
                    .ToList<City>();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Get a City by Id
        /// </summary>
        public City GetById(int id)
        {
            try
            {
                return this.context.Cities
                    .Include(city => city.Country)
                        .ThenInclude(country => country.Continent)
                    .Include(city => city.Country)
                        .ThenInclude(country => country.Cities)
                    .Include(city => city.Country)
                        .ThenInclude(country => country.Capitals)
                    .Include(city => city.Country)
                        .ThenInclude(country => country.Rivers)
                    .Where(c => c.Id == id).SingleOrDefault();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Update existing City 
        /// </summary>
        public void Update(City city)
        {
            try
            {
                this.context.Cities.Update(city);
            }
            catch (Exception) { throw new UpdateException(); }
        }
    }
}
