using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TwitterBackUp.Data.Identity;

namespace TwitterBackUp.Services.Utils
{
    public class UserManagerProvider : IUserManagerProvider
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserManagerProvider(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public IQueryable<ApplicationUser> GetAllUsers()
        {
            return this.userManager.Users;
        }
        public async Task<ApplicationUser> FindByIdAsync(string Id)
        {
            return await this.userManager.FindByIdAsync(Id);
        }
        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            return await this.userManager.DeleteAsync(user);
        }
        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role)
        {
            return await this.userManager.AddToRoleAsync(user, role);
        }
        public async Task<ICollection<ApplicationUser>> GetUsersInRoleAsync(string role)
        {
            return await this.userManager.GetUsersInRoleAsync(role);

        }
        public string GetUserId(ClaimsPrincipal user)
        {
          return this.userManager.GetUserId(user);
        }

        public ApplicationUser GetById(string id)
        {
            return this.userManager.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
