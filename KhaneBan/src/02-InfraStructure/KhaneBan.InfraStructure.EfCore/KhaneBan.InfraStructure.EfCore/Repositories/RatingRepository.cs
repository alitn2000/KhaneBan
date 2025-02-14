using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.InfraStructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace KhaneBan.InfraStructure.EfCore.Repositories;

public class RatingRepository : IRatingRepository
{
    private readonly AppDbContext _appDbContext;

    public RatingRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Rating>?> GetRatingsAsync(CancellationToken cancellationToken)
     => await _appDbContext.Ratings
     .ToListAsync(cancellationToken);

    public async Task<Rating?> GetRatingByIdAsync(int id, CancellationToken cancellationToken)
     => await _appDbContext
     .Ratings
     .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public async Task<List<Rating>> GetRatingsWithDetailsAsync(CancellationToken cancellationToken)
          => await _appDbContext
           .Ratings
           .Include(r => r.Expert)
           .Include(r => r.Customer)
           .ToListAsync(cancellationToken);

    public async Task<Rating?> GetRatingByIdWithDetailsAsync(int id, CancellationToken cancellationToken)
          => await _appDbContext
           .Ratings
           .Include(r => r.Expert)
           .Include(r => r.Customer)
           .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);


    public async Task<bool> UpdateStatusAsync(int ratingId, bool newStatus, CancellationToken cancellationToken)
    {
        var rating = await _appDbContext.Ratings.FirstOrDefaultAsync(r => r.Id == ratingId, cancellationToken);

        if (rating == null)
            return false;

        rating.Status = newStatus;

        await _appDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> CreateAsync(Rating rating, CancellationToken cancellationToken)
    {
        try
        {
            await _appDbContext.Ratings.AddAsync(rating, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Rating rating, CancellationToken cancellationToken)
    {
        try
        {
            _appDbContext.Ratings.Remove(rating);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<bool> IsDelete(int ratingId, CancellationToken cancellationToken)
    {
        var existRating = await _appDbContext.Ratings.FirstOrDefaultAsync(r => r.Id == ratingId, cancellationToken);
        if(existRating == null)
            return false;
        existRating.IsDeleted = true;
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateAsync(Rating rating, CancellationToken cancellationToken)
    {
        try
        {
            var existRating = await _appDbContext.Ratings.FirstOrDefaultAsync(x => x.Id == rating.Id);
            if (existRating == null)
                return false;

            existRating.Rate = rating.Rate;
            existRating.Comment = rating.Comment;
            existRating.Status = rating.Status;

            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
        
    }
}
