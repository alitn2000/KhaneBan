using System.ComponentModel.DataAnnotations;

namespace KhaneBan.EndPoints.MVC.Models;

public class CreateCategoryViewModel
{
    [Required(ErrorMessage = "عنوان نمی تواند خالی باشد.")]
    [Display(Name ="عنوان")]
    public string Title { get; set; }
    public string? ImagePath { get; set; } = null;
    [Required(ErrorMessage = "عکس نمی تواند خالی باشد.")]
    [Display(Name = "عکس")]
    public IFormFile? ImageFile { get; set; }
}
