using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TwitterBackUp.DTO;
using TwitterBackUp.DTO.TimelineDtos;
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

        public async Task<TimelineDto> GetTwitterTimelineAsync(string userId, int tweetsCount)
        {
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
                var tweets = this.jsonProvider.DeserializeObject<List<TweetDto>>(json.ToString());
                var twitter = this.jsonProvider.DeserializeObject<TwitterDto>(json.First["user"].ToString());

                return new TimelineDto {Twitter = twitter, Tweets = tweets};
            }

            return null;
        }

        public async Task<string> GetSearchSuggestionsByCategoryAsync(string category)
        {
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
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        public async Task<ExtendedTwitterDto> GetTwitterByScreenNameAsync(string screenName)
        {
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
                return this.jsonProvider.DeserializeObject<ExtendedTwitterDto>(json.ToString());
            }

            return null;
        }

        public async Task<string> GetBearerTokenAsync(string consumerKey, string consumerSecret)
        {
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

                return json["access_token"].ToString();
            }

            return null;
        }

        public async Task<string> GetTweetHtmlAsync(string userScreenName, string tweetId)
        {
            var uriString =
                $"https://publish.twitter.com/oembed?url=https://twitter.com/{userScreenName}/status/{tweetId}";

            var httpMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uriString)
            };

            var response = await this.httpClient.SendAsync(httpMessage);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return string.Empty;
        }

        public async Task<ExtendedTweetDto> GetTweetByIdAsync(string id)
        {
            var bearer = this.appCredentials.BearerToken;

            var uriString =
                $"https://api.twitter.com/1.1/statuses/show.json?id={id}";

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
                return this.jsonProvider.DeserializeObject<ExtendedTweetDto>(json.ToString());
            }

            return null;
        }
    }
}