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
                new() { Id = 1, RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "خدمات نظافت منزل",BasePrice=2000, SubCategoryId=1 ,PicturePath = "/images/HomeServices/nezafat_manzel.jpg"},
                new() { Id = 2, RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "نظافت راه پله",BasePrice=2000,SubCategoryId=1 , PicturePath = "/images/HomeServices/nezafat_rahpele.jpg" },
                new() { Id = 3, RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "قالیشویی",BasePrice=2000,SubCategoryId=2 ,PicturePath = "/images/HomeServices/ghalishoyi.jpg" },
                new() { Id = 4, RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "پرده شویی",BasePrice=2000,SubCategoryId=2 ,PicturePath = "/images/HomeServices/pardeshoyi.jpg" },
                new() { Id = 5, RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "سرامیک خودرو",BasePrice=2000,SubCategoryId=3 ,PicturePath = "/images/HomeServices/seramik_khodro.jpg" },
                new() { Id = 6, RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "صفرشویی خودرو",BasePrice=2000,SubCategoryId=3 ,PicturePath = "/images/HomeServices/sefrshoyi_khodro.jpg" },
                new() { Id = 7, RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "تعمیر و سرویس کولر آبی",BasePrice=2000,SubCategoryId=4 ,PicturePath = "/images/HomeServices/tamir_coolerabi.jpg" },
                new() { Id = 8, RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "کانال سازی کولر",BasePrice=2000,SubCategoryId=4 ,PicturePath = "/images/HomeServices/kanalsazi_cooler.jpg" },
                new() { Id = 9, RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "تعمیر و نگهداری موتورخانه",BasePrice=2000,SubCategoryId=4 ,PicturePath = "/images/HomeServices/tamir_motorkhane.jpg" },
                new() { Id = 10,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "سنگ کاری",BasePrice=2000,SubCategoryId=5 ,PicturePath = "/images/HomeServices/sangkari.jpg" },
                new() { Id = 11,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "بنایی",BasePrice=2000,SubCategoryId=5 ,PicturePath = "/images/HomeServices/banayi.jpg" },
                new() { Id = 12,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "کلیدسازی",BasePrice=2000,SubCategoryId=5 ,PicturePath = "/images/HomeServices/klidsazi.jpg" },
                new() { Id = 13,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "کفسابی",BasePrice=2000,SubCategoryId=5 ,PicturePath = "/images/HomeServices/kafsabi.jpg" },
                new() { Id = 14,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "خدمات لوله کشی ساختمان",BasePrice=2000,SubCategoryId=6 ,PicturePath = "/images/HomeServices/lolekeshi.jpg" },
                new() { Id = 15,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "تخلیه چاه و لوله بازکنی",BasePrice=2000,SubCategoryId=6 ,PicturePath = "/images/HomeServices/lolebazkoni.jpg" },
                new() { Id = 16,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "لوله کشی آب و فاضلاب",BasePrice=2000,SubCategoryId=6 ,PicturePath = "/images/HomeServices/lolekeshi_fazelab.jpg" },
                new() { Id = 17,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "مشاوره و بازسازی ساختمان",BasePrice=2000,SubCategoryId=7 ,PicturePath = "/images/HomeServices/moshavere_bazsazi_sakhteman.jpg" },
                new() { Id = 18,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "دکوراسیون و طراحی ساختمان",BasePrice=2000,SubCategoryId=7 ,PicturePath = "/images/HomeServices/dekorasion_sakhteman.jpg" },
                new() { Id = 19,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "خدمات باغبانی",BasePrice=2000,SubCategoryId=8 ,PicturePath = "/images/HomeServices/khadamat_baqbani.jpg" },
                new() { Id = 20,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "کاشت و تعویض گلدان",BasePrice=2000,SubCategoryId=8 ,PicturePath = "/images/HomeServices/kasht_goldan.jpg" },
                new() { Id = 21,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "تعمیرات مبلمان",BasePrice=2000,SubCategoryId=9 ,PicturePath = "/images/HomeServices/tamirat_mobleman.jpg" },
                new() { Id = 22,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "تعمیرات مبلمان اداری",BasePrice=2000,SubCategoryId=9 ,PicturePath = "/images/HomeServices/tamirat_mobleman_edari.jpg" },
                new() { Id = 23,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "تعمیر پنکه",BasePrice=2000,SubCategoryId=10 ,PicturePath = "/images/HomeServices/tamir_panke.jpg" },
                new() { Id = 24,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "نصب و تعمیر فر",BasePrice=2000,SubCategoryId=10 ,PicturePath = "/images/HomeServices/nasb_va_tamir_fer.jpg" },
                new() { Id = 25,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "تعمیر کامپیوتر و لپ تاپ",BasePrice=2000,SubCategoryId=11 ,PicturePath = "/images/HomeServices/tamir_laptop.jpg" },
                new() { Id = 26,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "مودم و اینترنت",BasePrice=2000,SubCategoryId=11 ,PicturePath = "/images/HomeServices/modem_va_internet.jpg" },
                new() { Id = 27,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "خدمات تعمیر موبایل",BasePrice=2000,SubCategoryId=12 ,PicturePath = "/images/HomeServices/tamirat_mobile.jpg" },
                new() { Id = 28,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "خدمات خرید موبایل و کالاهای دیجیتال",BasePrice=2000,SubCategoryId= 12,PicturePath = "/images/HomeServices/khadamt_kharid_mobile.jpg" },
                new() { Id = 29,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "خدمات دوربین",BasePrice=2000,SubCategoryId=12 ,PicturePath = "/images/HomeServices/khadamat_dorbin.jpg" },
                new() { Id = 30,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "اسباب کشی با خاور و کامیون",BasePrice=2000,SubCategoryId=13 ,PicturePath = "/images/HomeServices/asbabkeshi_ba_khavar.jpg" },
                new() { Id = 31,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "اسباب کشی با وانت و نیسان",BasePrice=2000,SubCategoryId=13 ,PicturePath = "/images/HomeServices/asabkeshi_ba_neysan.jpg" },
                new() { Id = 32,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "کارگر جابه جایی",BasePrice=2000,SubCategoryId=13 ,PicturePath = "/images/HomeServices/kargar_jabejayi.jpg" },
                new() { Id = 33,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "تعویض باتری خودرو",BasePrice=2000,SubCategoryId=14 ,PicturePath = "/images/HomeServices/taviz_batri_khodro.jpg" },
                new() { Id = 34,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "باتری به باتری",BasePrice=2000,SubCategoryId=14 ,PicturePath = "/images/HomeServices/batri_be_batri.jpg" },
                new() { Id = 35,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "حمل خودرو",BasePrice=2000,SubCategoryId=14 ,PicturePath = "/images/HomeServices/haml_khodro.jpg" },
                new() { Id = 36,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "تعویض وایر و شمع خودرو",BasePrice=2000,SubCategoryId=14 ,PicturePath = "/images/HomeServices/taviz_vayer_sham_khodro.jpg" },
                new() { Id = 37,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "براشینگ موی بانوان",BasePrice=2000,SubCategoryId=15 ,PicturePath = "/images/HomeServices/berashing_moye_banovan.jpg" },
                new() { Id = 38,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "کوتاهی موی بانوان",BasePrice=2000,SubCategoryId=15 ,PicturePath = "/images/HomeServices/kotahi_moye_banovan.jpg" },
                new() { Id = 39,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "بافت موی بانوان در خانه",BasePrice=2000,SubCategoryId=15 ,PicturePath = "/images/HomeServices/baft_moye_banovan.jpg" },
                new() { Id = 40,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "مراقبت و نگهداری",BasePrice=2000,SubCategoryId=16 ,PicturePath = "/images/HomeServices/moraqebat_negahdari.jpg" },
                new() { Id = 41,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "معاینه پزشکی",BasePrice=2000,SubCategoryId=16 ,PicturePath = "/images/HomeServices/moayene_pezeshki.jpg" },
                new() { Id = 42,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "پیراپزشکی",BasePrice=2000,SubCategoryId=16 ,PicturePath = "/images/HomeServices/pirapezeshki.jpg" },
                new() { Id = 43,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "پت شاپ",BasePrice=2000,SubCategoryId=17 ,PicturePath = "/images/HomeServices/petshop.jpg" },
                new() { Id = 44,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "خدمات دامپزشکی در محل",BasePrice=2000,SubCategoryId=17 ,PicturePath = "/images/HomeServices/khadamat_dampezshki.jpg" },
                new() { Id = 45,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "کلاس یوگا در خانه",BasePrice=2000,SubCategoryId=18 ,PicturePath = "/images/HomeServices/khadamt_yoga.jpg" },
                new() { Id = 46,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "کلاس پیلاتس در خانه",BasePrice=2000,SubCategoryId=18 ,PicturePath = "/images/HomeServices/kelas_polates.jpg" },
                new() { Id = 47,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "پیشنهاد فروش خدمات آچاره به شرکت ها",BasePrice=2000,SubCategoryId=19 ,PicturePath = "/images/HomeServices/khadamat_achare.jpg" },
                new() { Id = 48,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "استخدام نیروی خدمتکار",BasePrice=2000,SubCategoryId=20 ,PicturePath = "/images/HomeServices/estekhdam_niroye_khedmatkar.jpg" },
                new() { Id = 49,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "تعمیرات لباس",BasePrice=2000,SubCategoryId=21 ,PicturePath = "/images/HomeServices/tamirat_lebas.jpg" },
                new() { Id = 50,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "دوخت لباس زنانه",BasePrice=2000,SubCategoryId=21 ,PicturePath = "/images/HomeServices/dokht_lebas_zanane.jpg" },
                new() { Id = 51,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "تعمیر کیف و کفش",BasePrice=2000,SubCategoryId=21 ,PicturePath = "/images/HomeServices/tamir_kifokafsh.jpg" },
                new() { Id = 52,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "کیک و شیرینی",BasePrice=2000,SubCategoryId=22 ,PicturePath = "/images/HomeServices/keyko_shirini.jpg" },
                new() { Id = 53,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "دکور تولد",BasePrice=2000,SubCategoryId=22 ,PicturePath = "/images/HomeServices/dekor_tavalod.jpg" },
                new() { Id = 54,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "گل آرایی",BasePrice=2000,SubCategoryId=22 ,PicturePath = "/images/HomeServices/gol_arayi.jpg" },
                new() { Id = 55,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "فینگرفود",BasePrice=2000,SubCategoryId=22 ,PicturePath = "/images/HomeServices/finger_food.jpg" },
                new() { Id = 56,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "آموزش زبان های خارجی",BasePrice=2000,SubCategoryId=23 ,PicturePath = "/images/HomeServices/amozesh_zaban_khareji.jpg" },
                new() { Id = 57,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "آموزش ابتدایی تا متوسطه",BasePrice=2000,SubCategoryId=23 ,PicturePath = "/images/HomeServices/ebtedayi_motevasete.jpg" },
                new() { Id = 58,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "کوتاهی موی کودک",BasePrice=2000,SubCategoryId=24 ,PicturePath = "/images/HomeServices/kotahi_moye_kodak.jpg" },
                new() { Id = 59,RegisterAt = new DateTime(2025, 2, 2),  IsDeleted = false,VisitCount=210,Description = "Lorem ipsum lorem ipsum", Title = "طراحی و دیزاین اتاق کودک",BasePrice=2000,SubCategoryId=24 ,PicturePath = "/images/HomeServices/tarahi_otaq_kodak.jpg" },
                                                                                                                    

            });

            builder
                .HasMany(e => e.Experts)
                .WithMany(h => h.HomeServices)
                .UsingEntity(j => j.HasData(
                new { ExpertsId = 1, HomeServicesId = 1 },
                 new { ExpertsId = 2, HomeServicesId = 2 },
                  new { ExpertsId = 3, HomeServicesId = 2
                  }

                ));



        }
    }
}
