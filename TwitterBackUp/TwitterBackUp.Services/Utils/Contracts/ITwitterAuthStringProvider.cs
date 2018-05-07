using System.Net.Http;

namespace TwitterBackUp.Services.Utils.Contracts
{
    public interface ITwitterAuthStringProvider
    {
        string GetAuthorizationString(HttpRequestMessage requestMessage, string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret);
    }
}