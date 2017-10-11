using System;

namespace TaxiCore.Entities.Transport
{
    [Serializable]
    [Flags]
    public enum LicenseCategory
    {
        /// <summary>
        /// Passenger car
        /// </summary>
        B = 1 << 0,

        /// <summary>
        /// Truck
        /// </summary>
        C = 1 << 1,

        /// <summary>
        /// Bus
        /// </summary>
        D = 1 << 2
    }
}
