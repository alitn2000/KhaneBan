using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Repository;

public interface IRequestRepository
{
    Task<List<Request>?> GetRequestsAsync(CancellationToken cancellationToken);
    Task<List<Request>?> GetUserRequestsAsync(int customerId, CancellationToken cancellationToken);
    Task<Request?> GetRequestByIdAsync(int requestId, CancellationToken cancellationToken);
    Task<Request?> GetUserRequestByIdAsync(int customerId, int requestId, CancellationToken cancellationToken);
    Task<List<Request>> GetRequestsWithDetailsAsync(CancellationToken cancellationToken);
    Task<List<Request>> GetUserRequestsWithDetailsAsync(int customerId, CancellationToken cancellationToken);
    Task<Request?> GetRequestByIdWithDetailsAsync(int requestId, CancellationToken cancellationToken);
    Task<Request?> GetUserRequestByIdWithDetailsAsync(int customerId, int requestId, CancellationToken cancellationToken);
    Task<List<Request>> GetRequestsInfo(CancellationToken cancellationToken);
    Task ChangeStatus(int requestId, StatusEnum status, CancellationToken cancellationToken);
    Task<bool> CreateAsync(Request request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int requestId, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Request request, CancellationToken cancellationToken);
    Task<bool> IsDelete(int requestId, CancellationToken cancellationToken);
    Task<bool> ChangeStatus(StatusEnum status, int requestId, CancellationToken cancellationToken);

}
