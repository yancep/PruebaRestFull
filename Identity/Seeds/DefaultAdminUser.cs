using Application.Enums;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Seeds
{
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defautUser = new ApplicationUser
            {
                UserName = "userAdmin",
                Email = "userAdmin@mail.com", 
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u=> u.Id != defautUser.Id))
            {
                var user =await userManager.FindByEmailAsync(defautUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defautUser, "1234");
                    await userManager.AddToRoleAsync(defautUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defautUser, Roles.Basic.ToString());
                }
            }
        }
    }
}
