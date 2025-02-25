using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Service;

public interface IRatingService
{
    Task<bool> Accept(int id, CancellationToken cancellationToken);
    Task<bool> Reject(int id, CancellationToken cancellationToken);
    Task<List<Rating>> GetRatingsWithDetailsAsync(CancellationToken cancellationToken);
}
