using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiCore.Position;
using TaxiCore.Transport;

namespace TaxiCore.Taxi
{
    public class Taxi
    {
        public Taxi()
        {
        }

        public Taxi(Location location, TaxiState.State state, Car car, Driver driver)
        {
            Location = location;
            State = state;
            Car = car;
            Driver = driver;
        }

        public Location Location { get; set; }

        public TaxiState.State State { get; set; }

        public Car Car { get; set; }

        public Driver Driver { get; set; }
    }
}
