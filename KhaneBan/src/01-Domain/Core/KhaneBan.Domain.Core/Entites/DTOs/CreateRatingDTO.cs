using System.ComponentModel.DataAnnotations;

namespace KhaneBan.Domain.Core.Entites.DTOs;

    public class CreateRatingDTO
    {
    [Required(ErrorMessage = "شناسه درخواست الزامی است.")]
    public int RequestId { get; set; }

    [Required(ErrorMessage = "امتیاز الزامی است.")]
    [Range(1, 5, ErrorMessage = "امتیاز باید بین 1 تا 5 باشد.")]
    public int Rating { get; set; }

    [Required(ErrorMessage = "نظر شما الزامی است.")]
    [StringLength(500, ErrorMessage = "نظر شما نباید بیشتر از 500 کاراکتر باشد.")]
    public string Comment { get; set; }


    [StringLength(100, ErrorMessage = "عنوان موضوع نظر نباید بیشتر از 100 کاراکتر باشد.")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "شناسه متخصص الزامی است.")]
    public int ExpertId { get; set; }

    [Required(ErrorMessage = "شناسه کارشناس الزامی است.")]
    public int CustomerId { get; set; }
}

