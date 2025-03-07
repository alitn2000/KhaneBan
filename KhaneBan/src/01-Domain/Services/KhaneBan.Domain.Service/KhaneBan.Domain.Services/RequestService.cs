using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.BaseEntities;
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

    public async Task<List<Request>> GetCustomersRequestAsync(int userId, CancellationToken cancellationToken)
        => await _requestRepository.GetCustomersRequestAsync(userId, cancellationToken);

    public async Task<Request?> GetRequestByIdAsync(int requestId, CancellationToken cancellationToken)
        => await _requestRepository.GetRequestByIdAsync(requestId, cancellationToken);

    public async Task<Result> UpdateStatusAsync(int requestId, StatusEnum newStatus, CancellationToken cancellationToken)
    {
        var request = await _requestRepository.GetRequestByIdAsync(requestId, cancellationToken);
        if (request == null)
            return new Result("درخواست یافت نشد", false);

        if (await _requestRepository.UpdateStatus(request, newStatus, cancellationToken))
            return new Result("تغغیر وضعیت با موفقیت انجام شد", true);

        return new Result("تغغیر وضعیت انجام نشد", false);


    }

    public async Task<Result> SetWinner(int requestId, int suggestionId, CancellationToken cancellationToken)
    {
        var request = await _requestRepository.GetRequestByIdAsync(requestId, cancellationToken);
        if (request == null)
            return new Result("پیشنهاد یافت نشد", false);

        if (await _requestRepository.SetWinner(request, suggestionId, cancellationToken))
            return new Result("پیشنهاد با موفقیت انتخاب شد", true);

        return new Result("انتخاب پیشنهاد برنده انجام نشد", false);
    }

    public async Task<int> GetPaidByCustomerOrderCountAsync(int userId, CancellationToken cancellationToken)
        => await _requestRepository.GetPaidByCustomerOrderCountAsync(userId, cancellationToken);
}
