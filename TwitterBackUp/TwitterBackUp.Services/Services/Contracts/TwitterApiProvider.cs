using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TwitterBackUp.Services.Utils.Contracts;
using TwitterBackUp.DTO;

namespace TwitterBackUp.Services.Services.Contracts
{
    public class TwitterApiProvider : ITwitterApiProvider
    {
        private readonly IAppCredentials appCredentials;
        private readonly IJsonProvider jsonProvider;
        private readonly HttpMessageHandler messageHandler;

        public TwitterApiProvider(IAppCredentials appCredentials, IJsonProvider jsonProvider)
        {
            this.appCredentials = appCredentials;
            this.jsonProvider = jsonProvider;
            this.messageHandler = new HttpClientHandler();
        }

        public TwitterApiProvider(IAppCredentials appCredentials, IJsonProvider jsonProvider, HttpMessageHandler messageHandler)
        {
            this.appCredentials = appCredentials;
            this.jsonProvider = jsonProvider;
            this.messageHandler = messageHandler;
        }

        //public async Task<ICollection<TweetDto>> SearchTweetsAsync(string searchString)
        //{
        //    var tweets = new List<TweetDto>();

        //    var bearer = this.appCredentials.BearerToken;

        //    var uriString =
        //        $"https://api.twitter.com/1.1/search/tweets.json?q={searchString}&result_type=popular";

        //    using (var client = new HttpClient(this.messageHandler))
        //    {
        //        var uri = new Uri(uriString);

        //        client.DefaultRequestHeaders
        //            .Add("Authorization", "Bearer " + bearer);

        //        var response = await client.GetAsync(uri);

        //        if (response.StatusCode == HttpStatusCode.OK)
        //        {
        //            var json = this.jsonProvider.ParseToJObject(await response.Content.ReadAsStringAsync());
        //            tweets = this.jsonProvider.DeserializeObject<List<TweetDto>>(json["statuses"].ToString());
        //        }
        //    }

        //    return tweets;
        //}
        public async Task<SearchUserDto> SearchUser(string screenName)
        {
            var user = new SearchUserDto();
            var bearer = this.appCredentials.BearerToken;

            var uriString = $"https://api.twitter.com/1.1/users/show.json?screen_name={screenName}";
            using (var client = new HttpClient(this.messageHandler))
            {
                var uri = new Uri(uriString);

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearer);
                var response = await client.GetAsync(uri);
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    var json = this.jsonProvider.ParseToJObject(await response.Content.ReadAsStringAsync());
                    user = this.jsonProvider.DeserializeObject<SearchUserDto>(json.ToString());
                }
            }
            return user;
        }

        public async Task<string> GetBearerTokenAsync(string consumerKey, string consumerSecret)
        {
            var bearerResponseString = string.Empty;

            var uriString = "https://api.twitter.com/oauth2/token";

            var credentialsString = HttpUtility.UrlEncode(consumerKey)
                                   + ":" + HttpUtility.UrlEncode(consumerSecret);

            var bearerRequestString = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentialsString));

            using (var client = new HttpClient(this.messageHandler))
            {
                var uri = new Uri(uriString);

                client.DefaultRequestHeaders
                    .Add("Authorization", "Basic " + bearerRequestString);

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                });

                var response = await client.PostAsync(uri, content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var json = this.jsonProvider.ParseToJObject(jsonString);

                    bearerResponseString = json["access_token"].ToString();
                }
            }

            return bearerResponseString;
        }
    }
}