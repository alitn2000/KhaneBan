using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Entites.UserRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KhaneBan.EndPoints.API.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryDapperAppService _categoryDapperAppService;
    private readonly ICategoryAppService _categoryAppService;
    public CategoryController(ICategoryDapperAppService categoryDapperAppService, ICategoryAppService categoryAppService)
    {
        _categoryDapperAppService = categoryDapperAppService;
        _categoryAppService = categoryAppService;
    }

    [HttpGet("List")]
    public async Task<List<Category>> CategoryList(CancellationToken cancellationToken)
    {
        return await _categoryDapperAppService.GetAllAsync(cancellationToken);
    }

    [HttpGet("LisWithSubs")]
    public async Task<List<Category>> CategoryListSubs(CancellationToken cancellationToken)
    {
        return await _categoryAppService.GetAllWithDetailsAsync(cancellationToken);
    }
}
