﻿using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.Domain.Core.Enums;
using KhaneBan.InfraStructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace KhaneBan.InfraStructure.EfCore.Repositories;

public class RequestRepository : IRequestRepository
{
    private readonly AppDbContext _appDbContext;

    public RequestRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Request>?> GetRequestsAsync(CancellationToken cancellationToken)
       => await _appDbContext.Requests
       .ToListAsync(cancellationToken);

    public async Task<List<Request>?> GetUserRequestsAsync(int customerId ,CancellationToken cancellationToken)
       => await _appDbContext.Requests
        .Where(r => r.CustomerId == customerId)                     
        .ToListAsync(cancellationToken);

    public async Task<Request?> GetRequestByIdAsync(int requestId, CancellationToken cancellationToken)
     => await _appDbContext
     .Requests
     .FirstOrDefaultAsync(s => s.Id == requestId, cancellationToken);

    public async Task<Request?> GetUserRequestByIdAsync(int customerId,int requestId, CancellationToken cancellationToken)
    => await _appDbContext
    .Requests
    .FirstOrDefaultAsync(s => s.CustomerId == customerId && s.Id == requestId, cancellationToken);

    public async Task<List<Request>> GetRequestsWithDetailsAsync(CancellationToken cancellationToken)
          => await _appDbContext
           .Requests
           .Include(r => r.HomeService)
           .Include(r => r.City)
           .Include(r => r.Pictures)
           .Include(r => r.Suggestions)
           .ToListAsync(cancellationToken);

    public async Task<List<Request>> GetUserRequestsWithDetailsAsync(int customerId, CancellationToken cancellationToken)
         => await _appDbContext
          .Requests
          .Where(r =>r.CustomerId == customerId)
          .Include(r => r.HomeService)
          .Include(r => r.City)
          .Include(r => r.Pictures)
          .Include(r => r.Suggestions)
          .ToListAsync(cancellationToken);

    public async Task<Request?> GetRequestByIdWithDetailsAsync(int requestId, CancellationToken cancellationToken)
          => await _appDbContext
           .Requests
           .Include(r => r.HomeService)
           .Include(r => r.City)
           .Include(r => r.Pictures)
           .Include(r => r.Suggestions)
           .FirstOrDefaultAsync(e => e.Id == requestId, cancellationToken);

    public async Task<Request?> GetUserRequestByIdWithDetailsAsync(int customerId,int requestId, CancellationToken cancellationToken)
          => await _appDbContext
           .Requests
           .Include(r => r.HomeService)
           .Include(r => r.City)
           .Include(r => r.Pictures)
           .Include(r => r.Suggestions)
           .FirstOrDefaultAsync(r => r.CustomerId == customerId && r.Id == requestId, cancellationToken);



    public async Task<bool> CreateAsync(Request request, CancellationToken cancellationToken)
    {
        try
        {
            await _appDbContext.Requests.AddAsync(request, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
       
    }

    public async Task<bool> DeleteAsync(int requestId, CancellationToken cancellationToken)
    {
        try
        {
            var request = await _appDbContext.Requests
            .Include(x => x.Pictures)
            .Include(x => x.Suggestions)
            .FirstOrDefaultAsync(x => x.Id == requestId, cancellationToken);

            if (request == null)
                return false;

            _appDbContext.Requests.Remove(request);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            throw new Exception("Logic Errorr");
        }

    }

    public async Task<bool> IsDelete(int requestId, CancellationToken cancellationToken)
    {
        var existRequest = await _appDbContext.Requests.FirstOrDefaultAsync(r => r.Id == requestId, cancellationToken);
        if (existRequest == null)
            return false;
        existRequest.IsDeleted = true;
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> ChangeStatus(StatusEnum status, int requestId, CancellationToken cancellationToken)
    {
        var existRequest = await _appDbContext.Requests.FirstOrDefaultAsync(s => s.Id == requestId);
        if (existRequest == null)
            return false;
        existRequest.RequestStatus = status;

        await _appDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateAsync(Request request, CancellationToken cancellationToken)
    {
        try
        {
            var existRequest = await _appDbContext.Requests.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existRequest == null)
                return false;

            existRequest.Title = request.Title;
            existRequest.Description = request.Description;
            existRequest.StartTime = request.StartTime;
            existRequest.EndTime = request.EndTime;
            existRequest.RequestStatus = request.RequestStatus;
            existRequest.CityId = request.CityId;

            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            throw new Exception("Logic Error");
        }
        
        
    }



}
