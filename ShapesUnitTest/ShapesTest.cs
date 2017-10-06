using System;
using System.Collections.Generic;
using System.IO;
using CSharpInformSystem.Shape;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShapesUnitTest
{
    [TestClass]
    public class ShapesTest
    {
        [TestMethod]
        public void Test_CircleSquare()
        {
                var i = new Circle(1, new Point(0, 0));
                Assert.AreEqual(i.ComputeSquare(), (float)Math.PI);
        }

        [TestMethod]
        public void Test_CirclePerimetr()
        {
            var i = new Circle(1, new Point(0, 0));
            Assert.AreEqual(i.ComputePerimeter(), 2* (float)Math.PI);
        }

        [TestMethod]
        public void Test_SquareArea()
        {
            var i = new Square(new Point(1,1), new Point(0, 0));
            var j = new Square(new Point(0, 0), new Point(1, 1));
            Assert.AreEqual(i.ComputeSquare(), j.ComputeSquare());
            Assert.AreEqual(i.ComputeSquare(), 1);
        }

        [TestMethod]
        public void Test_SquareCtor()
        {
            var i = new Square(new Point(1, 1), new Point(0, 0));
            var j = new Square(new Point(0, 0), new Point(1, 1));
            Assert.AreEqual(i.Pin, new Point(0,0));
            Assert.AreEqual(i.RightDownPoint, new Point(1, 1));

        }

        [TestMethod]
        public void Test_SquarePerimeter()
        {
            var i = new Square(new Point(1, 1), new Point(0, 0));
            var j = new Square(new Point(0, 0), new Point(1, 1));
            Assert.AreEqual(i.ComputePerimeter(), j.ComputePerimeter());
            Assert.AreEqual(i.ComputePerimeter(), 4);
        }

        [TestMethod]
        public void Test_PointToString()
        {
            var testPoint = new Point(3,4);
            Assert.AreEqual(testPoint.ToString(), "[3,4]");
        }

        [TestMethod]
        public void Test_CircleToString()
        {
            var testCircle = new Circle(3, new Point(3, 4));
            Assert.AreEqual(testCircle.ToString(), "Circle:\tR [3]\tCenter [[3,4]]");
        }

        [TestMethod]
        public void Test_SquareToString()
        {
            var testSquare = new Square(new Point(0,0), new Point(3, 4));
            Assert.AreEqual(testSquare.ToString(), "Square:\tLeft up point [[3,4]] Right down point[[0,0]]");
        }

        [TestMethod]
        public void Test_FileManager()
        {
            List<AbstractShape> shapes = new List<AbstractShape>()
            {
                new Circle(3, new Point(3, 4)),
                new Square(new Point(0, 0), new Point(1, 1))
            };
            var fm = new ShapeFileManager();
            fm.SaveList(shapes, "testFile.xml");
            var loaded = fm.LoadList<AbstractShape>("testFile.xml");
            Assert.AreEqual(shapes.Count, loaded.Count);

        }

        [ExpectedException(typeof(FileNotFoundException))]
        [TestMethod]
        public void Test_FileMAnagerException()
        {
            var manager = new ShapeFileManager();
            manager.LoadList<AbstractShape>("none");
        }
    }
}
