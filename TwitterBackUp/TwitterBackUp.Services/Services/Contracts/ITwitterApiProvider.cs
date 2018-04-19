using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackUp.DTO;
using TwitterBackUp.DTO.TweetsTimeline;

namespace TwitterBackUp.Services.Services.Contracts
{
    public interface ITwitterApiProvider
    {
        Task<SearchUserDto> SearchUser(string searchString);
        Task<ICollection<TweetDto>> GetUserTimeLine(string userId);
    }
}