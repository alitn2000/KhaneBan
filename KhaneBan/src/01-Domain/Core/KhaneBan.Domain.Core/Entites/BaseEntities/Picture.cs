using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Entites.BaseEntities;

public class Picture
{
    #region Properties
    public int Id { get; set; }
    public string Path { get; set; }
    public int RequestId { get; set; }
    #endregion

    #region NavigationProperties
    public Request Request { get; set; }
    #endregion
}
