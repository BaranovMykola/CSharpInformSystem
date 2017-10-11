using System;

namespace TaxiCore.Entities.Transport
{
    [Serializable]
    public enum LicenseCategory
    {
        /// <summary>
        /// Passenger car
        /// </summary>
        B = 0,

        /// <summary>
        /// Truck
        /// </summary>
        C = 1,

        /// <summary>
        /// Bus
        /// </summary>
        D = 1 << 2
    }
}
