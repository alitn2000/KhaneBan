using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.InfraStructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.InfraStructure.EfCore.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<CategoryRepository> _logger;
    private readonly IMemoryCache _memoryCach;
    public CategoryRepository(AppDbContext context, ILogger<CategoryRepository> logger, IMemoryCache memoryCach)
    {
        _context = context;
        _logger = logger;
        _memoryCach = memoryCach;
    }

    //public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
    //{
    //    var categories = _memoryCach.Get<List<Category>>("GetAllAsync");
    //    if(categories is null)
    //    {
    //       categories = await _context.Categories.ToListAsync(cancellationToken);
    //    }
    //    _memoryCach.Set("GetAllAsync", categories, TimeSpan.FromMinutes(1));

    //    return categories;
    //}


    public async Task<List<Category>> GetAllWithDetailsAsync(CancellationToken cancellationToken)
    {
        var categories = _memoryCach.Get<List<Category>>("GetAllWithDetailsAsync");
        if (categories is null)
        {
            categories = await _context.Categories
         .Where(c => !c.IsDeleted)
         .Include(c => c.SubCategories.Where(s => !s.IsDeleted))
         .ToListAsync(cancellationToken);

        }

        _memoryCach.Set("GetAllWithDetailsAsync", categories, TimeSpan.FromMinutes(1));

        return categories;
    }




    public async Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken)

            => await _context.Categories
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<Category?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken)

        => await _context
             .Categories
             .Include(c => c.SubCategories)
             .ThenInclude(c => c.HomeServices)
             .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);


    public async Task<bool> CreateAsync(Category category, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Category Added Succesfully");
            return true;
        }
        catch
        {
            _logger.LogError("something is wrong in create category");
            return false;
        }

    }
    public async Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        try
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id, cancellationToken);

            if (existingCategory == null)
                return false;

            existingCategory.Title = category.Title;
            existingCategory.PicturePath = category.PicturePath;
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Category updated Succesfully");
            return true;

        }
        catch
        {
            _logger.LogError("something is wrong in update category");
            return false;
        }

    }
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _context.Categories
                                         .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (category == null)
                return false;

            category.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Category deleted Succesfully");
            return true;
        }
        catch
        {
            _logger.LogError("something is wrong in delete category");
            return false;
        }

    }
    public async Task ActiveCategoryAsync(int categoryId, CancellationToken cancellationToken)
    {

        try
        {
            var existCategory = await _context
            .Categories
            .FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);

            existCategory.IsDeleted = false;
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" Active category Succesfully");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in category repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
        }
    }

}

