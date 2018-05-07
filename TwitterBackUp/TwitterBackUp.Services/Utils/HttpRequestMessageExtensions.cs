using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace TwitterBackUp.Services.Utils
{
    static class HttpRequestMessageExtensions
    {
        public static IReadOnlyDictionary<string, string> GetQueryKeyValuePairs(
            this HttpRequestMessage request)
        {
            if (request == null) throw new NullReferenceException(nameof(request));
            if (request.RequestUri == null) throw new NullReferenceException(nameof(request.RequestUri));

            var requestUriQuery = request.RequestUri.Query;

            if (string.IsNullOrEmpty(requestUriQuery)) return new Dictionary<string, string>();

            return requestUriQuery
                .Substring(1)
                .Split("&")
                .Select(param =>
                {
                    var kvp = param.Split("=");
                    var key = kvp[0];
                    var value = kvp[1];

                    return new KeyValuePair<string, string>(key, value);
                })
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static IReadOnlyDictionary<string, string> GetContentKeyValuePairs(
            this HttpRequestMessage request)
        {
            if (request == null) throw new NullReferenceException(nameof(request));
            if (request.Content == null) throw new NullReferenceException(nameof(request.Content));

            var contentString = request.Content.ReadAsStringAsync().Result;

            if(string.IsNullOrEmpty(contentString)) return new Dictionary<string, string>();

            return contentString
                .Split('&')
                .Select(param =>
                {
                    var kvp = param.Split('=');
                    var key = kvp[0];
                    var value = kvp[1];

                    return new KeyValuePair<string, string>(key, value);
                })
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static string GetBaseUri(this HttpRequestMessage request)
        {
            if (request == null) throw new NullReferenceException(nameof(request));
            if (request.RequestUri == null) throw new NullReferenceException(nameof(request.RequestUri));

            var absoluteUri = request.RequestUri.AbsoluteUri;

            return string.IsNullOrEmpty(request.RequestUri.Query) 
                ? absoluteUri 
                : absoluteUri.Substring(0, absoluteUri.IndexOf('?'));
        }
    }
}