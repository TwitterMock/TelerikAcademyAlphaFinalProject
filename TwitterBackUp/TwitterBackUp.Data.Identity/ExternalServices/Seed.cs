
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackUp.Data.Identity;

public static class Seed
{
    public static async Task Initialize(IServiceProvider provider)
    {
        var context = provider.GetRequiredService<ApplicationDbContext>();
        var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

        await EnsureSeedData(context, userManager, roleManager);
    }

    public static async Task EnsureSeedData(ApplicationDbContext context, UserManager<ApplicationUser> userMgr, RoleManager<IdentityRole> roleMgr)
    {
        if (!context.Users
            .Any(u => u.UserName == "admin@mydomain.com"))
        {
            // Add 'admin' role
            var adminRole = await roleMgr.FindByNameAsync("Administrator");
            if (adminRole == null)
            {
                adminRole = new IdentityRole("Administrator");
                await roleMgr.CreateAsync(adminRole);
            }

            // create admin user
            var adminUser = new ApplicationUser();
            adminUser.UserName = "admin@mydomain.com";
            adminUser.Email = "admin@mydomain.com";

            await userMgr.CreateAsync(adminUser, "MYP@55word");

            await userMgr.SetLockoutEnabledAsync(adminUser, false);
            await userMgr.AddToRoleAsync(adminUser, "Administrator");
        }
    }
}