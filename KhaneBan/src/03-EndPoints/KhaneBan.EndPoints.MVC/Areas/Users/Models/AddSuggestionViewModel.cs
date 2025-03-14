using System.ComponentModel.DataAnnotations;

namespace KhaneBan.EndPoints.MVC.Areas.Users.Models;

public class AddSuggestionViewModel
{
    public int RequestId { get; set; }

    public int ExpertId { get; set; }

    [Required(ErrorMessage = "وارد کردن قیمت پیشنهادی الزامی است.")]
    [Display(Name = "قیمت پیشنهادی")]
    public int SuggestedPrice { get; set; }


    [Required(ErrorMessage = "تاریخ پیشنهادی الزامی است.")]
    [Display(Name = "تاریخ تحویل")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime OfferDate { get; set; }

    [Required(ErrorMessage = "ارائه توضیحات الزامی است.")]
    [Display(Name = "توضیحات")]
    public string Description { get; set; }
}
