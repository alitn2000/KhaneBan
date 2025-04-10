using System.ComponentModel.DataAnnotations;

namespace KhaneBan.Domain.Core.Entites.DTOs;

public class RegisterUserDTO
{
    [Required(ErrorMessage = "ایمیل اجباری است")]

    public string Email { get; set; }
    [Required(ErrorMessage = "رمز عبور اجباری است")]
    public string Password { get; set; }
    [Required(ErrorMessage = "تکرار رمز عبور اجباری است")]
    [Compare("Password", ErrorMessage = "رمز عبور با تکرار آن یکی نمیباشد")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "نقش اجباری است")]
    [Display(Name = "نقش ")]
    public string Role { get; set; }

    [Required(ErrorMessage = " شهر اجباری است")]
    [Display(Name = "شهر ")]
    public int CityId { get; set; }

}
