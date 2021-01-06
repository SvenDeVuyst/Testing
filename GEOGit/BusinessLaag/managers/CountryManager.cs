using BusinessLaag.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLaag.managers
{
    public class CountryManager
    {
        public IUnitOfWork uow;

        public CountryManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void Add(Country country)
        {
            try
            {
                var land = GetAllCountries().FirstOrDefault(x => x.Name == country.Name && x.Continent.Name == country.Continent.Name);

                if (land == null)
                {
                    uow.countryRepo.Add(country);
                }
                else
                {
                    throw new Exception("Country is already past of the continent " + land.ID + " + " + land.Name);
                }

                PlacePopulation();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Country Manager in the Add function (" + ex + ")");
            }
        }

        public void Update(Country country, string name, int population, double surface, Continent continent)
        {
            try
            {
                var Cntry = GetAllCountries().FindAll(x => x.Name == name && x.Continent == continent);
                Cntry.Remove(country);
                if (Cntry.Count == 0)
                {
                    int tmpPop = 0;
                    foreach (var city in country.Cities)
                    {
                        tmpPop += city.Population;
                    }

                    if (tmpPop <= population)
                    {
                        country.SetName(name);
                        country.SetPopulation(population);
                        country.SetSurface(surface);
                        country.SetContinent(uow.continentRepo.GetById(continent.ID));
                        uow.Complete();
                    }
                    else
                    {
                        throw new Exception("Population is to small!");
                    }
                }
                else
                {
                    throw new Exception("Country Error");
                }
                PlacePopulation();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Country Manager in the Update function (" + ex + ")");
            }
        }

        public void PlacePopulation()
        {
            foreach (var continent in uow.continentRepo.GetAllContinents())
            {
                int population = 0;
                var countries = uow.countryRepo.GetCountriesInContinent(continent);

                if (countries != null && continent != null)
                {
                    foreach (var country in countries)
                    {
                        population += country.Population;
                    }
                }
                continent.SetPopulation(population);
                uow.Complete();
            }
        }

        public List<Country> GetAllCountries()
        {
            try
            {
                return uow.countryRepo.GetAllCountries();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Country Manager in the GetAllCountries function (" + ex + ")");
            }
        }

        public Country GetCountry(int id)
        {
            try
            {
                var country = uow.countryRepo.GetById(id);
                return country;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Country Manager in the GetCountry function (" + ex + ")");
            }
        }

        public void RemoveCountry(int id)
        {
            try
            {
                uow.countryRepo.DeleteCountry(id);
                PlacePopulation();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Country Manager in the RemoveCountry function (" + ex + ")");
            }
        }

        public void RemoveAllCountries()
        {
            try
            {
                uow.countryRepo.RemoveAllCountries();
                PlacePopulation();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Country Manager in the RemoveAllCountries function (" + ex + ")");
            }
        }

        public List<Country> GetContinentWithName(Continent continent)
        {
            try
            {
                return GetAllCountries().FindAll(x => x.Continent.Name == continent.Name);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Country Manager in the GetContinentWithName function (" + ex + ")");
            }
        }
    }
}
