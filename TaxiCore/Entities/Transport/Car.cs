using System;
using TaxiCore.Entities.Taxi;

namespace TaxiCore.Entities.Transport
{
    [Serializable]
    public class Car : ICar, IIndentify
    {
        public Car()
        {
        }

        public Car(string carModel, LicenseCategory carCategory, int seatsCount)
        {
            Model = carModel;
            Category = carCategory;
            SeatsCouunt = seatsCount;
        }

        public string Model { get; set; }

        public LicenseCategory Category { get; set; }

        public int SeatsCouunt { get; set; }

        public bool CanDrive(Driver driver) => (driver.DriverCategory & Category) != 0;

        public int Id { get; set; }
    }
}
