using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.InfraStructure.EfCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> ActiveCategoryAsync(int categoryId, CancellationToken cancellationToken)
    {
        var customer = await _categoryRepository.GetByIdAsync(categoryId, cancellationToken);
        if (customer != null)
        {
            await _categoryRepository.ActiveCategoryAsync(categoryId, cancellationToken);
            return true;

        }

        return false;
    }

    public async Task<bool> CreateAsync(Category category, CancellationToken cancellationToken)

           => await _categoryRepository.CreateAsync(category, cancellationToken);

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

        => await _categoryRepository.DeleteAsync(id, cancellationToken);

    public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)

        => await _categoryRepository.GetAllAsync(cancellationToken);

    public async Task<List<Category>> GetAllWithDetailsAsync(CancellationToken cancellationToken)
        => await _categoryRepository.GetAllWithDetailsAsync(cancellationToken);

    public async Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken)

        => await _categoryRepository.GetByIdAsync(id, cancellationToken);


    public async Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken)

       => await _categoryRepository.UpdateAsync(category, cancellationToken);
}
