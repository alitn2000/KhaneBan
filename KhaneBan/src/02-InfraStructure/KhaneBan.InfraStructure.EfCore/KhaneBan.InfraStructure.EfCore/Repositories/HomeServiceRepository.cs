using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.InfraStructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace KhaneBan.InfraStructure.EfCore.Repositories;

public class HomeServiceRepository : IHomeServiceRepository
{
    private readonly AppDbContext _appDbContext;

    public HomeServiceRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<HomeService>?> GetHomeServicesAsync(CancellationToken cancellationToken)
     => await _appDbContext.HomeServices
     .ToListAsync(cancellationToken);

    public async Task<List<HomeService>?> GetHomeServicesWithDetailsAsync(CancellationToken cancellationToken)
    => await _appDbContext.HomeServices
        .Include(h => h.Requests)
        .Include(h => h.Experts)
    .ToListAsync(cancellationToken);

    public async Task<HomeService?> GetHomeServiceByIdAsync(int id, CancellationToken cancellationToken)
     => await _appDbContext
     .HomeServices
     .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public async Task<HomeService?> GetHomeServiceByIdWithDetailsAsync(int homeServiceId, CancellationToken cancellationToken)
     => await _appDbContext
     .HomeServices
        .Include(h => h.Requests)
        .Include(h => h.Experts)
        .FirstOrDefaultAsync(s => s.Id == homeServiceId, cancellationToken);

    public async Task<bool> CreateAsync(HomeService homeService, CancellationToken cancellationToken)
    {
        try
        {
            await _appDbContext.HomeServices.AddAsync(homeService, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
       
    }

    public async Task<bool> DeleteAsync(int homeServiceId, CancellationToken cancellationToken)
    {
        try
        {
            var homeService = await _appDbContext.HomeServices
            .Include(x => x.Requests)
            .FirstOrDefaultAsync(x => x.Id == homeServiceId, cancellationToken);

            if (homeService == null)
                return false;

            _appDbContext.HomeServices.Remove(homeService);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            throw new Exception("Logic Errorr");
        }
    }

    public async Task<bool> IsDelete(int homeServiceId, CancellationToken cancellationToken)
    {
        var existHomeService = await _appDbContext.HomeServices.FirstOrDefaultAsync(r => r.Id == homeServiceId, cancellationToken);
        if (existHomeService == null)
            return false;
        existHomeService.IsDeleted = true;
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
    public async Task<bool> UpdateAsync(HomeService homeService, CancellationToken cancellationToken)
    {
        try
        {
            var existHomeService = await _appDbContext.HomeServices.FirstOrDefaultAsync(x => x.Id == homeService.Id);
            if (existHomeService == null)
                return false;
            
                existHomeService.Title = homeService.Title;
                existHomeService.SubCategoryId = homeService.SubCategoryId;
                existHomeService.VisitCount = homeService.VisitCount;
                existHomeService.ImagePath = homeService.ImagePath;
                existHomeService.BasePrice = homeService.BasePrice;
                await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
            
        }
        catch
        {
            throw new Exception("Logic Errorr");
        }
       
    }
}
