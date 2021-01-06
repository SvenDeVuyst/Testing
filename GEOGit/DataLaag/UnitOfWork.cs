using BusinessLaag;
using BusinessLaag.irepositories;
using BusinessLaag.repositories;
using DataLaag.repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLaag
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext DataContext;

        public IContinentRepo continentRepo { get; }
        public ICountryRepo countryRepo { get; }
        public ICityRepo cityRepo { get; }

        public UnitOfWork(DataContext context)
        {
            this.DataContext = context;
            countryRepo = new CountryRepo(context);
            continentRepo = new ContinentRepo(context);
            cityRepo = new CityRepo(context);
        }

        public int Complete()
        {
            return DataContext.SaveChanges();
        }

        public void Dispose()
        {
            DataContext.Dispose();
        }
    }
}
