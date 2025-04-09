using KhaneBan.Domain.AppServices;
using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.EndPoints.API.WebFrameWork.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KhaneBan.EndPoints.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(ApiKeyCheck))]
public class RequestController : ControllerBase
{
    private readonly IRequestAppService _requestAppService;

    public RequestController(IRequestAppService requestAppService)
    {
        _requestAppService = requestAppService;
    }

    [HttpGet("List")]
    public async Task<List<Request>> RequestList(CancellationToken cancellationToken)
    {
        var r = await _requestAppService.GetRequestsInfo(cancellationToken);
        return r;
    }
}
