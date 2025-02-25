using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.UserRequests;
using Microsoft.AspNetCore.Identity;

namespace KhaneBan.Domain.Core.Entites.User;

public class User : IdentityUser<int>
{
    #region Properties
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public double Balance { get; set; }
    public DateTime RegisterDate { get; set; } = DateTime.Now;
    public int CityId { get; set; }
    public string? PicturePath { get; set; }
    public bool IsDeleted { get; set; } = false;

    #endregion


    #region NavigationProperties
    public Expert? Expert { get; set; }
    public Customer? Customer { get; set; }
    public Admin? Admin { get; set; }
    public City City { get; set; }
    #endregion
}
