using System;
using TaxiCore.Entities.Position;

namespace TaxiCore.Entities.Demand
{
    [Serializable]
    public class Customer : IIndentify
    {
        public Customer()
        {
        }

        public Customer(Location currentLocation, Location targetLocation, uint peoplesCount, string name)
        {
            CurrentLocation = currentLocation;
            TargetLocation = targetLocation;
            PeoplesCount = peoplesCount;
            Name = name;
        }

        public Customer(string currentLocation, string targetLocation, uint peoplesCount, string name)
        {
            CurrentLocation = GoogleApiProcessing.ParseJsonAddress(GoogleApiProcessing.FindLocation(currentLocation));
            TargetLocation = CurrentLocation = GoogleApiProcessing.ParseJsonAddress(GoogleApiProcessing.FindLocation(targetLocation));
            PeoplesCount = peoplesCount;
            Name = name;
        }

        public Location CurrentLocation { get; set; }

        public Location TargetLocation { get; set; }

        public uint PeoplesCount { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }
    }
}
