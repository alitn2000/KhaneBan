using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.InfraStructure.EfCore.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace KhaneBan.InfraStructure.EfCore.Common;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings =>
        warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        base.OnConfiguring(optionsBuilder);

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        UserConfiguration.Configuration(modelBuilder);
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new HomeServiceConfiguration());
        modelBuilder.ApplyConfiguration(new PictureConfigurations());
        modelBuilder.ApplyConfiguration(new SubcategoryConfiguration());
        modelBuilder.ApplyConfiguration(new RequestConfiguration());
        modelBuilder.ApplyConfiguration(new SuggestionConfiguration());
        modelBuilder.ApplyConfiguration(new RatingConfiguration());
        modelBuilder.ApplyConfiguration(new AdminConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new ExpertConfiguration());


        base.OnModelCreating(modelBuilder);


    }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Expert> Experts { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Picture> Pictures { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Suggestion> Suggestions { get; set; }
    public DbSet<HomeService> HomeServices { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }

}

