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

public class SuggestionConfiguration : IEntityTypeConfiguration<Suggestion>
{
    public void Configure(EntityTypeBuilder<Suggestion> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Price)
            .IsRequired();

        builder.Property(s => s.Description)
            .HasMaxLength(500);

        builder.HasOne(s => s.Request)
            .WithMany(s => s.Suggestions)
            .HasForeignKey(s => s.RequestId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasOne(s => s.Expert)
            .WithMany(s => s.Suggestions)
            .HasForeignKey(s => s.ExpertId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(
        new Suggestion
        {
            Id = 1,
            Price = 5000,
            DeliveryDate = new DateTime(2025, 2, 2, 0, 0, 0),
            RegisterDate = new DateTime(2025, 2, 2, 0, 0, 0),
            Description = "ارزون",
            SuggestionStatus  = StatusEnum.WatingForChoosingExpert,
            IsDeleted = false,
            RequestId = 1,
            ExpertId = 1,
        },
        new Suggestion
        {
            Id = 2,
            Price = 6000,
            DeliveryDate = new DateTime(2025, 2, 2, 0, 0, 0),
            RegisterDate = new DateTime(2025, 2, 2, 0, 0, 0),
            Description = "گرون",
            SuggestionStatus = StatusEnum.WatingForChoosingExpert,
            IsDeleted = false,
            RequestId = 2,
            ExpertId = 1,
        }
        );

    }
}
