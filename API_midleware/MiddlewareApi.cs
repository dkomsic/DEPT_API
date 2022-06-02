using System.Net.Http;
using System.Net.Http.Headers;

namespace Middleware_Api_Lib
{
    public class MiddlewareApi
    {
        public static HttpClient httpClient { get; set; }

        public static void InitializeClient()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
        }
    }
}
