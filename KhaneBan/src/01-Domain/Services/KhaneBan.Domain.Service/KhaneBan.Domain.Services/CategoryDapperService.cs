using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Services;

public class CategoryDapperService :ICategoryDapperService
{
    private readonly ICategoryDapperRepository _categoryDapperRepository;

    public CategoryDapperService(ICategoryDapperRepository categoryDapperRepository)
    {
        _categoryDapperRepository = categoryDapperRepository;
    }

    public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
        => await _categoryDapperRepository.GetAllAsync(cancellationToken);

    //public async Task<List<Category>> GetAllWithDetailsAsync(CancellationToken cancellationToken)
    //    => await _categoryDapperRepository.GetAllWithDetailsAsync(cancellationToken);
}
