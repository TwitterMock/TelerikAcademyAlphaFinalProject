using System.Threading.Tasks;

namespace TwitterBackUp.Data.Identity.ExternalServices
{
    public interface IDbInitializer
    {
        Task Initialize();
    }
}