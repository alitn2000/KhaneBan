using KhaneBan.Domain.Core.Entites.UserRequests;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.InfraStructure.EfCore.Configurations
{
    public class SubcategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {

            builder.HasKey(x => x.Id);
            builder.ToTable("SubCategories");
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);


            builder.HasOne(x => x.Category)
               .WithMany(x => x.SubCategories)
               .HasForeignKey(x => x.CategoryId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.HomeServices)
               .WithOne(x => x.SubCategory)
               .HasForeignKey(x => x.SubCategoryId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(new List<SubCategory>()
            {
                new SubCategory() { Id = 1, RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "نظافت و پذیرایی",CategoryId=1},
                new SubCategory() { Id = 2, RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "شستشو",CategoryId=1},
                new SubCategory() { Id = 3, RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "کارواش و دیتیلینگ",CategoryId=1},
                new SubCategory() { Id = 4, RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "سرمایش و گرمایش",CategoryId=2},
                new SubCategory() { Id = 5, RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "تعمیرات ساختمان",CategoryId=2},
                new SubCategory() { Id = 6, RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "لوله کشی",CategoryId=2},
                new SubCategory() { Id = 7, RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "طراحی و بازسازی ساختمان",CategoryId=2},
                new SubCategory() { Id = 8, RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "باغبانی و فضای سبز",CategoryId=2},
                new SubCategory() { Id = 9, RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "چوب و کابینت",CategoryId=2},
                new SubCategory() { Id = 10,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "نصب و تعمیر لوازم خانگی",CategoryId=3},
                new SubCategory() { Id = 11,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "خدمات کامپیوتری",CategoryId=3},
                new SubCategory() { Id = 12,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "تعمیرات موبایل",CategoryId=3 },
                new SubCategory() { Id = 13,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "باربری و جابجایی",CategoryId=4},
                new SubCategory() { Id = 14,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "خدمات و تعمیرات خودرو",CategoryId=5 },
                new SubCategory() { Id = 15,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "زیبایی بانوان",CategoryId=6 },
                new SubCategory() { Id = 16,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "پزشکی و پرستاری",CategoryId=6 },
                new SubCategory() { Id = 17,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "حیوانات خانگی",CategoryId=6 },
                new SubCategory() { Id = 18,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "تندرستی و ورزش",CategoryId=6 },
                new SubCategory() { Id = 19,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "خدمات شرکتی",CategoryId=7 },
                new SubCategory() { Id = 20,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "تامین نیروی انسانی",CategoryId=7 },
                new SubCategory() { Id = 21,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "خیاطی و تعمیرات لباس",CategoryId=8 },
                new SubCategory() { Id = 22,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "مجالس و رویدادها",CategoryId=8 },
                new SubCategory() { Id = 23,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "آموزش",CategoryId=8},
                new SubCategory() { Id = 24,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "کودک",CategoryId=8 },
            });


        }
    }
}
