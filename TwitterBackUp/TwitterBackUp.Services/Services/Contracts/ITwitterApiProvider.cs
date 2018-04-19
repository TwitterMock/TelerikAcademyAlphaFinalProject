using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackUp.DTO;

namespace TwitterBackUp.Services.Services.Contracts
{
    public interface ITwitterApiProvider
    {
        Task<SearchUserDto> SearchUser(string searchString);
    }
}