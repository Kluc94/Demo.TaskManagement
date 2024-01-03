using Demo.TaskManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Demo.TaskManagement.Services
{
    public static class RoleService 
    {
        public static async Task EnsureRoles(RoleManager<IdentityRole<int>> roleManager)
        {
            if ((await roleManager.RoleExistsAsync("SPRÁVCE")) == false)
            {
                var a = await roleManager.CreateAsync(new IdentityRole<int>()
                {
                    Name = "Správce",
                    NormalizedName = "SPRÁVCE",

                });
            }

            if ((await roleManager.RoleExistsAsync("VÝVOJÁŘ")) == false)
            {
                await roleManager.CreateAsync(new IdentityRole<int>()
                {
                    Name = "Vývojář",
                    NormalizedName = "VÝVOJÁŘ"
                });
            }

            if ((await roleManager.RoleExistsAsync("ZADÁVAJÍCÍ")) == false)
            {
                await roleManager.CreateAsync(new IdentityRole<int>()
                {
                    Name = "Zadávající",
                    NormalizedName = "ZADÁVAJÍCÍ"
                });
            }

            if ((await roleManager.RoleExistsAsync("NAHLÍŽEJÍCÍ")) == false)
            {
                await roleManager.CreateAsync(new IdentityRole<int>()
                {
                    Name = "Nahlížející",
                    NormalizedName = "NAHLÍŽEJÍCÍ"
                });
            }
        }
    }
}
