﻿using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Service;

public interface ICategoryService
{
    //Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
    Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> CreateAsync(Category category, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<bool> ActiveCategoryAsync(int categoryId, CancellationToken cancellationToken);
    Task<List<Category>> GetAllWithDetailsAsync(CancellationToken cancellationToken);
}
