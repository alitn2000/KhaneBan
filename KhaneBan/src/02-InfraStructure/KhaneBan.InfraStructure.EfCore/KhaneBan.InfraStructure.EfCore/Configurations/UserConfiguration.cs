using KhaneBan.Domain.Core.Entites.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KhaneBan.InfraStructure.EfCore.Configurations;

public class UserConfiguration
{
    public static void Configuration(ModelBuilder modelBuilder)
    {
        

        modelBuilder.Entity<User>()
              .HasOne(u => u.Admin)
              .WithOne(a => a.User)
              .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<User>()
                .HasOne(u => u.Customer)
                .WithOne(c => c.User)
                .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<User>()
              .HasOne(u => u.Expert)
             .WithOne(e => e.User)
             .OnDelete(DeleteBehavior.NoAction);



        var passHasher = new PasswordHasher<User>();

        modelBuilder.Entity<IdentityRole<int>>().HasData(
            new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole<int> { Id = 2, Name = "Expert", NormalizedName = "EXPERT" },
            new IdentityRole<int> { Id = 3, Name = "Customer", NormalizedName = "CUSTOMER" }
        );

        var adminUser = new User
        {
            Id = 1,
            FirstName = "Admin",
            LastName = "Admin",
            Address = "test1",
            Balance = 10000,
            PicturePath = "desktop",
            CityId = 1,
            RegisterDate = new DateTime(2025, 2, 2, 0, 0, 0),
            UserName = "Admin",
            NormalizedUserName = "ADMIN",
            Email = "Admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            PhoneNumber = "09102123542",
            LockoutEnabled = false,
            SecurityStamp = "1168BED7-A787-44E1-A869-7D150A038916",
            ConcurrencyStamp = "1168BED7-A787-44E1-A869-7D150A038915"
        };
        adminUser.PasswordHash = passHasher.HashPassword(adminUser, "1234");

        var user1 = new User
        {
            Id = 2,
            FirstName = "Ali",
            LastName = "Tahmasebinia",
            Address = "test2",
            Balance = 20000,
            PicturePath = "desktop1",
            CityId = 1,
            RegisterDate = new DateTime(2025, 2, 2, 0, 0, 0),
            UserName = "alitn2000",
            NormalizedUserName = "ALITN2000",
            Email = "alitn2000@gmail.com",
            NormalizedEmail = "ALITN2000@GMAIL.COM",
            PhoneNumber = "09022004453",
            LockoutEnabled = false,
            SecurityStamp = "5780E9A6-7966-48F0-AC09-20FA8EA4B213",
            ConcurrencyStamp = "5780E9A6-7966-48F0-AC09-20FA8EA4B212"
        };
        user1.PasswordHash = passHasher.HashPassword(user1, "1234");

        var user2 = new User
        {
            Id = 3,
            FirstName = "Reza",
            LastName = "Rezaei",
            Address = "test3",
            Balance = 20000,
            PicturePath = "desktop2",
            CityId = 1,
            RegisterDate = new DateTime(2025, 2, 2, 0, 0, 0),
            UserName = "reza2000",
            NormalizedUserName = "REZA2000",
            Email = "reza2000@gmail.com",
            NormalizedEmail = "REZA2000@GMAIL.COM",
            PhoneNumber = "09102123543",
            LockoutEnabled = false,
            SecurityStamp = "3FEB408E-2E7D-4BB9-B80C-12A88348057E",
            ConcurrencyStamp = "3FEB408E-2E7D-4BB9-B80C-12A88348057D",
        };
        user2.PasswordHash = passHasher.HashPassword(user2, "1234");

        var user3 = new User
        {
            Id = 4,
            FirstName = "Sara",
            LastName = "Saraei",
            Address = "test4",
            Balance = 20000,
            PicturePath = "desktop3",
            CityId = 1,
            RegisterDate = new DateTime(2025, 2, 2, 0, 0, 0),
            UserName = "sara2000",
            NormalizedUserName = "SARA2000",
            Email = "sara2000@gmail.com",
            NormalizedEmail = "SARA2000@GMAIL.COM",
            PhoneNumber = "09102123545",
            LockoutEnabled = false,
            SecurityStamp = "1DBE15F3-BB61-4FC0-87EE-5383DC66CF52",
            ConcurrencyStamp = "1DBE15F3-BB61-4FC0-87EE-5383DC66CF51",
        };
        user3.PasswordHash = passHasher.HashPassword(user3, "1234");

        modelBuilder.Entity<User>().HasData(adminUser, user1, user2, user3);

        modelBuilder.Entity<IdentityUserRole<int>>().HasData(
            new IdentityUserRole<int> { RoleId = 1, UserId = 1 },
            new IdentityUserRole<int> { RoleId = 2, UserId = 2 },
            new IdentityUserRole<int> { RoleId = 3, UserId = 3 },
            new IdentityUserRole<int> { RoleId = 3, UserId = 4 }
        );



       
    }



}
