using BusinessLaag.models;
using DataLaag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DataLaagTest
{
    [TestClass]
    public class CityTest
    {
        [TestMethod]
        public void CityRepoTest()
        {
            UnitOfWork uow = new UnitOfWork(new DataContext());

            //Verwijderen van alle data
            uow.cityRepo.RemoveAll();
            uow.countryRepo.RemoveAllCountries();
            uow.continentRepo.RemoveAllContinents();

            //Aanmaak Test Continent
            uow.continentRepo.Add(new Continent("Test"));

            //Vergelijking Continent
            Continent continent = uow.continentRepo.GetAllContinents()[0];
            Assert.AreEqual("Test", continent.Name);

            //Aanmaak Test Country
            uow.countryRepo.Add(new Country("TestC", continent, 11000000, 34000));

            //Toevoegen aan lijst om op te kunnen halen
            List<Country> countries = uow.countryRepo.GetAllCountries();
            Country country = uow.countryRepo.GetById(countries[0].ID);

            //Vergelijking Country
            Assert.AreEqual("TestC", country.Name);

            //Aanmaak Test City
            uow.cityRepo.Add(new City("TestCity", 250000, country, true));

            //Toevoegen aan lijst om op te kunnen halen
            List<City> cities = uow.cityRepo.GetAllCities();
            City city = uow.cityRepo.GetById(cities[0].ID);

            //Vergelijking City
            Assert.AreEqual("TestCity", city.Name);

            //City aanpassen
            city.SetPopulation(100);
            city.SetName("TestCityAangepast");

            uow.cityRepo.Update(city);

            //Aangepaste City in lijst steken
            List<City> citiesAangepast = uow.cityRepo.GetAllCities();
            City CityAangepast = uow.cityRepo.GetById(citiesAangepast[0].ID);

            //Aangepaste City vergelijken
            Assert.AreEqual(1, citiesAangepast.Count);
            Assert.AreEqual("TestCityAangepast", CityAangepast.Name);

            uow.cityRepo.RemoveAll();
            uow.countryRepo.RemoveAllCountries();
            uow.continentRepo.RemoveAllContinents();

            List<City> LegeCities = uow.cityRepo.GetAllCities();
            List<Country> LegeCountries = uow.countryRepo.GetAllCountries();
            List<Continent> LegeContinents = uow.continentRepo.GetAllContinents();

            Assert.AreEqual(0, LegeCities.Count);
            Assert.AreEqual(0, LegeCountries.Count);
            Assert.AreEqual(0, LegeContinents.Count);
        }
    }
}
