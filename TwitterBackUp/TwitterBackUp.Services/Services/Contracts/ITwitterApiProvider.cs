using System.Threading.Tasks;

namespace TwitterBackUp.Services.Services.Contracts
{
    public interface ITwitterApiProvider
    {
        Task<string> SearchTweetsAsync(string searchString);
    }
}