using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public Bitmap Icon { get{ return new Bitmap(Image.FromFile($"{icon}"+"@2x"+".png")); } }
    }
}
