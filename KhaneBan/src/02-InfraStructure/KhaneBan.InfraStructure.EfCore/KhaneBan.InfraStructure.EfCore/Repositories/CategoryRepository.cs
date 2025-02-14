using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.InfraStructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.InfraStructure.EfCore.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _appDbContext;

    public CategoryRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Category>> GetCategoriesAsync(CancellationToken cancellationToken)
        => await _appDbContext.Categories
        .ToListAsync(cancellationToken);

    public async Task<Category?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken)
     => await _appDbContext
     .Categories
     .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task<List<Category>> GetCategoriesWithDetailsAsync(CancellationToken cancellationToken)
       => await _appDbContext
        .Categories
        .Include(c => c.SubCategories)
        .ThenInclude(c => c.HomeServices)
        .ToListAsync(cancellationToken);

    public async Task<Category?> GetCategoryByIdWithDetailsAsync(int id, CancellationToken cancellationToken)
        => await _appDbContext
        .Categories
         .Include(c => c.SubCategories)
         .ThenInclude(c => c.HomeServices)
         .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);



    public async Task<bool> CreateAsync(Category category, CancellationToken cancellationToken)
    {
        try
        {
            await _appDbContext.Categories.AddAsync(category, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _appDbContext.Categories
            .Include(x => x.SubCategories)
            .ThenInclude(x => x.HomeServices)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (category == null)
                return false;
            _appDbContext.Categories.Remove(category);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            throw new Exception("Logic Error");
        }





    }

    public async Task<bool> IsDelete(int categoryId, CancellationToken cancellationToken)
    {
        var existCategory = await _appDbContext.Categories.FirstOrDefaultAsync(r => r.Id == categoryId, cancellationToken);
        if (existCategory == null)
            return false;
        existCategory.IsDeleted = true;
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        try
        {
            var existingCategory = await _appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (existingCategory == null)
                return false;
            
                existingCategory.Title = category.Title;
                await _appDbContext.SaveChangesAsync(cancellationToken);
                return true;
            
        }
        catch
        {
            throw new Exception("Logic Errorr!!!");
        }
       
    }
}
 