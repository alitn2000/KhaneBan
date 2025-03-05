using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.Domain.Core.Enums;
using KhaneBan.InfraStructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KhaneBan.InfraStructure.EfCore.Repositories;

public class RequestRepository : IRequestRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<RequestRepository> _logger;

    public RequestRepository(AppDbContext appDbContext, ILogger<RequestRepository> logger)
    {

        _appDbContext = appDbContext;
        _logger = logger;
    }

    public async Task<List<Request>?> GetRequestsAsync(CancellationToken cancellationToken)
       => await _appDbContext.Requests
       .ToListAsync(cancellationToken);

    public async Task<List<Request>?> GetUserRequestsAsync(int customerId, CancellationToken cancellationToken)
       => await _appDbContext.Requests
        .Where(r => r.CustomerId == customerId)
        .ToListAsync(cancellationToken);

    public async Task<Request?> GetRequestByIdAsync(int requestId, CancellationToken cancellationToken)
     => await _appDbContext
     .Requests
     .FirstOrDefaultAsync(s => s.Id == requestId, cancellationToken);

    public async Task<List<Request>> GetCustomersRequestAsync(int userId, CancellationToken cancellationToken)
    => await _appDbContext.Requests
        .Where(r => r.Customer.UserId == userId)
        .Include(r => r.Customer) 
        .Include(r => r.HomeService)
        .ToListAsync(cancellationToken);

    public async Task<List<Request>> GetRequestsInfo(CancellationToken cancellationToken)
        => await _appDbContext.Requests
        .Where(r => r.IsDeleted == false)
        .Include(c => c.Customer)
        .ThenInclude(c => c.User)
        .Include(h => h.HomeService)
        .ToListAsync(cancellationToken);

    public async Task<Request?> GetUserRequestByIdAsync(int customerId, int requestId, CancellationToken cancellationToken)
    => await _appDbContext
    .Requests
    .FirstOrDefaultAsync(s => s.CustomerId == customerId && s.Id == requestId, cancellationToken);

    public async Task<List<Request>> GetRequestsWithDetailsAsync(CancellationToken cancellationToken)
          => await _appDbContext
           .Requests
           .Include(r => r.HomeService)
           .Include(r => r.City)
           .Include(r => r.RequestImages)
           .Include(r => r.Suggestions)
           .ToListAsync(cancellationToken);

    public async Task<List<Request>> GetUserRequestsWithDetailsAsync(int customerId, CancellationToken cancellationToken)
         => await _appDbContext
          .Requests
          .Where(r => r.CustomerId == customerId)
          .Include(r => r.HomeService)
          .Include(r => r.City)
          .Include(r => r.RequestImages)
          .Include(r => r.Suggestions)
          .ToListAsync(cancellationToken);

    public async Task<Request?> GetRequestByIdWithDetailsAsync(int requestId, CancellationToken cancellationToken)
          => await _appDbContext
           .Requests
           .Include(r => r.HomeService)
           .Include(r => r.City)
           .Include(r => r.RequestImages)
           .Include(r => r.Suggestions)
           .FirstOrDefaultAsync(e => e.Id == requestId, cancellationToken);

    public async Task<Request?> GetUserRequestByIdWithDetailsAsync(int customerId, int requestId, CancellationToken cancellationToken)
          => await _appDbContext
           .Requests
           .Include(r => r.HomeService)
           .Include(r => r.City)
           .Include(r => r.RequestImages)
           .Include(r => r.Suggestions)
           .FirstOrDefaultAsync(r => r.CustomerId == customerId && r.Id == requestId, cancellationToken);

    public async Task ChangeStatus(int requestId, StatusEnum status, CancellationToken cancellationToken)
    {
        try
        {
            var request = await _appDbContext.Requests.FirstOrDefaultAsync(r => r.Id == requestId, cancellationToken);

            if (request == null)
                return;

            request.RequestStatus = status;

            _logger.LogInformation(" changestatus request Succesfully");
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in request repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);


        }
    }
    public async Task<bool> CreateAsync(Request request, CancellationToken cancellationToken)
    {
        try
        {
            await _appDbContext.Requests.AddAsync(request, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" create request Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in request repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }

    }

    public async Task<bool> DeleteAsync(int requestId, CancellationToken cancellationToken)
    {
        try
        {
            var request = await _appDbContext.Requests
            .FirstOrDefaultAsync(x => x.Id == requestId, cancellationToken);

            if (request == null)
                return false;

            request.IsDeleted = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" delete request Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in request repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }

    }

    public async Task<bool> IsDelete(int requestId, CancellationToken cancellationToken)   
    {
        try
        {
            var existRequest = await _appDbContext.Requests.FirstOrDefaultAsync(r => r.Id == requestId, cancellationToken);
            if (existRequest == null)
                return false;
            existRequest.IsDeleted = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" isdelete request Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in request repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }


    }

    public async Task<bool> UpdateStatus( Request request, StatusEnum newStatus, CancellationToken cancellationToken)
    {
        try
        {

            request.RequestStatus = newStatus;

            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" ChangeStatus request Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in request repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }
        
    }

    public async Task<bool> UpdateAsync(Request request, CancellationToken cancellationToken)
    {
        try
        {
            var existRequest = await _appDbContext.Requests.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (existRequest == null)
                return false;

            existRequest.Title = request.Title;
            existRequest.Description = request.Description;
            existRequest.EndTime = request.EndTime;
            existRequest.RequestStatus = request.RequestStatus;
            existRequest.CityId = request.CityId;

            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" update request Succesfully");
            return true;
        }
        catch(Exception ex)
        {
            _logger.LogError("Error in request repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }


    }



}
