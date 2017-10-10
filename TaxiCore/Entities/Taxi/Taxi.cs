﻿using System;
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