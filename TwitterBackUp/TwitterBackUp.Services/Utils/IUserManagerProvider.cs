using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TwitterBackUp.Data.Identity;

namespace TwitterBackUp.Services.Utils
{
    public interface IUserManagerProvider
    {
        Task<ApplicationUser> FindByIdAsync(string Id);
        IQueryable<ApplicationUser> GetAllUsers();
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);
        Task<ICollection<ApplicationUser>> GetUsersInRoleAsync(string role);
        string GetUserId(ClaimsPrincipal user);
    }
}