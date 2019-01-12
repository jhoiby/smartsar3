using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SSar.Presentation.WebUI.Areas.Identity.Models;

namespace SSar.Presentation.WebUI.Data
{
    public class DummyData
    {
        public static async Task Initialize(AppIdentityDbContext context,
                              UserManager<ApplicationUser> userManager,
                              RoleManager<ApplicationRole> roleManager)
        {
            context.Database.EnsureCreated();

            Guid adminId1;
            Guid adminId2;

            string role1 = "Admin";
            string desc1 = "This is the administrator role";

            string role2 = "Member";
            string desc2 = "This is the members role";

            string password = "P@$$w0rd";

            if (await roleManager.FindByNameAsync(role1) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role1, desc1, DateTime.Now));
            }
            if (await roleManager.FindByNameAsync(role2) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role2, desc2, DateTime.Now));
            }

            if (await userManager.FindByNameAsync("a") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "a@a.a",
                    Email = "a@a.a",
                    FirstName = "Adam",
                    LastName = "Aldridge",
                    PhoneNumber = "6902341234"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
                adminId1 = user.Id;
            }

            if (await userManager.FindByNameAsync("b") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "b@b.b",
                    Email = "b@b.b",
                    FirstName = "Bob",
                    LastName = "Barker",
                    PhoneNumber = "7788951456"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
                adminId2 = user.Id;
            }

            if (await userManager.FindByNameAsync("m") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "m@m.m",
                    Email = "m@m.m",
                    FirstName = "Mike",
                    LastName = "Myers",
                    PhoneNumber = "6572136821"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
            }

            if (await userManager.FindByNameAsync("d") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "d@d.d",
                    Email = "d@d.d",
                    FirstName = "Donald",
                    LastName = "Duck",
                    PhoneNumber = "6041234567"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
            }
        }
    }
}
