namespace DataConnectors;

public class SingleHttpClient
{
    private static HttpClient _client = null!;

    public static void Init(int timeoutMs)
    {
        _client = new HttpClient
        {
            Timeout = TimeSpan.FromMilliseconds(timeoutMs)
        };
    }

    public static string GetJson(string queryString) => Task.Run(async () => await _client.GetStringAsync(queryString)).Result;
}
