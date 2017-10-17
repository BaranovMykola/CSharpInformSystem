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

        public static void ReadDB()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string connstring = $"Server={"localhost"};Port={"5432"};" +
                                    $"User Id={"postgres"};Password={"1111"};Database={"Taxi"};";
            // Making connection with Npgsql provider
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();
            string sql = "SELECT * FROM car";
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
                   var c = ReadCar(dRead);
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
            for (int i = 0; i < 3; i++) // magic
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
