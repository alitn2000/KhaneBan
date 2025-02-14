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

public class SubCategoryRepository : ISubCategoryRepository
{
    private readonly AppDbContext _appDbContext;

    public SubCategoryRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<SubCategory>?> GetSubCategoriesAsync(CancellationToken cancellationToken)
     => await _appDbContext.SubCategories
     .ToListAsync(cancellationToken);

    public async Task<SubCategory?> GetSubCategoryByIdAsync(int id, CancellationToken cancellationToken)
     => await _appDbContext
     .SubCategories
     .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public async Task<List<SubCategory>?> GetSubCategoriesWithDetailsAsync(CancellationToken cancellationToken)
       => await _appDbContext
        .SubCategories
        .Include(s => s.HomeServices)
        .ToListAsync(cancellationToken);

    public async Task<SubCategory?> GetCategoryByIdWithDetailsAsync(int id, CancellationToken cancellationToken)
        => await _appDbContext
         .SubCategories
         .Include(s => s.HomeServices)
         .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);



    public async Task<bool> CreateAsync(SubCategory subCategory, CancellationToken cancellationToken)
    {
        try
        {
            await _appDbContext.SubCategories.AddAsync(subCategory, cancellationToken);
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
            var subCategory = await _appDbContext.SubCategories
            .Include(x => x.HomeServices)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (subCategory == null)
                return false;

            _appDbContext.SubCategories.Remove(subCategory);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            throw new Exception("Logic Errorr");
        }
    }

    public async Task<bool> UpdateAsync(SubCategory subCategory, CancellationToken cancellationToken)
    {
        try
        {
            var existSubCategory = await _appDbContext.SubCategories.FirstOrDefaultAsync(x => x.Id == subCategory.Id);
            if (existSubCategory == null)
                return false;

            existSubCategory.Title = subCategory.Title;
            existSubCategory.PicturePath = subCategory.PicturePath;
            existSubCategory.CategoryId = subCategory.CategoryId;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            throw new Exception("Logic error");
        }

    }

}
