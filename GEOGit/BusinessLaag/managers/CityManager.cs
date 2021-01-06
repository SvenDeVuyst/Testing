using BusinessLaag.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLaag.managers
{
    public class CityManager
    {
        public IUnitOfWork uow;
        public CityManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public void Add(City city)
        {
            try
            {
                uow.cityRepo.Add(city);
                uow.Complete();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR City Manager in the Add function (" + ex + ")");
            }
        }

        public City GetCityById(int id)
        {
            try
            {
                return uow.cityRepo.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR City Manager in the GetCityById function (" + ex + ")");
            }
        }

        public List<City> GetAllCities()
        {
            try
            {
                return uow.cityRepo.GetAllCities();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR City Manager in the GitAllCities function (" + ex + ")");
            }
        }

        public void RemoveAll()
        {
            try
            {
                uow.cityRepo.RemoveAll();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR City Manager in the RemoveAll function (" + ex + ")");
            }
        }

        public void RemoveCityById(int id)
        {
            try
            {
                uow.cityRepo.RemoveCity(id);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR City Manager in the RemoveCityById function (" + ex + ")");
            }
        }

        public void Update(City city, string name, int population, bool isCapital, Country country)
        {
            try
            {
                city.SetCountry(uow.countryRepo.GetById(country.ID));
                city.SetPopulation(population);
                city.SetName(name);
                city.IsCapital = isCapital;
                uow.Complete();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR City Manager in the Update function (" + ex + ")");
            }
        }
    }
}
