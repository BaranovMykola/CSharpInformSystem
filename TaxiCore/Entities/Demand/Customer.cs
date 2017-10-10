using System;
using TaxiCore.Entities.Position;

namespace TaxiCore.Entities.Demand
{
    [Serializable]
    public class Customer
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

        public Location CurrentLocation { get; set; }

        public Location TargetLocation { get; set; }

        public uint PeoplesCount { get; set; }

        public string Name { get; set; }
    }
}
