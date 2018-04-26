using System.Threading.Tasks;
using TwitterBackUp.DTO;
using TwitterBackUp.DTO.TimelineDtos;

namespace TwitterBackUp.Services.Services.Contracts
{
    public interface ITwitterApiProvider
    {
        Task<ExtendedTwitterDto> GetTwitterByScreenNameAsync(string searchString);
        Task<TimelineDto> GetTwitterTimelineAsync(string userId, int count);
        Task<string> GetSearchSuggestionsByCategoryAsync(string category);
        Task<string> GetBearerTokenAsync(string consumerKey, string consumerSecret);
        Task<string> GetTweetHtmlAsync(string userScreenName, string tweetId);
    }
}