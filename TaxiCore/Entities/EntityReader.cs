using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using TaxiCore.Entities.Demand;
using TaxiCore.Entities.Position;
using TaxiCore.Entities.Taxi;
using TaxiCore.Entities.Transport;

namespace TaxiCore.Entities
{
    public class EntityReader
    {
        public EntityReader()
        {
        }

        public TaxiPark TaxiParkEntity { get; set; } = new TaxiPark();

        public void Write(string fileName)
        {
            using (var stream = File.Open(fileName,FileMode.OpenOrCreate, FileAccess.Write, FileShare.Delete))
            {
                var bin = new BinaryFormatter();
                bin.Serialize(stream,TaxiParkEntity);
            }
        }

        public void Read(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var bin = new BinaryFormatter();
                TaxiParkEntity = bin.Deserialize(stream) as TaxiPark;
            }
        }

        public static TaxiPark ReadDB()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string connstring = $"Server={"localhost"};Port={"5432"};" +
                                    $"User Id={"postgres"};Password={"1111"};Database={"Taxi"};";
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();
            string sql = "SELECT * FROM car; SELECT * FROM location; SELECT * FROM driver;SELECT * FROM customer; SELECT * FROM taxi;select* from customer where NOT EXISTS(select client_id from taxi where client_id = customer.id)";

            //         select* from customer where NOT EXISTS(
            //select client_id from taxi where client_id = customer.id)
            NpgsqlCommand com = new NpgsqlCommand(sql, conn);
            NpgsqlDataAdapter ad = new NpgsqlDataAdapter(com);
            Console.WriteLine("Conection to server established successfuly \n");
            if (conn != null && conn.State == ConnectionState.Open)
            {
                Console.WriteLine("Connection Open");
            }
            else
            {
                throw new AccessViolationException();
            }
            ad.Fill(dt);
            NpgsqlDataReader dRead = com.ExecuteReader();
            var cars = new List<Car>();
            var locations = new List<Location>();
            var drivers = new List<Driver>();
            var clients = new List<Customer>();
            var taxis = new List<Taxi.Taxi>();
            var freeClients = new List<Customer>();
            try
            {
                Console.WriteLine("Contents of table in database: \n");
                while (dRead.Read())
                {
                   cars.Add(ReadCar(dRead));
                }
                dRead.NextResult();
                while (dRead.Read())
                {
                    locations.Add(ReadLocation(dRead));
                }
                dRead.NextResult();
                while (dRead.Read())
                {
                    drivers.Add(ReadDriver(dRead));
                }
                dRead.NextResult();
                while (dRead.Read())
                {
                    string name = dRead[1].ToString();
                    var curr_loc = locations.FirstOrDefault(s => s.Id == (int) dRead[2]);
                    var target_loc = locations.FirstOrDefault(s => s.Id == (int)dRead[3]);
                    var peop_count = (int) dRead[4];
                    var client = new Customer(curr_loc, target_loc, (uint)peop_count, name) {Id = (int)dRead[0]};
                    clients.Add(client);
                }
                dRead.NextResult();
                while (dRead.Read())
                {
                    var driver = drivers.FirstOrDefault(s => s.Id == (int)dRead[1]);
                    var car = cars.FirstOrDefault(s => s.Id == (int) dRead[2]);
                    var location = locations.FirstOrDefault(s => s.Id == (int) dRead[3]);
                    var target = (dRead[4] is DBNull) ? null : locations.FirstOrDefault(s => s.Id == (int) dRead[4]);
                    var client = (dRead[5] is DBNull) ? null : clients.FirstOrDefault(s => s.Id == (int) dRead[5]);
                    var state = (Taxi.Taxi.State) (dRead[6]);
                    var taxi = new Taxi.Taxi(location,target, car,driver,client) {Id = (int)dRead[0], CurrentState = state};
                    taxis.Add(taxi);
                }
                dRead.NextResult();
                while (dRead.Read())
                {
                    string name = dRead[1].ToString();
                    var curr_loc = locations.FirstOrDefault(s => s.Id == (int)dRead[2]);
                    var target_loc = locations.FirstOrDefault(s => s.Id == (int)dRead[3]);
                    var peop_count = (int)dRead[4];
                    var client = new Customer(curr_loc, target_loc, (uint)peop_count, name) { Id = (int)dRead[0] };
                    freeClients.Add(client);
                }
                var park = new TaxiPark(taxis);
                foreach (var i in freeClients)
                {
                    park.AddClient(i);
                }
                return park;
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
            return null;
        }

        public static void WriteDB(TaxiPark park)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string connstring = $"Server={"localhost"};Port={"5432"};" +
                                    $"User Id={"postgres"};Password={"1111"};Database={"Taxi"};";
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();

            foreach (var taxi in park.Taxis)
            {
                string addr;
                string update;
                NpgsqlCommand com;
                int aff;
                WriteLocation(conn, taxi.Location);
                if (taxi.Client != null)
                {
                    WriteLocation(conn, taxi.Client.CurrentLocation);

                    WriteLocation(conn, taxi.Client.TargetLocation);
                }

                if (taxi.TaxiTarget != null)
                {
                    WriteLocation(conn, taxi.TaxiTarget);
                }


                update =
                    $"UPDATE car set category = {(int)taxi.Car.Category}, model = '{taxi.Car.Model}', seats_count = {taxi.Car.SeatsCouunt} where id = {taxi.Car.Id}";
                com = new NpgsqlCommand(update,conn);
                aff = com.ExecuteNonQuery();
                if (aff == 0)
                {
                    update =
                    $"INSERT INTO car (category,model,seats_count) values ({(int)taxi.Car.Category}, '{taxi.Car.Model}', {taxi.Car.SeatsCouunt}) RETURNING id";
                    com = new NpgsqlCommand(update, conn);
                    taxi.Car.Id = (int)com.ExecuteScalar();
                }
                update =
                    $"UPDATE driver set driver_category = {(int)taxi.Driver.DriverCategory}, name = '{taxi.Driver.Name}' where id = {taxi.Driver.Id}";
                com = new NpgsqlCommand(update, conn);
                aff = com.ExecuteNonQuery();
                if (aff == 0)
                {
                    update =
                        $"INSERT INTO driver (driver_category,name) values ({(int)taxi.Driver.DriverCategory}, '{taxi.Driver.Name}') RETURNING id";
                    com = new NpgsqlCommand(update, conn);
                    taxi.Driver.Id = (int)com.ExecuteScalar();
                }
                if (taxi.Client != null)
                {
                    WriteCustomer(taxi.Client,conn);
                }
                var tgid = taxi.TaxiTarget?.Id;
                var tgidstr = tgid?.ToString() ?? "";
                update =
                    $"UPDATE taxi set driver_id = {taxi.Driver.Id}, car_id = {taxi.Car.Id}, location_id = {taxi.Location.Id}, taxi_target_id = {taxi.TaxiTarget?.Id.ToString() ?? "null"}, client_id = {taxi.Client?.Id.ToString() ?? "null"}, current_state = {(int) taxi.CurrentState} where id = {taxi.Id}";
                com = new NpgsqlCommand(update, conn);
                aff = com.ExecuteNonQuery();
                if (aff == 0)
                {
                    update =
                        $"INSERT INTO taxi (driver_id,car_id,location_id,taxi_target_id,client_id,current_state) values ({taxi.Driver.Id},{taxi.Car.Id},{taxi.Location.Id},{taxi.TaxiTarget?.Id.ToString() ?? "null"},{taxi.Client?.Id.ToString() ?? "null"},{(int)taxi.CurrentState}) RETURNING id";
                    com = new NpgsqlCommand(update, conn);
                    taxi.Id = (int)com.ExecuteScalar();
                }
            }

            foreach (var customer in park.clientsQueue)
            {
                WriteLocation(conn, customer.CurrentLocation);
                WriteLocation(conn, customer.TargetLocation);
                WriteCustomer(customer, conn);
            }

        }

        private static void WriteCustomer(Customer customer, NpgsqlConnection conn)
        {
            var update =
                $"UPDATE customer set name = '{customer.Name}', current_location_id = {customer?.CurrentLocation.Id}, target_location_id = {customer.TargetLocation.Id}, peoples_count = {customer.PeoplesCount} where id = {customer.Id}";
            var com = new NpgsqlCommand(update, conn);
            var aff = com.ExecuteNonQuery();
            if (aff == 0)
            {
                update =
                    $"INSERT INTO customer (name,current_location_id, target_location_id,peoples_count) values ('{customer.Name}', {customer?.CurrentLocation.Id}, {customer.TargetLocation.Id}, {customer.PeoplesCount}) RETURNING id";
                com = new NpgsqlCommand(update, conn);
                customer.Id = (int) com.ExecuteScalar();
            }
        }

        private static void WriteLocation(NpgsqlConnection conn, Location loc)
        {
            Taxi.Taxi taxi;
            var addr = Convert.ToBase64String(Encoding.UTF8.GetBytes(loc.Address ?? ""));
            string update =
                $"UPDATE location set lat = {loc.Lattitude}, lng = {loc.Longtitude}, address = '{addr}' where id = {loc.Id};";
            NpgsqlCommand com = new NpgsqlCommand(update, conn);
            int aff = com.ExecuteNonQuery();
            if (aff == 0)
            {
                update =
                    $"INSERT INTO location (lat, lng, address) values ({loc.Lattitude}, {loc.Longtitude}, '{addr}') RETURNING id";
                com = new NpgsqlCommand(update, conn);
                loc.Id = (int)com.ExecuteScalar();
            }
        }

        private static Location ReadLocation(NpgsqlDataReader dRead)
        {
            var address = Encoding.UTF8.GetString(Convert.FromBase64String(dRead[3].ToString()));
            return new Location((double)dRead[1], (double)dRead[2], address) { Id = (int)dRead[0] };
        }

        private static Driver ReadDriver(NpgsqlDataReader dRead)
        {
            string name = dRead[1].ToString();
            int cat = (int) dRead[2];
            LicenseCategory license = 0;
            for (int i = 0; i < 3; i++) // magic
            {
                if ((cat & 1) == 1)
                {
                    license |= (LicenseCategory)(1 << i);
                }
                cat = cat >> 1;
            }
            return  new Driver(name, license) { Id = (int)dRead[0] };
        }

        private static Car ReadCar(NpgsqlDataReader dRead)
        {
            int cat = (int) dRead[1];
            LicenseCategory license = 0;
            for (int i = 0; i < 3; i++) // black magic
            { 
                if ((cat & 1) == 1)
                {
                    license = (LicenseCategory)(1 << i);
                    break;
                }
                cat = cat >> 1;
            }
            string model = dRead[2].ToString();
            int seatsCount = (int) dRead[3];
            return  new Car(model, license, (uint)seatsCount) {Id = (int)dRead[0]};
        }
    }
}
