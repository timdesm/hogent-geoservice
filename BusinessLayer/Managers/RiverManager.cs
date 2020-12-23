using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static BusinessLayer.Utils.ExceptionUtil;

namespace BusinessLayer.Managers
{
    public class RiverManager
    {
        private readonly IUnitOfWork uow;

        /// <summary> 
        /// Manage the Rivers
        /// </summary>
        public RiverManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary> 
        /// Add a new River 
        /// </summary>
        public River Add(River river)
        {
            if (uow.Rivers.Exist(river)) throw new ExistException("river");
            try
            {
                return uow.Rivers.Add(river);
            }
            catch (Exception) { throw new AddException("river"); }
        }

        /// <summary> 
        /// Get a River by Id
        /// </summary>
        public River Get(int id)
        {
            try
            {
                return uow.Rivers.GetById(id);
            }
            catch (Exception) { throw new FetchException("river"); }
        }

        /// <summary> 
        /// Get list of all Rivers 
        /// </summary>
        public List<River> GetAll()
        {
            try
            {
                return uow.Rivers.GetAll();
            }
            catch (Exception) { throw new FetchException("rivers"); }
        }

        /// <summary> 
        /// Delete River by Id
        /// </summary>
        public void Delete(int id)
        {
            try
            {
                uow.Rivers.Delete(id);
            }
            catch (Exception) { throw new DeleteException("river"); }
        }

        /// <summary> 
        /// Delete all Rivers 
        /// </summary>
        public void DeleteAll()
        {
            try
            {
                uow.Rivers.DeleteAll();
            }
            catch (Exception) { throw new DeleteException("rivers"); }
        }

        /// <summary> 
        /// Update existing River 
        /// </summary>
        public void Update(River river)
        {
            if (uow.Rivers.Exist(river, true)) throw new ExistException("river");
            try
            {
                uow.Rivers.Update(river);
            }
            catch (Exception) { throw new DeleteException("river"); }
        }

        /// <summary> 
        /// Check if River exist
        /// </summary>
        public bool Exist(River river, bool ignoreId = false)
        {
            return uow.Rivers.Exist(river, ignoreId);
        }
    }
}
