using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.InfraStructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KhaneBan.InfraStructure.EfCore.Repositories;

public class HomeServiceRepository : IHomeServiceRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<HomeServiceRepository> _logger;

    public HomeServiceRepository(AppDbContext context, ILogger<HomeServiceRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.HomeServices
            .Include(x => x.SubCategory)
            .ToListAsync(cancellationToken);
    }

    public async Task<HomeService> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.HomeServices
            .Include(x => x.SubCategory)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> CreateAsync(HomeService homeService, CancellationToken cancellationToken)
    {
        try
        {
            await _context.HomeServices.AddAsync(homeService, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("homeservice Added Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in homeservice repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }


    }

    public async Task<bool> UpdateAsync(HomeService homeService, CancellationToken cancellationToken)
    {
        try
        {
            var existingHomeService = await _context.HomeServices
                .FirstOrDefaultAsync(x => x.Id == homeService.Id, cancellationToken);
            if (existingHomeService == null)
                return false;

            existingHomeService.Title = homeService.Title;
            existingHomeService.SubCategoryId = homeService.SubCategoryId;
            existingHomeService.VisitCount = homeService.VisitCount;
            existingHomeService.PicturePath = homeService.PicturePath;
            existingHomeService.BasePrice = homeService.BasePrice;
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("homeservice update Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in homeservice repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }
    }

    public async Task ActiveHomeServiceAsync(int homeServiceId, CancellationToken cancellationToken)
    {

        try
        {
            var existHomeServicce= await _context
            .HomeServices
            .FirstOrDefaultAsync(c => c.Id == homeServiceId, cancellationToken);

            existHomeServicce.IsDeleted = false;
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" Active homeservice Succesfully");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in homeservice repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var homeService = await _context.HomeServices
           .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (homeService == null)
                return false;

            homeService.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("homeservice update Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in homeservice repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }

    }


}