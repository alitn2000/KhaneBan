using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.Domain.Core.Enums;
namespace KhaneBan.InfraStructure.EfCore.Configurations;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {


        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description)
            .HasMaxLength(500);


        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Requests)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);




        builder.HasData(
        new Request
        {
            Id = 1,
            Title = "نقاشی",
            Description ="نقاشی 4 طبقه",
            RegisterDate = new DateTime(2025, 2, 2, 0, 0, 0),
            EndTime = new DateTime(2025, 2, 2, 0, 0, 0),
            IsDeleted = false,
            CityId = 1,
            CustomerId = 1,
            HomeServiceId = 1,
            RequestStatus = StatusEnum.WatingForChoosingExpert
        },
        new Request
        {
            Id = 2,
            Title = "نقاشی",
            Description = "نقاشی 2 طبقه",
            RegisterDate = new DateTime(2025, 2, 2, 0, 0, 0),
            EndTime = new DateTime(2025, 2, 2, 0, 0, 0),
            IsDeleted = false,
            CityId = 1,
            CustomerId = 2,
            HomeServiceId = 1,
            RequestStatus = StatusEnum.WatingForChoosingExpert
        }
        );

    }
}
