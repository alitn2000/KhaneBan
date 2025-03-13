using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.AppServices;

public class HomeServiceDapperAppService : IHomeServiceDapperAppService
{
    private readonly IHomeServiceDapperService _homeServiceDapperService;

    public HomeServiceDapperAppService(IHomeServiceDapperService homeServiceDapperService)
    {
        _homeServiceDapperService = homeServiceDapperService;
    }

    public async Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken)
        => await _homeServiceDapperService.GetAllAsync(cancellationToken);

    public async Task<List<HomeService>> GetHomeServicesBySubCategoryId(int subCategoryId, CancellationToken cancellationToken)  
        => await _homeServiceDapperService.GetHomeServicesBySubCategoryId(subCategoryId, cancellationToken);
}
