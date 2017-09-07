using System;

namespace CSharpInformSystem.Shape
{
    [Serializable]
    public struct Point
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"[{X},{Y}]";
    }
}