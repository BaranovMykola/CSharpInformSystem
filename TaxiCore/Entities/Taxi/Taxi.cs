using System;
using System.Collections.Generic;
using TaxiCore.Entities.Position;
using TaxiCore.Entities.Transport;

namespace TaxiCore.Entities.Taxi
{
    [Serializable]
    public class Taxi
    {
        public Taxi()
        {
        }

        public Taxi(Location location, State state, Car car, Driver driver)
        {
            Location = location;
            CurrentState = state;
            Car = car;
            Driver = driver;
        }

        public Location Location { get; set; }

        public State CurrentState { get; set; }

        public Car Car { get; set; }

        public Driver Driver { get; set; }

        [Serializable]
        public enum State
        {
            /// <summary>
            /// Taxi can apply new order
            /// </summary>
            Free,

            /// <summary>
            /// Taxi cannot apply new order
            /// </summary>
            Busy
        }
    }
}
