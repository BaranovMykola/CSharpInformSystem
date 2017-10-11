using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiCore.Entities.Demand;
using TaxiCore.Entities.Position;
using TaxiCore.Entities.Taxi;
using TaxiCore.Entities.Transport;

namespace TaxiCore
{
    class TaxiConsole
    {
        static void Main(string[] argv)
        {
            List<Car> cars = new List<Car>
            {
                new Car("Zhigulini", LicenseCategory.B, 5),
                new Car("Zhigulini", LicenseCategory.B, 5),
                new Car("Kamaz", LicenseCategory.C, 3),
                new Car("NeoPlan", LicenseCategory.D, 90)
            };
            List<Driver> drivers = new List<Driver>
            {
                new Driver("Petya", LicenseCategory.C),
                new Driver("Vasya", LicenseCategory.D),
                new Driver("Imanuil", LicenseCategory.C|LicenseCategory.C),
                new Driver("Valera", LicenseCategory.D)
            };

            List<Customer> customers = new List<Customer>
            {
                new Customer(new Location(49.795251, 24.046477), new Location(49.840043, 24.021903), 1, "Izya"),
                new Customer(new Location(49.828797, 24.056647), new Location(49.812421, 24.017313), 2, "Sara")
            };



            Console.ReadKey();
        }
    }
}
