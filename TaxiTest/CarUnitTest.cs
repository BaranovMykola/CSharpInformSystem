using System;
using System.Collections.Generic;
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
            var carMoq = new Mock<ICar>() {CallBase = true};
            carMoq.SetupGet(m => m.Category).Returns(LicenseCategory.C|LicenseCategory.B);

            var driverMoq = new Mock<IDriver>() { CallBase = true };
            driverMoq.SetupGet(m => m.DriverCategory).Returns(LicenseCategory.B);


            var vehcile = new Car();
            var driver = new Driver();

            Assert.AreEqual(driverMoq.Object.DriverCategory, LicenseCategory.B);
        }
    }
}
