using BusinessLaag.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLaag.irepositories
{
    public interface ICountryRepo
    {
        void Add(Country country);
        void DeleteCountry(int id);
        Country GetById(int id);
        List<Country> GetAllCountries();
        void RemoveAllCountries();
        void UpdateCountry(Country country);
        List<Country> GetCountriesInContinent(Continent continent);
    }
}