using TwitterBackUp.DomainModels;

namespace TwitterBackUp.Services.Services.Contracts
{
    public interface ITweetService
    {
        void StoreTweetByUserId(string userId, Tweet tweet);
    }
}
