using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.Domain.Core.Enums;

namespace KhaneBan.Domain.Core.Entites.UserRequests;

public class Request
{
    #region Properties
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime RegisterDate { get; set; }  = DateTime.Now;
    public DateTime? EndTime { get; set; }
    public DateTime RequestedDate { get; set; }
    public StatusEnum RequestStatus { get; set; } = StatusEnum.WatingExpertOffer;
    public bool IsDeleted { get; set; } = false;
    public int CityId { get; set; }
    public int? RatingId { get; set; }
    public int CustomerId { get; set; }
    public int HomeServiceId { get; set; }
    public int WinnerId { get; set; }

    #endregion

    #region NavigationProperties
    public HomeService HomeService { get; set; }
    public City City { get; set; }
    public Rating? Rating { get; set; }
    public Customer Customer  { get; set; }
    public List<string>? RequestImages { get; set; }
    public List<Suggestion>? Suggestions { get; set; }
    #endregion
}
