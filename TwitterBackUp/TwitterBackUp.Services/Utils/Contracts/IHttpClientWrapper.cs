using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TwitterBackUp.Services.Utils.Contracts
{
    public interface IHttpClientWrapper:IDisposable
    {

        Task<HttpResponseMessage> GetResponseAsync(Uri uri);

        Task<HttpResponseMessage> PostResponseAsync(Uri uri, HttpContent content);

        void addHeaders(string name, string value);
    }
}
