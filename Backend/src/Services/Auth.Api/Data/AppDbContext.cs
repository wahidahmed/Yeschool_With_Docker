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

        public DbSet<Tbl_User> Tbl_Users { get; set; }
        public DbSet<Tbl_Role> Tbl_Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<OtpManager> OtpManagers { get; set; }
        public DbSet<PwdManager> PwdManagers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<RoleMenuMap> RoleMenuMaps { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<SubTable> SubTables { get; set; }
        public DbSet<TempUser> TempUsers { get; set; }

        public DbSet<User> Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<User>().ToTable("Users");
        //}
    }
}
