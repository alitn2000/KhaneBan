using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Repository;

public interface ISubCategoryRepository
{
    Task<bool> CreateAsync(SubCategory subCategory, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    //Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken);
    Task<SubCategory> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(SubCategory subCategory, CancellationToken cancellationToken);
    Task ActiveSubCategoryAsync(int subCategoryId, CancellationToken cancellationToken);
    Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken);
}
