using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpInformSystem.Shape
{
    /// <summary>
    /// Circle entities
    /// </summary>
    [Serializable]
    public class Circle : AbstractShape
    {
        /// <summary>
        /// Default circle
        /// </summary>
        public Circle()
        {
        }

        /// <summary>
        /// Creates circle with user params
        /// </summary>
        /// <param name="r">Radius</param>
        /// <param name="center">Center of circle</param>
        public Circle(float r, Point center) : base(center)
        {
            R = r;
        }

        public float R { get; set; }

        public override string ToString() => $"Circle:\tR [{R}]\tCenter [{Pin}]";

        public override float ComputeSquare() => (float)(Math.PI * Math.Pow(R, 2));

        public override float ComputePerimetr() => (float)(2 * Math.PI * R);
    }
}
