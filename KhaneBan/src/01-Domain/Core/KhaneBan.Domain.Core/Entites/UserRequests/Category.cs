using KhaneBan.Domain.Core.Entites.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Entites.UserRequests;

public class Category
{
    #region Properties
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsDeleted { get; set; } = false;
    public string? PicturePath { get; set; }
    public DateTime RegisterAt { get; set; } = DateTime.Now;

    #endregion Properties

    #region NavigationProperties
    public List<SubCategory>? SubCategories { get; set; }
    #endregion
}
