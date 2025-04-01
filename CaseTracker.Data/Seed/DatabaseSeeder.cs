using CaseTracker.Data.ContextModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTracker.Data.Seed
{
    public static class DatabaseSeeder
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                context.Database.Migrate(); // Ensure the database is updated

                if (!context.SubscriptionPackages.Any()) // Prevent duplicate seeding
                {
                    context.SubscriptionPackages.AddRange(
                        new SubscriptionPackage { Name = "Free", DurationDays = 15, IsTrial = true, PackagePrice = 0, CreatedDate = DateTime.UtcNow },
                        new SubscriptionPackage { Name = "1Month", DurationDays = 30, IsTrial = false, PackagePrice = 100, CreatedDate = DateTime.UtcNow },
                        new SubscriptionPackage { Name = "3Month", DurationDays = 90, IsTrial = false, PackagePrice = 200, CreatedDate = DateTime.UtcNow },
                        new SubscriptionPackage { Name = "6Month", DurationDays = 180, IsTrial = false, PackagePrice = 300, CreatedDate = DateTime.UtcNow },
                        new SubscriptionPackage { Name = "9Month", DurationDays = 270, IsTrial = false, PackagePrice = 400, CreatedDate = DateTime.UtcNow },
                        new SubscriptionPackage { Name = "12Month", DurationDays = 365, IsTrial = false, PackagePrice = 500, CreatedDate = DateTime.UtcNow }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
