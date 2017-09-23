using System;

namespace CSharpInformSystem.Shape
{
    [Serializable]
    public struct Point
    {
        /// <summary>
        /// Contructs point with users defined coordinates
        /// </summary>
        /// <param name="x">X cooridnate</param>
        /// <param name="y">Y coordinate</param>
        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }

        public float Y { get; set; }

        public override string ToString() => $"[{X},{Y}]";

        public static Point operator -(Point a, Point b) => new Point(a.X-b.X,a.Y-b.Y);
    }
}