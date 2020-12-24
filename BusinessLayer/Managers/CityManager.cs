using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                uow.Countries.Update(city.Country);
                uow.Complete();
                return uow.Cities.GetAll().Last();
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
        public void Delete(City city)
        {
            try
            {
                uow.Cities.Delete(city);
                uow.Complete();
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
                uow.Complete();
            }
            catch (Exception) { throw new DeleteException("cities"); }
        }

        /// <summary> 
        /// Update existing City 
        /// </summary>
        public void Update(City city)
        {
            try
            {
                uow.Cities.Update(city);
                uow.Complete();
            }
            catch (Exception) { throw new DeleteException("city"); }
        }

        /// <summary> 
        /// Check if City exist
        /// </summary>
        public bool Exist(City city)
        {
            return uow.Cities.Exist(city);
        }
    }
}
