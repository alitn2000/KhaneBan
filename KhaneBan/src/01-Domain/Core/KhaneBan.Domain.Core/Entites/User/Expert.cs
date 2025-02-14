using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Entites.User;

public class Expert
{
    #region Properties
    public int Id { get; set; }
    public int UserId { get; set; }
    #endregion

    #region NavigationProperties
    public User User { get; set; }
    public List<HomeService>? HomeServices { get; set; }   
    public List<Suggestion>? Suggestions { get; set; } 
    public List<Rating>? Ratings { get; set; }
    #endregion
}