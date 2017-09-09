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
        public Square(Point leftUpPoint, Point rightDownPoint) : base(leftUpPoint)
        {
            RightDownPoint = rightDownPoint;
            if (!(Pin.X <= RightDownPoint.X && Pin.Y >= RightDownPoint.Y))
            {
                Point swap = Pin;
                Pin = RightDownPoint;
                RightDownPoint = swap;

            }
        }

        //public float Side { get; set; }

        public Point RightDownPoint { get; set; }

        public override string ToString() => $"Square:\tLeft up point [{Pin}] Right down point[{RightDownPoint}]";

        public override float ComputeSquare()
        {
            Point normPoint = Pin - RightDownPoint;
            float square = Math.Abs(normPoint.X*normPoint.Y);
            return square;
        }

        public override float ComputePerimetr()
        {
            Point normPoint = Pin - RightDownPoint;
            return normPoint.X*2 + normPoint.Y*2;
        }
    }
}