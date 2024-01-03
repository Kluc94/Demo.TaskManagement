using Demo.TaskManagement.Data;
using Demo.TaskManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Demo.TaskManagement.Services
{
    public static class UserService
    {
        public static async Task EnsureAdmin(UserManager<User> userManager)
        {

            var admin = await userManager.FindByEmailAsync("admin@demo.cz");
            if (admin == null)
            {
                admin = new User()
                {
                    UserName = "admin@demo.cz",
                    FirstName = "Admin",
                    EmailConfirmed = true,
                    Email = "admin@demo.cz"
                };
                await userManager.CreateAsync(admin);
            }

            await userManager.AddPasswordAsync(admin, "Aa123456-");
            await userManager.AddToRoleAsync(admin, "SPRÁVCE");

        }
    }
}
