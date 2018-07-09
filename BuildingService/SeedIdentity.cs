using BuildingService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BuildingService
{
    public static class SeedIdentity
    {
        public async static void Seed(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<AppIdentityContext>();
            var userManager = provider.GetRequiredService<UserManager<AppUser>>();
            var roleManger = provider.GetRequiredService<RoleManager<IdentityRole>>();

            context.Database.EnsureCreated();


            if (!context.Users.Any())
            {



                var admin = new AppUser
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "Admin_jd",
                    Email = "johnDoe@mydomain.com",
                    FirstName = "John",
                    IsSuperUser = true,
                    LastName = "Doe",
                    CreatedAt = DateTime.Now
                };






                if (!await roleManger.RoleExistsAsync("Administrator"))
                {
                    var role = new IdentityRole("Administrator");
                    await roleManger.CreateAsync(role);
                    await roleManger.AddClaimAsync(role, new Claim("IsAdmin", "G.M"));
                    await roleManger.AddClaimAsync(role, new Claim("IsSuper", "SuperUser"));

                }
                if (await userManager.FindByNameAsync(admin.UserName) == null)
                {
                    await userManager.CreateAsync(admin, "Pass4Admin$$");
                    await userManager.AddToRoleAsync(admin, "Administrator");
                    admin.EmailConfirmed = true;
                    admin.LockoutEnabled = false;
                }
            }
        }
    }
}
