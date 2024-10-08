﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
            modelBuilder.Entity<Case>(entity =>
                entity.HasOne(x => x.Lawyer)
                      .WithMany(x => x.Cases)
                      .OnDelete(DeleteBehavior.Restrict));
        }

        public DbSet<User> UserData { get; set; }
        public DbSet<Lawyer> Lawyers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<CaseDocument> CaseDocuments { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
