using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TaxiCore.Entities.Demand;
using TaxiCore.Entities.Position;
using TaxiCore.Entities.Taxi;
using TaxiCore.Entities.Transport;

namespace TaxiTest
{
    [TestClass]
    public class TaxiUnitTest
    {
        [TestMethod]
        public void Constructor_True()
        {
            var locationMoq = new Mock<Location>();
            Location location = new Location();
            locationMoq.Object.Lattitude = 49.795156;
            locationMoq.Object.Longtitude = 24.046242;
            locationMoq.SetupAllProperties();

            Taxi.State state = Taxi.State.Free;

            var driverMoq = new Mock<Driver>();
            driverMoq.Object.DriverCategory = LicenseCategory.B;
            driverMoq.Object.Name = "Petya";
            var driver = new Driver();

            var carMoq = new Mock<Car>();
            carMoq.Object.Category = LicenseCategory.B;
            carMoq.Object.Model = "Zhigulini";
            carMoq.Object.SeatsCouunt = 4;
            carMoq.SetupAllProperties();
            var car = new Car();

            var taxi = new Taxi(location, state, car, driver);

            Assert.AreEqual(taxi.Location.Lattitude, location.Lattitude);
            Assert.AreEqual(taxi.Location.Longtitude, location.Longtitude);
            Assert.AreEqual(taxi.Car.Category, car.Category);
            Assert.AreEqual(taxi.Car.Model, car.Model);
            Assert.AreEqual(taxi.Car.SeatsCouunt, car.SeatsCouunt);
            Assert.AreEqual(taxi.Driver.DriverCategory, driver.DriverCategory);
            Assert.AreEqual(taxi.Driver.Name, driver.Name);

            Assert.AreEqual(taxi.Location, location);
            Assert.AreEqual(taxi.Car, car);
            Assert.AreEqual(taxi.Driver, driver);
            Assert.AreEqual(taxi.CurrentState, state);
        }
    }
}

