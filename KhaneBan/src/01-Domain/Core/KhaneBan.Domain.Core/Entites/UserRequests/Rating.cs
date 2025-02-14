using KhaneBan.Domain.Core.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Entites.UserRequests;

public class Rating
{
    #region Properties
    public int Id { get; set; }
    public int Rate { get; set; }  
    public string Comment { get; set; }
    public DateTime RegisterDate { get; set; }
    public bool Status { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public int CustomerId { get; set; }
    public int ExpertId { get; set; }
    #endregion

    #region NavigationProperties
    public Customer Customer { get; set; }
    public Expert Expert { get; set; }
    #endregion

}
