using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using TaxiCore.Entities.Demand;
using TaxiCore.Entities.Taxi;
using TaxiCore.Entities.Transport;

namespace TaxiCore.Entities
{
    public class EntityReader
    {
        public EntityReader()
        {
        }

        public List<Car> Cars { get; set; } = new List<Car>();

        public List<Driver> Drivers { get; set; } = new List<Driver>();

        public List<Customer> Customers { get; set; } = new List<Customer>();

        public void Write(string fileName)
        {
            ArrayList allData = new ArrayList() {Cars,Drivers,Customers};
            using (var stream = File.Open(fileName,FileMode.OpenOrCreate, FileAccess.Write, FileShare.Delete))
            {
                var bin = new BinaryFormatter();
                bin.Serialize(stream,allData);
            }
        }

        public void Read(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var bin = new BinaryFormatter();
                var allData = bin.Deserialize(stream) as ArrayList;
                if (allData != null)
                {
                    Cars = allData[0] as List<Car>;
                    Drivers = allData[1] as List<Driver>;
                    Customers = allData[2] as List<Customer>;
                }
            }
        }
    }
}
