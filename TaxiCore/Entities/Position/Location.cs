using System;

namespace TaxiCore.Entities.Position
{
    [Serializable]
    public class Location : IIndentify
    {
        public Location()
        {
        }

        public Location(double lattitude, double longtitude)
        {
            Lattitude = lattitude;
            Longtitude = longtitude;
        }

        public Location(double lattitude, double longtitude, string address)
        {
            Lattitude = lattitude;
            Longtitude = longtitude;
            Address = address;
        }

        public double Lattitude { get; set; }

        public double Longtitude { get; set; }

        public string Address { get; set; }

        public int Id { get; set; }
    }
}
