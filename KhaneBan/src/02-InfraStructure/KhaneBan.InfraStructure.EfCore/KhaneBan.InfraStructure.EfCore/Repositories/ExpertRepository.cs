using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.InfraStructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.InfraStructure.EfCore.Repositories;

public class ExpertRepository : IExpertRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<ExpertRepository> _logger;

    public ExpertRepository(AppDbContext appDbContext, ILogger<ExpertRepository> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }

    public async Task<List<Expert>> GetExpertsAsync(CancellationToken cancellationToken)
        => await _appDbContext.Experts.AsNoTracking()
        .ToListAsync(cancellationToken);

    public async Task<List<Expert>> GetExpertInfoAsync(CancellationToken cancellationToken)
       => await _appDbContext.Experts.Include(e => e.User).ToListAsync(cancellationToken);

    public async Task<Expert?> GetExpertInfoByIdAsync(int id, CancellationToken cancellationToken)
     => await _appDbContext
     .Experts
     .Include(c => c.User)
     .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task<Expert?> GetExpertByIdAsync(int id, CancellationToken cancellationToken)
     => await _appDbContext
     .Experts
     .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task<List<Expert>> GetExpertsWithDetailsAsync(CancellationToken cancellationToken)
       => await _appDbContext
        .Experts
        .Include(e => e.User)
        .Include(e => e.HomeServices)
        .ThenInclude(e => e.SubCategory)
        .ThenInclude(e => e.Category)
        .Include(e => e.Suggestions)
        .ToListAsync(cancellationToken);

    public async Task<Expert?> GetExpertByIdWithDetailsAsync(int id, CancellationToken cancellationToken)
        => await _appDbContext
        .Experts
        .Include(e => e.User)
        .Include(e => e.HomeServices)
        .ThenInclude(e => e.SubCategory)
        .ThenInclude(e => e.Category)
        .Include(e => e.Suggestions)
        .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task<int> GetCountExpertAsync(CancellationToken cancellationToken)
       => await _appDbContext
       .Experts.AsNoTracking().CountAsync(cancellationToken);



    public async Task<bool> CreateAsync(Expert expert, CancellationToken cancellationToken)
    {
        try
        {
            await _appDbContext.Experts.AddAsync(expert, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" create expert Succesfully");
            return true;
        }
        catch(Exception ex)
        {
            _logger.LogError("Error in expert repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int userId, CancellationToken cancellationToken)
    {
        try
        {
            var existExpert = await _appDbContext
                .Experts.Include(u => u.User)
                .FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);

            if (existExpert == null)
                return false;

            existExpert.User.IsDeleted = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" delete expert Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in expert repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }
    }

    public async Task ActiveExpert(int userId, CancellationToken cancellationToken)
    {

        try
        {
            var existExpert = await _appDbContext
            .Experts
            .Include(u => u.User)
            .FirstOrDefaultAsync(c => c.Id == userId, cancellationToken);
            if (existExpert == null)
                return;

            existExpert.User.IsDeleted = false;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" Active expert Succesfully");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in expert repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
        }
    }

    public async Task<bool> UpdateAsync(Expert expert, CancellationToken cancellationToken)
    {
        try
        {
            var existExpert = await _appDbContext.Experts.Include(a => a.User).FirstOrDefaultAsync(c => c.Id == expert.Id, cancellationToken);

            if (existExpert == null)
                return false;

            existExpert.User.Address = expert.User.Address;
            existExpert.User.FirstName = expert.User.FirstName;
            existExpert.User.LastName = expert.User.LastName;
            existExpert.User.Email = expert.User.Email;
            existExpert.User.CityId = expert.User.CityId;
            existExpert.User.PicturePath = expert.User.PicturePath;
            existExpert.User.PhoneNumber = expert.User.PhoneNumber;

            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" update expert Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in expert repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }
    }
}
