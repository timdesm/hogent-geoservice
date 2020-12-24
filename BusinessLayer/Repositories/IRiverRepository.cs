using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Repositories
{
    public interface IRiverRepository
    {
        public River Add(River river);
        public River GetById(int id);
        public List<River> GetAll();
        public void Delete(River river);
        public void DeleteAll();
        public void Update(River river);
        public bool Exist(River river);
    }
}
