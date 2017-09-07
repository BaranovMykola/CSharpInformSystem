using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CSharpInformSystem.Shape
{
    [Serializable]
    [XmlInclude(typeof(Circle))]
    [XmlInclude(typeof(Square))]
    public abstract class AbstractShape : IShape
    {
        public Point Pin { get; set; }

        protected AbstractShape(Point pin)
        {
            Pin = pin;
        }
        protected AbstractShape()
        {
            
        }

        public abstract float ComputeSquare();
        public abstract float ComputePerimetr();
    }
}
