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
        private static string GoogleGeocodingApiKey = "AIzaSyB05tLipGmlJ7ZmMvBmJYETfQc9q7I243Y";
        private static string urlTemplate = "https://maps.googleapis.com/maps/api/distancematrix/json?";
        private static string urlAddresTemplate = "https://maps.googleapis.com/maps/api/geocode/json?";

        public static string FindDistance(Location from, Location to, Dictionary<string, string> parametrs)
        {
            if (from == null || to == null)
            {
                throw new ArgumentException("Location is null");
            }
            NumberFormatInfo info = new NumberFormatInfo();
            info.NumberDecimalSeparator = ".";
            var urlQuery = parametrs.Aggregate(urlTemplate, (current, parametr) => current + (parametr.Key + "=" + parametr.Value + "&"));
            urlQuery +=
                $"origins={from.Lattitude.ToString(info)},%20{from.Longtitude.ToString(info)}&destinations={to.Lattitude.ToString(info)},%20{to.Longtitude.ToString(info)}&";
            urlQuery += "key=" + GoogleApiKey;

            return GetUrlResponse(urlQuery);
        }

        public static Dictionary<string,int> ParseJsonDistance(string json)
        {
            Dictionary<string, int> response = new Dictionary<string, int>();
            JObject info = JObject.Parse(json);
            if (info["status"].ToString() != "OK")
            {
                throw new ArgumentException($"Response is not valid: STATUS: {info["status"]}", nameof(json));
            }
            var data = info["rows"].First;
            var elementData = data["elements"].First;
            if (elementData["status"].ToString() != "OK")
            {
                throw new ArgumentException($"Result status equls to {elementData["status"]}");
            }
            response.Add("distance", (int)elementData["distance"]["value"]);
            response.Add("duration", (int)elementData["duration"]["value"]);
            return response;
        }

        private static string GetUrlResponse(string url)
        {
            WebClient client = new WebClient();
            string downloadedString = client.DownloadString(url);
            return downloadedString;
        }

        public static string FindLocation(string addres)
        {
            string urlQuery = urlAddresTemplate;
            urlQuery += $"address={addres}&key={GoogleGeocodingApiKey}";
            return GetUrlResponse(urlQuery);
        }

        public static Location ParseJsonAddress(string json)
        {
            var jsonObject = JObject.Parse(json);
            var status = jsonObject["status"];
            if (status.ToString() != "OK")
            {
                throw new ArgumentException($"Response is not valid: STATUS: {status}", nameof(json));
            }

            var info = (jsonObject)["results"].First;
            double lattitude = double.Parse(info["geometry"]["location"]["lat"].ToString());
            double longtitude = double.Parse(info["geometry"]["location"]["lng"].ToString());
            string address = (info["formatted_address"].ToString());
            return  new Location(lattitude, longtitude, address);
        }
    }
}