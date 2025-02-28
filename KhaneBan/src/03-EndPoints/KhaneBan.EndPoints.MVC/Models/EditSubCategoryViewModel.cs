using System.ComponentModel.DataAnnotations;

namespace KhaneBan.EndPoints.MVC.Models
{
    public class EditSubCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان اجباری است.")]
        [StringLength(100, ErrorMessage = "عنوان نباید بیشتر از ۱۰۰ کاراکتر باشد.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "انتخاب دسته‌بندی اجباری است.")]
        public int CategoryId { get; set; }

        public string? ImagePath { get; set; }
        [Required(ErrorMessage = "انتخاب عکس اجباری است.")]
        public IFormFile? ImageFile { get; set; }
    }
}
