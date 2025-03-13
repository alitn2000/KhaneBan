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

    public CategoryController(ICategoryDapperAppService categoryDapperAppService)
    {
        _categoryDapperAppService = categoryDapperAppService;
    }

    [HttpGet("List")]
    public async Task<List<Category>> CategoryList(CancellationToken cancellationToken)
    {
        return await _categoryDapperAppService.GetAllAsync(cancellationToken);
    }
}
