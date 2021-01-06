using BusinessLaag.models;
using DataLaag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLaagTest
{
    [TestClass]
    public class CountryTest
    {
        [TestMethod]
        public void CountryRepoTest()
        {
            UnitOfWork uow = new UnitOfWork(new DataContext());

            //Leegmaken van tabellen
            uow.cityRepo.RemoveAll();
            uow.countryRepo.RemoveAllCountries();
            uow.continentRepo.RemoveAllContinents();

            //Aanmaak Continent
            uow.continentRepo.Add(new Continent("TestCont"));
            Continent continent = uow.continentRepo.GetAllContinents()[0];

            //Vergelijken van Continent
            Assert.AreEqual("TestCont", continent.Name);

            //Aanmaak van Country
            uow.countryRepo.Add(new Country("TestCountry", continent, 1000, 200));

            //In lijst steken
            List<Country> countries = uow.countryRepo.GetAllCountries();
            Country country = uow.countryRepo.GetById(countries[0].ID);

            Assert.AreEqual(1, countries.Count);
            Assert.AreEqual("TestCountry", country.Name);

            //Verwijderen van Country
            uow.countryRepo.DeleteCountry(country.ID);
            countries = uow.countryRepo.GetAllCountries();

            //Aantal controlleren in lijst
            Assert.AreEqual(0, countries.Count);

            //Leegmaken van tabellen
            uow.cityRepo.RemoveAll();
            uow.countryRepo.RemoveAllCountries();
            uow.continentRepo.RemoveAllContinents();
        }
    }
}
