using System;
using CSharpInformSystem.Shape;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShapesUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var m = new Moq.Mock<IShape>();
            //m.SetupProperty(s => s.ComputeSquare(), 228);

            IShape i = new Circle(0,new Point(0,0));

            Assert.AreEqual(i.ComputeSquare(), 0);

        }
    }
}
