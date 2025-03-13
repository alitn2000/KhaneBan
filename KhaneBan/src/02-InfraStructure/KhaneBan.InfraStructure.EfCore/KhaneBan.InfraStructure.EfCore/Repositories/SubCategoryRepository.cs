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

public class SubCategoryRepository : ISubCategoryRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<SubCategoryRepository> _logger;
    private readonly IMemoryCache _memoryCach;

    public SubCategoryRepository(AppDbContext context, ILogger<SubCategoryRepository> logger, IMemoryCache memoryCach)
    {
        _context = context;
        _logger = logger;
        _memoryCach = memoryCach;
    }
    //public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)
    //{
    //    var subcategories = _memoryCach.Get<List<SubCategory>>("GetAllsubsAsync");
    //    if (subcategories is null)
    //    {
    //        subcategories = await _context.SubCategories.ToListAsync(cancellationToken);
    //    }


    //    _memoryCach.Set("GetAllsubsAsync", subcategories, TimeSpan.FromMinutes(1));

    //    return subcategories;

    //}





    public async Task<SubCategory> GetByIdAsync(int id, CancellationToken cancellationToken)

        => await _context.SubCategories
                         .Include(x => x.HomeServices)
                         .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<bool> CreateAsync(SubCategory subCategory, CancellationToken cancellationToken)
    {
        try
        {
            await _context.SubCategories.AddAsync(subCategory, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("subcategory created Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in subcategory repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }

    }
    public async Task<bool> UpdateAsync(SubCategory subCategory, CancellationToken cancellationToken)
    {
        try
        {
            var existingSubCategory = await _context.SubCategories.FirstOrDefaultAsync(x => x.Id == subCategory.Id, cancellationToken);

            if (existingSubCategory == null)
                return false;

            existingSubCategory.Title = subCategory.Title;
            existingSubCategory.CategoryId = subCategory.CategoryId;
            existingSubCategory.PicturePath = subCategory.PicturePath;
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("subcategory updated Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in subcategory repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }


    }

    public async Task ActiveSubCategoryAsync(int subCategoryId, CancellationToken cancellationToken)
    {

        try
        {
            var existSubCategory = await _context
            .SubCategories
            .FirstOrDefaultAsync(c => c.Id == subCategoryId, cancellationToken);

            existSubCategory.IsDeleted = false;
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" Active subcategory Succesfully");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in subcategory repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
        }
    }


    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var subCategory = await _context.SubCategories
                                            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (subCategory == null)
                return false;

            subCategory.IsDeleted = true;
            _logger.LogInformation("subcategory deleted Succesfully");
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("sError in subcategory repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }


    }

    //public async Task<List<SubCategory>> GetAllSubCategoriesAsync(CancellationToken cancellationToken)
    //    => await _context.SubCategories.ToListAsync(cancellationToken);

}
