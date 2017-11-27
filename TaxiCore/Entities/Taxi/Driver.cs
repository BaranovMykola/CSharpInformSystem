using System;
using TaxiCore.Entities.Transport;
using System.ComponentModel.DataAnnotations;

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

        [Key]
        public int Id { get; set; }
    }
}
