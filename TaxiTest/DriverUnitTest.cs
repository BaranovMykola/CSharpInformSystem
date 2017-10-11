using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxiCore.Entities.Position;
using TaxiCore.Entities.Transport;

namespace TaxiTest
{
    [TestClass]
    public class DriverUnitTest
    {
        [TestMethod]
        public void Constructor_True()
        {
            string name = "Petya";
            var category = LicenseCategory.B;

            var driver = new TaxiCore.Entities.Taxi.Driver(name,category);
            Assert.AreEqual(driver.Name, name);
            Assert.AreEqual(driver.DriverCategory, category);
        }
    }
}
