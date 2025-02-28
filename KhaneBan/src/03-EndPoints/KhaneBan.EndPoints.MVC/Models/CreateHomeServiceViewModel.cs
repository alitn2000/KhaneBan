using System.ComponentModel.DataAnnotations;

namespace KhaneBan.EndPoints.MVC.Models;

public class CreateHomeServiceViewModel
{
    [Required(ErrorMessage = "عنوان اجباری است.")]
    [StringLength(100, ErrorMessage = "عنوان نباید بیشتر از ۱۰۰ کاراکتر باشد.")]
    public string Title { get; set; }


    [Required(ErrorMessage = "انتخاب دسته اجباری است.")]
    public int SubCategoryId { get; set; }

    public string? ImagePath { get; set; } = null;

    public IFormFile? ImageFile { get; set; }

    [Required(ErrorMessage = "قیمت پایه اجباری است.")]
    [Range(0, int.MaxValue, ErrorMessage = "قیمت باید مقدار مثبت باشد.")]
    public int Price { get; set; }



}
