using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace KhaneBan.Domain.AppServices;

public class SubCategoryDapperAppService : ISubCategoryDapperAppService
{
    private readonly ISubCategoryDapperService _subCategoryDapperService;

    public SubCategoryDapperAppService(ISubCategoryDapperService subCategoryDapperService)
    {
        _subCategoryDapperService = subCategoryDapperService;
    }

    public Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)
        => _subCategoryDapperService.GetAllAsync(cancellationToken);
}
