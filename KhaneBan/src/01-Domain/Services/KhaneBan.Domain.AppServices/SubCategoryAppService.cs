using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.AppServices;

public class SubCategoryAppService : ISubCategoryAppService
{
    private readonly ISubCategoryService _subCategoryService;

    public SubCategoryAppService(ISubCategoryService subCategoryService)
    {
        _subCategoryService = subCategoryService;
    }

    public Task<bool> ActiveSubCategoryAsync(int subCategoryId, CancellationToken cancellationToken)
        => _subCategoryService.ActiveSubCategoryAsync(subCategoryId, cancellationToken);

    public async Task<bool> CreateAsync(SubCategory subCategory, CancellationToken cancellationToken)

        => await _subCategoryService.CreateAsync(subCategory, cancellationToken);

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

        => await _subCategoryService.DeleteAsync(id, cancellationToken);

    //public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)

    //     => await _subCategoryService.GetAllAsync(cancellationToken);

    public async Task<SubCategory> GetByIdAsync(int id, CancellationToken cancellationToken)

        => await _subCategoryService.GetByIdAsync(id, cancellationToken);

    public async Task<bool> UpdateAsync(SubCategory subCategory, CancellationToken cancellationToken)

        => await _subCategoryService.UpdateAsync(subCategory, cancellationToken);
}