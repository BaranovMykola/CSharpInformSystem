using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiCore.Taxi
{
    public static class TaxiState
    {
        public enum State
        {
            /// <summary>
            /// Taxi can apply new order
            /// </summary>
            Free,

            /// <summary>
            /// Taxi cannot apply new order
            /// </summary>
            Busy
        }
    }
}
