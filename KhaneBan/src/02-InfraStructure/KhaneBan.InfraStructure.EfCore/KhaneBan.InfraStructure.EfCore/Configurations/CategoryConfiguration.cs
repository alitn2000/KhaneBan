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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Categories");
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);

            builder.HasMany(x => x.SubCategories)
              .WithOne(x => x.Category)
              .HasForeignKey(x => x.CategoryId)
              .OnDelete(DeleteBehavior.Cascade);


            builder.HasData(new List<Category>()
            {
                new() { Id = 1, RegisterAt = new DateTime(2025, 2, 2), IsDeleted = false, Title = "تمیزکاری", PicturePath = "/images/Categories/tamizkari.jpg"},
                new() { Id = 2, RegisterAt = new DateTime(2025, 2, 2), IsDeleted = false, Title = "ساختمان", PicturePath = "/images/Categories/sakhteman.jpg" },
                new() { Id = 3, RegisterAt = new DateTime(2025, 2, 2), IsDeleted = false, Title = "تعمیرات اشیا",PicturePath = "/images/Categories/tamirat_ashya.jpg" },
                new() { Id = 4, RegisterAt = new DateTime(2025, 2, 2), IsDeleted = false, Title = "اسباب کشی و حمل بار",PicturePath = "/images/Categories/asbabkeshi.jpg" },
                new() { Id = 5, RegisterAt = new DateTime(2025, 2, 2), IsDeleted = false, Title = "خودرو",PicturePath = "/images/category/khodro.jpg" },
                new() { Id = 6, RegisterAt = new DateTime(2025, 2, 2), IsDeleted = false, Title = "سلامت و زیبایی",PicturePath = "/images/Categories/salamat_zibayi.jpg" },
                new() { Id = 7, RegisterAt = new DateTime(2025, 2, 2), IsDeleted = false, Title = "سازمان ها و مجتمع ها",PicturePath = "/images/Categories/sazmanha_va_mojtamha.jpg" },
                new() { Id = 8, RegisterAt = new DateTime(2025, 2, 2), IsDeleted = false, Title = "سایر",PicturePath = "/images/Categories/sayer.jpg" },


            });

        }
    }
}
