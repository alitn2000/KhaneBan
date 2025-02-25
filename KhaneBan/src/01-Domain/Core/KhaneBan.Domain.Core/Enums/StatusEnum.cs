using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Enums;

public enum StatusEnum
{

    [Display(Name = "منتظر پیشنهاد کارشناس")]
    WatingExpertOffer = 1,

    [Display(Name = "منتظر انتخاب کارشناس")]
    WatingForChoosingExpert = 2,

    [Display(Name = "در انتظار کارشناس")]
    WatingExpertComeToYourPlace = 3,

    [Display(Name = " آغاز کار کارشناس")]
    WorkStarted = 4,

    [Display(Name = " انجام شده")]
    WorkDoneByExpert = 5,

    [Display(Name = "پرداخت شده ")]
    WorkPaidByCustomer = 6
}
