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
            Location location = new Location();
            Taxi.State state = Taxi.State.Free;
            var driver = new Driver();
            var car = new Car();

            var taxi = new Taxi(location, state, car, driver);

            Assert.AreEqual(taxi.Location, location);
            Assert.AreEqual(taxi.Car, car);
            Assert.AreEqual(taxi.Driver, driver);
            Assert.AreEqual(taxi.CurrentState, state);
        }
    }
}

