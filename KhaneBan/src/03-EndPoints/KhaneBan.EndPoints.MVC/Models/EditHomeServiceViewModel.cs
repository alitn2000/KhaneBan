using System.ComponentModel.DataAnnotations;

namespace KhaneBan.EndPoints.MVC.Models
{
    public class EditHomeServiceViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان اجباری است.")]
        [StringLength(100, ErrorMessage = "عنوان نباید بیشتر از ۱۰۰ کاراکتر باشد.")]
        public string Title { get; set; }


        [Required(ErrorMessage ="دسته بندی اجباری است")]
        public int SubCategoryId { get; set; }

        public string? ImagePath { get; set; } = null;


        [Required(ErrorMessage = "قیمت پایه اجباری است.")]
        [Range(0, int.MaxValue, ErrorMessage = "قیمت پایه باید مقدار مثبت باشد.")]
        public double Price { get; set; }



        public IFormFile? ImageFile { get; set; }
    }
}
