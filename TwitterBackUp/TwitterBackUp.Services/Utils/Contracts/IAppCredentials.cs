using System.Threading.Tasks;

namespace TwitterBackUp.Services.Utils.Contracts
{
    public interface IAppCredentials
    {
        string ConsumerKey { get; }
        string ConsumerSecret { get; }
        string BearerToken { get; }
    }
}