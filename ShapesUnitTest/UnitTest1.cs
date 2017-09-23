﻿using System;
using System.Collections.Generic;
using CSharpInformSystem.Shape;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShapesUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCircleSquare()
        {
                var i = new Circle(1, new Point(0, 0));
                Assert.AreEqual(i.ComputeSquare(), (float)Math.PI);
        }

        [TestMethod]
        public void TestCirclePerimetr()
        {
            var i = new Circle(1, new Point(0, 0));
            Assert.AreEqual(i.ComputePerimetr(), 2* (float)Math.PI);
        }

        [TestMethod]
        public void TestSquareSquare()
        {
            var i = new Square(new Point(1,1), new Point(0, 0));
            var j = new Square(new Point(0, 0), new Point(1, 1));
            Assert.AreEqual(i.ComputeSquare(), j.ComputeSquare());
            Assert.AreEqual(i.ComputeSquare(), 1);
        }

        [TestMethod]
        public void TestSquarePerimetr()
        {
            var i = new Square(new Point(1, 1), new Point(0, 0));
            var j = new Square(new Point(0, 0), new Point(1, 1));
            Assert.AreEqual(i.ComputePerimetr(), j.ComputePerimetr());
            Assert.AreEqual(i.ComputePerimetr(), 4);
        }

        [TestMethod]
        public void TestPointToString()
        {
            var testPoint = new Point(3,4);
            Assert.AreEqual(testPoint.ToString(), "[3,4]");
        }

        [TestMethod]
        public void TestCircleToString()
        {
            var testCircle = new Circle(3, new Point(3, 4));
            Assert.AreEqual(testCircle.ToString(), "Circle:\tR [3]\tCenter [[3,4]]");
        }

        [TestMethod]
        public void TestSquareToString()
        {
            var testSquare = new Square(new Point(0,0), new Point(3, 4));
            Assert.AreEqual(testSquare.ToString(), "Square:\tLeft up point [[3,4]] Right down point[[0,0]]");
        }

        [TestMethod]
        public void TestFileManager()
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
    }
}
