using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using TwitterBackUp.Services.Utils.Contracts;

namespace TwitterBackUp.Services.Utils
{
    public class TwitterAuthStringProvider : ITwitterAuthStringProvider
    {
        protected string AccessToken { get; set; }
        protected string AccessTokenSecret { get; set; }
        protected string Nonce { get; set; }
        protected string Timestamp { get; set; }
        protected string ConsumerKey { get; set; }
        protected string ConsumerSecret { get; set; }
        protected string Version => "1.0";
        protected string SignatureMethod => "HMAC-SHA1";

        public string GetAuthorizationString(HttpRequestMessage requestMessage, string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            if (requestMessage == null) throw new ArgumentNullException(nameof(requestMessage));

            this.InitializeAuthMembers(consumerKey, consumerSecret, accessToken, accessTokenSecret);

            this.Nonce = this.GetNonce();
            this.Timestamp = this.GetTimestamp();

            var signature = this.GetSignature(requestMessage);

            var authHeaderStringTemplate = "OAuth " +
                                           "oauth_consumer_key=\"{0}\", " +
                                           "oauth_nonce=\"{1}\", " +
                                           "oauth_signature=\"{2}\", " +
                                           "oauth_signature_method=\"{3}\", " +
                                           "oauth_timestamp=\"{4}\", " +
                                           "oauth_token=\"{5}\", " +
                                           "oauth_version=\"{6}\"";

            return string.Format(authHeaderStringTemplate,
                Uri.EscapeDataString(this.ConsumerKey),
                Uri.EscapeDataString(this.Nonce),
                Uri.EscapeDataString(signature),
                Uri.EscapeDataString(this.SignatureMethod),
                Uri.EscapeDataString(this.Timestamp),
                Uri.EscapeDataString(this.AccessToken),
                Uri.EscapeDataString(this.Version));
        }

        private void InitializeAuthMembers(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            this.ConsumerKey = consumerKey ?? throw new ArgumentNullException(nameof(consumerKey));
            this.ConsumerSecret = consumerSecret ?? throw new ArgumentException(nameof(consumerSecret));
            this.AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            this.AccessTokenSecret = accessTokenSecret ?? throw new ArgumentException(nameof(accessTokenSecret));
        }

        private string GetSignature(HttpRequestMessage requestMessage)
        {
            var signingKey = this.GetSigningKey();
            var signatureBaseString = this.GetSignatureBaseString(requestMessage);

            var signingKeyBytes = Encoding.ASCII.GetBytes(signingKey);
            var signatureBaseStringBytes = Encoding.ASCII.GetBytes(signatureBaseString);

            var hmacsha1 = new HMACSHA1(signingKeyBytes);
            return Convert.ToBase64String(hmacsha1.ComputeHash(signatureBaseStringBytes));
        }

        private string GetSignatureBaseString(HttpRequestMessage requestMessage)
        {
            var authKeyValuePairs = this.GetAuthKeyValuePairs();

            var queryKeyValuePairs = requestMessage.RequestUri != null
                ? requestMessage.GetQueryKeyValuePairs()
                : new Dictionary<string, string>();

            var contentKeyValuePairs = requestMessage.Content != null
                ? requestMessage.GetContentKeyValuePairs()
                : new Dictionary<string, string>();

            var parameters = authKeyValuePairs
                .Union(queryKeyValuePairs)
                .Union(contentKeyValuePairs)
                .ToDictionary(kvp => Uri.EscapeDataString(kvp.Key), kvp => Uri.EscapeDataString(kvp.Value))
                .OrderBy(kvp => kvp.Key)
                .Select(kvp => kvp.Key + "=" + kvp.Value)
                .ToList();

            var baseUrl = requestMessage.GetBaseUri();

            return requestMessage.Method.Method.ToUpper() + "&" + Uri.EscapeDataString(baseUrl)
                + "&" + Uri.EscapeDataString(string.Join("&", parameters));
        }

        private string GetSigningKey()
        {
            return Uri.EscapeDataString(this.ConsumerSecret) + "&" + Uri.EscapeDataString(this.AccessTokenSecret);
        }

        private IDictionary<string, string> GetAuthKeyValuePairs()
        {
            return new Dictionary<string, string>()
            {
                {"oauth_consumer_key", this.ConsumerKey},
                {"oauth_nonce", this.Nonce},
                {"oauth_signature_method", this.SignatureMethod},
                {"oauth_timestamp", this.Timestamp},
                {"oauth_token", this.AccessToken},
                {"oauth_version", this.Version}
            };
        }

        private string GetNonce()
        {
            return Guid.NewGuid().ToString("N");
        }

        private string GetTimestamp()
        {
            var totalSeconds = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return totalSeconds.ToString();
        }
    }
}