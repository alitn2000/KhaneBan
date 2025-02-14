using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Entites.User;

public class Admin
{
    #region Properties
    public int UserId { get; set; }
    public int Id { get; set; }
    #endregion

    #region NavigationProperties
    public User User { get; set; }
    #endregion


}
