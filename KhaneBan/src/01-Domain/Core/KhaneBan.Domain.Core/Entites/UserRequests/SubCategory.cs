namespace KhaneBan.Domain.Core.Entites.UserRequests;

public class SubCategory
{
    #region Properties
    public int Id { get; set; }
    public string Title { get; set; }
    public int CategoryId { get; set; }
    public string? PicturePath { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime RegisterAt { get; set; } = DateTime.Now;

    #endregion

    #region NavigationProperties
    public Category Category { get; set; }
    public List<HomeService>? HomeServices { get; set; }
    #endregion
}
