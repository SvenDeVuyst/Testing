using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Samples
{
    public class SampleCountry
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public double Surface { get; set; }
        public string Continent { get; set; }
        public List<string> Cities { get; set; } = new List<string>();
    }
}
