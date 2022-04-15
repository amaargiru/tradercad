// Read more at TraderCAD.com

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Core;

namespace ReadOnlyDataProviders
{
    // AlphavantageResponse, MetaData and TimeSeriesDaily classes created with app.quicktype.io/#l=cs&r=json2csharp
    public class AlphavantageStockResponse
    {
        [JsonProperty("Meta Data")]
        public MetaData MetaData { get; set; } = null!;

        [JsonProperty("Time Series (Daily)")]
        public Dictionary<string, TimeSeriesDaily> TimeSeriesDaily { get; set; } = null!;
    }

#pragma warning disable IDE1006 // Naming styles
    public class MetaData
    {
        [JsonProperty("1. Information")]

        public string Information { get; set; } = null!;

        [JsonProperty("2. Symbol")]
        public string Symbol { get; set; } = null!;

        [JsonProperty("3. Last Refreshed")]
        public DateTimeOffset LastRefreshed { get; set; }

        [JsonProperty("4. Output Size")]
        public string OutputSize { get; set; } = null!;

        [JsonProperty("5. Time Zone")]
        public string TimeZone { get; set; } = null!;
    }

    public class TimeSeriesDaily
    {
        [JsonProperty("1. open")]
        public string Open { get; set; } = null!;

        [JsonProperty("2. high")]
        public string High { get; set; } = null!;

        [JsonProperty("3. low")]
        public string Low { get; set; } = null!;

        [JsonProperty("4. close")]
        public string Close { get; set; } = null!;

        [JsonProperty("5. volume")]
        public string Volume { get; set; } = null!;
    }
#pragma warning restore IDE1006 // Naming styles

    class Alphavantage
    {
        public static string JsonRequest(string function, string outputsize, string datatype, string symbol, string apiKey)
        {
            string request = "https://www.alphavantage.co/query?" +
               "function=" + function +
               "&outputsize=" + outputsize +
               "&datatype=" + datatype +
               "&symbol=" + symbol +
               "&apikey=" + apiKey;

            SingleHttpClient.Init(10000);
            string responseString = Core.SingleHttpClient.GetJson(request);
            return responseString;
        }
    }
}