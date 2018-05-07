using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.Extensions.Caching.Memory;
using TwitterBackUp.DTO;
using TwitterBackUp.Services.Services.Contracts;
using TwitterBackUp.Services.Utils.Contracts;

namespace TwitterBackUp.Services.Services
{
    public class TwitterRequestHandler : ITwitterRequestHandler
    {
        private readonly IAppCredentials appCredentials;
        private readonly IJsonProvider jsonProvider;
        private readonly IHttpClientWrapper httpClient;
        private readonly IMemoryCache memoryCache;
        private readonly ITwitterAuthStringProvider twitterAuthStringProvider;

        public TwitterRequestHandler(IAppCredentials appCredentials, IJsonProvider jsonProvider, IHttpClientWrapper httpClient, IMemoryCache memoryCache, ITwitterAuthStringProvider twitterAuthStringProvider)
        {
            this.appCredentials = appCredentials;
            this.jsonProvider = jsonProvider;
            this.httpClient = httpClient;
            this.memoryCache = memoryCache;
            this.twitterAuthStringProvider = twitterAuthStringProvider;
        }

        public async Task<TweetDto> Retweet(string tweetId, string accessToken, string accessTokenSecret)
        {
            var uriString =
                $"https://api.twitter.com/1.1/statuses/retweet/{tweetId}.json";

            var httpMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(uriString),
            };

            var authHeaderString = this.twitterAuthStringProvider.GetAuthorizationString(httpMessage,
                    this.appCredentials.ConsumerKey,
                    this.appCredentials.ConsumerSecret,
                    accessToken,
                    accessTokenSecret);

            httpMessage.Headers.Add("Authorization", authHeaderString);

            var response = await this.httpClient.SendAsync(httpMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return this.jsonProvider.DeserializeObject<TweetDto>(jsonString);
            }

            return null;
        }

        public async Task<TwitterDto> FollowTwitter(string screenName, string accessToken, string accessTokenSecret)
        {
            var uriString =
                $"https://api.twitter.com/1.1/friendships/create.json?screen_name={screenName}&follow=true";

            var httpMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(uriString),
            };

            var authHeaderString = this.twitterAuthStringProvider.GetAuthorizationString(httpMessage,
                this.appCredentials.ConsumerKey,
                this.appCredentials.ConsumerSecret,
                accessToken,
                accessTokenSecret);

            httpMessage.Headers.Add("Authorization", authHeaderString);

            var response = await this.httpClient.SendAsync(httpMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return this.jsonProvider.DeserializeObject<TwitterDto>(jsonString);
            }

            return null;
        }

        public async Task<ICollection<TweetDto>> GetTwitterTimelineAsync(string screenName, int tweetsCount)
        {
            var bearer = this.appCredentials.BearerToken;

            var uriString =
                $"https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={screenName}&count={tweetsCount}";

            var httpMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uriString),
                Headers = { { "Authorization", "Bearer " + bearer } }
            };

            var response = await this.httpClient.SendAsync(httpMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return this.jsonProvider.DeserializeObject<List<TweetDto>>(jsonString);
            }

            return null;
        }

        public async Task<string> GetSearchSuggestionsByCategoryAsync(string category)
        {
            string suggestions;

            if (this.memoryCache.TryGetValue($"{category}", out suggestions))
            {
                return suggestions;
            }

            var bearer = this.appCredentials.BearerToken;

            var uriString =
                $"https://api.twitter.com/1.1/users/suggestions/{category}/members.json";

            var httpMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uriString),
                Headers = { { "Authorization", "Bearer " + bearer } }
            };

            var response = await this.httpClient.SendAsync(httpMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                suggestions = await response.Content.ReadAsStringAsync();
                this.memoryCache.Set($"{category}", suggestions, TimeSpan.FromMinutes(20));
            }

            return suggestions;
        }

        public async Task<TwitterDto> GetTwitterByScreenNameAsync(string screenName)
        {
            var bearer = this.appCredentials.BearerToken;

            var uriString = $"https://api.twitter.com/1.1/users/show.json?screen_name={screenName}";

            var httpMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uriString),
                Headers = { { "Authorization", "Bearer " + bearer } }
            };

            var response = await this.httpClient.SendAsync(httpMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return this.jsonProvider.DeserializeObject<TwitterDto>(jsonString);
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
                }),
                Headers = { { "Authorization", "Basic " + bearerRequestString } }
            };

            var response = await this.httpClient.SendAsync(httpMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var json = this.jsonProvider.ParseToJObject(jsonString);

                return json["access_token"].ToString();
            }

            return null;
        }

        public async Task<string> GetTweetHtmlAsync(string tweetUrl)
        {
            var uriString =
                $"https://publish.twitter.com/oembed?url={tweetUrl}&align=center";

            var httpMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uriString)
            };

            var response = await this.httpClient.SendAsync(httpMessage);

            if (response.IsSuccessStatusCode)
            {
                var json = this.jsonProvider.ParseToJObject(await response.Content.ReadAsStringAsync());
                return json["html"].ToString();
            }

            return null;
        }

        public async Task<TweetDto> GetTweetByIdAsync(string id)
        {
            var bearer = this.appCredentials.BearerToken;

            var uriString =
                $"https://api.twitter.com/1.1/statuses/show.json?id={id}";

            var httpMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uriString),
                Headers = { { "Authorization", "Bearer " + bearer } }
            };

            var response = await this.httpClient.SendAsync(httpMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return this.jsonProvider.DeserializeObject<TweetDto>(jsonString);
            }

            return null;
        }
    }
}