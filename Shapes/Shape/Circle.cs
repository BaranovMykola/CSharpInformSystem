using System;

namespace CSharpInformSystem.Shape
{
    /// <summary>
    /// Circle entities
    /// </summary>
    [Serializable]
    public class Circle : AbstractShape
    {
        /// <summary>
        /// Initializes a new instance of the Circle class
        /// </summary>
        public Circle()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Circle class
        /// </summary>
        /// <param name="r">Radius of circle</param>
        /// <param name="center">Center of circle</param>
        public Circle(float r, Point center) : base(center)
        {
            R = r;
        }

        /// <summary>
        /// Radius of square
        /// </summary>
        public float R { get; set; }

        /// <summary>
        /// Converts current element to string
        /// </summary>
        /// <returns>Circle written in string</returns>
        public override string ToString() => $"Circle:\tR [{R}]\tCenter [{Pin}]";

        /// <summary>
        /// Implementation of IShape method
        /// </summary>
        /// <returns>Returns square of circle</returns>
        public override float ComputeSquare() => (float)(Math.PI * Math.Pow(R, 2));

        /// <summary>
        /// Implementation of IShape method
        /// </summary>
        /// <returns>Returns perimeter of circle</returns>
        public override float ComputePerimeter() => (float)(2 * Math.PI * R);
    }
}
