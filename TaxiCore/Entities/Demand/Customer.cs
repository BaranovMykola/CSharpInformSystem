using System;
using TaxiCore.Entities.Position;
using System.ComponentModel.DataAnnotations;

namespace TaxiCore.Entities.Demand
{
    [Serializable]
    public class Customer : IIndentify
    {
        public Customer()
        {
        }

        public Customer(Location currentLocation, Location targetLocation, int peoplesCount, string name)
        {
            CurrentLocation = currentLocation;
            TargetLocation = targetLocation;
            PeoplesCount = peoplesCount;
            Name = name;
        }

        public Customer(string currentLocation, string targetLocation, int peoplesCount, string name)
        {
            CurrentLocation = GoogleApiProcessing.ParseJsonAddress(GoogleApiProcessing.FindLocation(currentLocation));
            TargetLocation = CurrentLocation = GoogleApiProcessing.ParseJsonAddress(GoogleApiProcessing.FindLocation(targetLocation));
            PeoplesCount = peoplesCount;
            Name = name;
        }

        public Location CurrentLocation { get; set; }

        public Location TargetLocation { get; set; }

        public int PeoplesCount { get; set; }

        public string Name { get; set; }

        [Key]
        public int Id { get; set; }
    }
}
