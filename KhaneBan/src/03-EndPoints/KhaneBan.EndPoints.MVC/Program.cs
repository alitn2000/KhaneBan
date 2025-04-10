using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.InfraStructure.EfCore.Common;
using KhaneBan.InfraStructure.EfCore.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.Extensions.Logging.Configuration;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.AppServices;
using KhaneBan.Domain.Services;
using KhaneBan.Domain.Core.Contracts.AppService;
using Microsoft.Extensions.Options;
using KhaneBan.InfraStructure.Dapper.Common;
using KhaneBan.InfraStructure.Dapper.DapperRepositories;



Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);          ////////////

builder.Services.AddMemoryCache();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() 
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341", apiKey: "m1P3sZ70TIi58jV7WpO9")
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.ConfigureLogging(x =>
{
    x.ClearProviders();
    x.AddSerilog();

}).UseSerilog((context, config) =>
{
    config.WriteTo.Console();
    config.WriteTo.Seq("http://localhost:5341", apiKey: "m1P3sZ70TIi58jV7WpO9");
    config.Enrich.FromLogContext();

});
builder.Services.AddSerilog();

try
{
    // Add services to the container.
    builder.Services.AddControllersWithViews();
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(connectionString, sqlOptions =>
        {
            sqlOptions.CommandTimeout(60); // Set timeout to 180 seconds
        });
    });

    builder.Services.AddSingleton<DapperAppDbContext>();


    // repository
    builder.Services.AddScoped<IAdminRepository, AdminRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<ICityRepository, CityRepository>();
    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
    builder.Services.AddScoped<IExpertRepository, ExpertRepository>();
    builder.Services.AddScoped<IHomeServiceRepository, HomeServiceRepository>();
    builder.Services.AddScoped<IRatingRepository, RatingRepository>();
    builder.Services.AddScoped<IRequestRepository, RequestRepository>();
    builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
    builder.Services.AddScoped<ISuggestionRepository, SuggestionRepository>();
    builder.Services.AddScoped<ICityDapperRepository, CityDapperRepository>();
    builder.Services.AddScoped<ICategoryDapperRepository, CategoryDapperRepository>();
    builder.Services.AddScoped<ISubCategoryDapperRepository, SubCategoryDapperRepository>();
    builder.Services.AddScoped<IHomeServiceDapperRepository,  HomeServiceDapperRepository>();

    //service
    builder.Services.AddScoped<IAdminAccountService, AdminAccountService>();
    builder.Services.AddScoped<IAdminService, AdminService>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
    builder.Services.AddScoped<IHomeServiceService, HomeServiceService>();
    builder.Services.AddScoped<ICustomerService, CustomerService>();
    builder.Services.AddScoped<IExpertService, ExpertService>();
    builder.Services.AddScoped<ICityService, CityService>();
    builder.Services.AddScoped<ICityDapperService, CityDapperService>();
    builder.Services.AddScoped<IRequestService, RequestService>();
    builder.Services.AddScoped<ISuggestionService, SuggestionService>();
    builder.Services.AddScoped<IRatingService, RatingService>();
    builder.Services.AddScoped<IPictureService, PictureService>();
    builder.Services.AddScoped<IAccountService, AccountService>();


    //appservice
    builder.Services.AddScoped<IAdminAccountAppService, AdminAccountAppService>();
    builder.Services.AddScoped<IAdminAppService, AdminAppService>();
    builder.Services.AddScoped<ICustomerAppService, CustomerAppService>();
    builder.Services.AddScoped<IExpertAppService, ExpertAppService>();
    builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
    builder.Services.AddScoped<ISubCategoryAppService, SubCategoryAppService>();
    builder.Services.AddScoped<IHomeServiceAppService, HomeServiceAppService>();
    builder.Services.AddScoped<ICityAppService, CityAppService>();
    builder.Services.AddScoped<ICityDapperAppService, CityDapperAppService>();
    builder.Services.AddScoped<IRequestAppService, RequestAppService>();
    builder.Services.AddScoped<ISuggestionAppService, SuggestionAppService>();
    builder.Services.AddScoped<IRatingAppService, RatingAppService>();
    builder.Services.AddScoped<IPictureAppService, PictureAppService>();
    builder.Services.AddScoped<IAccountAppService, AccountAppService>();

    builder.Services.AddScoped<IHomeServiceDapperService, HomeServiceDapperService>();
    builder.Services.AddScoped<ISubCategoryDapperService, SubCategoryDapperService>();
    builder.Services.AddScoped<ICategoryDapperService, CategoryDapperService>();


    builder.Services.AddScoped<IHomeServiceDapperAppService, HomeServiceDapperAppService>();
    builder.Services.AddScoped<ISubCategoryDapperAppService, SubCategoryDapperAppService>();
    builder.Services.AddScoped<ICategoryDapperAppService, CategoryDapperAppService>();


    builder.Services.AddIdentity<User, IdentityRole<int>>
        (options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 4;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddRoles<IdentityRole<int>>()
        .AddEntityFrameworkStores<AppDbContext>();

    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/Users/Account/Login";
    });


    var app = builder.Build();
    app.UseStaticFiles();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseRouting();


    app.UseAuthorization();

    app.MapStaticAssets();

    app.MapAreaControllerRoute(
    name: "areas",
    areaName: "Users",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    app.MapAreaControllerRoute(
    name: "areas",
    areaName: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();


    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Logic Error in program.cs");
}
finally
{
    Log.CloseAndFlush();
}
