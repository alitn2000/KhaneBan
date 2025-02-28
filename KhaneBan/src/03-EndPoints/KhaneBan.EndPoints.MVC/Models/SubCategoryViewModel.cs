using System.ComponentModel.DataAnnotations;

namespace KhaneBan.EndPoints.MVC.Models;

public class SubCategoryViewModel
{
    [Required(ErrorMessage = "عنوان الزامی است.")]
    [StringLength(100, ErrorMessage = "عنوان نباید بیشتر از ۱۰۰ کاراکتر باشد.")]
    public string Title { get; set; }


    [Required(ErrorMessage = "انتخاب دسته‌بندی الزامی است.")]
    public int CategoryId { get; set; }


   
    public string? ImagePath { get; set; } = null;

    [Required(ErrorMessage = "عکس الزامی است.")]
    public IFormFile? ImageFile { get; set; }
}
