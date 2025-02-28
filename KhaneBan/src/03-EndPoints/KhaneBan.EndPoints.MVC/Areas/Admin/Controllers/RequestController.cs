using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhaneBan.EndPoints.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles ="Admin")]
public class RequestController : Controller
{
    private readonly IRequestAppService _requestAppService;
    private readonly ISuggestionAppService _suggestionAppService;

    public RequestController(IRequestAppService requestAppService, ISuggestionAppService suggestionAppService)
    {
        _requestAppService = requestAppService;
        _suggestionAppService = suggestionAppService;
    }

    public async Task<IActionResult> RequestList(CancellationToken cancellationToken)
    {
        var requests = await _requestAppService.GetRequestsInfo(cancellationToken);

        return View(requests);
    }

    public async Task<IActionResult> RequestsSuggestions(int requestId, CancellationToken cancellationToken)
    {
        var suggestions = await _suggestionAppService.GetRequestSuggestions(requestId, cancellationToken);

        return View(suggestions);
    }

    public async Task<IActionResult> ChangeStatus(int requestId, StatusEnum Statusenum, CancellationToken cancellationToken)
    {
        bool flag = await _requestAppService.ChangeStatus(requestId, Statusenum, cancellationToken);

        if (flag)
        {
            ViewBag.StatusMessage = "وضعیت سفارش با موفقیت تغییر یافت";
            return RedirectToAction("RequestList");
        }

        ViewBag.StatusMessageFaild = "تغییر وضعیت صورت نگرفت";
        return RedirectToAction("RequestList");

    }
}
