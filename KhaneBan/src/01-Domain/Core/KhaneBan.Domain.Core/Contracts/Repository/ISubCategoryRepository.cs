using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Repository;

public interface ISubCategoryRepository
{
    Task<List<SubCategory>?> GetSubCategoriesAsync(CancellationToken cancellationToken);
    Task<SubCategory?> GetSubCategoryByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<SubCategory>?> GetSubCategoriesWithDetailsAsync(CancellationToken cancellationToken);
    Task<SubCategory?> GetCategoryByIdWithDetailsAsync(int id, CancellationToken cancellationToken);
    Task<bool> CreateAsync(SubCategory subCategory, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(SubCategory subCategory, CancellationToken cancellationToken);
}
