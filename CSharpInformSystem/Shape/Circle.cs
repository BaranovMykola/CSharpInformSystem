using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpInformSystem.Shape
{
    [Serializable]
    public class Circle : AbstractShape
    {
        public float R { get; set; }

        public Circle()
        {
            
        }

        public Circle(float r, Point center): base(center)
        {
            R = r;
        }

        public override string ToString() => $"Circle:\tR [{R}]\tCenter [{Pin}]";

        public override float ComputeSquare() => (float)(Math.PI * Math.Pow(R, 2));

        public override float ComputePerimetr() => (float)(2 * Math.PI * R);
    }
}
