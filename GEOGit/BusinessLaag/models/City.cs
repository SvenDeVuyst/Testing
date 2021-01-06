using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLaag.models
{
    public class City
    {
        //PROPERTIES
        public int ID { get; private set; }
        public string Name { get; private set; }
        public bool IsCapital { get; set; }
        public int Population { get; private set; }
        public Country Country { get; private set; }

        //METHODS
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Give a name!");
            }
            this.Name = name;
        }

        public void SetPopulation(int population)
        {
            if (population <= 0)
            {
                throw new Exception("Population must be higher then 0");
            }
            this.Population = population;
        }

        public void SetCountry(Country country)
        {
            if (country == null)
            {
                throw new Exception("Country is null!");
            }
            this.Country = country;
        }

        //CONSTRUCTORS
        public City() { }
        public City(string name, int population, Country country, bool isCapital)
        {
            SetName(name);
            SetPopulation(population);
            SetCountry(country);
            this.IsCapital = isCapital;
        }
    }
}
