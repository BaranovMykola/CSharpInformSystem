using System;
using TaxiCore.Entities.Transport;

namespace TaxiCore.Entities.Taxi
{
    [Serializable]
    public class Driver : IDriver, IIndentify
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

        public int Id { get; set; }
    }
}
