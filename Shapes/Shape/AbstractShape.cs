using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// Default constructor
        /// </summary>
        protected AbstractShape()
        {
        }

        /// <summary>
        /// Constructor for pin
        /// </summary>
        /// <param name="pin">Default pin of shape</param>
        protected AbstractShape(Point pin)
        {
            Pin = pin;
        }

        public Point Pin { get; set; }

        public abstract float ComputeSquare();

        public abstract float ComputePerimetr();
    }
}
