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
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
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
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    HomeServiceId = table.Column<int>(type: "int", nullable: false)
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Requests_HomeServices_HomeServiceId",
                        column: x => x.HomeServiceId,
                        principalTable: "HomeServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                columns: new[] { "Id", "IsDeleted", "RegisterAt", "Title" },
                values: new object[,]
                {
                    { 1, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "تمیزکاری" },
                    { 2, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "ساختمان" },
                    { 3, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "تعمیرات اشیا" },
                    { 4, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "اسباب کشی و حمل بار" },
                    { 5, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "خودرو" },
                    { 6, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "سلامت و زیبایی" },
                    { 7, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "سازمان ها و مجتمع ها" },
                    { 8, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "سایر" }
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
                columns: new[] { "Id", "AccessFailedCount", "Address", "Balance", "CityId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PicturePath", "RegisterDate", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "test1", 10000.0, 1, "1168BED7-A787-44E1-A869-7D150A038915", "Admin@gmail.com", false, "Admin", "Admin", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEOmDtcVs3Rkk6RMUA+5z0wZ9Neipy/C5IXwjy+nfg7U75/p+PU0vuQqooMhZXmxITA==", "09102123542", false, "desktop", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "1168BED7-A787-44E1-A869-7D150A038916", false, "Admin" },
                    { 2, 0, "test2", 20000.0, 1, "5780E9A6-7966-48F0-AC09-20FA8EA4B212", "alitn2000@gmail.com", false, "Ali", "Tahmasebinia", false, null, "ALITN2000@GMAIL.COM", "ALITN2000", "AQAAAAIAAYagAAAAEPeBBZ4pQC4H/DOwgQDeTtJR2rsxmD8X8X53Lc/AeSYrOpLaV2z6D5zt0J7YMukCfA==", "09022004453", false, "desktop1", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "5780E9A6-7966-48F0-AC09-20FA8EA4B213", false, "alitn2000" },
                    { 3, 0, "test3", 20000.0, 1, "3FEB408E-2E7D-4BB9-B80C-12A88348057D", "reza2000@gmail.com", false, "Reza", "Rezaei", false, null, "REZA2000@GMAIL.COM", "REZA2000", "AQAAAAIAAYagAAAAEL7r3uolUUXIJfES5jnSuZkpCilWHgO+Ask3O7jCxtsyExzzITOWDM2BpVzTU9SR2Q==", "09102123543", false, "desktop2", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "3FEB408E-2E7D-4BB9-B80C-12A88348057E", false, "reza2000" },
                    { 4, 0, "test4", 20000.0, 1, "1DBE15F3-BB61-4FC0-87EE-5383DC66CF51", "sara2000@gmail.com", false, "Sara", "Saraei", false, null, "SARA2000@GMAIL.COM", "SARA2000", "AQAAAAIAAYagAAAAEBvbRpxi0eHTD1AdIhIuTfwobiHa6wrspUVi4L3RvZ0aemhApxbK+c35pfH8Ozn5rQ==", "09102123545", false, "desktop3", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "1DBE15F3-BB61-4FC0-87EE-5383DC66CF52", false, "sara2000" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "IsDeleted", "PicturePath", "RegisterAt", "Title" },
                values: new object[,]
                {
                    { 1, 1, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "نظافت و پذیرایی" },
                    { 2, 1, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "شستشو" },
                    { 3, 1, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "کارواش و دیتیلینگ" },
                    { 4, 2, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "سرمایش و گرمایش" },
                    { 5, 2, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "تعمیرات ساختمان" },
                    { 6, 2, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "لوله کشی" },
                    { 7, 2, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "طراحی و بازسازی ساختمان" },
                    { 8, 2, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "باغبانی و فضای سبز" },
                    { 9, 2, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "چوب و کابینت" },
                    { 10, 3, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "نصب و تعمیر لوازم خانگی" },
                    { 11, 3, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "خدمات کامپیوتری" },
                    { 12, 3, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "تعمیرات موبایل" },
                    { 13, 4, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "باربری و جابجایی" },
                    { 14, 5, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "خدمات و تعمیرات خودرو" },
                    { 15, 6, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "زیبایی بانوان" },
                    { 16, 6, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "پزشکی و پرستاری" },
                    { 17, 6, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "حیوانات خانگی" },
                    { 18, 6, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "تندرستی و ورزش" },
                    { 19, 7, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "خدمات شرکتی" },
                    { 20, 7, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "تامین نیروی انسانی" },
                    { 21, 8, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "خیاطی و تعمیرات لباس" },
                    { 22, 8, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "مجالس و رویدادها" },
                    { 23, 8, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "آموزش" },
                    { 24, 8, false, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "کودک" }
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
                    { 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "Experts",
                columns: new[] { "Id", "UserId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "HomeServices",
                columns: new[] { "Id", "BasePrice", "ImagePath", "IsDeleted", "RegisterAt", "SubCategoryId", "Title", "VisitCount" },
                values: new object[,]
                {
                    { 1, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "نقاشی", 120 },
                    { 2, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "نظافت راه پله", 120 },
                    { 3, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "قالیشویی", 120 },
                    { 4, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "پرده شویی", 120 },
                    { 5, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "سرامیک خودرو", 120 },
                    { 6, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "صفرشویی خودرو", 120 },
                    { 7, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "تعمیر و سرویس کولر آبی", 120 },
                    { 8, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "کانال سازی کولر", 120 },
                    { 9, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "تعمیر و نگهداری موتورخانه", 120 },
                    { 10, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "سنگ کاری", 120 },
                    { 11, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "بنایی", 120 },
                    { 12, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "کلیدسازی", 120 },
                    { 13, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "کفسابی", 120 },
                    { 14, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "خدمات لوله کشی ساختمان", 120 },
                    { 15, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "تخلیه چاه و لوله بازکنی", 120 },
                    { 16, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "لوله کشی آب و فاضلاب", 120 },
                    { 17, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "مشاوره و بازسازی ساختمان", 120 },
                    { 18, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "دکوراسیون و طراحی ساختمان", 120 },
                    { 19, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "خدمات باغبانی", 120 },
                    { 20, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "کاشت و تعویض گلدان", 120 },
                    { 21, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "تعمیرات مبلمان", 120 },
                    { 22, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "تعمیرات مبلمان اداری", 120 },
                    { 23, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "تعمیر پنکه", 120 },
                    { 24, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "نصب و تعمیر فر", 120 },
                    { 25, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "تعمیر کامپیوتر و لپ تاپ", 120 },
                    { 26, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "مودم و اینترنت", 120 },
                    { 27, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "خدمات تعمیر موبایل", 120 },
                    { 28, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "خدمات خرید موبایل و کالاهای دیجیتال", 120 },
                    { 29, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "خدمات دوربین", 120 },
                    { 30, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, "اسباب کشی با خاور و کامیون", 120 },
                    { 31, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, "اسباب کشی با وانت و نیسان", 120 },
                    { 32, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, "کارگر جابه جایی", 120 },
                    { 33, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, "تعویض باتری خودرو", 120 },
                    { 34, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, "باتری به باتری", 120 },
                    { 35, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, "حمل خودرو", 120 },
                    { 36, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, "تعویض وایر و شمع خودرو", 120 },
                    { 37, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "براشینگ موی بانوان", 120 },
                    { 38, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "کوتاهی موی بانوان", 120 },
                    { 39, 100.0, null, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "بافت موی بانوان در خانه", 120 }
                });

            migrationBuilder.InsertData(
                table: "ExpertHomeService",
                columns: new[] { "ExpertsId", "HomeServicesId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "Comment", "CustomerId", "ExpertId", "IsDeleted", "Rate", "RegisterDate", "Status" },
                values: new object[,]
                {
                    { 1, "اوکی", 1, 1, false, 5, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 2, "اوکی", 2, 1, false, 5, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "CityId", "CustomerId", "Description", "EndTime", "HomeServiceId", "IsDeleted", "RegisterDate", "RequestStatus", "StartTime", "Title" },
                values: new object[,]
                {
                    { 1, 1, 1, "نقاشی 4 طبقه", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "نقاشی" },
                    { 2, 1, 2, "نقاشی 2 طبقه", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "نقاشی" }
                });

            migrationBuilder.InsertData(
                table: "Suggestions",
                columns: new[] { "Id", "DeliveryDate", "Description", "ExpertId", "IsDeleted", "Price", "RegisterDate", "RequestId", "SuggestionStatus" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "ارزون", 1, false, 5000.0, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "گرون", 1, false, 6000.0, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2 }
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
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Experts");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "HomeServices");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
