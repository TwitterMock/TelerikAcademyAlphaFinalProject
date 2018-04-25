using System.Threading.Tasks;
using TwitterBackUp.DTO;
using TwitterBackUp.DTO.TwitterTimelineDtos;

namespace TwitterBackUp.Services.Services.Contracts
{
    public interface ITwitterApiProvider
    {
        Task<TwitterSearchDto> SearchUser(string searchString);
        Task<TwitterTimelineDto> GetUserTimeLine(string userId, int count);
        Task<string> GetSearchSuggestionsByCategory(string category);
        Task<string> GetBearerTokenAsync(string consumerKey, string consumerSecret);
        Task<string> GetTweetHtml(string userScreenName, string tweetId);
    }
}