using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using TwitterBackUp.Services.Utils.Contracts;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace TwitterBackUp.Services.Utils
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient httpClient = new HttpClient();

        bool disposed = false;

        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);


        public async Task<HttpResponseMessage> GetResponseAsync(Uri uri)
        {
            var result = await this.httpClient.GetAsync(uri);
            return result;
        }
        public async Task<HttpResponseMessage> PostResponseAsync(Uri uri, HttpContent content)
        {
            var result = await this.httpClient.PostAsync(uri, content);
            return result;
        }
        public void addHeaders(string name, string value)
        {
            this.httpClient.DefaultRequestHeaders.Add(name, value);
        }

        public void Dispose()
        {

            Dispose(true);

            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();

            }


            disposed = true;
        }



    }
}
