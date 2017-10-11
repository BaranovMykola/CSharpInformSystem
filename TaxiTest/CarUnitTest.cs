using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TaxiCore.Entities.Position;
using TaxiCore.Entities.Taxi;
using TaxiCore.Entities.Transport;

namespace TaxiTest
{
    [TestClass]
    public class CarUnitTest
    {
        [TestMethod]
        public void Constructor_True()
        {
            string model = "Ferrari";
            var category = LicenseCategory.B;
            uint seats = 4;
            
            var vechile = new TaxiCore.Entities.Transport.Car(model, category, seats);
            Assert.AreEqual(vechile.Model, model);
            Assert.AreEqual(vechile.Category, category);
            Assert.AreEqual(vechile.SeatsCouunt, seats);
        }

        [TestMethod]
        public void CanDrive_True()
        {
            var driver = new Driver();
            var driverMoq = new Mock<Driver>();
            driverMoq.Object.DriverCategory = LicenseCategory.B;
            driverMoq.SetupAllProperties();

            var vechile = new Car();
            var carMoq = new Mock<Car>();
            carMoq.Object.Category = LicenseCategory.B;
            carMoq.SetupAllProperties();

            Assert.IsTrue(vechile.CanDrive(driver));
        }
    }
}
