using System.ComponentModel.DataAnnotations;

namespace KhaneBan.EndPoints.API.ViewModels
{
    public class AddCustomerViewModel
    {
        [Required(ErrorMessage ="ایمیل اجباری است")]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(4)]
        public string Password { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public double Balance { get; set; }
        [Required]
        public int CityId { get; set; }
    }
}
