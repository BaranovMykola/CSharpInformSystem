using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaxiCore.Entities.Position
{
    public static class GoogleApiProcessing
    {
        private static string GoogleApiKey = "AIzaSyCh2nuJrUcfQTtBDcz1ExUHxHgfGu3Kcso";
        //example using: https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins=49.805823,%2023.980947&destinations=49.839067,%2024.030776&key=AIzaSyCh2nuJrUcfQTtBDcz1ExUHxHgfGu3Kcso

        public static void FindDistance(Location from, Location to)
        {
            NumberFormatInfo info = new NumberFormatInfo();
            info.NumberDecimalSeparator = ".";
            var urlQuery =
                $"https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins={from.Lattitude.ToString(info)},%20{from.Longtitude.ToString(info)}&destinations={to.Lattitude.ToString(info)},%20{to.Longtitude.ToString(info)}&key=AIzaSyCh2nuJrUcfQTtBDcz1ExUHxHgfGu3Kcso";

            WebClient client = new WebClient();
            string downloadedString = client.DownloadString(urlQuery);
            Console.WriteLine(downloadedString);
        }
    }
}
