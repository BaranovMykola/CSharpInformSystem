using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TaxiCore.Entities.Position
{
    public static class GoogleApiProcessing
    {
        private static string GoogleApiKey = "AIzaSyCh2nuJrUcfQTtBDcz1ExUHxHgfGu3Kcso";
        //example using: https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins=49.805823,%2023.980947&destinations=49.839067,%2024.030776&key=AIzaSyCh2nuJrUcfQTtBDcz1ExUHxHgfGu3Kcso

        public static string FindDistance(Location from, Location to)
        {
            NumberFormatInfo info = new NumberFormatInfo();
            info.NumberDecimalSeparator = ".";
            var urlQuery =
                $"https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins={from.Lattitude.ToString(info)},%20{from.Longtitude.ToString(info)}&destinations={to.Lattitude.ToString(info)},%20{to.Longtitude.ToString(info)}&key=AIzaSyCh2nuJrUcfQTtBDcz1ExUHxHgfGu3Kcso";

            WebClient client = new WebClient();
            string downloadedString = client.DownloadString(urlQuery);
            return downloadedString;


        }

        public static void ParseJson(string json)
        {
            Dictionary<string, int> response = new Dictionary<string, int>();
            JObject o = JObject.Parse(json);
            var ar = o["rows"];
            foreach (var i in ar)
            {
                foreach (var j in i["elements"])
                {
                    response.Add("distance", (int)j["distance"]["value"]);
                    response.Add("duration", (int)j["duration"]["value"]);
                }
            }

            foreach (var i in response)
            {
                Console.WriteLine(i.Key);
                Console.WriteLine(i.Value);
            }
        }
    }
}
/*
 {
   "destination_addresses" : [ "Miskevycha Square, 10, L'viv, Lviv Oblast, Ukraine, 79000" ],
   "origin_addresses" : [ "Р?Р?С'Р?С?Р>С?С: Рў 1416, L'viv, Lviv Oblast, Ukraine, 79000" ],
   "rows" : [
      {
         "elements" : [
            {
               "distance" : {
                  "text" : "6.2 km",
                  "value" : 6173
               },
               "duration" : {
                  "text" : "14 mins",
                  "value" : 862
               },
               "status" : "OK"
            }
         ]
      }
   ],
   "status" : "OK"
}
*/