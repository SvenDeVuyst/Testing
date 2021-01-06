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
    public class ContinentTest
    {
        [TestMethod]
        public void ContinentManagerTest()
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

            Assert.AreEqual("TestContinent", continent.Name);

            continent.SetName("TestContAangepast");
            continent.SetPopulation(100);

            continentManager.UpdateContinent(continent);

            Assert.AreEqual("TestContAangepast", continent.Name);
            Assert.AreEqual(100, continent.Population);

            continentManager.RemoveContinent(continent.ID);
            continents = continentManager.GetAllContinents();

            Assert.AreEqual(0, continents.Count);

            //Verwijder alles in tabels
            cityManager.RemoveAll();
            countryManager.RemoveAllCountries();
            continentManager.RemoveAll();
        }
    }
}
