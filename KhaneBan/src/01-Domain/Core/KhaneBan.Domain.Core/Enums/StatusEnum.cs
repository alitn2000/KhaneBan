using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KhaneBan.Domain.Core.Enums;

public enum StatusEnum
{

    [Display(Name = "منتظر پیشنهاد کارشناس")]
    WatingExpertOffer = 1,

    [Display(Name = "منتظر انتخاب مشتری")]
    WaitingCustomerToChoose = 2,

    [Display(Name = " آغاز کار کارشناس")]
    WorkStarted = 3,

    [Display(Name = " انجام شده")]
    WorkDoneByExpert = 4,

    [Display(Name = "پرداخت شده ")]
    WorkPaidByCustomer = 5,

    [Display(Name = "لغو شده ")]
    Canceled = 6



}
