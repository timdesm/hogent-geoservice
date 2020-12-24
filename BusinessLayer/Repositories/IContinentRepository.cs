using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Repositories
{
    public interface IContinentRepository
    {
        public Continent Add(Continent continent);
        public Continent GetById(int id);
        public List<Continent> GetAll();
        public void Delete(Continent continent);
        public void DeleteAll();
        public void Update(Continent continent);
        public bool Exist(Continent continent);
    }
}
