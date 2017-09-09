using System;
using System.Collections.Generic;
using System.Linq;
using CSharpInformSystem.Shape;

namespace CSharpInformSystem
{
    /// <summary>
    /// Main Class
    /// </summary>
    public class ShapeProgram
    {
        private static readonly Random Rnd = new Random();

        private static AbstractShape GenerateSingleShape(Type figure)
        {
            AbstractShape randomFigure = null;
            if (figure == typeof(Circle))
            {
                randomFigure = new Circle(Rnd.Next(1, 10), new Point(Rnd.Next(-10, 10), Rnd.Next(-10, 10)));
            }
            else if (figure == typeof(Square))
            {
                randomFigure = new Square(new Point(Rnd.Next(-10, 10), Rnd.Next(-10, 10)), new Point(Rnd.Next(-10, 10), Rnd.Next(-10, 10)));
            }

            return randomFigure;
        }

        private static List<AbstractShape> GenerateShapes(int count)
        {
            List<AbstractShape> shapes = new List<AbstractShape>();
            Type[] figuresTypes = { typeof(Circle), typeof(Square) };

            for (int i = 0; i < count; i++)
            {
                shapes.Insert(0, GenerateSingleShape(figuresTypes[Rnd.Next(0, 2)]));
            }

            return shapes;
        }

        private static void Main(string[] args)
        {
            var sfm = new ShapeFileManager();

            try
            {
                Console.WriteLine("Shape loading...");
                var shapes = sfm.LoadList<AbstractShape>("../../Data/RawData.xml");
                //sfm.SaveList(GenerateShapes(100), "../../Data/RawData.xml");

                Console.WriteLine("Shape sorting...");
                List<AbstractShape> sortedShapes = shapes.OrderBy(s => s.ComputeSquare()).ToList();
                Console.WriteLine("Shape selecting...");
                var posSelect =
                    from item in shapes
                    where item.Pin.X < 0 && item.Pin.Y < 0
                    select item;
                Console.WriteLine("Saving shapes to xml files...");
                sfm.SaveList(sortedShapes, "../../Data/SquareSort.xml");
                sfm.SaveList(posSelect.ToList(), "../../Data/PosSelect.xml");
                Console.WriteLine("Done.");
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
