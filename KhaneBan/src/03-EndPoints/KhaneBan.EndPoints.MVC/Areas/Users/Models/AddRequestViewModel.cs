using System.ComponentModel.DataAnnotations;

namespace KhaneBan.EndPoints.MVC.Areas.Users.Models;

public class AddRequestViewModel
{
    public int HomeServiceId { get; set; }

    [Required(ErrorMessage = "عنوان سرویس خانگی الزامی است.")]
    [Display(Name = "عنوان سرویس خانگی")]
    public string HomeServiceTitle { get; set; }

    [Required(ErrorMessage = "تاریخ درخواست الزامی است.")]
    [Display(Name = "تاریخ درخواست")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime RequestForDate { get; set; }

    [Required(ErrorMessage = "ساعت درخواست الزامی است.")]
    [Display(Name = "ساعت درخواست")]
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime RequestForTime { get; set; }

    [Display(Name = "مسیر تصویر")]
    public List<string>? ImagePaths { get; set; } = null;

    [Display(Name = "فایل تصویر")]
    [DataType(DataType.Upload)]
    public List<IFormFile>? ImageFiles { get; set; }

    [Required(ErrorMessage = "توضیحات الزامی است.")]
    [Display(Name = "توضیحات")]
    [MaxLength(500, ErrorMessage = "توضیحات نباید بیشتر از 500 کاراکتر باشد.")]
    public string Description { get; set; }
}
