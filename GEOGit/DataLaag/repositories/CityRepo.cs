using BusinessLaag.models;
using BusinessLaag.repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLaag.repositories
{
    public class CityRepo : ICityRepo
    {
        private DataContext DataContext;

        public CityRepo(DataContext dataContext)
        {
            try
            {
                this.DataContext = dataContext;
            }
            catch
            {
                throw new Exception("ERROR Continent Repository");
            }
        }

        public void Add(City city)
        {
            try
            {
                city.SetCountry(DataContext.DbCountry.FirstOrDefault(x => x.Name == city.Country.Name));
                DataContext.DbCity.Add(city);
                DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR City Repository in the Add function (" + ex + ")");
            }
        }

        public void RemoveCity(int id)
        {
            try
            {
                var city = GetById(id);
                DataContext.DbCity.Remove(city);
                DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR City Repository in the DeleteCity function (" + ex + ")");
            }
        }

        public City GetById(int id)
        {
            try
            {
                City city = DataContext.DbCity.Include(x => x.Country).ThenInclude(x => x.Continent).FirstOrDefault(x => x.ID == id);
                if (city == null)
                {
                    throw new Exception("City is null!");
                }
                else
                {
                    return city;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR City Repository in the GetById function (" + ex + ")");
            }
        }

        public List<City> GetAllCities()
        {
            try
            {
                return DataContext.DbCity.Include(x => x.Country).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR City Repository in the GetAllCities function (" + ex + ")");
            }
        }

        public List<City> GetAllCitiesOfCountry(Country country)
        {
            try
            {
                return DataContext.DbCity.Include(x => x.Country).ToList().FindAll(x => x.Country == country);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR City Repository in the GetAllCitiesOfCountry function (" + ex + ")");
            }
        }

        public void Update(City city)
        {
            try
            {
                var country = DataContext.DbCountry.FirstOrDefault(x => x.Name == city.Country.Name);
                var continent = DataContext.DbContinent.FirstOrDefault(x => x.Name == country.Continent.Name);
                country.SetContinent(continent);
                city.SetCountry(country);

                DataContext.DbCity.Update(city);
                DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR City Repository in the Update function (" + ex + ")");
            }
        }

        public void RemoveAll()
        {
            try
            {
                foreach (City city in GetAllCities())
                {
                    RemoveCity(city.ID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR City Repository in the RemoveAll function (" + ex + ")");
            }
        }
    }
}
