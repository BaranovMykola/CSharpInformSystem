using System;

namespace TaxiCore.Entities.Taxi
{
    [Serializable]
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
