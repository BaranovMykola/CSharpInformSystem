using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiCore.Entities.Demand;
using TaxiCore.Entities.Position;
using TaxiCore.Entities.Taxi;
using TaxiCore.Entities.Transport;
using Npgsql;

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
                new Driver("Vasya", LicenseCategory.B),
                new Driver("Imanuil", LicenseCategory.C|LicenseCategory.B),
                new Driver("Valera", LicenseCategory.D)
            };

            List<Taxi> taxis = new List<Taxi>()
            {
                new Taxi(new Location(49.805823, 23.980947, "Т1416, L'viv, Lviv Oblast, 79000"), Taxi.State.Free, cars[0], drivers[1]),
                new Taxi(new Location(49.839067, 24.030776, "Halytska Square, L'viv, Lviv Oblast"), Taxi.State.Free, cars[1], drivers[2]),
                new Taxi(new Location(49.843379, 24.118381, "Hlynyanskyi Trakt St, L'viv, Lviv Oblast"), Taxi.State.Free, cars[2], drivers[0]),
                new Taxi(new Location(49.749379, 24.148790, "Н09"), Taxi.State.Free, cars[3], drivers[3])
            };

            List<Customer> customers = new List<Customer>
            {
                new Customer(new Location(49.795251, 24.046477, "Gnata Khotkevycha St, 16, L'viv, Lviv Oblast, 79000"), new Location(49.840043, 24.021903, "Universytetska St, 1, L'viv, Lviv Oblast"), 1, "Izya"),
                new Customer(new Location(49.828797, 24.056647, "Anatoliya Vakhnyanyna St, 29, L'viv, Lviv Oblast"), new Location(49.812421, 24.017313, "Luhanska St"), 2, "Sara")

                ,
                new Customer(new Location(49.795251, 24.046477, "Gnata Khotkevycha St, 16, L'viv, Lviv Oblast, 79000"), new Location(49.840043, 24.021903, "Universytetska St, 1, L'viv, Lviv Oblast"), 1, "Izya1"),
                new Customer(new Location(49.828797, 24.056647, "Anatoliya Vakhnyanyna St, 29, L'viv, Lviv Oblast"), new Location(49.812421, 24.017313, "Luhanska St"), 2, "Sara1"),
                new Customer(new Location(49.795251, 24.046477, "Gnata Khotkevycha St, 16, L'viv, Lviv Oblast, 79000"), new Location(49.840043, 24.021903, "Universytetska St, 1, L'viv, Lviv Oblast"), 1, "Izya2"),
                new Customer(new Location(49.828797, 24.056647, "Anatoliya Vakhnyanyna St, 29, L'viv, Lviv Oblast"), new Location(49.812421, 24.017313, "Luhanska St"), 2, "Sara2")
            };

            TaxiPark park = new TaxiPark(taxis);

            park.AddClient(customers[0]);
            park.AddClient(customers[1]);
            park.AddClient(customers[2]);
            park.AddClient(customers[3]);
            park.AddClient(customers[4]);

            Entities.EntityReader.EFWWrite(park);
            var p = Entities.EntityReader.EFWLoad();

            Console.WriteLine("Ended");
            Console.ReadKey();


            //park.Taxis[0].CurrentState = Taxi.State.Free;
            //var pk = Entities.EntityReader.ReadDB();
            ////pk.Taxis.First().Client= new Customer(new Location(), new Location(), 1, "baran");
            //pk.Taxis.Add(new Taxi
            //    (
            //        new Location(0,0,"from"), new Location(), new Car("car-car", LicenseCategory.B, 4),   new Driver("petro", LicenseCategory.B), new Customer(new Location(),new Location(), 4,"client" )
            //    ));
            //Entities.EntityReader.WriteDB(park);
        }
    }

    public class A
    {
        [Key]
        public int Id { get; set; }
        public int i { get; set; }
    }
}
