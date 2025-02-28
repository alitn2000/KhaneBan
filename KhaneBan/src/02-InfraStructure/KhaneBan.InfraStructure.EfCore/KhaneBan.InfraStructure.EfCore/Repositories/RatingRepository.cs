using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.InfraStructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Logging;

namespace KhaneBan.InfraStructure.EfCore.Repositories;

public class RatingRepository : IRatingRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<RatingRepository> _logger;

    public RatingRepository(AppDbContext appDbContext, ILogger<RatingRepository> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }




    public async Task<Rating?> GetRatingByIdAsync(int id, CancellationToken cancellationToken)
     => await _appDbContext
     .Ratings
     .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public async Task<List<Rating>> GetRatingsWithDetailsAsync(CancellationToken cancellationToken)
          => await _appDbContext
           .Ratings
           .Where(r => r.IsAccepted == null)
           .Include(r => r.Expert)
           .ThenInclude(r => r.User)
           .ToListAsync(cancellationToken);

    public async Task<Rating?> GetRatingByIdWithDetailsAsync(int id, CancellationToken cancellationToken)
          => await _appDbContext
           .Ratings
           .Include(r => r.Expert)
           .ThenInclude(r => r.User)
           .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task<bool> RatingSet(int expertId, int rate, CancellationToken cancellationToken)
    {
        try
        {
            var existingReview = await _appDbContext.Ratings.FirstOrDefaultAsync(x => x.ExpertId == expertId, cancellationToken);

            if (existingReview == null)
                return false;

            existingReview.Rate = rate;

            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" set rating Succesfully");
            return true;
        }

        catch (Exception ex)
        {
            _logger.LogError("Error in rating repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }


    }




    public async Task<bool> Accept(int id, CancellationToken cancellationToken)
    {
        try
        {
            var existRating = await _appDbContext.Ratings.FirstOrDefaultAsync(x => x.Id == id);

            if (existRating == null)
                return false;

            existRating.IsAccepted = true;

            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" accept  rating Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in rating repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;

        }

    }

    public async Task<bool> Reject(int id, CancellationToken cancellationToken)
    {
        try
        {
            var existingreview = await _appDbContext.Ratings.FirstOrDefaultAsync(x => x.Id == id);
            if (existingreview == null)
                return false;

            existingreview.IsAccepted = false;
            existingreview.IsDeleted = true;

            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" reject rating Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in rating repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }


    }

    public async Task<bool> CreateAsync(Rating rating, CancellationToken cancellationToken)
    {
        try
        {
            await _appDbContext.Ratings.AddAsync(rating, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" create rating Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in rating repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);

            return false;
        }
    }

    public async Task<bool> DeleteAsync(Rating rating, CancellationToken cancellationToken)
    {
        try
        {
            _appDbContext.Ratings.Remove(rating);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" delete rating Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in rating repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }
    }
    public async Task<bool> IsDelete(int ratingId, CancellationToken cancellationToken)
    {
        try
        {
            var existRating = await _appDbContext.Ratings.FirstOrDefaultAsync(r => r.Id == ratingId, cancellationToken);
            if (existRating == null)
                return false;
            existRating.IsDeleted = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" isdeleted rating Succesfully");
            return true;
        }
        catch(Exception ex)
        {
            _logger.LogError("Error in rating repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false ;
        }


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
            existRating.IsAccepted = rating.IsAccepted;

            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" update rating Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in rating repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }

    }


}
