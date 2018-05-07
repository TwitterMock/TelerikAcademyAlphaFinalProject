using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackUp.DTO;

namespace TwitterBackUp.Services.Services.Contracts
{
    public interface ITwitterRequestHandler
    {
        Task<TwitterDto> GetTwitterByScreenNameAsync(string screenName);
        Task<ICollection<TweetDto>> GetTwitterTimelineAsync(string screenName, int count);
        Task<string> GetSearchSuggestionsByCategoryAsync(string category);
        Task<string> GetBearerTokenAsync(string consumerKey, string consumerSecret);
        Task<string> GetTweetHtmlAsync(string tweetUrl);
        Task<TweetDto> Retweet(string tweetId, string accessToken, string accessTokenSecret);
        Task<TweetDto> GetTweetByIdAsync(string id);
        Task<TwitterDto> FollowTwitter(string screenName, string accessToken, string accessTokenSecret);
    }
}