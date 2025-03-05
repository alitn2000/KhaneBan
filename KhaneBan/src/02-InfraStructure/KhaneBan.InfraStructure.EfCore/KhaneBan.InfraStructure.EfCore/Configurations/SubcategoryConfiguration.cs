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
                new() { Id = 1,  RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "نظافت و پذیرایی",CategoryId=1 , PicturePath = "/images/subcategpries/nezafat_pazirayi.jpg"},
                new() { Id = 2,  RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "شستشو",CategoryId=1 , PicturePath = "/images/subcategpries/shostosho.jpg" },
                new() { Id = 3,  RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "کارواش و دیتیلینگ",CategoryId=1 ,PicturePath = "/images/subsubcategpriescategory/karvash_detailing" },
                new() { Id = 4,  RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "سرمایش و گرمایش",CategoryId=2 ,PicturePath = "/images/subcategpries/sarmayesh_garmayesh" },
                new() { Id = 5,  RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "تعمیرات ساختمان",CategoryId=2 ,PicturePath = "/images/subcategpries/tamirat_sakhteman" },
                new() { Id = 6,  RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "لوله کشی",CategoryId=2 ,PicturePath = "/images/subcategpries/lolekeshi" },
                new() { Id = 7,  RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "طراحی و بازسازی ساختمان",CategoryId=2 ,PicturePath = "/images/subcategpries/tarahi_bazsazi.jpg" },
                new() { Id = 8,  RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "باغبانی و فضای سبز",CategoryId=2 ,PicturePath = "/images/subcategpries/baqbani_fazayesabz.jpg"  },
                new() { Id = 9,  RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "چوب و کابینت",CategoryId=2 ,PicturePath = "/images/subcategpries/choob_kabinet.jpg"  },
                new() { Id = 10, RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "نصب و تعمیر لوازم خانگی",CategoryId=3 ,PicturePath = "/images/subcategpries/nasab_tamir_lavazem.jpg" },
                new() { Id = 11, RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "خدمات کامپیوتری",CategoryId=3 ,PicturePath = "/images/subcategpries/khadamt_cp.jpg" },
                new() { Id = 12, RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "تعمیرات موبایل",CategoryId=3 ,PicturePath = "/images/subcategpries/tamirat_mobile.jpg" },
                new() { Id = 13, RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "باربری و جابجایی",CategoryId=4 ,PicturePath = "/images/subcategpries/barbari_jabejayi.jpg" },
                new() { Id = 14, RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "خدمات و تعمیرات خودرو",CategoryId=5 ,PicturePath = "/images/subcategpries/khadamat_khodro.jpg" },
                new() { Id = 15, RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "زیبایی بانوان",CategoryId=6 ,PicturePath = "/images/subcategpries/zibayi_banovan.jpg" },
                new() { Id = 16, RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "پزشکی و پرستاری",CategoryId=6 ,PicturePath = "/images/subcategpries/pezeshki_parastari.jpg" },
                new() { Id = 17, RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "حیوانات خانگی",CategoryId=6 ,PicturePath = "/images/subcategpries/heyvanat_khanegi.jpg" },
                new() { Id = 18, RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "تندرستی و ورزش",CategoryId=6 ,PicturePath = "/images/subcategpries/tandorosti_varzesh.jpg" },
                new() { Id = 19, RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "خدمات شرکتی",CategoryId=7 ,PicturePath = "/images/subcategpries/khadamat_sherkati.jpg" },
                new() { Id = 20, RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "تامین نیروی انسانی",CategoryId=7 ,PicturePath = "/images/subcategpries/tamin_niroye_ensani.jpg" },
                new() { Id = 21, RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "خیاطی و تعمیرات لباس",CategoryId=8 ,PicturePath = "/images/subcategpries/khayati_tamirat.jpg" },
                new() { Id = 22, RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "مجالس و رویدادها",CategoryId=8 ,PicturePath = "/images/subcategpries/majales_roydad.jpg" },
                new() { Id = 23, RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "آموزش",CategoryId=8,PicturePath = "/images/subcategpries/amozesh.jpg" },
                new() { Id = 24, RegisterAt = new DateTime(2025,2,2), IsDeleted = false, Title = "کودک",CategoryId=8 ,PicturePath = "/images/subcategpries/kodak.jpg" },
            });


        }
    }
}
