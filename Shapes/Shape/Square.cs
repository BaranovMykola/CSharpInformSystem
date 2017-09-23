using System;

namespace CSharpInformSystem.Shape
{
    /// <summary>
    /// Square entities
    /// </summary>
    [Serializable]
    public class Square : AbstractShape
    {
        /// <summary>
        /// Initializes a new instance of the Square class
        /// </summary>
        public Square()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Square class
        /// </summary>
        /// <param name="leftUpPoint">Left up corner point</param>
        /// <param name="rightDownPoint">Right down corner point</param>
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

        /// <summary>
        /// Right down point of the square
        /// </summary>
        public Point RightDownPoint { get; set; }

        /// <summary>
        /// Converts Square to string, representing it by left up and right down point
        /// </summary>
        /// <returns>Square converted to string</returns>
        public override string ToString() => $"Square:\tLeft up point [{Pin}] Right down point[{RightDownPoint}]";

        /// <summary>
        /// Implementation of IShape method
        /// </summary>
        /// <returns>Square of square</returns>
        public override float ComputeSquare()
        {
            Point normPoint = Pin - RightDownPoint;
            float square = Math.Abs(normPoint.X * normPoint.Y);
            return square;
        }

        /// <summary>
        /// Implementation of IShape method
        /// </summary>
        /// <returns>Perimeter of square</returns>
        public override float ComputePerimeter()
        {
            Point normPoint = Pin - RightDownPoint;
            return Math.Abs(normPoint.X * 2 + normPoint.Y * 2);
        }
    }
}