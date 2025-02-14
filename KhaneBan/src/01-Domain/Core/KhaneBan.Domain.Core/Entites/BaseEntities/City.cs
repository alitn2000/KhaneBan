using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.Domain.Core.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Entites.BaseEntities
{
    public class City
    {
        #region Properties
        public int Id { get; set; }
        public string Title { get; set; }
        #endregion

        #region NavigationProperties
        public List<User.User>? Users { get; set; }
        public List<Request>? Requests { get; set; }
        #endregion
    }
}
