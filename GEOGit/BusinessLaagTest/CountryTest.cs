using BusinessLaag.managers;
using BusinessLaag.models;
using DataLaag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLaagTest
{
    [TestClass]
    public class CountryTest
    {
        [TestMethod]
        public void CountryManagerTest()
        {
            CityManager cityManager = new CityManager(new UnitOfWork(new DataContext()));
            CountryManager countryManager = new CountryManager(new UnitOfWork(new DataContext()));
            ContinentManager continentManager = new ContinentManager(new UnitOfWork(new DataContext()));

            //Verwijder alles in tabels
            cityManager.RemoveAll();
            countryManager.RemoveAllCountries();
            continentManager.RemoveAll();

            //Aanmaak Continent
            continentManager.Add(new Continent("TestContinent"));
            List<Continent> continents = continentManager.GetAllContinents();
            Continent continent = continents[0];

            Assert.AreEqual(1, continents.Count);

            //Toevoegen van Country
            countryManager.Add(new Country("TestCountry", continent, 100, 10));

            List<Country> countries = countryManager.GetAllCountries();
            Country country = countries[0];

            Assert.AreEqual(1, countries.Count);
            Assert.AreEqual("TestCountry", country.Name);

            //Verwijder alles in tabels
            cityManager.RemoveAll();
            countryManager.RemoveAllCountries();
            continentManager.RemoveAll();
        }
    }
}
