using System.Net.Http;
using System.Threading.Tasks;

namespace TwitterBackUp.Services.Utils.Contracts
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage);
    }
}
