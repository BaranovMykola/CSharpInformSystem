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
            //foreach (var taxi in Taxis)
            //{
            //    taxi.OnFree += Taxi_OnFree;
            //}
        }

        public List<Taxi> Taxis
        {
            get { return _taxis; }
            set
            {
                _taxis = value;
                foreach (var taxi in _taxis)
                {
                    taxi.OnFree += Taxi_OnFree;
                }
            }
        }

        protected List<Customer> clientsQueue = new EditableList<Customer>();

        private Dictionary<string, string> geoRarametrs = new Dictionary<string, string>();

        private List<Taxi> _taxis;

        public void AddClient(Customer client)
        {
            //clientsQueue.Add(client);
            FindTaxi(client);
        }

        public int FindTaxi(Customer client)
        {
            var selectionLinq =
                (from item in Taxis
                where item.CurrentState == Taxi.State.Free && item.Car.SeatsCouunt > client.PeoplesCount
                orderby item.Car.SeatsCouunt
                let info =
                    GoogleApiProcessing.ParseJsonDistance(GoogleApiProcessing.FindDistance(item.Location, client.CurrentLocation,
                        geoRarametrs))
                orderby info["duration"]
                select new {clientTaxi = item, Duration = info["duration"]});

            if (selectionLinq.Count() != 0)
            {
                var selection = selectionLinq.First();
                selection.clientTaxi.CurrentState = Taxi.State.InWay;
                clientsQueue.Remove(client);
                selection.clientTaxi.Client = client;
                selection.clientTaxi.TaxiTarget = client.CurrentLocation;
                return selection.Duration;
            }
            else
            {
                    clientsQueue.Add(client);
                return -1;
            }
        }

        public Taxi.State Taxi_OnFree(Taxi sender)
        {
            foreach (var client in clientsQueue)
            {
                if (sender.Car.SeatsCouunt > client.PeoplesCount)
                {
                    sender.CurrentState = Taxi.State.InWay;
                    clientsQueue.Remove(client);
                    sender.Client = client;
                    sender.TaxiTarget = client.CurrentLocation;
                    return Taxi.State.InWay;
                }
            }
            return Taxi.State.Free;
        }
    }
}
