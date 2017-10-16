using System;
using System.Collections.Generic;
using System.Data;
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
                new Taxi(new Location(49.805823, 23.980947), Taxi.State.Free, cars[0], drivers[1]),
                new Taxi(new Location(49.839067, 24.030776), Taxi.State.Free, cars[1], drivers[2]),
                new Taxi(new Location(49.843379, 24.118381), Taxi.State.Free, cars[2], drivers[0]),
                new Taxi(new Location(49.749379, 24.148790), Taxi.State.Free, cars[3], drivers[3])
            };

            List<Customer> customers = new List<Customer>
            {
                new Customer(new Location(49.795251, 24.046477), new Location(49.840043, 24.021903), 1, "Izya"),
                new Customer(new Location(49.828797, 24.056647), new Location(49.812421, 24.017313), 2, "Sara")
            };

            TaxiPark park = new TaxiPark(taxis);

            //park.AddClient(customers[0]);
            //park.AddClient(customers[0]);
            //park.AddClient(customers[0]);
            //park.AddClient(customers[0]);
            //park.AddClient(customers[0]);

            //park.Taxis[0].CurrentState = Taxi.State.Free;
                   DataSet ds = new DataSet();
                   DataTable dt = new DataTable();

        string connstring = $"Server={"localhost"};Port={"5432"};" +
                                $"User Id={"postgres"};Password={"1111"};Database={"EventsSite"};";
            // Making connection with Npgsql provider
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();
            string sql = "SELECT * FROM ticket";
            NpgsqlCommand com = new NpgsqlCommand(sql, conn);
            NpgsqlDataAdapter ad = new NpgsqlDataAdapter(com);
            // Execute the query and obtain the value of the first column of the first row
            Console.WriteLine("Conection to server established successfuly \n");
            // check if connection is open or not
            if (conn != null && conn.State == ConnectionState.Open)
            {
                Console.WriteLine("Connection Open");
                //conn.Close();
            }
            else
            {
                conn.Open();
            }

            // Fill data table with data and start reading
            ad.Fill(dt);
            NpgsqlDataReader dRead = com.ExecuteReader();

            try
            {
                Console.WriteLine("Contents of table in database: \n");
                while (dRead.Read())
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Console.Write("{0} \t \n", row[i].ToString());
                        }
                    }
                }
            }
            catch (NpgsqlException ne)
            {
                Console.WriteLine("Problem connecting to server, Error details {0}", ne.ToString());
            }
            finally
            {
                Console.WriteLine("Closing connections");
                dRead.Close();
                dRead = null;
                conn.Close();
                conn = null;
                com.Dispose();
                com = null;
            }
            // connect grid to DataTable
            // since we only showing the result we don't need connection anymore
            Console.ReadKey();
        }
    }
}
