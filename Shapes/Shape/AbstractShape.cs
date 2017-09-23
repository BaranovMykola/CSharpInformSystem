using System;
using System.Xml.Serialization;

namespace CSharpInformSystem.Shape
{
    /// <summary>
    /// Abstract class for Circle and Square
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(Circle))]
    [XmlInclude(typeof(Square))]
    public abstract class AbstractShape : IShape
    {
        /// <summary>
        /// Initializes a new instance of the AbstractShape class
        /// </summary>
        protected AbstractShape()
        {
        }

        /// <summary>
        /// Initializes a new instance of the AbstractShape class
        /// </summary>
        /// <param name="pin">Default pin of shape</param>
        protected AbstractShape(Point pin)
        {
            Pin = pin;
        }

        /// <summary>
        /// Main point in figure(for example, center in circle)
        /// </summary>
        public Point Pin { get; set; }

        /// <summary>
        /// Calculates square of current figure
        /// </summary>
        /// <returns>float, square of figure</returns>
        public abstract float ComputeSquare();

        /// <summary>
        /// Calculates perimeter of current figure
        /// </summary>
        /// <returns>float, perimeter of figure</returns>
        public abstract float ComputePerimeter();
    }
}
