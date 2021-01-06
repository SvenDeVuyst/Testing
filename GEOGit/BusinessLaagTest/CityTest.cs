using BusinessLaag.managers;
using BusinessLaag.models;
using DataLaag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BusinessLaagTest
{
    [TestClass]
    public class CityTest
    {
        [TestMethod]
        public void CityManagerTest()
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

            //Toevoegen van City
            cityManager.Add(new City("TestCity", 5000, country, true));

            List<City> cities = cityManager.GetAllCities();
            City city = cities[0];

            Assert.AreEqual(1, cities.Count);
            Assert.AreEqual("TestCity", city.Name);

            cityManager.RemoveCityById(city.ID);

            List<City> citiesAangepast = cityManager.GetAllCities();
            Assert.AreEqual(0, citiesAangepast.Count);

            //Verwijder alles in tabels
            cityManager.RemoveAll();
            countryManager.RemoveAllCountries();
            continentManager.RemoveAll();
        }
    }
}
