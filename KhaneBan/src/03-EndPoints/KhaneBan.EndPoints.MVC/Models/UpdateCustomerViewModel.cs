using System.ComponentModel.DataAnnotations;

namespace KhaneBan.EndPoints.MVC.Models;

public class UpdateCustomerViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage ="ایمیل مشتری نمیتواند خالی باشد")]
    [Display(Name = "ایمیل")]
    public string Email { get; set; }

    [Required(ErrorMessage = "نام مشتری نمیتواند خالی باشد")]
    [Display(Name = "نام")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "نام خانوادگی مشتری نمیتواند خالی باشد")]
    [Display(Name = "نام خانوادگی")]
    public string LastName { get; set; }

}
