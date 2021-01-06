using BusinessLaag.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLaag.repositories
{
    public interface ICityRepo
    {
        void Add(City city);
        void RemoveCity(int id);
        City GetById(int id);
        List<City> GetAllCities();
        List<City> GetAllCitiesOfCountry(Country country);
        void Update(City city);
        void RemoveAll();
    }
}
