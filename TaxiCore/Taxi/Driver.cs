using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiCore.Transport;

namespace TaxiCore.Taxi
{
    public class Driver
    {
        public Driver()
        {
        }

        public Driver(string name, LicenseCategory.Category driverCategory)
        {
            Name = name;
            DriverCategory = driverCategory;
        }

        public string Name { get; set; }

        public LicenseCategory.Category DriverCategory { get; set; }
    }
}
