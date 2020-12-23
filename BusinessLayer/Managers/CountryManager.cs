using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static BusinessLayer.Utils.ExceptionUtil;

namespace BusinessLayer.Managers
{
    public class CountryManager
    {
        private readonly IUnitOfWork uow;

        /// <summary> 
        /// Manage the Countries
        /// </summary>
        public CountryManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary> 
        /// Add a new Country 
        /// </summary>
        public Country Add(Country country)
        {
            if (uow.Countries.Exist(country)) throw new ExistException("country");
            try
            {
                return uow.Countries.Add(country);
            }
            catch (Exception) { throw new AddException("country"); }
        }

        /// <summary> 
        /// Get a Country by Id
        /// </summary>
        public Country Get(int id)
        {
            try
            {
                return uow.Countries.GetById(id);
            }
            catch (Exception) { throw new FetchException("country"); }
        }

        /// <summary> 
        /// Get list of all Counties 
        /// </summary>
        public List<Country> GetAll()
        {
            try
            {
                return uow.Countries.GetAll();
            }
            catch (Exception) { throw new FetchException("countries"); }
        }

        /// <summary> 
        /// Delete Country by Id
        /// </summary>
        public void Delete(int id)
        {
            try
            {
                uow.Countries.Delete(id);
            }
            catch (Exception) { throw new DeleteException("country"); }
        }

        /// <summary> 
        /// Delete all Countries 
        /// </summary>
        public void DeleteAll()
        {
            try
            {
                uow.Countries.DeleteAll();
            }
            catch (Exception) { throw new DeleteException("countries"); }
        }

        /// <summary> 
        /// Update existing Country 
        /// </summary>
        public void Update(Country country)
        {
            if (uow.Countries.Exist(country, true)) throw new ExistException("country");
            try
            {
                uow.Countries.Update(country);
            }
            catch (Exception) { throw new DeleteException("country"); }
        }

        /// <summary> 
        /// Check if Country exist
        /// </summary>
        public bool Exist(Country country, bool ignoreId = false)
        {
            return uow.Countries.Exist(country, ignoreId);
        }
    }
}
