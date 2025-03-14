using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.AppServices;

public class CategoryDapperAppService : ICategoryDapperAppService
{
    private readonly ICategoryDapperService _categoryDapperService;

    public CategoryDapperAppService(ICategoryDapperService categoryDapperService)
    {
        _categoryDapperService = categoryDapperService;
    }

    public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
        => await _categoryDapperService.GetAllAsync(cancellationToken);

    //public async Task<List<Category>> GetAllWithDetailsAsync(CancellationToken cancellationToken)
    //    => await _categoryDapperService.GetAllWithDetailsAsync(cancellationToken);
}
