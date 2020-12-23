using BusinessLayer;
using BusinessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        protected DataContext context;

        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        public ICityRepository Cities { get; private set; }
        public IContinentRepository Continents { get; private set; }
        public ICountryRepository Countries { get; private set; }
        public IRiverRepository Rivers { get; private set; }

        public int Complete()
        {
            try
            {
                return context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
