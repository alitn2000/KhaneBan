using KhaneBan.Domain.Core.Entites.DTOs;
using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Repository;

public interface IRatingRepository
{
    Task<Rating?> GetRatingByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<Rating>> GetRatingsWithDetailsAsync(CancellationToken cancellationToken);
    Task<Rating?> GetRatingByIdWithDetailsAsync(int id, CancellationToken cancellationToken);
    Task CreateAsync(CreateRatingDTO review, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Rating rating, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Rating rating, CancellationToken cancellationToken);
    Task<bool> IsDelete(int ratingId, CancellationToken cancellationToken);
    Task<bool> RatingSet(int expertId, int rate, CancellationToken cancellationToken);
    Task<bool> Accept(int id, CancellationToken cancellationToken);
    Task<bool> Reject(int id, CancellationToken cancellationToken);
}
