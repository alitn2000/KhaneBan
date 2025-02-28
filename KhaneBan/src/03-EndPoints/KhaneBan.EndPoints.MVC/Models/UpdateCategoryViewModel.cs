using System.ComponentModel.DataAnnotations;

namespace KhaneBan.EndPoints.MVC.Models;

public class UpdateCategoryViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "عکس نمیتواند خالی باشد .عنوان")]
    [Display(Name = "عنوان")]
    public string Title { get; set; }
    public string? ImagePath { get; set; } = null;

    [Required(ErrorMessage = "عکس نمیتواند خالی باشد .")]
    [Display(Name = "عکس")]
    public IFormFile? ImageFile { get; set; }
}
