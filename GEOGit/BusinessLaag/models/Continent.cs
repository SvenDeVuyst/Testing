using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLaag.models
{
    public class Continent
    {
        //PROPERTIES
        public int ID { get; private set; }
        public string Name { get; private set; }
        public int Population { get; private set; }

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
            if (population < 0)
            {
                throw new Exception("Population must be higher then 0");
            }
            this.Population = population;
        }

        //CONSTRUCTORS
        public Continent() { }
        public Continent(string name)
        {
            SetName(name);
        }
    }
}
