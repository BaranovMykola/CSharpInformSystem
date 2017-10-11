using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TaxiCore.Entities.Demand;
    using TaxiCore.Entities.Position;

namespace TaxiTest
{
    [TestClass]
    public class CustomerUnitTest
    {
        [TestMethod]
        public void Constructor_True()
        {
            var locationMoq = new Mock<Location>();
            locationMoq.Object.Lattitude = 49.795156;
            locationMoq.Object.Longtitude = 24.046242;
            var pos = new Location();
            uint peoplesCount = 4;
            string name = "Vasya";

            var client = new Customer(pos,pos,peoplesCount,name);

            Assert.AreEqual(client.CurrentLocation.Longtitude,pos.Longtitude);
            Assert.AreEqual(client.CurrentLocation.Lattitude, pos.Lattitude);
            Assert.AreEqual(client.TargetLocation.Longtitude, pos.Longtitude);
            Assert.AreEqual(client.TargetLocation.Lattitude, pos.Lattitude);

            Assert.AreEqual(client.Name, name);
            Assert.AreEqual(client.PeoplesCount, peoplesCount);
        }
    }
}
