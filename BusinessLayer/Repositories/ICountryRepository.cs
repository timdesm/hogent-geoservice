using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Repositories
{
    public interface ICountryRepository
    {
        public Country Add(Country country);
        public Country GetById(int id);
        public List<Country> GetAll();
        public void Delete(int id);
        public void DeleteAll();
        public void Update(Country country);
        public bool Exist(Country country, bool ignoreId = false);
    }
}
