using System.ComponentModel.DataAnnotations;

namespace KhaneBan.EndPoints.MVC.Models
{
    public class AdminViewModel
    {
        [Display(Name = "شناسه کاربری")]
        [Required(ErrorMessage = "فیلد شناسه کاربری اجباری است ")]
        public string UserName { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "فیلد ایمیل اجباری است")]
        [StringLength(4, ErrorMessage = "طول رمز عبور اشتباه است")]
        
        public string Password { get; set; }
    }
}
