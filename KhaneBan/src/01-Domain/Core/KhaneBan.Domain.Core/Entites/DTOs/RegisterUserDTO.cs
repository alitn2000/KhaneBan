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

}
