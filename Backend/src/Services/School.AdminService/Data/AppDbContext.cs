using Microsoft.EntityFrameworkCore;
using School.AdminService.Data.Entities;
using System.Xml;

namespace School.AdminService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<PersonalInfo> PersonalInfos { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Thana> Thanas { get; set; }
        public DbSet<StudentInfo> StudentInfos { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<StudentAcademicHistory> StudentAcademicHistories { get; set; }
        public DbSet<FeesName> FeesNames { get; set; }
        public DbSet<FeesSetup> FeesSetups { get; set; }
        public DbSet<SpecialFee> SpecialFees { get; set; }
        public DbSet<FeesCollectionMaster> FeesCollectionMasters { get; set; }
        public DbSet<FeesCollectionDetail> FeesCollectionDetails { get; set; }

    }
}
