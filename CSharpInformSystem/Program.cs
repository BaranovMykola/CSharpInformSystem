using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CSharpInformSystem.Shape;

namespace CSharpInformSystem
{
    class Program
    {
        static readonly Random rnd = new Random();

        static AbstractShape GenerateSingleShape(Type figure)
        {
            AbstractShape randomFigure = null;
            if (figure == typeof (Circle))
            {
                randomFigure = new Circle(rnd.Next(1, 10), new Point(rnd.Next(-10, 10), rnd.Next(-10, 10)));
            }
            else if (figure == typeof (Square))
            {
                randomFigure = new Square(rnd.Next(1, 10), new Point(rnd.Next(-10, 10), rnd.Next(-10, 10)));
            }
            return randomFigure;
        }

        static List<AbstractShape> GenerateShapes(int count)
        {
            List<AbstractShape> shapes = new List<AbstractShape>();
            Type[] figuresTypes = {typeof (Circle), typeof (Square)};

            for (int i = 0; i < count; i++)
            {
                shapes.Insert(0,GenerateSingleShape(figuresTypes[rnd.Next(0,2)]));
            }
            return shapes;
        }

        static void Main(string[] args)
        {
            var sfm = new ShapeFileManager();
            var shapes = GenerateShapes(100);
            List<AbstractShape> sortedShapes = shapes.OrderBy(s => s.ComputeSquare()).ToList();

            var posSelect =
                from item in shapes
                where item.Pin.X < 0 && item.Pin.Y < 0
                select item;

            try
            {
                sfm.SaveList(sortedShapes, "../../Data/SquareSort.xml");
                sfm.SaveList(posSelect.ToList(), "../../Data/PosSelect.xml");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while writting");
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
