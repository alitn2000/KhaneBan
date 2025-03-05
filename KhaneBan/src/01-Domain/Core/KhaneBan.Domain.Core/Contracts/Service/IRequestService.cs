using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Service;

public interface IRequestService
{
    Task<bool> CreateAsync(Request request, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Request request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int requestId, CancellationToken cancellationToken);
    Task<List<Request>> GetRequestsInfo(CancellationToken cancellationToken);
    Task<bool> ChangeStatus(int requestId, StatusEnum status, CancellationToken cancellationToken);
    Task<List<Request>> GetCustomersRequestAsync(int userId, CancellationToken cancellationToken);
    Task<Request?> GetRequestByIdAsync(int requestId, CancellationToken cancellationToken);


}
