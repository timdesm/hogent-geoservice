using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static BusinessLayer.Utils.ExceptionUtil;

namespace BusinessLayer.Managers
{
    public class CityManager
    {
        private readonly IUnitOfWork uow;

        /// <summary> 
        /// Manage the Cities
        /// </summary>
        public CityManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary> 
        /// Add a new City 
        /// </summary>
        public City Add(City city)
        {
            if (uow.Cities.Exist(city)) throw new ExistException("city");
            try
            {
                return uow.Cities.Add(city);
            }
            catch(Exception) { throw new AddException("city"); }
        }

        /// <summary> 
        /// Get a City by Id
        /// </summary>
        public City Get(int id)
        {
            try
            {
                return uow.Cities.GetById(id);
            }
            catch(Exception) { throw new FetchException("city"); }
        }

        /// <summary> 
        /// Get list of all Cities 
        /// </summary>
        public List<City> GetAll()
        {
            try
            {
                return uow.Cities.GetAll();
            }
            catch (Exception) { throw new FetchException("cities"); }
        }

        /// <summary> 
        /// Delete City by Id
        /// </summary>
        public void Delete(int id)
        {
            try
            {
                uow.Cities.Delete(id);
            }
            catch (Exception) { throw new DeleteException("city"); }
        }

        /// <summary> 
        /// Delete all Cities 
        /// </summary>
        public void DeleteAll()
        {
            try
            {
                uow.Cities.DeleteAll();
            }
            catch (Exception) { throw new DeleteException("cities"); }
        }

        /// <summary> 
        /// Update existing City 
        /// </summary>
        public void Update(City city)
        {
            if(uow.Cities.Exist(city, true)) throw new ExistException("city");
            try
            {
                uow.Cities.Update(city);
            }
            catch (Exception) { throw new DeleteException("city"); }
        }

        /// <summary> 
        /// Check if City exist
        /// </summary>
        public bool Exist(City city, bool ignoreId = false)
        {
            return uow.Cities.Exist(city, ignoreId);
        }
    }
}
