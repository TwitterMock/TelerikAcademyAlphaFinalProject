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

        public TwitterApiProvider(IAppCredentials appCredentials)
        {
            this.appCredentials = appCredentials;
        }

        public async Task<ICollection<SingleTweetDTO>> SearchTweetsAsync(string searchString)
        {
            var bearer = this.appCredentials.BearerToken ??
                (this.appCredentials.BearerToken = await this.GetBearerTokenAsync());

            using (var client = new HttpClient())
            {
                var uri = new Uri(
                    $"https://api.twitter.com/1.1/search/tweets.json?q={searchString}&result_type=popular");

                client.DefaultRequestHeaders
                    .Add("Authorization", "Bearer " + bearer);

                var response = await client.GetAsync(uri);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = JObject.Parse(await response.Content.ReadAsStringAsync());
                    var jsonDes = JsonConvert.DeserializeObject<ICollection<SingleTweetDTO>>(json["statuses"].ToString());
                    return jsonDes;
                }
                return new List<SingleTweetDTO>();
         
            }
        }

        private async Task<string> GetBearerTokenAsync()
        {
            var uriString = "https://api.twitter.com/oauth2/token";

            var strBearerRequest = HttpUtility.UrlEncode(appCredentials.ConsumerKey)
                                   + ":" + HttpUtility.UrlEncode(appCredentials.ConsumerSecret);

            strBearerRequest = Convert.ToBase64String(Encoding.UTF8.GetBytes(strBearerRequest));

            using (var client = new HttpClient())
            {
                var uri = new Uri(uriString);

                client.DefaultRequestHeaders
                    .Add("Authorization", "Basic " + strBearerRequest);

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                });

                var response = await client.PostAsync(uri, content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(jsonString);

                    return json["access_token"].ToString();
                }

                return string.Empty;
            }
        }
    }
}