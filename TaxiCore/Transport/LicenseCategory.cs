using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiCore.Transport
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
