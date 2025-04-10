using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.InfraStructure.EfCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Services;

public class SubCategoryService : ISubCategoryService
{
    private readonly ISubCategoryRepository _subCategoryRepository;

    public SubCategoryService(ISubCategoryRepository subCategoryRepository)
    {
        _subCategoryRepository = subCategoryRepository;
    }

    public async Task<bool> CreateAsync(SubCategory subCategory, CancellationToken cancellationToken)

        => await _subCategoryRepository.CreateAsync(subCategory, cancellationToken);

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

        => await _subCategoryRepository.DeleteAsync(id, cancellationToken);

    public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)

       => await _subCategoryRepository.GetAllAsync(cancellationToken);


    public async Task<SubCategory> GetByIdAsync(int id, CancellationToken cancellationToken)

      => await _subCategoryRepository.GetByIdAsync(id, cancellationToken);

    public async Task<bool> UpdateAsync(SubCategory subCategory, CancellationToken cancellationToken)

      => await _subCategoryRepository.UpdateAsync(subCategory, cancellationToken);

    public async Task<bool> ActiveSubCategoryAsync(int subCategoryId, CancellationToken cancellationToken)
    {
        var subCategory = await _subCategoryRepository.GetByIdAsync(subCategoryId, cancellationToken);
        if (subCategory != null)
        {
            await _subCategoryRepository.ActiveSubCategoryAsync(subCategoryId, cancellationToken);
            return true;

        }

        return false;
    }
}

