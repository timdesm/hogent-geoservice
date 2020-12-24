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
    public class CountryRepository : ICountryRepository
    {
        protected DataContext context;

        /// <summary> 
        /// Create Country Repository with DataContext
        /// </summary>
        public CountryRepository(DataContext context)
        {
            this.context = context;
        }

        /// <summary> 
        /// Add a new Country 
        /// </summary>
        public Country Add(Country country)
        {
            try
            {
                this.context.Counties.Add(country);
                return country;
            }
            catch (Exception) { throw new InsertException(); }
        }

        /// <summary> 
        /// Delete Country
        /// </summary>
        public void Delete(Country country)
        {
            try
            {
                this.context.Counties.Remove(country);
            }
            catch (Exception) { throw new DeleteException(); }
        }

        /// <summary>
        /// Delete all Countries
        /// </summary>
        public void DeleteAll()
        {
            try
            {
                this.context.Counties.RemoveRange(context.Counties);
            }
            catch (Exception) { throw new DeleteException(); }
        }

        /// <summary> 
        /// Check if Country exist
        /// </summary>
        public bool Exist(Country country)
        {
            try
            {
                return this.context.Counties.Contains(country);
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Get list of all Countries 
        /// </summary>
        public List<Country> GetAll()
        {
            try
            {
                return this.context.Counties
                    .Include(country => country.Continent)
                        .ThenInclude(continent => continent.Countries)
                    .Include(country => country.Cities)
                        .ThenInclude(city => city.Country)
                    .Include(country => country.Capitals)
                        .ThenInclude(city => city.Country)
                    .Include(country => country.Rivers)
                        .ThenInclude(river => river.Countries)
                    .ToList<Country>();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Get an Country by Id
        /// </summary>
        public Country GetById(int id)
        {
            try
            {
                return this.context.Counties
                    .Include(country => country.Continent)
                        .ThenInclude(continent => continent.Countries)
                    .Include(country => country.Cities)
                        .ThenInclude(city => city.Country)
                    .Include(country => country.Capitals)
                        .ThenInclude(capital => capital.Country)
                    .Include(country => country.Rivers)
                        .ThenInclude(river => river.Countries)
                    .Where(c => c.Id == id).SingleOrDefault();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Update existing Country 
        /// </summary>
        public void Update(Country country)
        {
            try
            {
                this.context.Counties.Update(country);
            }
            catch (Exception) { throw new UpdateException(); }
        }
    }
}
