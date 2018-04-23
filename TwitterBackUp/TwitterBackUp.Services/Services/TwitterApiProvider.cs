using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TwitterBackUp.Services.Utils.Contracts;
using TwitterBackUp.DTO;
using TwitterBackUp.DTO.TweetDtos;
using TwitterBackUp.Services.Services.Contracts;
using TwitterBackUp.Services.Utils.Contracts;

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

        public async Task<ICollection<TweetDto>> GetUserTimeLine(string userId, int tweetsCount)
        {
            var tweets = new List<TweetDto>();

            var bearer = this.appCredentials.BearerToken;

            var uriString =
                $"https://api.twitter.com/1.1/statuses/user_timeline.json?user_id={userId}&count={tweetsCount}";

            using (var client = new HttpClient(this.messageHandler))
            {
                var uri = new Uri(uriString);

                client.DefaultRequestHeaders
                    .Add("Authorization", "Bearer " + bearer);

                var response = await client.GetAsync(uri);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = this.jsonProvider.ParseToJArray(await response.Content.ReadAsStringAsync());            
                    tweets = this.jsonProvider.DeserializeObject<List<TweetDto>>(json.ToString());
                }
            }

            return tweets;
        }
        public async Task<string> GetSearchSuggestions(string input)
        {
            string result = null;
            var userSuggestions = new List<TwitterSuggestionsDto>();

            var bearer = this.appCredentials.BearerToken;

            var uriString =
                $"https://api.twitter.com/1.1/users/suggestions/{input}/members.json";

            using (var client = new HttpClient(this.messageHandler))
            {
                var uri = new Uri(uriString);

                client.DefaultRequestHeaders
                    .Add("Authorization", "Bearer " + bearer);

                var response = await client.GetAsync(uri);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = this.jsonProvider.ParseToJArray(await response.Content.ReadAsStringAsync());
                    userSuggestions = this.jsonProvider.DeserializeObject<List<TwitterSuggestionsDto>>(json.ToString());
                    result = this.jsonProvider.SerializeObject(userSuggestions);
                   
                }
                
            }

            return result;
        }


        public async Task<TwitterSearchDto> SearchUser(string screenName)
        {
            var user = new TwitterSearchDto();
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
                    user = this.jsonProvider.DeserializeObject<TwitterSearchDto>(json.ToString());
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