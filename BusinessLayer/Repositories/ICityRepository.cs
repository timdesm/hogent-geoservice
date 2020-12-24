using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Repositories
{
    public interface ICityRepository
    {
        public City Add(City city);
        public City GetById(int id);
        public List<City> GetAll();
        public void Delete(City city);
        public void DeleteAll();
        public void Update(City city);
        public bool Exist(City city);
    }
}
