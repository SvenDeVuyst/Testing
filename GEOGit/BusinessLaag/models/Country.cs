using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLaag.models
{
    public class Country
    {
        //PROPERTIES
        public int ID { get; private set; }
        public string Name { get; private set; }
        public int Population { get; private set; }
        public double Surface { get; private set; }
        public Continent Continent { get; private set; }
        public List<City> Cities { get; private set; } = new List<City>();

        //METHODS
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Put a name!");
            }
            this.Name = name;
        }

        public void SetPopulation(int population)
        {
            if (population < 0)
            {
                throw new Exception("Population must be higher then 0!");
            }
            this.Population = population;
        }

        public void SetSurface(double surface)
        {
            if (surface <= 0)
            {
                throw new Exception("Surface must be higher then 0!");
            }
            this.Surface = surface;
        }

        public void SetContinent(Continent continent)
        {
            if (continent == null)
            {
                throw new Exception("Put a continent!");
            }
            this.Continent = continent;
        }

        //CONSTRUCTORS
        public Country() { }
        public Country(string name, Continent continent, int population, double surface)
        {
            SetName(name);
            SetContinent(continent);
            SetPopulation(population);
            SetSurface(surface);
        }
    }
}
