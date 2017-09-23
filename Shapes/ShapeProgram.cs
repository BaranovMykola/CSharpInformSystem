using System;
using System.Collections.Generic;
using System.Linq;
using CSharpInformSystem.Shape;

namespace CSharpInformSystem
{
    /// <summary>
    /// Entry point class
    /// </summary>
    public class ShapeProgram
    {
        /// <summary>
        /// Random generator field.
        /// </summary>
        private static readonly Random Rnd = new Random();

        /// <summary>
        /// Generates random figure(circle or square)
        /// </summary>
        /// <param name="figure">Determines type of income figure</param>
        /// <returns>Return randomly generated figure</returns>
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

        /// <summary>
        /// Randomly fills List of shapes with circles and squares
        /// </summary>
        /// <param name="count">Quantity of elements to insert</param>
        /// <returns>List of Abstract shapes</returns>
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

        /// <summary>
        /// Entry point of program, reading from file, selecting necessary elements and saving to file takes place here
        /// </summary>
        private static void Main()
        {
            var sfm = new ShapeFileManager();
            try
            {
                Console.WriteLine("Shape loading...");
                var shapes = sfm.LoadList<AbstractShape>("../../Data/RawData.xml");
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
