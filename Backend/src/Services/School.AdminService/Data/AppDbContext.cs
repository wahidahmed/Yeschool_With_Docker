using Microsoft.EntityFrameworkCore;
using School.AdminService.Data.Entities;
using School.AdminService.Helpers;

namespace School.AdminService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<ClassSection> ClassSections { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<PersonalInfo> PersonalInfos { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Thana> Thanas { get; set; }
        public DbSet<StudentInfo> StudentInfos { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<StudentAcademicHistory> StudentAcademicHistories { get; set; }
        public DbSet<FeesName> FeesNames { get; set; }
        public DbSet<FeesSetup> FeesSetups { get; set; }
        public DbSet<SpecialFee> SpecialFees { get; set; }
        public DbSet<FeesCollectionMaster> FeesCollectionMasters { get; set; }
        public DbSet<FeesCollectionDetail> FeesCollectionDetails { get; set; }
        public DbSet<IdSequence> IdSequences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: Seed default sequences
            modelBuilder.Entity<IdSequence>().HasData(
                new IdSequence { EntityType = "PersonalInfos", NextId = 1 },
                new IdSequence { EntityType = "StudentInfos", NextId = 1 },
                new IdSequence { EntityType = "StudentAcademicHistories", NextId = 1 }
            );

            modelBuilder.Entity<StudentInfo>(entity =>
            {
                entity.Property(e => e.Status)
                     .HasConversion<string>()
                     .HasDefaultValue(StudentStatus.PENDING);

                // ✅ Check constraint for string values
                entity.ToTable(t => t.HasCheckConstraint(
                    "CK_Student_Status",
                    "Status IN ('PENDING', 'ACTIVE', 'ENROLLED')"
                ));

            });

            modelBuilder.Entity<PersonalInfo>()
                           .HasIndex(u => u.PersonCode)
                           .IsUnique();

            // Make all foreign keys restrict delete by default
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // ---- Country ----
            modelBuilder.Entity<Country>().HasData(
                 new Country { CountryId = 1, CountryName = "Bangladesh" }
             );

            // ---- Division ----
            modelBuilder.Entity<Division>().HasData(
                new Division { DivisionId = 1, CountriesId = 1, DivisionName = "Chattogram" }
            );


            // ---- Districts under Chattogram Division ----
            modelBuilder.Entity<District>().HasData(
                 new District { DistrictId = 101, DivisionsId = 1, DistrictName = "Chattogram", Remarks = "Metropolitan City" },
                 new District { DistrictId = 102, DivisionsId = 1, DistrictName = "Cox’s Bazar", Remarks = "Tourist City" },
                 new District { DistrictId = 103, DivisionsId = 1, DistrictName = "Bandarban", Remarks = "Hill Tract" },
                 new District { DistrictId = 104, DivisionsId = 1, DistrictName = "Rangamati", Remarks = "Hill Tract" },
                 new District { DistrictId = 105, DivisionsId = 1, DistrictName = "Khagrachari", Remarks = "Hill Tract" },
                 new District { DistrictId = 106, DivisionsId = 1, DistrictName = "Feni", Remarks = "North of Chattogram" },
                 new District { DistrictId = 107, DivisionsId = 1, DistrictName = "Noakhali", Remarks = "Coastal District" },
                 new District { DistrictId = 108, DivisionsId = 1, DistrictName = "Lakshmipur", Remarks = "Near Noakhali" },
                 new District { DistrictId = 109, DivisionsId = 1, DistrictName = "Brahmanbaria", Remarks = "Eastern District" },
                 new District { DistrictId = 110, DivisionsId = 1, DistrictName = "Cumilla", Remarks = "Major City" },
                 new District { DistrictId = 111, DivisionsId = 1, DistrictName = "Chandpur", Remarks = "River District" }
             );


            // ---- Thanas under Chattogram District ----
            modelBuilder.Entity<Thana>().HasData(
               // Chattogram
               new Thana { ThanaId = 1001, DistrictsId = 101, ThanaName = "Kotwali", Remarks = "City center" },
               new Thana { ThanaId = 1002, DistrictsId = 101, ThanaName = "Pahartali", Remarks = "Industrial area" },
               new Thana { ThanaId = 1003, DistrictsId = 101, ThanaName = "Double Mooring", Remarks = "Port area" },
               new Thana { ThanaId = 1004, DistrictsId = 101, ThanaName = "Hathazari Sadar", Remarks = "Main Hathazari" },
               new Thana { ThanaId = 1005, DistrictsId = 101, ThanaName = "Fatehpur", Remarks = "Hathazari Union" },
               new Thana { ThanaId = 1006, DistrictsId = 101, ThanaName = "Dhalai", Remarks = "Hathazari Union" },

               // Cox’s Bazar
               new Thana { ThanaId = 2001, DistrictsId = 102, ThanaName = "Cox’s Bazar Sadar", Remarks = "Tourist town" },
               new Thana { ThanaId = 2002, DistrictsId = 102, ThanaName = "Teknaf", Remarks = "Southern tip" },
               new Thana { ThanaId = 2003, DistrictsId = 102, ThanaName = "Ukhiya", Remarks = "Near Rohingya camps" },

               // Bandarban
               new Thana { ThanaId = 3001, DistrictsId = 103, ThanaName = "Bandarban Sadar", Remarks = "Hill district HQ" },
               new Thana { ThanaId = 3002, DistrictsId = 103, ThanaName = "Thanchi", Remarks = "Hill upazila" },
               new Thana { ThanaId = 3003, DistrictsId = 103, ThanaName = "Ruma", Remarks = "Hill upazila" },

               // Rangamati
               new Thana { ThanaId = 4001, DistrictsId = 104, ThanaName = "Rangamati Sadar", Remarks = "Lake city" },
               new Thana { ThanaId = 4002, DistrictsId = 104, ThanaName = "Kaptai", Remarks = "Hydroelectric project" },
               new Thana { ThanaId = 4003, DistrictsId = 104, ThanaName = "Baghaichhari", Remarks = "Hill upazila" },

               // Khagrachari
               new Thana { ThanaId = 5001, DistrictsId = 105, ThanaName = "Khagrachari Sadar", Remarks = "Hill town" },
               new Thana { ThanaId = 5002, DistrictsId = 105, ThanaName = "Dighinala", Remarks = "Border area" },
               new Thana { ThanaId = 5003, DistrictsId = 105, ThanaName = "Mahalchhari", Remarks = "Hill upazila" },

               // Feni
               new Thana { ThanaId = 6001, DistrictsId = 106, ThanaName = "Feni Sadar", Remarks = "Town area" },
               new Thana { ThanaId = 6002, DistrictsId = 106, ThanaName = "Chhagalnaiya", Remarks = "North Feni" },
               new Thana { ThanaId = 6003, DistrictsId = 106, ThanaName = "Parshuram", Remarks = "Border town" },

               // Noakhali
               new Thana { ThanaId = 7001, DistrictsId = 107, ThanaName = "Noakhali Sadar", Remarks = "Town area" },
               new Thana { ThanaId = 7002, DistrictsId = 107, ThanaName = "Begumganj", Remarks = "Industrial area" },
               new Thana { ThanaId = 7003, DistrictsId = 107, ThanaName = "Hatiya", Remarks = "Island upazila" },

               // Lakshmipur
               new Thana { ThanaId = 8001, DistrictsId = 108, ThanaName = "Lakshmipur Sadar", Remarks = "Town" },
               new Thana { ThanaId = 8002, DistrictsId = 108, ThanaName = "Raipur", Remarks = "Coastal upazila" },
               new Thana { ThanaId = 8003, DistrictsId = 108, ThanaName = "Ramganj", Remarks = "North Lakshmipur" },

               // Brahmanbaria
               new Thana { ThanaId = 9001, DistrictsId = 109, ThanaName = "Brahmanbaria Sadar", Remarks = "Town area" },
               new Thana { ThanaId = 9002, DistrictsId = 109, ThanaName = "Ashuganj", Remarks = "Power plant area" },
               new Thana { ThanaId = 9003, DistrictsId = 109, ThanaName = "Nabinagar", Remarks = "Agricultural hub" },

               // Cumilla
               new Thana { ThanaId = 10001, DistrictsId = 110, ThanaName = "Cumilla Sadar", Remarks = "Town" },
               new Thana { ThanaId = 10002, DistrictsId = 110, ThanaName = "Debidwar", Remarks = "North Cumilla" },
               new Thana { ThanaId = 10003, DistrictsId = 110, ThanaName = "Laksam", Remarks = "Railway town" },

               // Chandpur
               new Thana { ThanaId = 11001, DistrictsId = 111, ThanaName = "Chandpur Sadar", Remarks = "Town" },
               new Thana { ThanaId = 11002, DistrictsId = 111, ThanaName = "Haimchar", Remarks = "River area" },
               new Thana { ThanaId = 11003, DistrictsId = 111, ThanaName = "Shahrasti", Remarks = "Upazila" }
           );

        }


    }
}
