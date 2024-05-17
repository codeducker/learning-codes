using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpNetConsoleApp
{
    internal class SimpleHttpMessageHandler : HttpClientHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Console.WriteLine(request.Method == HttpMethod.Put);
            if (request.Method == HttpMethod.Put)
            {
                var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return Task.FromResult(httpResponseMessage);
            }
            return base.SendAsync(request,cancellationToken);
        }
    }
}
