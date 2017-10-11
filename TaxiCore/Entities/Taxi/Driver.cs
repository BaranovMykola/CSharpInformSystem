﻿using TaxiCore.Entities.Transport;

namespace TaxiCore.Entities.Taxi
{
    public class Driver
    {
        public Driver()
        {
        }

        public Driver(string name, LicenseCategory driverCategory)
        {
            Name = name;
            DriverCategory = driverCategory;
        }

        public string Name { get; set; }

        public LicenseCategory DriverCategory { get; set; }
    }
}
