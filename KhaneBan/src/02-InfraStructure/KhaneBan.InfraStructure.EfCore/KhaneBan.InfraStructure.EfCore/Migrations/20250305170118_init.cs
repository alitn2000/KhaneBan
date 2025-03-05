using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KhaneBan.InfraStructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PicturePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisterAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    PicturePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RegisterAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    PicturePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HomeServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    VisitCount = table.Column<int>(type: "int", nullable: false),
                    PicturePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BasePrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeServices_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Experts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExpertHomeService",
                columns: table => new
                {
                    ExpertsId = table.Column<int>(type: "int", nullable: false),
                    HomeServicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertHomeService", x => new { x.ExpertsId, x.HomeServicesId });
                    table.ForeignKey(
                        name: "FK_ExpertHomeService_Experts_ExpertsId",
                        column: x => x.ExpertsId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertHomeService_HomeServices_HomeServicesId",
                        column: x => x.HomeServicesId,
                        principalTable: "HomeServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ratings_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    RatingId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    HomeServiceId = table.Column<int>(type: "int", nullable: false),
                    RequestImages = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Requests_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_HomeServices_HomeServiceId",
                        column: x => x.HomeServiceId,
                        principalTable: "HomeServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pictures_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SuggestionStatus = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suggestions_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Suggestions_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Admin", "ADMIN" },
                    { 2, null, "Expert", "EXPERT" },
                    { 3, null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsDeleted", "PicturePath", "RegisterAt", "Title" },
                values: new object[,]
                {
                    { 1, false, "/images/Categories/tamizkari.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "تمیزکاری" },
                    { 2, false, "/images/Categories/sakhteman.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "ساختمان" },
                    { 3, false, "/images/Categories/tamirat_ashya.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "تعمیرات اشیا" },
                    { 4, false, "/images/Categories/asbabkeshi.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "اسباب کشی و حمل بار" },
                    { 5, false, "/images/category/khodro.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "خودرو" },
                    { 6, false, "/images/Categories/salamat_zibayi.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "سلامت و زیبایی" },
                    { 7, false, "/images/Categories/sazmanha_va_mojtamha.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "سازمان ها و مجتمع ها" },
                    { 8, false, "/images/Categories/sayer.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "سایر" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "تهران" },
                    { 2, "مشهد" },
                    { 3, "اصفهان" },
                    { 4, "شیراز" },
                    { 5, "تبریز" },
                    { 6, "کرج" },
                    { 7, "قم" },
                    { 8, "اهواز" },
                    { 9, "اردبیل" },
                    { 10, "کرمانشاه" },
                    { 11, "زاهدان" },
                    { 12, "ارومیه" },
                    { 13, "یزد" },
                    { 14, "همدان" },
                    { 15, "قزوین" },
                    { 16, "سنندج" },
                    { 17, "بندرعباس" },
                    { 18, "زنجان" },
                    { 19, "ساری" },
                    { 20, "بوشهر" },
                    { 21, "مازندران" },
                    { 22, "گرگان" },
                    { 23, "کرمان" },
                    { 24, "خرم آباد" },
                    { 25, "سمنان" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "Balance", "CityId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PicturePath", "RegisterDate", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "test1", 10000.0, 1, "1168BED7-A787-44E1-A869-7D150A038915", "Admin@gmail.com", false, "Admin", false, "Admin", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEKWY6yw5aR8hmBvumbMpzbaRa3EazJvyTUAaIh0skvw7CnIY4yStqcV4ITwbAuvoWw==", "09102123542", false, "desktop", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "1168BED7-A787-44E1-A869-7D150A038916", false, "Admin" },
                    { 2, 0, "test2", 20000.0, 1, "5780E9A6-7966-48F0-AC09-20FA8EA4B212", "alitn2000@gmail.com", false, "Ali", false, "Tahmasebinia", false, null, "ALITN2000@GMAIL.COM", "ALITN2000", "AQAAAAIAAYagAAAAEHYMFXlYYWIJFvCqk3V358dmgxdD0hU2pQYVw5SIRnxWqePfiI1/OPOa51aU57XsSw==", "09022004453", false, "desktop1", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "5780E9A6-7966-48F0-AC09-20FA8EA4B213", false, "alitn2000" },
                    { 3, 0, "test3", 20000.0, 1, "3FEB408E-2E7D-4BB9-B80C-12A88348057D", "reza2000@gmail.com", false, "Reza", false, "Rezaei", false, null, "REZA2000@GMAIL.COM", "REZA2000", "AQAAAAIAAYagAAAAEM67/wyH+QkqFhhXV7rnMch++J1C+fA9hcnBUrknljxvhbPXkA3sUDvieilOqRmnqw==", "09102123543", false, "desktop2", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "3FEB408E-2E7D-4BB9-B80C-12A88348057E", false, "reza2000" },
                    { 4, 0, "test4", 20000.0, 1, "1DBE15F3-BB61-4FC0-87EE-5383DC66CF51", "sara2000@gmail.com", false, "Sara", false, "Saraei", false, null, "SARA2000@GMAIL.COM", "SARA2000", "AQAAAAIAAYagAAAAEG+aJYqhKqwjDpTdUZO1ETPT+mHdwSEXzGk+t4qA+eJC3aCmG4YZnKVocAlGlCoT7Q==", "09102123545", false, "desktop3", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "1DBE15F3-BB61-4FC0-87EE-5383DC66CF52", false, "sara2000" },
                    { 5, 0, "test2", 20000.0, 1, "5780E9A6-7966-48F0-AC09-20FA8EA4B212", "expert12000@gmail.com", false, "expert1", false, "expertinia", false, null, " EXPERT12000@GMAIL.COM", "EXPERT12000", "AQAAAAIAAYagAAAAEJcCkmhWXTXzIOx2/45Z24dOqozKz2rhJQx1ffBG/V/tjFAh8mgHFuDXEdFf1D1UFQ==", "09102123541", false, "desktop1", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "5780E9A6-7966-48F0-AC09-20FA8EA4B213", false, "expert12000" },
                    { 6, 0, "test5", 20000.0, 1, "5780E9A6-7966-48F0-AC09-20FA8EA4B212", "expert22000@gmail.com", false, "expert2", false, "expertinia", false, null, " EXPERT22000@GMAIL.COM", "EXPERT22000", "AQAAAAIAAYagAAAAEPNIil5bNMBD7a6et28t+wkjqrghmCFFrNV3f4KJj1aJxUktD3S7MRpmtjNFIQotqA==", "09102123542", false, "desktop1", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "5780E9A6-7966-48F0-AC09-20FA8EA4B213", false, "expert22000" },
                    { 7, 0, "test7", 20000.0, 1, "5780E9A6-7966-48F0-AC09-20FA8EA4B212", "customer12000@gmail.com", false, "customer1", false, "customernia", false, null, "CUSTOMER12000@GMAIL.COM", "CUSTOMER12000", "AQAAAAIAAYagAAAAEANrkFM0yp7B4yZ8UToR1Hb0V2lEzhSGOelb96CXTTx/Hdz+JDgFtzLvf51G3LAvnQ==", "09102123555", false, "desktop1", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "5780E9A6-7966-48F0-AC09-20FA8EA4B213", false, "customer12000" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "IsDeleted", "PicturePath", "RegisterAt", "Title" },
                values: new object[,]
                {
                    { 1, 1, false, "/images/subcategpries/nezafat_pazirayi.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "نظافت و پذیرایی" },
                    { 2, 1, false, "/images/subcategpries/shostosho.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "شستشو" },
                    { 3, 1, false, "/images/subsubcategpriescategory/karvash_detailing", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "کارواش و دیتیلینگ" },
                    { 4, 2, false, "/images/subcategpries/sarmayesh_garmayesh", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "سرمایش و گرمایش" },
                    { 5, 2, false, "/images/subcategpries/tamirat_sakhteman", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "تعمیرات ساختمان" },
                    { 6, 2, false, "/images/subcategpries/lolekeshi", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "لوله کشی" },
                    { 7, 2, false, "/images/subcategpries/tarahi_bazsazi.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "طراحی و بازسازی ساختمان" },
                    { 8, 2, false, "/images/subcategpries/baqbani_fazayesabz.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "باغبانی و فضای سبز" },
                    { 9, 2, false, "/images/subcategpries/choob_kabinet.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "چوب و کابینت" },
                    { 10, 3, false, "/images/subcategpries/nasab_tamir_lavazem.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "نصب و تعمیر لوازم خانگی" },
                    { 11, 3, false, "/images/subcategpries/khadamt_cp.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "خدمات کامپیوتری" },
                    { 12, 3, false, "/images/subcategpries/tamirat_mobile.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "تعمیرات موبایل" },
                    { 13, 4, false, "/images/subcategpries/barbari_jabejayi.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "باربری و جابجایی" },
                    { 14, 5, false, "/images/subcategpries/khadamat_khodro.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "خدمات و تعمیرات خودرو" },
                    { 15, 6, false, "/images/subcategpries/zibayi_banovan.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "زیبایی بانوان" },
                    { 16, 6, false, "/images/subcategpries/pezeshki_parastari.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "پزشکی و پرستاری" },
                    { 17, 6, false, "/images/subcategpries/heyvanat_khanegi.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "حیوانات خانگی" },
                    { 18, 6, false, "/images/subcategpries/tandorosti_varzesh.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "تندرستی و ورزش" },
                    { 19, 7, false, "/images/subcategpries/khadamat_sherkati.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "خدمات شرکتی" },
                    { 20, 7, false, "/images/subcategpries/tamin_niroye_ensani.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "تامین نیروی انسانی" },
                    { 21, 8, false, "/images/subcategpries/khayati_tamirat.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "خیاطی و تعمیرات لباس" },
                    { 22, 8, false, "/images/subcategpries/majales_roydad.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "مجالس و رویدادها" },
                    { 23, 8, false, "/images/subcategpries/amozesh.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "آموزش" },
                    { 24, 8, false, "/images/subcategpries/kodak.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "کودک" }
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 3, 4 },
                    { 2, 5 },
                    { 2, 6 },
                    { 3, 7 }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 2, 4 },
                    { 3, 7 }
                });

            migrationBuilder.InsertData(
                table: "Experts",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 5 },
                    { 3, 6 }
                });

            migrationBuilder.InsertData(
                table: "HomeServices",
                columns: new[] { "Id", "BasePrice", "Description", "IsDeleted", "PicturePath", "RegisterAt", "SubCategoryId", "Title", "VisitCount" },
                values: new object[,]
                {
                    { 1, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/nezafat_manzel.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "خدمات نظافت منزل", 210 },
                    { 2, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/nezafat_rahpele.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "نظافت راه پله", 210 },
                    { 3, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/ghalishoyi.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "قالیشویی", 210 },
                    { 4, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/pardeshoyi.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "پرده شویی", 210 },
                    { 5, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/seramik_khodro.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "سرامیک خودرو", 210 },
                    { 6, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/sefrshoyi_khodro.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "صفرشویی خودرو", 210 },
                    { 7, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/tamir_coolerabi.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "تعمیر و سرویس کولر آبی", 210 },
                    { 8, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/kanalsazi_cooler.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "کانال سازی کولر", 210 },
                    { 9, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/tamir_motorkhane.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "تعمیر و نگهداری موتورخانه", 210 },
                    { 10, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/sangkari.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "سنگ کاری", 210 },
                    { 11, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/banayi.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "بنایی", 210 },
                    { 12, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/klidsazi.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "کلیدسازی", 210 },
                    { 13, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/kafsabi.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "کفسابی", 210 },
                    { 14, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/lolekeshi.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "خدمات لوله کشی ساختمان", 210 },
                    { 15, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/lolebazkoni.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "تخلیه چاه و لوله بازکنی", 210 },
                    { 16, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/lolekeshi_fazelab.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "لوله کشی آب و فاضلاب", 210 },
                    { 17, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/moshavere_bazsazi_sakhteman.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "مشاوره و بازسازی ساختمان", 210 },
                    { 18, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/dekorasion_sakhteman.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "دکوراسیون و طراحی ساختمان", 210 },
                    { 19, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/khadamat_baqbani.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "خدمات باغبانی", 210 },
                    { 20, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/kasht_goldan.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "کاشت و تعویض گلدان", 210 },
                    { 21, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/tamirat_mobleman.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "تعمیرات مبلمان", 210 },
                    { 22, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/tamirat_mobleman_edari.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "تعمیرات مبلمان اداری", 210 },
                    { 23, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/tamir_panke.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "تعمیر پنکه", 210 },
                    { 24, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/nasb_va_tamir_fer.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "نصب و تعمیر فر", 210 },
                    { 25, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/tamir_laptop.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "تعمیر کامپیوتر و لپ تاپ", 210 },
                    { 26, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/modem_va_internet.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "مودم و اینترنت", 210 },
                    { 27, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/tamirat_mobile.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "خدمات تعمیر موبایل", 210 },
                    { 28, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/khadamt_kharid_mobile.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "خدمات خرید موبایل و کالاهای دیجیتال", 210 },
                    { 29, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/khadamat_dorbin.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "خدمات دوربین", 210 },
                    { 30, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/asbabkeshi_ba_khavar.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, "اسباب کشی با خاور و کامیون", 210 },
                    { 31, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/asabkeshi_ba_neysan.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, "اسباب کشی با وانت و نیسان", 210 },
                    { 32, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/kargar_jabejayi.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, "کارگر جابه جایی", 210 },
                    { 33, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/taviz_batri_khodro.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, "تعویض باتری خودرو", 210 },
                    { 34, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/batri_be_batri.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, "باتری به باتری", 210 },
                    { 35, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/haml_khodro.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, "حمل خودرو", 210 },
                    { 36, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/taviz_vayer_sham_khodro.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, "تعویض وایر و شمع خودرو", 210 },
                    { 37, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/berashing_moye_banovan.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "براشینگ موی بانوان", 210 },
                    { 38, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/kotahi_moye_banovan.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "کوتاهی موی بانوان", 210 },
                    { 39, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/baft_moye_banovan.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "بافت موی بانوان در خانه", 210 },
                    { 40, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/moraqebat_negahdari.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, "مراقبت و نگهداری", 210 },
                    { 41, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/moayene_pezeshki.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, "معاینه پزشکی", 210 },
                    { 42, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/pirapezeshki.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, "پیراپزشکی", 210 },
                    { 43, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/petshop.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, "پت شاپ", 210 },
                    { 44, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/khadamat_dampezshki.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, "خدمات دامپزشکی در محل", 210 },
                    { 45, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/khadamt_yoga.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, "کلاس یوگا در خانه", 210 },
                    { 46, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/kelas_polates.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, "کلاس پیلاتس در خانه", 210 },
                    { 47, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/khadamat_achare.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, "پیشنهاد فروش خدمات آچاره به شرکت ها", 210 },
                    { 48, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/estekhdam_niroye_khedmatkar.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "استخدام نیروی خدمتکار", 210 },
                    { 49, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/tamirat_lebas.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, "تعمیرات لباس", 210 },
                    { 50, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/dokht_lebas_zanane.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, "دوخت لباس زنانه", 210 },
                    { 51, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/tamir_kifokafsh.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, "تعمیر کیف و کفش", 210 },
                    { 52, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/keyko_shirini.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, "کیک و شیرینی", 210 },
                    { 53, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/dekor_tavalod.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, "دکور تولد", 210 },
                    { 54, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/gol_arayi.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, "گل آرایی", 210 },
                    { 55, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/finger_food.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, "فینگرفود", 210 },
                    { 56, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/amozesh_zaban_khareji.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 23, "آموزش زبان های خارجی", 210 },
                    { 57, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/ebtedayi_motevasete.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 23, "آموزش ابتدایی تا متوسطه", 210 },
                    { 58, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/kotahi_moye_kodak.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, "کوتاهی موی کودک", 210 },
                    { 59, 2000.0, "Lorem ipsum lorem ipsum", false, "/images/HomeServices/tarahi_otaq_kodak.jpg", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, "طراحی و دیزاین اتاق کودک", 210 }
                });

            migrationBuilder.InsertData(
                table: "ExpertHomeService",
                columns: new[] { "ExpertsId", "HomeServicesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "Comment", "CustomerId", "ExpertId", "IsAccepted", "IsDeleted", "Rate", "RegisterDate" },
                values: new object[,]
                {
                    { 1, "اوکی", 1, 1, null, false, 5.0, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "اوکی", 2, 1, null, false, 5.0, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "CityId", "CustomerId", "Description", "HomeServiceId", "IsDeleted", "RatingId", "RegisterDate", "RequestImages", "RequestStatus", "RequestedDate", "StartTime", "Title" },
                values: new object[,]
                {
                    { 3, 1, 3, "نظافت راه پله ساختمان 4 طبقه", 2, false, null, new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "نظافت راه پله" },
                    { 1, 1, 1, "نقاشی 4 طبقه", 1, false, 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "نقاشی" },
                    { 2, 1, 2, "نقاشی 2 طبقه", 1, false, 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "نقاشی" }
                });

            migrationBuilder.InsertData(
                table: "Suggestions",
                columns: new[] { "Id", "Description", "ExpertId", "IsDeleted", "Price", "RegisterDate", "RequestId", "StartDate", "SuggestionStatus" },
                values: new object[,]
                {
                    { 3, "قیمت مناسب میگیرم", 2, false, 2500.0, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 4, "قیمت مناسب تر میگیرم", 3, false, 2100.0, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 1, "ارزون", 1, false, 6000.0, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 2, "گرون", 1, false, 6000.0, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId",
                table: "Admins",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpertHomeService_HomeServicesId",
                table: "ExpertHomeService",
                column: "HomeServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_Experts_UserId",
                table: "Experts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomeServices_SubCategoryId",
                table: "HomeServices",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_RequestId",
                table: "Pictures",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CustomerId",
                table: "Ratings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ExpertId",
                table: "Ratings",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CityId",
                table: "Requests",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CustomerId",
                table: "Requests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_HomeServiceId",
                table: "Requests",
                column: "HomeServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RatingId",
                table: "Requests",
                column: "RatingId",
                unique: true,
                filter: "[RatingId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_ExpertId",
                table: "Suggestions",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_RequestId",
                table: "Suggestions",
                column: "RequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ExpertHomeService");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "HomeServices");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Experts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
