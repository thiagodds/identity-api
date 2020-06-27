using Identity.Model.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Identity.Model.Database.Core
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public static void SeedData(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var database = serviceProvider.GetService<DatabaseContext>();

            database.Database.Migrate();

            SeedAdminUser(userManager, database);
        }

        private static void SeedAdminUser(UserManager<ApplicationUser> userManager, DatabaseContext database)
        {
            if (!database.Users.Any())
            {
                _ = userManager.CreateAsync(new ApplicationUser
                {
                    Email = "admin@email.com",
                    UserName = "admin"                    
                }, "123qweEWQ#@!").Result;
            }
        }
    }
}