using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace School.AdminService.Migrations
{
    /// <inheritdoc />
    public partial class initTablesAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicYears",
                columns: table => new
                {
                    AcademicYearId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYears", x => x.AcademicYearId);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassesId = table.Column<int>(type: "int", nullable: false),
                    ClassesName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassesId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "FeesNames",
                columns: table => new
                {
                    FeesNameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FeesCollectionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeesNames", x => x.FeesNameId);
                });

            migrationBuilder.CreateTable(
                name: "PersonalInfos",
                columns: table => new
                {
                    PersonalnfoId = table.Column<long>(type: "bigint", nullable: false),
                    PersonName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FatherMobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    FatherOccupation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MotherMobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    MotherOccupation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Religion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInfos", x => x.PersonalnfoId);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    SectionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.SectionId);
                });

            migrationBuilder.CreateTable(
                name: "Divisions",
                columns: table => new
                {
                    DivisionId = table.Column<int>(type: "int", nullable: false),
                    CountriesId = table.Column<int>(type: "int", nullable: false),
                    DivisionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisions", x => x.DivisionId);
                    table.ForeignKey(
                        name: "FK_Divisions_Countries_CountriesId",
                        column: x => x.CountriesId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeesSetups",
                columns: table => new
                {
                    FeesSetupId = table.Column<long>(type: "bigint", nullable: false),
                    ClassesId = table.Column<int>(type: "int", nullable: false),
                    FeesNameId = table.Column<int>(type: "int", nullable: false),
                    FeesAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AppliedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeesSetups", x => x.FeesSetupId);
                    table.ForeignKey(
                        name: "FK_FeesSetups_Classes_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "Classes",
                        principalColumn: "ClassesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeesSetups_FeesNames_FeesNameId",
                        column: x => x.FeesNameId,
                        principalTable: "FeesNames",
                        principalColumn: "FeesNameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassSection",
                columns: table => new
                {
                    ClassSectionId = table.Column<int>(type: "int", nullable: false),
                    ClassesId = table.Column<int>(type: "int", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSection", x => x.ClassSectionId);
                    table.ForeignKey(
                        name: "FK_ClassSection_Classes_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "Classes",
                        principalColumn: "ClassesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassSection_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    DivisionsId = table.Column<int>(type: "int", nullable: true),
                    DistrictName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.DistrictId);
                    table.ForeignKey(
                        name: "FK_Districts_Divisions_DivisionsId",
                        column: x => x.DivisionsId,
                        principalTable: "Divisions",
                        principalColumn: "DivisionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentAcademicHistories",
                columns: table => new
                {
                    StudentAcademicHistoryId = table.Column<long>(type: "bigint", nullable: false),
                    ClassSectionId = table.Column<int>(type: "int", nullable: false),
                    AcademicYearId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAcademicHistories", x => x.StudentAcademicHistoryId);
                    table.ForeignKey(
                        name: "FK_StudentAcademicHistories_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "AcademicYearId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAcademicHistories_ClassSection_ClassSectionId",
                        column: x => x.ClassSectionId,
                        principalTable: "ClassSection",
                        principalColumn: "ClassSectionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Thanas",
                columns: table => new
                {
                    ThanaId = table.Column<int>(type: "int", nullable: false),
                    DistrictsId = table.Column<int>(type: "int", nullable: true),
                    ThanaName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thanas", x => x.ThanaId);
                    table.ForeignKey(
                        name: "FK_Thanas_Districts_DistrictsId",
                        column: x => x.DistrictsId,
                        principalTable: "Districts",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentInfos",
                columns: table => new
                {
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    PersonalInfoId = table.Column<long>(type: "bigint", nullable: false),
                    GuardianName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    GuardianMobileNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    GuardianRelation = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StudentAcademicHistoryId = table.Column<long>(type: "bigint", nullable: false),
                    AcademicYearId = table.Column<int>(type: "int", nullable: true),
                    ClassesId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentInfos", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_StudentInfos_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "AcademicYearId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentInfos_Classes_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "Classes",
                        principalColumn: "ClassesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentInfos_PersonalInfos_PersonalInfoId",
                        column: x => x.PersonalInfoId,
                        principalTable: "PersonalInfos",
                        principalColumn: "PersonalnfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentInfos_StudentAcademicHistories_StudentAcademicHistoryId",
                        column: x => x.StudentAcademicHistoryId,
                        principalTable: "StudentAcademicHistories",
                        principalColumn: "StudentAcademicHistoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    ThanaId = table.Column<int>(type: "int", nullable: false),
                    StreetDetail = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AddressType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PersonalInfoId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_PersonalInfos_PersonalInfoId",
                        column: x => x.PersonalInfoId,
                        principalTable: "PersonalInfos",
                        principalColumn: "PersonalnfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_Thanas_ThanaId",
                        column: x => x.ThanaId,
                        principalTable: "Thanas",
                        principalColumn: "ThanaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeesCollectionMasters",
                columns: table => new
                {
                    FeesCollectionMasterId = table.Column<long>(type: "bigint", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    StudentInfoId = table.Column<long>(type: "bigint", nullable: false),
                    AcademicYearId = table.Column<int>(type: "int", nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ExtraDiscount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ExtraDiscountReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    GrandTotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsAdmitFees = table.Column<bool>(type: "bit", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    CollectDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeesCollectionMasters", x => x.FeesCollectionMasterId);
                    table.ForeignKey(
                        name: "FK_FeesCollectionMasters_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "AcademicYearId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeesCollectionMasters_StudentInfos_StudentInfoId",
                        column: x => x.StudentInfoId,
                        principalTable: "StudentInfos",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpecialFees",
                columns: table => new
                {
                    SpecialFeesId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeesNamesId = table.Column<int>(type: "int", nullable: false),
                    StudentInfosId = table.Column<long>(type: "bigint", nullable: false),
                    AcademicYear = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ActiveDate = table.Column<DateOnly>(type: "date", nullable: false),
                    InactiveDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialFees", x => x.SpecialFeesId);
                    table.ForeignKey(
                        name: "FK_SpecialFees_AcademicYears_AcademicYear",
                        column: x => x.AcademicYear,
                        principalTable: "AcademicYears",
                        principalColumn: "AcademicYearId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpecialFees_FeesNames_FeesNamesId",
                        column: x => x.FeesNamesId,
                        principalTable: "FeesNames",
                        principalColumn: "FeesNameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpecialFees_StudentInfos_StudentInfosId",
                        column: x => x.StudentInfosId,
                        principalTable: "StudentInfos",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeesCollectionDetails",
                columns: table => new
                {
                    FeesCollectionDetailId = table.Column<long>(type: "bigint", nullable: false),
                    FeesCollectionMasterId = table.Column<long>(type: "bigint", nullable: false),
                    FeesNameId = table.Column<int>(type: "int", nullable: false),
                    FromMonth = table.Column<int>(type: "int", nullable: true),
                    ToMonth = table.Column<int>(type: "int", nullable: true),
                    IsSpecial = table.Column<bool>(type: "bit", nullable: false),
                    FeesAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeesCollectionDetails", x => x.FeesCollectionDetailId);
                    table.ForeignKey(
                        name: "FK_FeesCollectionDetails_FeesCollectionMasters_FeesCollectionMasterId",
                        column: x => x.FeesCollectionMasterId,
                        principalTable: "FeesCollectionMasters",
                        principalColumn: "FeesCollectionMasterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeesCollectionDetails_FeesNames_FeesNameId",
                        column: x => x.FeesNameId,
                        principalTable: "FeesNames",
                        principalColumn: "FeesNameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryName", "Remarks" },
                values: new object[] { 1, "Bangladesh", null });

            migrationBuilder.InsertData(
                table: "Divisions",
                columns: new[] { "DivisionId", "CountriesId", "DivisionName" },
                values: new object[] { 1, 1, "Chattogram" });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "DistrictId", "DistrictName", "DivisionsId", "Remarks" },
                values: new object[,]
                {
                    { 101, "Chattogram", 1, "Metropolitan City" },
                    { 102, "Cox’s Bazar", 1, "Tourist City" },
                    { 103, "Bandarban", 1, "Hill Tract" },
                    { 104, "Rangamati", 1, "Hill Tract" },
                    { 105, "Khagrachari", 1, "Hill Tract" },
                    { 106, "Feni", 1, "North of Chattogram" },
                    { 107, "Noakhali", 1, "Coastal District" },
                    { 108, "Lakshmipur", 1, "Near Noakhali" },
                    { 109, "Brahmanbaria", 1, "Eastern District" },
                    { 110, "Cumilla", 1, "Major City" },
                    { 111, "Chandpur", 1, "River District" }
                });

            migrationBuilder.InsertData(
                table: "Thanas",
                columns: new[] { "ThanaId", "DistrictsId", "Remarks", "ThanaName" },
                values: new object[,]
                {
                    { 1001, 101, "City center", "Kotwali" },
                    { 1002, 101, "Industrial area", "Pahartali" },
                    { 1003, 101, "Port area", "Double Mooring" },
                    { 1004, 101, "Main Hathazari", "Hathazari Sadar" },
                    { 1005, 101, "Hathazari Union", "Fatehpur" },
                    { 1006, 101, "Hathazari Union", "Dhalai" },
                    { 2001, 102, "Tourist town", "Cox’s Bazar Sadar" },
                    { 2002, 102, "Southern tip", "Teknaf" },
                    { 2003, 102, "Near Rohingya camps", "Ukhiya" },
                    { 3001, 103, "Hill district HQ", "Bandarban Sadar" },
                    { 3002, 103, "Hill upazila", "Thanchi" },
                    { 3003, 103, "Hill upazila", "Ruma" },
                    { 4001, 104, "Lake city", "Rangamati Sadar" },
                    { 4002, 104, "Hydroelectric project", "Kaptai" },
                    { 4003, 104, "Hill upazila", "Baghaichhari" },
                    { 5001, 105, "Hill town", "Khagrachari Sadar" },
                    { 5002, 105, "Border area", "Dighinala" },
                    { 5003, 105, "Hill upazila", "Mahalchhari" },
                    { 6001, 106, "Town area", "Feni Sadar" },
                    { 6002, 106, "North Feni", "Chhagalnaiya" },
                    { 6003, 106, "Border town", "Parshuram" },
                    { 7001, 107, "Town area", "Noakhali Sadar" },
                    { 7002, 107, "Industrial area", "Begumganj" },
                    { 7003, 107, "Island upazila", "Hatiya" },
                    { 8001, 108, "Town", "Lakshmipur Sadar" },
                    { 8002, 108, "Coastal upazila", "Raipur" },
                    { 8003, 108, "North Lakshmipur", "Ramganj" },
                    { 9001, 109, "Town area", "Brahmanbaria Sadar" },
                    { 9002, 109, "Power plant area", "Ashuganj" },
                    { 9003, 109, "Agricultural hub", "Nabinagar" },
                    { 10001, 110, "Town", "Cumilla Sadar" },
                    { 10002, 110, "North Cumilla", "Debidwar" },
                    { 10003, 110, "Railway town", "Laksam" },
                    { 11001, 111, "Town", "Chandpur Sadar" },
                    { 11002, 111, "River area", "Haimchar" },
                    { 11003, 111, "Upazila", "Shahrasti" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_DistrictId",
                table: "Addresses",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PersonalInfoId",
                table: "Addresses",
                column: "PersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ThanaId",
                table: "Addresses",
                column: "ThanaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSection_ClassesId",
                table: "ClassSection",
                column: "ClassesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSection_SectionId",
                table: "ClassSection",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_DivisionsId",
                table: "Districts",
                column: "DivisionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Divisions_CountriesId",
                table: "Divisions",
                column: "CountriesId");

            migrationBuilder.CreateIndex(
                name: "IX_FeesCollectionDetails_FeesCollectionMasterId",
                table: "FeesCollectionDetails",
                column: "FeesCollectionMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_FeesCollectionDetails_FeesNameId",
                table: "FeesCollectionDetails",
                column: "FeesNameId");

            migrationBuilder.CreateIndex(
                name: "IX_FeesCollectionMasters_AcademicYearId",
                table: "FeesCollectionMasters",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_FeesCollectionMasters_StudentInfoId",
                table: "FeesCollectionMasters",
                column: "StudentInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_FeesSetups_ClassesId",
                table: "FeesSetups",
                column: "ClassesId");

            migrationBuilder.CreateIndex(
                name: "IX_FeesSetups_FeesNameId",
                table: "FeesSetups",
                column: "FeesNameId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialFees_AcademicYear",
                table: "SpecialFees",
                column: "AcademicYear");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialFees_FeesNamesId",
                table: "SpecialFees",
                column: "FeesNamesId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialFees_StudentInfosId",
                table: "SpecialFees",
                column: "StudentInfosId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicHistories_AcademicYearId",
                table: "StudentAcademicHistories",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicHistories_ClassSectionId",
                table: "StudentAcademicHistories",
                column: "ClassSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentInfos_AcademicYearId",
                table: "StudentInfos",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentInfos_ClassesId",
                table: "StudentInfos",
                column: "ClassesId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentInfos_PersonalInfoId",
                table: "StudentInfos",
                column: "PersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentInfos_StudentAcademicHistoryId",
                table: "StudentInfos",
                column: "StudentAcademicHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Thanas_DistrictsId",
                table: "Thanas",
                column: "DistrictsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "FeesCollectionDetails");

            migrationBuilder.DropTable(
                name: "FeesSetups");

            migrationBuilder.DropTable(
                name: "SpecialFees");

            migrationBuilder.DropTable(
                name: "Thanas");

            migrationBuilder.DropTable(
                name: "FeesCollectionMasters");

            migrationBuilder.DropTable(
                name: "FeesNames");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "StudentInfos");

            migrationBuilder.DropTable(
                name: "Divisions");

            migrationBuilder.DropTable(
                name: "PersonalInfos");

            migrationBuilder.DropTable(
                name: "StudentAcademicHistories");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "AcademicYears");

            migrationBuilder.DropTable(
                name: "ClassSection");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Sections");
        }
    }
}
