﻿using System;
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
        private static string urlTemplate = "https://maps.googleapis.com/maps/api/distancematrix/json?";

        public static string FindDistance(Location from, Location to, Dictionary<string, string> parametrs)
        {
            NumberFormatInfo info = new NumberFormatInfo();
            info.NumberDecimalSeparator = ".";
            var urlQuery = parametrs.Aggregate(urlTemplate, (current, parametr) => current + (parametr.Key + "=" + parametr.Value + "&"));
            urlQuery +=
                $"origins={from.Lattitude.ToString(info)},%20{from.Longtitude.ToString(info)}&destinations={to.Lattitude.ToString(info)},%20{to.Longtitude.ToString(info)}&";
            urlQuery += "key=" + GoogleApiKey;

            WebClient client = new WebClient();
            string downloadedString = client.DownloadString(urlQuery);
            return downloadedString;
        }

        public static Dictionary<string,int> ParseJson(string json)
        {
            Dictionary<string, int> response = new Dictionary<string, int>();
            JObject info = JObject.Parse(json);
            if (info["status"].ToString() != "OK")
            {
                throw new ArgumentException($"Response is not valid: STATUS: {info["status"]}", nameof(json));
            }
            var data = info["rows"].First;
            var elementData = data["elements"].First;
            response.Add("distance", (int)elementData["distance"]["value"]);
            response.Add("duration", (int)elementData["duration"]["value"]);
            return response;
        }
    }
}