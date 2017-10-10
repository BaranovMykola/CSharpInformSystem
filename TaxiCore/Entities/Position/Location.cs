using System;

namespace TaxiCore.Entities.Position
{
    [Serializable]
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
