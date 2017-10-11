using System;

namespace TaxiCore.Entities.Transport
{
    [Serializable]
    public enum LicenseCategory
    {
        /// <summary>
        /// Passanger car
        /// </summary>
        B,

        /// <summary>
        /// Truck
        /// </summary>
        C,

        /// <summary>
        /// Bus
        /// </summary>
        D
    }
}
