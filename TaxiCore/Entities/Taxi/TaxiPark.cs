using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using TaxiCore.Entities.Demand;
using TaxiCore.Entities.Transport;
using MoreLinq;

namespace TaxiCore.Entities.Taxi
{
    [Serializable]
    public class TaxiPark
    {
        public TaxiPark()
        {
        }

        public TaxiPark(List<Taxi> taxis)
        {
            Taxis = taxis;
        }

        public List<Taxi> Taxis{ get; set; }

        private List<Customer> clientsQueue = new EditableList<Customer>();

        public void AddClient(Customer client)
        {
            clientsQueue.Add(client);
            FindTaxi(client);
        }

        public void FindTaxi(Customer client)
        {
            var avalibleTaxis = new List<Taxi>();
            foreach (var taxi in Taxis)
            {
                if (taxi.CurrentState == Taxi.State.Free && taxi.Car.SeatsCouunt >= client.PeoplesCount)
                {
                    avalibleTaxis.Add(taxi);
                }
            }

            var clientTaxi = avalibleTaxis.MinBy(s => s.Car.SeatsCouunt);
            clientTaxi.CurrentState = Taxi.State.Busy;
            clientsQueue.Remove(client);
        }
    }
}
