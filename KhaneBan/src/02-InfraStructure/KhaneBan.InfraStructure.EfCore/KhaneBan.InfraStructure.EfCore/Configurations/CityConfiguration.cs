using KhaneBan.Domain.Core.Entites.BaseEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.InfraStructure.EfCore.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);

            builder.HasMany(x => x.Requests)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Users)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(new List<City>()
            {
                new() { Id = 1, Title = "تهران" },
                new() { Id = 2, Title = "مشهد" },
                new() { Id = 3, Title = "اصفهان" },
                new() { Id = 4, Title = "شیراز" },
                new() { Id = 5, Title = "تبریز" },
                new() { Id = 6, Title = "کرج" },
                new() { Id = 7, Title = "قم" },
                new() { Id = 8, Title = "اهواز" },
                new() { Id = 9, Title = "اردبیل" },
                new() { Id = 10, Title = "کرمانشاه" },
                new() { Id = 11, Title = "زاهدان" },
                new() { Id = 12, Title = "ارومیه" },
                new() { Id = 13, Title = "یزد" },
                new() { Id = 14, Title = "همدان" },
                new() { Id = 15, Title = "قزوین" },
                new() { Id = 16, Title = "سنندج" },
                new() { Id = 17, Title = "بندرعباس" },
                new() { Id = 18, Title = "زنجان" },
                new() { Id = 19, Title = "ساری" },
                new() { Id = 20, Title = "بوشهر" },
                new() { Id = 21, Title = "مازندران" },
                new() { Id = 22, Title = "گرگان" },
                new() { Id = 23, Title = "کرمان" },
                new() { Id = 24, Title = "خرم آباد" },
                new() { Id = 25, Title = "سمنان" }
            });
        }
    }
}
