using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Entites.UserRequests;

public class Suggestion
{
    #region Properties
    public int Id { get; set; }
    public double Price { get; set; }
    public DateTime StartDate { get; set; } 
    public DateTime RegisterDate { get; set; } = DateTime.Now;
    public string Description { get; set; }
    public StatusEnum SuggestionStatus { get; set; } = StatusEnum.WaitingCustomerToChoose;
    public bool IsDeleted { get; set; } = false;
    public int RequestId { get; set; }
    public int ExpertId { get; set; }
    #endregion

    #region NavigationProperties
    public Request Request { get; set; }
    public Expert Expert { get; set; }

    #endregion
}
