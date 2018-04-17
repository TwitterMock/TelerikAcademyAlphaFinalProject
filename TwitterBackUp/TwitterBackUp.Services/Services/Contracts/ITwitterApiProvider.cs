using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackUp.DTO;

namespace TwitterBackUp.Services.Services.Contracts
{
    public interface ITwitterApiProvider
    {
        Task<ICollection<TweetDto>> SearchTweetsAsync(string searchString);
    }
}