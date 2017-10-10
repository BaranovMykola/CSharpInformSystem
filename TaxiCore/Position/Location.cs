using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiCore.Position
{
    public class Location
    {
        public Location()
        {
        }

        public Location(double lattitude, double longtitude)
        {
            Lattitude = lattitude;
            Longtitude = longtitude;
        }

        public double Lattitude { get; set; }

        public double Longtitude { get; set; }
    }
}
