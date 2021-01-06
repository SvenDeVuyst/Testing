using BusinessLaag.irepositories;
using BusinessLaag.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLaag.repositories
{
    public class CountryRepo : ICountryRepo
    {
        private DataContext DataContext;

        public CountryRepo(DataContext dataContext)
        {
            try
            {
                this.DataContext = dataContext;
            }
            catch
            {
                throw new Exception("ERROR Country Repository");
            }
        }

        public void Add(Country country)
        {
            try
            {
                country.SetContinent(DataContext.DbContinent.FirstOrDefault(x => x.Name == country.Continent.Name));
                DataContext.DbCountry.Add(country);
                DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Country Repository in the Add function (" + ex + ")");
            }
        }

        public void DeleteCountry(int id)
        {
            try
            {
                Continent continent = GetById(id).Continent;
                DataContext.DbCountry.Remove(GetById(id));
                DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Country Repository in the DeleteCountry function (" + ex + ")");
            }
        }

        public Country GetById(int id)
        {
            try
            {
                Country country = DataContext.DbCountry.Include(x => x.Continent).Include(x => x.Cities).FirstOrDefault(x => x.ID == id);

                if (country == null)
                {
                    throw new Exception("No country found");
                }
                else
                {
                    return country;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Country Repository in the GetById function (" + ex + ")");
            }
        }

        public List<Country> GetAllCountries()
        {
            try
            {
                return DataContext.DbCountry.Include(x => x.Continent).Include(x => x.Cities).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Country Repository in the GetAllCountries function (" + ex + ")");
            }
        }

        public void RemoveAllCountries()
        {
            try
            {
                foreach (var country in GetAllCountries())
                {
                    DeleteCountry(country.ID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Country Repository in the RemoveAllCountries function (" + ex + ")");
            }
        }

        public void UpdateCountry(Country country)
        {
            try
            {
                DataContext.DbCountry.Update(country);
                DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Country Repository in the UpdateCountry function (" + ex + ")");
            }
        }

        public List<Country> GetCountriesInContinent(Continent continent)
        {
            try
            {
                var countries = GetAllCountries().FindAll(x => x.Continent == continent);
                return countries;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Country Repository in the GetCountriesInContinent function (" + ex + ")");
            }
        }
    }
}
