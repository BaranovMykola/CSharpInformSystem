using System;
using System.Collections.Generic;

namespace CSharpInformSystem.Shape
{
    [Serializable]
    public class Square : AbstractShape
    {
        public float Side { get; set; }

        public Square()
        {
            
        }

        public Square(float side, Point leftUpPoint): base(leftUpPoint)
        {
            Side = side;
        }

        public override string ToString() => $"Square:\tLeft up point [{Pin}] Side Lenght [{Side}]";

        public override float ComputeSquare() => (float) Math.Pow(Side, 2);

        public override float ComputePerimetr() => Side*4;
    }
}