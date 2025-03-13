using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Entites.UserRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KhaneBan.EndPoints.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeServiceController : ControllerBase
    {
        private readonly IHomeServiceDapperAppService _homeServiceDapperAppService;

        public HomeServiceController(IHomeServiceDapperAppService homeServiceDapperAppService)
        {
            _homeServiceDapperAppService = homeServiceDapperAppService;
        }

        [HttpGet("List")]
        public async Task<List<HomeService>> CategoryList(CancellationToken cancellationToken)
        {
            return await _homeServiceDapperAppService.GetAllAsync(cancellationToken);
        }

    }
}
