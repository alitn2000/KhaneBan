using KhaneBan.Domain.Core.Entites.User;

namespace KhaneBan.Domain.Core.Entites.UserRequests;

public class Rating
{
    #region Properties
    public int Id { get; set; }
    public double Rate { get; set; }
    public string Comment { get; set; }
    public DateTime RegisterDate { get; set; }
    public bool? IsAccepted { get; set; }
    public bool IsDeleted { get; set; } = false;
    public int CustomerId { get; set; }
    public int ExpertId { get; set; }
    #endregion

    #region NavigationProperties
    public  Request Request { get; set; }
    public Customer Customer { get; set; }
    public Expert Expert { get; set; }
    #endregion

}
