using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxiCore.Entities.Position;

namespace TaxiTest
{
    [TestClass]
    public class LocationUnitTest
    {
        [TestMethod]
        public void Constructor_True()
        {
            var lattitude = 49.795253;
            var longtitude = 24.045856;
            var pos = new Location(lattitude, longtitude);
            Assert.AreEqual(pos.Lattitude, lattitude);
            Assert.AreEqual(pos.Longtitude, longtitude);
        }
    }
}
