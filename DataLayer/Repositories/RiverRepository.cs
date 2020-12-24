using BusinessLayer.Models;
using BusinessLayer.Repositories;
using System;
using System.Collections.Generic;
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

        public River Add(River river)
        {
            throw new NotImplementedException();
        }

        public void Delete(River river)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public bool Exist(River river)
        {
            throw new NotImplementedException();
        }

        public List<River> GetAll()
        {
            throw new NotImplementedException();
        }

        public River GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(River river)
        {
            throw new NotImplementedException();
        }
    }
}
