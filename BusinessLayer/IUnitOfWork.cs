using BusinessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IUnitOfWork : IDisposable
    {
        ICityRepository Cities { get; }
        IContinentRepository Continents { get; }
        ICountryRepository Countries { get; }
        IRiverRepository Rivers { get; }
        int Complete();
    }
}
