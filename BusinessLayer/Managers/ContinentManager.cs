using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static BusinessLayer.Utils.ExceptionUtil;

namespace BusinessLayer.Managers
{
    public class ContinentManager
    {
        private readonly IUnitOfWork uow;

        /// <summary> 
        /// Manage the Continents
        /// </summary>
        public ContinentManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary> 
        /// Add a new Continent 
        /// </summary>
        public Continent Add(Continent continent)
        {
            if (uow.Continents.Exist(continent)) throw new ExistException("continent");
            try
            {
                continent = uow.Continents.Add(continent);
                uow.Complete();
                return continent;
            }
            catch (Exception) { throw new AddException("continent"); }
        }

        /// <summary> 
        /// Get a Continent by Id
        /// </summary>
        public Continent Get(int id)
        {
            try
            {
                return uow.Continents.GetById(id);
            }
            catch (Exception) { throw new FetchException("city"); }
        }

        /// <summary> 
        /// Get list of all Continents 
        /// </summary>
        public List<Continent> GetAll()
        {
            try
            {
                return uow.Continents.GetAll();
            }
            catch (Exception) { throw new FetchException("continents"); }
        }

        /// <summary> 
        /// Delete Continent by Id
        /// </summary>
        public void Delete(Continent continent)
        {
            try
            {
                if(continent.Countries.Count != 0) throw new DeleteException("continent");
                uow.Continents.Delete(continent);
                uow.Complete();
            }
            catch (Exception) { throw new DeleteException("continent"); }
        }

        /// <summary> 
        /// Delete all Continents 
        /// </summary>
        public void DeleteAll()
        {
            try
            {
                uow.Continents.DeleteAll();
                uow.Complete();
            }
            catch (Exception) { throw new DeleteException("continents"); }
        }

        /// <summary> 
        /// Update existing Continent 
        /// </summary>
        public void Update(Continent continent)
        {
            try
            {
                uow.Continents.Update(continent);
                uow.Complete();
            }
            catch (Exception) { throw new UpdateException("continent"); }
        }

        /// <summary> 
        /// Check if Continent exist
        /// </summary>
        public bool Exist(Continent continent)
        {
            return uow.Continents.Exist(continent);
        }
    }
}
