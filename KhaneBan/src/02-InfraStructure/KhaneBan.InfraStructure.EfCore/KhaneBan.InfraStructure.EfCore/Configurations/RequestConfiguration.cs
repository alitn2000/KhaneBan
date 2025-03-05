using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.Domain.Core.Enums;
using KhaneBan.Domain.Core.Entites.User;
using System.Reflection.Emit;
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
            .OnDelete(DeleteBehavior.Restrict);

        builder
                .HasOne(u => u.Rating)
                .WithOne(c => c.Request)
                .OnDelete(DeleteBehavior.NoAction);



        builder.HasData(
        new Request
        {
            Id = 1,
            Title = "نقاشی",
            Description ="نقاشی 4 طبقه",
            RegisterDate = new DateTime(2025, 2, 2, 0, 0, 0),
            RequestedDate = new DateTime(2025, 2, 10, 0, 0, 0),
            IsDeleted = false,
            CityId = 1,
            RatingId = 1,
            CustomerId = 1,
            HomeServiceId = 1,
            RequestStatus = StatusEnum.WorkPaidByCustomer
        },
        new Request
        {
            Id = 2,
            Title = "نقاشی",
            Description = "نقاشی 2 طبقه",
            RegisterDate = new DateTime(2025, 2, 2, 0, 0, 0),
            RequestedDate = new DateTime(2025, 2, 10, 0, 0, 0),
            RatingId = 2,
            IsDeleted = false,
            CityId = 1,
            CustomerId = 2,
            HomeServiceId = 1,
            RequestStatus = StatusEnum.WorkPaidByCustomer
        },

        new Request
        {
            Id = 3,
            Title = "نظافت راه پله",
            Description = "نظافت راه پله ساختمان 4 طبقه",
            RegisterDate = new DateTime(2025, 3, 2, 0, 0, 0),
            RequestedDate = new DateTime(2025, 3, 8, 0, 0, 0),
            IsDeleted = false,
            CityId = 1,
            CustomerId = 3,
            HomeServiceId = 2,
            RequestStatus = StatusEnum.WaitingCustomerToChoose
        }

        );



    }
}
