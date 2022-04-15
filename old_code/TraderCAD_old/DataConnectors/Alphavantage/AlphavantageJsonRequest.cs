namespace DataConnectors;

public class AlphavantageJsonRequest
{
    public static string GetJson(string function, string outputsize, string datatype, string symbol, string apiKey)
    {
        string request = "https://www.alphavantage.co/query?" +
           $"function={function}" +
           $"&outputsize={outputsize}" +
           $"&datatype={datatype}" +
           $"&symbol={symbol}" +
           $"&apikey={apiKey}";

        SingleHttpClient.Init(10000);
        string responseString = SingleHttpClient.GetJson(request);
        return responseString;
    }
}
