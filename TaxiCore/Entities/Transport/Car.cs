using System;
using TaxiCore.Entities.Taxi;

namespace TaxiCore.Entities.Transport
{
    [Serializable]
    public class Car
    {
        public Car()
        {
        }

        public Car(string carModel, LicenseCategory.Category carCategory, uint seatsCount)
        {
            Model = carModel;
            Category = carCategory;
            SeatsCouunt = seatsCount;
        }

        public string Model { get; set; }

        public LicenseCategory.Category Category { get; set; }

        public uint SeatsCouunt { get; set; }

        bool CanDrive(Driver driver) => driver.DriverCategory == Category;
    }
}
