using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CaseTracker.Data.ContextModels
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lawyer>(entity => entity.HasIndex(e => e.Lawyer_uniqueNumber).IsUnique());
            modelBuilder.Entity<Case>(entity =>
                entity.HasOne(x => x.Lawyer)
                      .WithMany(x => x.Cases)
                      .OnDelete(DeleteBehavior.Restrict));

            //modelBuilder.Entity<SubscriptionPackage>().HasData(
            //new SubscriptionPackage { Id = 1, Name = "Free", DurationDays = 15, IsTrial = true, PackagePrice = 0 },
            //new SubscriptionPackage { Id = 2, Name = "1Month", DurationDays = 30, IsTrial = false, PackagePrice = 100 },
            //new SubscriptionPackage { Id = 3, Name = "3Month", DurationDays = 90, IsTrial = false, PackagePrice = 200 },
            //new SubscriptionPackage { Id = 4, Name = "6Month", DurationDays = 180, IsTrial = false, PackagePrice = 300 },
            //new SubscriptionPackage { Id = 5, Name = "9Month", DurationDays = 270, IsTrial = false, PackagePrice = 400 },
            //new SubscriptionPackage { Id = 6, Name = "12Month", DurationDays = 365, IsTrial = false, PackagePrice = 500 }
            //);

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Amount).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(p => p.Status).HasMaxLength(50);
            });
        }

        public DbSet<User> UserData { get; set; }
        public DbSet<Lawyer> Lawyers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<CaseDocument> CaseDocuments { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<SubscriptionPackage> SubscriptionPackages { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
