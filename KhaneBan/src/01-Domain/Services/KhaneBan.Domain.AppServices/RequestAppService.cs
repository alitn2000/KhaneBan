using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.AppServices;

public class RequestAppService : IRequestAppService
{
    private readonly IRequestService _requestService;

    public RequestAppService(IRequestService requestService)
    {
        _requestService = requestService;
    }

    public async Task<bool> ChangeStatus(int requestId, StatusEnum status, CancellationToken cancellationToken)
    {
         return await _requestService.ChangeStatus(requestId, status, cancellationToken);
    }

    public async Task<bool> CreateAsync(Request request, CancellationToken cancellationToken)
    {
        return await _requestService.CreateAsync(request, cancellationToken);
    }

    public async Task<bool> DeleteAsync(int requestId, CancellationToken cancellationToken)
    {
        return await _requestService.DeleteAsync(requestId, cancellationToken);
    }

    public async Task<List<Request>> GetCustomersRequestAsync(int userId, CancellationToken cancellationToken)
        => await _requestService.GetCustomersRequestAsync(userId, cancellationToken);

    public async Task<int> GetPaidByCustomerOrderCountAsync(int userId, CancellationToken cancellationToken)
        => await _requestService.GetPaidByCustomerOrderCountAsync(userId, cancellationToken);

    public async Task<Request?> GetRequestByIdAsync(int requestId, CancellationToken cancellationToken)
        => await _requestService.GetRequestByIdAsync(requestId, cancellationToken);

    public async Task<List<Request>> GetRequestsByHomeServices(List<int> homeServiceIds, int cityId, CancellationToken cancellationToken)
        => await _requestService.GetRequestsByHomeServices(homeServiceIds, cityId, cancellationToken);

    public async Task<List<Request>> GetRequestsInfo(CancellationToken cancellationToken)
    {
        return await _requestService.GetRequestsInfo(cancellationToken);
    }

    public async Task<int?> GetWinnerExpertIdAsync(int requestId, CancellationToken cancellationToken)
        => await _requestService.GetWinnerExpertIdAsync(requestId, cancellationToken);

    public async Task<Result> SetWinner(int requestId, int suggestionId, CancellationToken cancellationToken)
        => await _requestService.SetWinner(requestId, suggestionId, cancellationToken);

    public async Task<bool> UpdateAsync(Request request, CancellationToken cancellationToken)
    {
        return await _requestService.UpdateAsync(request, cancellationToken);
    }

    public async Task<Result> UpdateStatusAsync(int requestId, StatusEnum newStatus, CancellationToken cancellationToken)
        => await _requestService.UpdateStatusAsync(requestId, newStatus, cancellationToken);

   
}
