using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text;

namespace HttpNetConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // var task = Task.Run(FuncRequest.GetDataSimpleAsync);
            // Task.WaitAll(task);
            // Console.ReadLine();

            // FuncRequest.UriSample("https://www.163.com");

            // FuncRequest.GetUrl();
        }
    }

    class FuncRequest
    {
        private const string NorthWindUrl =
            "http://www.baidu.com";

        private const int ReadBufferSize = 1024;

        public static async Task<string> RequestHtmlAsync(string hostname)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    await client.ConnectAsync(hostname, 80);
                    NetworkStream stream = client.GetStream();
                    //etc.
                    string header = "GET / HTTP/1.1\r\n" +
                                    $"Host: {hostname}:80\r\n" +
                                    "Connection: close\r\n" +
                                    "\r\n";
                    byte[] buffer = Encoding.UTF8.GetBytes(header);
                    await stream.WriteAsync(buffer, 0, buffer.Length);
                    await stream.FlushAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return NorthWindUrl;
        }

        public static void UriSample(string url)
        {
            var page = new Uri(url);
            Console.WriteLine(page.Scheme);
        }

        public static void GetUrl()
        {
            var uriBuilder = new UriBuilder();
            uriBuilder.Scheme = "https";
            uriBuilder.Host = "www.fucker.com";
            uriBuilder.Path = "/locker/func";
            uriBuilder.Query = "name=liy";
            var uriBuilderUri = uriBuilder.Uri;
            Console.WriteLine(uriBuilderUri.ToString());
        }


        public static async Task GetDataSimpleAsync()
        {
            using (var client = new HttpClient(new SimpleHttpMessageHandler()))
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("Auth","application/json");
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, NorthWindUrl);
                HttpResponseMessage response = await client.SendAsync(httpRequestMessage);
                // HttpResponseMessage response = await client.GetAsync(NorthWindUrl);
                Console.WriteLine(response.Version.ToString());
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Response Status Code: {(int)response.StatusCode} " +
                              $"{response.ReasonPhrase}");
                    string responseBodyAsText = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                    Console.WriteLine();
                    Console.WriteLine(responseBodyAsText);
                }
                else
                {
                    Console.WriteLine(
                        $"Failure Request: {response.StatusCode}, {response.Content.ReadAsStringAsync()}");
                }

                foreach (var httpResponseHeader in response.Headers)
                {
                    Console.WriteLine($"---  {httpResponseHeader.Key} : {string.Join(",",httpResponseHeader.Value)}");
                }
            }
        }
    }
}