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
                new Category() { Id = 1, RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "تمیزکاری"},
                new Category() { Id = 2, RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "ساختمان" },
                new Category() { Id = 3, RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "تعمیرات اشیا"},
                new Category() { Id = 4, RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "اسباب کشی و حمل بار"},
                new Category() { Id = 5, RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "خودرو"},
                new Category() { Id = 6, RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "سلامت و زیبایی"},
                new Category() { Id = 7, RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "سازمان ها و مجتمع ها"},
                new Category() { Id = 8, RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "سایر"},


            });

        }
    }
}
