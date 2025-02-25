using KhaneBan.Domain.Core.Entites.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace KhaneBan.EndPoints.MVC.Models;

public class CreateCustomerViewModel
{
    [Required(ErrorMessage = "نام اجباری است")]
    [Display(Name ="نام")]
    public string? FirstName { get; set; }

    [Display(Name = "نام خانوادگی")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "نام کاربری اجباری است")]
    [Display(Name = "نام کاربری")]
    public string UserName { get; set; }

    [Display(Name = "آدرس")]
    public string? Address { get; set; }

    [Required(ErrorMessage ="آدرس ایمیل اجباری است")]
    [Display(Name = "آدرس ایمیل")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "رمز عبور اجباری است")]
    [Display(Name = "رمزعبور")]
    public string Password { get; set; }


    [Display(Name = "عکس")]

    public IFormFile? ProfileImage { get; set; }

    [Required(ErrorMessage = "استان اجباری است")]
    [Display(Name = "استان")]
    public int CityId { get; set; }

}
