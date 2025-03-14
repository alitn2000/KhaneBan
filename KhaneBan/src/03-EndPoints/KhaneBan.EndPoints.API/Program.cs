using KhaneBan.Domain.AppServices;
using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.Domain.Services;
using KhaneBan.EndPoints.API.WebFrameWork.ActionFilters;
using KhaneBan.InfraStructure.Dapper.Common;
using KhaneBan.InfraStructure.Dapper.DapperRepositories;
using KhaneBan.InfraStructure.EfCore.Common;
using KhaneBan.InfraStructure.EfCore.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ICategoryDapperRepository, CategoryDapperRepository>();
builder.Services.AddScoped<ISubCategoryDapperRepository, SubCategoryDapperRepository>();
builder.Services.AddScoped<IHomeServiceDapperRepository, HomeServiceDapperRepository>();


builder.Services.AddScoped<IHomeServiceDapperService, HomeServiceDapperService>();
builder.Services.AddScoped<ISubCategoryDapperService, SubCategoryDapperService>();
builder.Services.AddScoped<ICategoryDapperService, CategoryDapperService>();


builder.Services.AddScoped<IHomeServiceDapperAppService, HomeServiceDapperAppService>();
builder.Services.AddScoped<ISubCategoryDapperAppService, SubCategoryDapperAppService>();
builder.Services.AddScoped<ICategoryDapperAppService, CategoryDapperAppService>();


builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IExpertRepository, ExpertRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountAppService, AccountAppService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IExpertService, ExpertService>();
builder.Services.AddScoped<IRequestService, RequestService>();

builder.Services.AddScoped<IRequestAppService, RequestAppService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
builder.Services.AddMemoryCache();
//appservice

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

builder.Services.AddScoped<DapperAppDbContext>();

builder.Services.AddSingleton<ApiKeyCheck>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.CommandTimeout(60); // Set timeout to 180 seconds
    });
});
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
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger(opt =>
{
    opt.RouteTemplate = "openapi/{documentName}.json";
});
app.MapScalarApiReference(opt =>
{
    opt.Title = "Scalar Example";
    opt.Theme = ScalarTheme.DeepSpace;
    opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
});
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
