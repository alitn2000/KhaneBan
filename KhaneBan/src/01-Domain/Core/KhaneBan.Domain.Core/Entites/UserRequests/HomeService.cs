using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KhaneBan.Domain.Core.Entites.UserRequests
{
    public class HomeService
    {
        #region Properties
        public int Id { get; set; }
        public string Title { get; set; }
        public int SubCategoryId { get; set; }
        public int VisitCount { get; set; }
        public string? PicturePath { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime RegisterAt { get; set; } = DateTime.Now;
        public double BasePrice { get; set; }


        #endregion

        #region NavigationProperties
        public SubCategory SubCategory { get; set; }
        public List<Request>? Requests { get; set; }
        public List<Expert>? Experts { get; set; }

  

        #endregion

    }
}
