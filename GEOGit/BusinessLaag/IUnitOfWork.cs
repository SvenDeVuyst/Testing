using BusinessLaag.irepositories;
using BusinessLaag.repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLaag
{
    public interface IUnitOfWork
    {
        public int Complete();
        public void Dispose();
        public ICountryRepo countryRepo { get; }
        public IContinentRepo continentRepo { get; }
        public ICityRepo cityRepo { get; }
    }
}
