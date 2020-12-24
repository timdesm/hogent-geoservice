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
    public class ContinentRepository : IContinentRepository
    {
        protected DataContext context;

        /// <summary> 
        /// Create Continent Repository with DataContext
        /// </summary>
        public ContinentRepository(DataContext context)
        {
            this.context = context;
        }

        /// <summary> 
        /// Add a new Continent 
        /// </summary>
        public Continent Add(Continent continent)
        {
           try
            {
                this.context.Continents.Add(continent);
                return continent;
            } catch(Exception) { throw new InsertException(); }
        }

        /// <summary> 
        /// Delete Continent
        /// </summary>
        public void Delete(Continent continent)
        {
            try
            {
                this.context.Continents.Remove(continent);
            } catch(Exception) { throw new DeleteException(); }
        }

        /// <summary>
        /// Delete all Continents
        /// </summary>
        public void DeleteAll()
        {
            try
            {
                this.context.Continents.RemoveRange(context.Continents);
            } catch (Exception) { throw new DeleteException(); }
        }

        /// <summary> 
        /// Check if Continent exist
        /// </summary>
        public bool Exist(Continent continent)
        {
            try
            {
                return this.context.Continents.Contains(continent);
            } catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Get list of all Continents 
        /// </summary>
        public List<Continent> GetAll()
        {
            try
            {
                return this.context.Continents
                    .Include(continent => continent.Countries) 
                        .ThenInclude(country => country.Cities)
                            .ThenInclude(city => city.Country)
                    .Include(continent => continent.Countries)
                        .ThenInclude(country => country.Rivers)
                            .ThenInclude(river => river.Countries)
                    .Include(continent => continent.Countries)
                        .ThenInclude(country => country.Capitals)
                            .ThenInclude(capital => capital.Country)
                    .ToList<Continent>();
            } catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Get an Continent by Id
        /// </summary>
        public Continent GetById(int id)
        {
            try
            {
                return this.context.Continents
                    .Include(continent => continent.Countries)
                        .ThenInclude(country => country.Cities)
                            .ThenInclude(city => city.Country)
                    .Include(continent => continent.Countries)
                        .ThenInclude(country => country.Rivers)
                            .ThenInclude(river => river.Countries)
                    .Include(continent => continent.Countries)
                        .ThenInclude(country => country.Capitals)
                            .ThenInclude(capital => capital.Country)
                    .Where(c => c.Id == id).SingleOrDefault();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary> 
        /// Update existing Continent 
        /// </summary>
        public void Update(Continent continent)
        {
            try
            {
                this.context.Continents.Update(continent);
            }
            catch (Exception) { throw new UpdateException(); }
        }
    }
}
