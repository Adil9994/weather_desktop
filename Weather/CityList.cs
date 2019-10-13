using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    class CityList
    {
        public string name { get; set; }
        public int id { get; set; }
        public string country { get; set; }
        public Coords coords { get; set; }
    }
}
