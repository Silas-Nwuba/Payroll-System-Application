using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Persistence
{
    public static class DataSeeding
    {
        public static async Task UserAndRoleAsync(UserManager<IdentityUser> userManager,
                                                 RoleManager<IdentityRole> roleManager)
        {
            string[] Role = { "Admin", "Manager", "Staff" };
            foreach (var role in Role)
            {
                var RoleExit = await roleManager.RoleExistsAsync(role);
                if (!RoleExit)
                {
                    _ = await roleManager.CreateAsync(new IdentityRole(role));
                }

            }
            //creating a user and role to Admin
            if (userManager.FindByEmailAsync("Nwubasila@yahoo.com").Result == null)
            {
                IdentityUser User = new IdentityUser
                {
                    UserName = "Nwubasilas@yahoo.com",
                    Email = "Nwubasilas@yahoo.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(User, "Password1").Result;

                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(User, "Admin").Wait();
                }
            }
            //Creating A user and role to Manager
            if (userManager.FindByEmailAsync("Amaka@gmail.com").Result == null)
            {
                IdentityUser User = new IdentityUser
                {
                    UserName = "Amaka@gmail.com",
                    Email = "Amaka@gmail.com",
                };
                IdentityResult identityResult = userManager.CreateAsync(User, "Password1").Result;

                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(User, "Manager").Wait();
                }
            }
            // Create a user And a Role for staff

            if (userManager.FindByEmailAsync("Caleb@gmail.com").Result == null)
            {
                IdentityUser User = new IdentityUser
                {
                    UserName = "Caleb@gmail.com",
                    Email = "Cable@gmail.com",
                };
                IdentityResult identityResult = userManager.CreateAsync(User, "Password1").Result;

                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(User, "Staff").Wait();
                }
            }
            //No role for john deo
            if (userManager.FindByEmailAsync("John.deo@gmail.com").Result == null)
            {
                IdentityUser User = new IdentityUser
                {
                    UserName = "John.deo@gmail.com",
                    Email = "John.deo@gmail.com",
                };
                IdentityResult identityResult = userManager.CreateAsync(User, "Mike10").Result;

            }





        }
    }
}
