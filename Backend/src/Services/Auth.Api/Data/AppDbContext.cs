using Auth.Api.Data.Entties;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Auth.Api.Data
{
    //public class AppDbContext : IdentityDbContext<ApplicationUser>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        

        public DbSet<User> Users { get; set; }
        public DbSet<AspRoleRight> AspRoleRights { get; set; }
        public DbSet<AppContent> AppContents { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<User>().ToTable("Users");
        //}
    }
}
