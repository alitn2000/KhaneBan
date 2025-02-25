using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.Domain.Core.Enums;
using KhaneBan.InfraStructure.EfCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Services;

public class RequestService : IRequestService
{
    private readonly IRequestRepository _requestRepository;

    public RequestService(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public async Task<bool> ChangeStatus(int requestId, StatusEnum status, CancellationToken cancellationToken)
    {
        var request = await _requestRepository.GetRequestByIdAsync(requestId,cancellationToken);

        if (request == null)
            return false;

       await _requestRepository.ChangeStatus(request.Id, status, cancellationToken);
        return true;

    }

    public async Task<bool> CreateAsync(Request request, CancellationToken cancellationToken)
        => await _requestRepository.CreateAsync(request, cancellationToken);

    public async Task<bool> DeleteAsync(int requestId, CancellationToken cancellationToken)         //////////fekr konam lazem nabash getesh koninm
    {
        var request = await _requestRepository.GetRequestByIdAsync(requestId, cancellationToken);
        if (request != null)
        {
            var flag = await _requestRepository.DeleteAsync(request.Id, cancellationToken);

            if (flag)
                return true;

            return false;
        }

        return false;
    }


    public async  Task<List<Request>> GetRequestsInfo(CancellationToken cancellationToken)
        => await _requestRepository.GetRequestsInfo(cancellationToken);

    public async Task<bool> UpdateAsync(Request request, CancellationToken cancellationToken)
        => await _requestRepository.UpdateAsync(request, cancellationToken);
}
