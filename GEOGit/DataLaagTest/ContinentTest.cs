using BusinessLaag.models;
using DataLaag;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLaagTest
{
    [TestClass]
    public class ContinentTest
    {
        [TestMethod]
        public void ContinentRepoTest()
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

            continent.SetName("NaamAangepastCont");
            uow.continentRepo.UpdateContinent(continent);

            continent = uow.continentRepo.GetAllContinents()[0];
            Assert.AreEqual("NaamAangepastCont", continent.Name);

            //Verwijderen van Continent
            uow.continentRepo.DeleteById(continent.ID);
            List<Continent> continents = uow.continentRepo.GetAllContinents();

            Assert.AreEqual(0, continents.Count);

            //Leegmaken van tabellen
            uow.cityRepo.RemoveAll();
            uow.countryRepo.RemoveAllCountries();
            uow.continentRepo.RemoveAllContinents();
        }
    }
}
