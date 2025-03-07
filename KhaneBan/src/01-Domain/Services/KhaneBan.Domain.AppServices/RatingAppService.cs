using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.DTOs;
using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.AppServices;

public class RatingAppService : IRatingAppService
{
    private readonly IRatingService _ratingService;

    public RatingAppService(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    public async Task<bool> Accept(int id, CancellationToken cancellationToken)
        => await _ratingService.Accept(id, cancellationToken).ConfigureAwait(false);

    public async Task<List<Rating>> GetRatingsWithDetailsAsync(CancellationToken cancellationToken)
        => await _ratingService.GetRatingsWithDetailsAsync(cancellationToken);

    public async Task<bool> Reject(int id, CancellationToken cancellationToken)
        => await _ratingService.Reject(id, cancellationToken);
    public async Task CreateAsync(CreateRatingDTO review, CancellationToken cancellationToken)

          => await _ratingService.CreateAsync(review, cancellationToken);
}
