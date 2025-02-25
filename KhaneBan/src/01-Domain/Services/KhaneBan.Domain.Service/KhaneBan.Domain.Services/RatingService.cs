using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.UserRequests;

namespace KhaneBan.Domain.Services;

public class RatingService : IRatingService
{
    private readonly IRatingRepository _ratingRepository;

    public RatingService(IRatingRepository ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    public async Task<bool> Accept(int id, CancellationToken cancellationToken)
        => await _ratingRepository.Accept(id, cancellationToken);

    public async Task<List<Rating>> GetRatingsWithDetailsAsync(CancellationToken cancellationToken)
        => await _ratingRepository.GetRatingsWithDetailsAsync(cancellationToken);

    public async Task<bool> Reject(int id, CancellationToken cancellationToken)
        => await _ratingRepository.Reject(id,cancellationToken);
}
