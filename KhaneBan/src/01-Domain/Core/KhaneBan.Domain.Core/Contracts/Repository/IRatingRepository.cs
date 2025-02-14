using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Repository;

public interface IRatingRepository
{
    Task<List<Rating>?> GetRatingsAsync(CancellationToken cancellationToken);
    Task<Rating?> GetRatingByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<Rating>> GetRatingsWithDetailsAsync(CancellationToken cancellationToken);
    Task<Rating?> GetRatingByIdWithDetailsAsync(int id, CancellationToken cancellationToken);
    Task<bool> CreateAsync(Rating rating, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Rating rating, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Rating rating, CancellationToken cancellationToken);
    Task UpdateStatus(Rating rating, bool newStatus, CancellationToken cancellationToken);
}
