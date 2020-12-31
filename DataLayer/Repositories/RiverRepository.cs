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
    public class RiverRepository : IRiverRepository
    {
        protected DataContext context;

        /// <summary> 
        /// Create River Repository with DataContext
        /// </summary>
        public RiverRepository(DataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Add a new River
        /// </summary>
        /// <param name="river"></param>
        /// <returns></returns>
        public River Add(River river)
        {
            try
            {
                this.context.Rivers.Add(river);
                return river;
            }
            catch (Exception) { throw new InsertException(); }
        }

        /// <summary>
        /// Delete River
        /// </summary>
        /// <param name="river"></param>
        public void Delete(River river)
        {
            try
            {
                this.context.Rivers.Remove(river);
            }
            catch (Exception) { throw new DeleteException(); }
        }

        /// <summary>
        /// Delete all Rivers
        /// </summary>
        public void DeleteAll()
        {
            try
            {
                this.context.Rivers.RemoveRange(context.Rivers);
            }
            catch (Exception) { throw new DeleteException(); }
        }

        /// <summary>
        /// Check if River exist
        /// </summary>
        /// <param name="river"></param>
        /// <returns></returns>
        public bool Exist(River river)
        {
            try
            {
                return this.context.Rivers.Contains(river);
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary>
        /// Get list of all Rivers  
        /// </summary>
        /// <returns></returns>
        public List<River> GetAll()
        {
            try
            {
                return this.context.Rivers
                    .Include(river => river.Countries)
                        .ThenInclude(country => country.Continent)
                            .ThenInclude(continent => continent.Countries)
                    .Include(river => river.Countries)
                        .ThenInclude(country => country.Cities)
                            .ThenInclude(city => city.Country)
                    .Include(river => river.Countries)
                        .ThenInclude(country => country.Rivers)
                            .ThenInclude(river => river.Countries)
                    .Include(river => river.Countries)
                        .ThenInclude(country => country.Capitals)
                            .ThenInclude(capital => capital.Country)
                    .ToList<River>();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary>
        /// Get river by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public River GetById(int id)
        {
            try
            {
                return this.context.Rivers
                    .Include(river => river.Countries)
                        .ThenInclude(country => country.Continent)
                            .ThenInclude(continent => continent.Countries)
                    .Include(river => river.Countries)
                        .ThenInclude(country => country.Cities)
                            .ThenInclude(city => city.Country)
                    .Include(river => river.Countries)
                        .ThenInclude(country => country.Rivers)
                            .ThenInclude(river => river.Countries)
                    .Include(river => river.Countries)
                        .ThenInclude(country => country.Capitals)
                            .ThenInclude(capital => capital.Country)
                    .Where(x => x.Id == id).SingleOrDefault();
            }
            catch (Exception) { throw new QueryException(); }
        }

        /// <summary>
        /// Update existing River
        /// </summary>
        /// <param name="river"></param>
        public void Update(River river)
        {
            try
            {
                this.context.Rivers.Update(river);
            }
            catch (Exception) { throw new UpdateException(); }
        }
    }
}
