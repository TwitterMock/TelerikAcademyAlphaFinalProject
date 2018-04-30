using TwitterBackUp.DomainModels;

namespace TwitterBackUp.Services.Services.Contracts
{
    public interface ITweetService
    {
        void SaveTweetByUserId(string userId, Tweet tweet);
    }
}
