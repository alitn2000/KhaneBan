using System.ComponentModel.DataAnnotations;

namespace KhaneBan.EndPoints.MVC.Areas.Users.Models;

public class LoginViewModel
{
    [Display (Name ="ایمیل")]
    [Required (ErrorMessage ="ایمیل اجباری است")]
    [EmailAddress(ErrorMessage ="فرمت اجباری است ")]
    public string Email { get; set; }

    [Required(ErrorMessage = "رمز عبور اجباری است")]
    [Display(Name = "رمز عبور")]
    public string Password { get; set; }

    public bool IsPresistent { get; set; } = false;

    [Required(ErrorMessage = "نقش اجباری است")]
    [Display(Name = "ورود به عنوان ")]
    public string Role { get; set; }
}
