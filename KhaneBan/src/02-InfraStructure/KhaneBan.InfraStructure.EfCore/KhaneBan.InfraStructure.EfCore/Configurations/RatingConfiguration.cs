using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KhaneBan.Domain.Core.Entites.UserRequests;

namespace KhaneBan.InfraStructure.EfCore.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Rate)
            .IsRequired();

        builder.Property(r => r.Comment)
            .HasMaxLength(1000);

        builder.HasOne(x => x.Customer)
         .WithMany(x => x.Ratings)
         .HasForeignKey(x => x.CustomerId)
         .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Expert)
        .WithMany(x => x.Ratings)
        .HasForeignKey(x => x.ExpertId)
        .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(

           new Rating
           {
               Id = 1,
               Rate = 5,
               CustomerId = 1,
               ExpertId = 1,
               Status = true,
               Comment = "اوکی",
               IsDeleted = false,
               RegisterDate = new DateTime(2025, 2, 2, 0, 0, 0)
           },

            new Rating
            {
                Id = 2,
                Rate = 5,
                CustomerId = 2,
                ExpertId = 1,
                Status = true,
                Comment = "اوکی",
                IsDeleted = false,
                RegisterDate = new DateTime(2025, 2, 2, 0, 0, 0)
            }
        );
        

    }
}
