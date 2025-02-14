using KhaneBan.Domain.Core.Entites.UserRequests;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KhaneBan.Domain.Core.Entites.User;

namespace KhaneBan.InfraStructure.EfCore.Configurations
{
    public class HomeServiceConfiguration : IEntityTypeConfiguration<HomeService>
    {
        public void Configure(EntityTypeBuilder<HomeService> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("HomeServices");
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);


            builder.HasOne(x => x.SubCategory)
               .WithMany(x => x.HomeServices)
               .HasForeignKey(x => x.SubCategoryId)
               .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(x => x.Requests)
                .WithOne(x => x.HomeService);

            

            builder.HasData(new List<HomeService>()
            {
                new HomeService() { Id = 1,BasePrice = 100,  IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "نقاشی",SubCategoryId=1 /*, ImagePath = ""*/},
                new HomeService() { Id = 2,BasePrice = 100,  IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "نظافت راه پله",SubCategoryId=1 /*, ImagePath = ""*/ },
                new HomeService() { Id = 3,BasePrice = 100,  IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "قالیشویی",SubCategoryId=2 /*,ImagePath = "" */},
                new HomeService() { Id = 4,BasePrice = 100,  IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "پرده شویی",SubCategoryId=2 /*,ImagePath = "" */},
                new HomeService() { Id = 5,BasePrice = 100,  IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "سرامیک خودرو",SubCategoryId=3 /*,ImagePath = "" */},
                new HomeService() { Id = 6,BasePrice = 100,  IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "صفرشویی خودرو",SubCategoryId=3 /*,ImagePath = "" */},
                new HomeService() { Id = 7,BasePrice = 100,  IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "تعمیر و سرویس کولر آبی",SubCategoryId=4 /*,ImagePath = "" */},
                new HomeService() { Id = 8,BasePrice = 100,  IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "کانال سازی کولر",SubCategoryId=4 /*,ImagePath = "" */},
                new HomeService() { Id = 9,BasePrice = 100,  IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0),Title = "تعمیر و نگهداری موتورخانه",SubCategoryId=4 /*,ImagePath = "" */},
                new HomeService() { Id = 10,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "سنگ کاری",SubCategoryId=5 /*,ImagePath = "" */},
                new HomeService() { Id = 11,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "بنایی",SubCategoryId=5 /*,ImagePath = "" */},
                new HomeService() { Id = 12,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "کلیدسازی",SubCategoryId=5 /*,ImagePath = "" */},
                new HomeService() { Id = 13,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "کفسابی",SubCategoryId=5 /*,ImagePath = "" */},
                new HomeService() { Id = 14,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "خدمات لوله کشی ساختمان",SubCategoryId=6 /*,ImagePath = "" */},
                new HomeService() { Id = 15,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "تخلیه چاه و لوله بازکنی",SubCategoryId=6 /*,ImagePath = "" */},
                new HomeService() { Id = 16,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "لوله کشی آب و فاضلاب",SubCategoryId=6 /*,ImagePath = "" */},
                new HomeService() { Id = 17,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "مشاوره و بازسازی ساختمان",SubCategoryId=7 /*,ImagePath = "" */},
                new HomeService() { Id = 18,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "دکوراسیون و طراحی ساختمان",SubCategoryId=7 /*,ImagePath = "" */},
                new HomeService() { Id = 19,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "خدمات باغبانی",SubCategoryId=8 /*,ImagePath = "" */},
                new HomeService() { Id = 20,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "کاشت و تعویض گلدان",SubCategoryId=8 /*,ImagePath = "" */},
                new HomeService() { Id = 21,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "تعمیرات مبلمان",SubCategoryId=9 /*,ImagePath = "" */},
                new HomeService() { Id = 22,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "تعمیرات مبلمان اداری",SubCategoryId=9 /*,ImagePath = "" */},
                new HomeService() { Id = 23,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "تعمیر پنکه",SubCategoryId=10 /*,ImagePath = "" */},
                new HomeService() { Id = 24,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "نصب و تعمیر فر",SubCategoryId=10 /*,ImagePath = "" */},
                new HomeService() { Id = 25,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "تعمیر کامپیوتر و لپ تاپ",SubCategoryId=11 /*,ImagePath = "" */},
                new HomeService() { Id = 26,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "مودم و اینترنت",SubCategoryId=11 /*,ImagePath = "" */},
                new HomeService() { Id = 27,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "خدمات تعمیر موبایل",SubCategoryId=12 /*,ImagePath = "" */},
                new HomeService() { Id = 28,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "خدمات خرید موبایل و کالاهای دیجیتال",SubCategoryId= 12/*,ImagePath = "" */},
                new HomeService() { Id = 29,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "خدمات دوربین",SubCategoryId=12 /*,ImagePath = "" */},
                new HomeService() { Id = 30,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "اسباب کشی با خاور و کامیون",SubCategoryId=13 /*,ImagePath = "" */},
                new HomeService() { Id = 31,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "اسباب کشی با وانت و نیسان",SubCategoryId=13 /*,ImagePath = "" */},
                new HomeService() { Id = 32,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "کارگر جابه جایی",SubCategoryId=13 /*,ImagePath = "" */},
                new HomeService() { Id = 33,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "تعویض باتری خودرو",SubCategoryId=14 /*,ImagePath = "" */},
                new HomeService() { Id = 34,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "باتری به باتری",SubCategoryId=14 /*,ImagePath = "" */},
                new HomeService() { Id = 35,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "حمل خودرو",SubCategoryId=14 /*,ImagePath = "" */},
                new HomeService() { Id = 36,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "تعویض وایر و شمع خودرو",SubCategoryId=14 /*,ImagePath = "" */},
                new HomeService() { Id = 37,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "براشینگ موی بانوان",SubCategoryId=15 /*,ImagePath = "" */},
                new HomeService() { Id = 38,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "کوتاهی موی بانوان",SubCategoryId=15 /*,ImagePath = "" */},
                new HomeService() { Id = 39,BasePrice = 100, IsDeleted = false,VisitCount = 120,RegisterAt = new DateTime(2025, 2, 2, 0, 0, 0), Title = "بافت موی بانوان در خانه",SubCategoryId=15 /*,ImagePath = "" */},
                

            });

            builder
                .HasMany(e => e.Experts)
                .WithMany(h => h.HomeServices)
                .UsingEntity(j => j.HasData(
                new { ExpertsId = 1, HomeServicesId = 1 }
                ));



        }
    }
}
