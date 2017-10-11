using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiCore.Entities.Transport;

namespace TaxiCore.Entities.Taxi
{
    public class TaxiPark
    {
        List<Taxi> taxis;

        public static TaxiPark MakeTaxi(List<Car> cars, List<Driver> drivers)
        {
            int[,] matrix = new int[cars.Count,drivers.Count];

            for (int i = 0; i < cars.Count; i++)
            {
                for (int j = 0; j < drivers.Count; j++)
                {
                    
                }
            }

            return new TaxiPark();
        }
    }
}
