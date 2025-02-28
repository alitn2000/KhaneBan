using KhaneBan.Domain.Core.Contracts.AppService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhaneBan.EndPoints.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles ="Admin")]
public class RatingController : Controller
{
    private readonly IRatingAppService _ratingAppService;

    public RatingController(IRatingAppService ratingAppService)
    {
        _ratingAppService = ratingAppService;
    }

    public async Task<IActionResult> RatingList(CancellationToken cancellationToken)
    {
        var ratings = await _ratingAppService.GetRatingsWithDetailsAsync(cancellationToken);
        return View(ratings);
    }

    public async Task<IActionResult> UpdateStatus(int ratingId, bool newStatus, CancellationToken cancellationToken)
    {
        if (newStatus)
        {
            if (await _ratingAppService.Accept(ratingId, cancellationToken))
            {
                TempData["UpdateStatus"] = "تغییر وضعیت با موفقییت ثبت شد";
                return RedirectToAction("RatingList");
            }

            TempData["UpdateStatus"] = "نظر مشتری یافت نشد";
            return RedirectToAction("RatingList");
        }

        else
        {
            if (await _ratingAppService.Reject(ratingId, cancellationToken))
            {
                TempData["UpdateStatus"] = "نظر مشتری رد صلاحیت شد و حذف شد";
                return RedirectToAction("RatingList");
            }



            TempData["UpdateStatus"] = "نظر مشتری یافت نشد";
            return RedirectToAction("RatingList");
        }

    }
}
