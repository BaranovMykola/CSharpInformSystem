using System;
using System.Collections.Generic;

namespace CSharpInformSystem.Shape
{
    /// <summary>
    /// Square entities
    /// </summary>
    [Serializable]
    public class Square : AbstractShape
    {
        /// <summary>
        /// Defualt consturctor
        /// </summary>
        public Square()
        {
        }

        /// <summary>
        /// Cnstructs square with user defined params
        /// </summary>
        /// <param name="side">Side lenth</param>
        /// <param name="leftUpPoint">Left up corner point</param>
        public Square(float side, Point leftUpPoint) : base(leftUpPoint)
        {
            Side = side;
        }

        public float Side { get; set; }

        public override string ToString() => $"Square:\tLeft up point [{Pin}] Side Lenght [{Side}]";

        public override float ComputeSquare() => (float)Math.Pow(Side, 2);

        public override float ComputePerimetr() => Side * 4;
    }
}