using CMS.Data.ContextModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.API.Utilities
{
    public class SeedMethod
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationContext>();

                var user = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "John Carl",
                    Gender = "Male",
                    City = "Surat",
                    Address = "101,Adajan",
                    Email = "superadmin@cms.com",
                    UserName = "superadmin@cms.com",
                    PasswordHash = "SuperAdmin@123",
                    PhoneNumber = "9879555324",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "johncarl11@gmail.com",
                    Role = "SuperAdmin",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };


                if (!context.ApplicationUser.Any(u => u.Email == user.Email))
                {
                    var password = new PasswordHasher<ApplicationUser>();
                    var hashed = password.HashPassword(user, "secret");
                    user.PasswordHash = hashed;

                    var userStore = new UserStore<ApplicationUser>(context);
                    var result = userStore.CreateAsync(user);

                }
                context.SaveChangesAsync();

                string[] roles = new string[] { "SuperAdmin", "Admin", "Lawyer", "Client" };

                foreach (string role in roles)
                {
                    var roleStore = new RoleStore<IdentityRole>(context);

                    if (!context.Roles.Any(r => r.Name == role))
                    {
                        roleStore.CreateAsync(new IdentityRole(role));
                    }
                }

                var res = AssignRoles(serviceProvider, user.Email, roles);

                context.SaveChangesAsync();

                //context.Database.Migrate();
            }
        }

        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<ApplicationUser> _userManager = services.GetService<UserManager<ApplicationUser>>();
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }
    }
}
