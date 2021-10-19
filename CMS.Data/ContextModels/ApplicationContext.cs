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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lawyer>(entity => entity.HasIndex(e => e.Lawyer_uniqueNumber).IsUnique());
        }
        public DbSet<User> UserData { get; set; }
        public DbSet<Lawyer> Lawyers { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
