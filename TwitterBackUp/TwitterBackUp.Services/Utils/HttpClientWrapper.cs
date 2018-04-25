using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using TwitterBackUp.Services.Utils.Contracts;
using System.Threading.Tasks;
namespace TwitterBackUp.Services.Utils
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient httpClient = new HttpClient();
        
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
        {
            return this.httpClient.SendAsync(requestMessage);
        }
    }
}
