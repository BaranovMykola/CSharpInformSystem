using System.Reflection;
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
            var pos = new Location();
            var target = new Location();
            uint peoplesCount = 4;
            string name = "Vasya";

            var client = new Customer(pos,target,peoplesCount,name);

            Assert.AreEqual(client.Name, name);
            Assert.AreEqual(client.PeoplesCount, peoplesCount);
            Assert.AreEqual(client.CurrentLocation,pos);
            Assert.AreEqual(client.TargetLocation, target);
        }
    }
}
