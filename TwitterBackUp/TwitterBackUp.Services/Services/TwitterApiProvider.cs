using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TwitterBackUp.DTO;
using TwitterBackUp.DTO.TweetDtos;
using TwitterBackUp.Services.Services.Contracts;
using TwitterBackUp.Services.Utils.Contracts;

namespace TwitterBackUp.Services.Services
{
    public class TwitterApiProvider : ITwitterApiProvider
    {
        private readonly IAppCredentials appCredentials;
        private readonly IJsonProvider jsonProvider;
        private readonly IHttpClientWrapper httpClient;

        public TwitterApiProvider(IAppCredentials appCredentials, IJsonProvider jsonProvider, IHttpClientWrapper httpClient)
        {
            this.appCredentials = appCredentials;
            this.jsonProvider = jsonProvider;
            this.httpClient = httpClient;
        }

        public async Task<ICollection<TweetDto>> GetUserTimeLine(string userId, int tweetsCount)
        {
            var tweets = new List<TweetDto>();

            var bearer = this.appCredentials.BearerToken;

            var uriString =
                $"https://api.twitter.com/1.1/statuses/user_timeline.json?user_id={userId}&count={tweetsCount}";

            var httpMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uriString)
            };

            httpMessage.Headers.Add("Authorization", "Bearer " + bearer);

            var response = await this.httpClient.SendAsync(httpMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = this.jsonProvider.ParseToJArray(await response.Content.ReadAsStringAsync());
                tweets = this.jsonProvider.DeserializeObject<List<TweetDto>>(json.ToString());
            }

            return tweets;
        }

        public async Task<string> GetSearchSuggestionsByCategory(string category)
        {
            string result = null;

            var bearer = this.appCredentials.BearerToken;

            var uriString =
                $"https://api.twitter.com/1.1/users/suggestions/{category}/members.json";

            var httpMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uriString)
            };

            httpMessage.Headers.Add("Authorization", "Bearer " + bearer);

            var response = await this.httpClient.SendAsync(httpMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = this.jsonProvider.ParseToJArray(await response.Content.ReadAsStringAsync());
                var userSuggestions = this.jsonProvider.DeserializeObject<List<TwitterSuggestionsDto>>(json.ToString());
                result = this.jsonProvider.SerializeObject(userSuggestions);
            }

            return result;
        }

        public async Task<TwitterSearchDto> SearchUser(string screenName)
        {
            var user = new TwitterSearchDto();
            var bearer = this.appCredentials.BearerToken;

            var uriString = $"https://api.twitter.com/1.1/users/show.json?screen_name={screenName}";

            var httpMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uriString)
            };

            httpMessage.Headers.Add("Authorization", "Bearer " + bearer);

            var response = await this.httpClient.SendAsync(httpMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = this.jsonProvider.ParseToJObject(await response.Content.ReadAsStringAsync());
                user = this.jsonProvider.DeserializeObject<TwitterSearchDto>(json.ToString());
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

            var httpMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(uriString),
                Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                })
            };

            httpMessage.Headers.Add("Authorization", "Basic " + bearerRequestString);

            var response = await this.httpClient.SendAsync(httpMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var json = this.jsonProvider.ParseToJObject(jsonString);

                bearerResponseString = json["access_token"].ToString();
            }


            return bearerResponseString;
        }
    }
}