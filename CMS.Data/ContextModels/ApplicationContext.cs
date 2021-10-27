using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.ContextModels
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public ApplicationContext() : base()
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

        }
        public DbSet<Lawyer> Lawyers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
