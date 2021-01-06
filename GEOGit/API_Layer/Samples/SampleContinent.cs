using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Samples
{
    public class SampleContinent
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public List<string> Countries { get; set; } = new List<string>();
    }
}
