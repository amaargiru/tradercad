// Read more at TraderCAD.com

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core
{
    // It's recommended to use a static HttpClient instance (docs.microsoft.com/en-us/azure/architecture/antipatterns/improper-instantiation/)
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

        public static string GetJson(string queryString)
        {
            // Wait for async method to complete (stackoverflow.com/questions/15149811/how-to-wait-for-async-method-to-complete)
            string responseString = Task.Run(async () => await _client.GetStringAsync(queryString)).Result;

            return responseString;
        }
    }
}