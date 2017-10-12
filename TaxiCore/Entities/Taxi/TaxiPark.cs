using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using TaxiCore.Entities.Demand;
using TaxiCore.Entities.Transport;
using MoreLinq;
using TaxiCore.Entities.Position;

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

        private Dictionary<string, string> geoRarametrs = new Dictionary<string, string>();

        public void AddClient(Customer client)
        {
            clientsQueue.Add(client);
            FindTaxi(client);
        }

        public int FindTaxi(Customer client)
        {
            var selection =
                (from item in Taxis
                where item.CurrentState == Taxi.State.Free && item.Car.SeatsCouunt > client.PeoplesCount
                orderby item.Car.SeatsCouunt
                let info =
                    GoogleApiProcessing.ParseJsonDistance(GoogleApiProcessing.FindDistance(item.Location, client.CurrentLocation,
                        geoRarametrs))
                orderby info["duration"]
                select new {clientTaxi = item, Duration = info["duration"]}).First();

            selection.clientTaxi.CurrentState = Taxi.State.Busy;
            clientsQueue.Remove(client);
            return selection.Duration;
        }
    }
}
