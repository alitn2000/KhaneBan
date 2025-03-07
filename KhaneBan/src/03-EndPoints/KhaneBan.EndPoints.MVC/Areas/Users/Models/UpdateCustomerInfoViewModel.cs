using System.ComponentModel.DataAnnotations;

namespace KhaneBan.EndPoints.MVC.Areas.Users.Models;

public class UpdateCustomerInfoViewModel
{
    [Display(Name = "نام")]
    [Required(ErrorMessage = "نام اجباری است")]
    public string FirstName { get; set; }

    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "نام خانوادگی اجباری است")]
    public string LastName { get; set; }

    [Display(Name = "نام کاربری")]
    [Required(ErrorMessage = "نام کاربری اجباری است")]
    public string UserName { get; set; }

    [Display(Name = "آدرس")]
    [Required(ErrorMessage = "آدرس اجباری است")]
    public string Address { get; set; }

    [Display(Name = "استان")]
    [Required(ErrorMessage = "استان اجباری است")]
    public int CityId { get; set; }

    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "ایمیل اجباری است")]
    public string Email { get; set; }

    [Display(Name = "شماره تماس")]
    [Required(ErrorMessage = "شماره تماس اجباری است")]
    [StringLength(11, ErrorMessage = "طول شماره تلفن اشتباه است")]
    [RegularExpression(@"^09\d{9}$", ErrorMessage = "فرمت شماره تلفن اشتباه است")]
    public string PhoneNumber { get; set; }

    [Display(Name = "تصویر پروفایل")]
    public IFormFile? ImageFile { get; set; }


    public string? ImagePath { get; set; }

    [Display(Name = " موجودی")]
    [Range(0,9999999999999999999,ErrorMessage ="مقدار پول نمیتواند منفی باشد")]
    //[Required(ErrorMessage = " موجودی اجباری است")]
    public double Balance { get; set; }

    //[Display(Name = " رمز عبور")]
    //[Required(ErrorMessage = " رمزعبور اجباری است")]
    //[StringLength(4, ErrorMessage = "طول رمز عبور اشتباه است")]
    //public string Password { get; set; }


}
