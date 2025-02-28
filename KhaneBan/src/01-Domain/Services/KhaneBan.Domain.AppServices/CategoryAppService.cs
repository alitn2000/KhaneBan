using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.AppServices;


    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryService _categoryService;

        public CategoryAppService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

    public async Task<bool> ActiveCategoryAsync(int categoryId, CancellationToken cancellationToken)
        => await _categoryService.ActiveCategoryAsync(categoryId, cancellationToken);

    public async Task<bool> CreateAsync(Category category, CancellationToken cancellationToken)

            => await _categoryService.CreateAsync(category, cancellationToken);

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

              => await _categoryService.DeleteAsync(id, cancellationToken);

        public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)

              => await _categoryService.GetAllAsync(cancellationToken);


        public async Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken)

              => await _categoryService.GetByIdAsync(id, cancellationToken);


        public async Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken)

             => await _categoryService.UpdateAsync(category, cancellationToken);

    }


