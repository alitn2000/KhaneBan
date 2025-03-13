using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Services;

public class SubCategoryDapperService : ISubCategoryDapperService
{
    private readonly ISubCategoryDapperRepository _subCategoryDapperRepository;

    public SubCategoryDapperService(ISubCategoryDapperRepository subCategoryDapperRepository)
    {
        _subCategoryDapperRepository = subCategoryDapperRepository;
    }

    public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)
        => await _subCategoryDapperRepository.GetAllAsync(cancellationToken);
}
