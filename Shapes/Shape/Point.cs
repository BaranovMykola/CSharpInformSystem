using System;

namespace CSharpInformSystem.Shape
{
    /// <summary>
    /// Represents point in Cartesian coordinate system
    /// </summary>
    [Serializable]
    public struct Point
    {
        /// <summary>
        /// Initializes a new instance of the Point struct
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// x coordinate of point
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// y coordinate of point
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Finds difference between two points
        /// </summary>
        /// <param name="a">First point</param>
        /// <param name="b">Second point</param>
        /// <returns>Return type: Point, difference between points</returns>
        public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);
        
        /// <summary>
        /// Converts point to string, separates X and Y by comma
        /// </summary>
        /// <returns>Returns string</returns>
        public override string ToString() => $"[{X},{Y}]";
    }
}