using System;

namespace TaxiCore.Entities.Transport
{
    [Serializable]
    public static class LicenseCategory
    {
        public enum Category
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
}
