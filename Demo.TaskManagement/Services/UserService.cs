using Demo.TaskManagement.Data;
using Demo.TaskManagement.Data.Entities;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Task = System.Threading.Tasks.Task;

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
                    UserName = "admin@elegis.cz",
                    FirstName = "Admin",
                    EmailConfirmed = true,
                    Email = "admin@demo.cz"
                };
                await userManager.CreateAsync(admin);
            }

            await userManager.AddPasswordAsync(admin, "Aa123456-");
            await userManager.AddToRoleAsync(admin, "SPRÁVCE");
        }

        public static async Task EnsureSeedDataUsers(UserManager<User> userManager)
        {
            //Should be at SeedData, but had some weird problem...
            var user = await userManager.FindByEmailAsync("michal.ondrak@elegis.cz");
            if (user == null)
            {
                user = new User()
                {
                    UserName = "michal.ondrak@elegis.cz",
                    DegreeBefore = "Bc.",
                    FirstName = "Michal",
                    LastName = "Ondrák",
                    Email = "michal.ondrak@elegis.cz",
                    EmailConfirmed = true,
                    PhoneNumber = "+420724788985",
                    PhoneNumberConfirmed = true
                };
                await userManager.CreateAsync(user);
            }

            await userManager.AddPasswordAsync(user, "Password123-");
            await userManager.AddToRoleAsync(user, "VÝVOJÁŘ");


            var tester = await userManager.FindByEmailAsync("tester@elegis.cz");
            if (tester == null)
            {
                tester = new User()
                {
                    UserName = "tester@elegis.cz",
                    DegreeBefore = "Bc.",
                    FirstName = "Pan",
                    LastName = "Tester",
                    Email = "tester@elegis.cz",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(tester);
            }

            await userManager.AddPasswordAsync(tester, "Password123-");
            await userManager.AddToRoleAsync(tester, "VÝVOJÁŘ");
        }
    }
}
