using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackUp.DTO;
using TwitterBackUp.DTO.TweetDtos;

namespace TwitterBackUp.Services.Services.Contracts
{
    public interface ITwitterApiProvider
    {
        Task<TwitterSearchDto> SearchUser(string searchString);
        Task<ICollection<TweetDto>> GetUserTimeLine(string userId, int count);
        Task<string> GetSearchSuggestions(string input);
    }
}