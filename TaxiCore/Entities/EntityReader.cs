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
    }
}
