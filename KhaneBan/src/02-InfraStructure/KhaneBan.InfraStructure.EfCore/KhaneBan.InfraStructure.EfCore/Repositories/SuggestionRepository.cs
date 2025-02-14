using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.InfraStructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.InfraStructure.EfCore.Repositories;

public class SuggestionRepository : ISuggestionRepository
{
    private readonly AppDbContext _appDbContext;

    public SuggestionRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Suggestion>?> GetSuggestionesAsync(CancellationToken cancellationToken)
    => await _appDbContext.Suggestions
    .ToListAsync(cancellationToken);

    public async Task<List<Suggestion>?> GetUserSuggestionsAsync(int expertId, CancellationToken cancellationToken)
    => await _appDbContext.Suggestions
        .Where(s => s.ExpertId == expertId)
        .ToListAsync(cancellationToken);


    public async Task<Suggestion?> GetSuggestionByIdAsync(int suggestionId, CancellationToken cancellationToken)
     => await _appDbContext
     .Suggestions
     .FirstOrDefaultAsync(s => s.Id == suggestionId, cancellationToken);

    public async Task<Suggestion?> GetUserSuggestionByIdAsync(int expertId, int suggestionId, CancellationToken cancellationToken)
    => await _appDbContext.Suggestions
        .FirstOrDefaultAsync(s => s.ExpertId == expertId && s.Id == suggestionId, cancellationToken);


    public async Task<List<Suggestion>> GetSuggestionsWithDetailsAsync(CancellationToken cancellationToken)
          => await _appDbContext
           .Suggestions
           .Include(s => s.Request)
           .ThenInclude(s => s.HomeService)
           .Include(s => s.Expert)
           .ThenInclude(s => s.User)
           .ToListAsync(cancellationToken);

    public async Task<List<Suggestion>> GetUserSuggestionsWithDetailsAsync(int expertId, CancellationToken cancellationToken)
     => await _appDbContext
         .Suggestions
         .Where(s => s.ExpertId == expertId)

         .Include(s => s.Request)
         .ThenInclude(s => s.HomeService)
         .Include(s => s.Expert)
         .ThenInclude(s => s.User)
         .ToListAsync(cancellationToken);


    public async Task<Suggestion?> GeSuggestionByIdWithDetailsAsync(int id, CancellationToken cancellationToken)
        => await _appDbContext
        .Suggestions
         .Include(s => s.Request)
         .ThenInclude(s => s.HomeService)
         .Include(s => s.Expert)
         .ThenInclude(s => s.User)
        .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task<Suggestion?> GetUserSuggestionByIdWithDetailsAsync(int expertId, int suggestionId, CancellationToken cancellationToken)
    => await _appDbContext
        .Suggestions
         .Include(s => s.Request)
         .ThenInclude(s => s.HomeService)
         .Include(s => s.Expert)
         .ThenInclude(s => s.User)
        .FirstOrDefaultAsync(s => s.ExpertId == expertId && s.Id == suggestionId, cancellationToken);

    public async Task<bool> CreateAsync(Suggestion suggestion, CancellationToken cancellationToken)
    {
        try
        {
            await _appDbContext.Suggestions.AddAsync(suggestion, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
       
    }

    public async Task<bool> DeleteAsync(Suggestion suggestion, CancellationToken cancellationToken)
    {
        try
        {
            _appDbContext.Suggestions.Remove(suggestion);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(Suggestion suggestion, CancellationToken cancellationToken)
    {
        try
        {

            var existSuggestion = await _appDbContext.Suggestions.FirstOrDefaultAsync(x => x.Id == suggestion.Id);
            if (existSuggestion == null)
                return false;
            
                existSuggestion.RegisterDate = suggestion.RegisterDate;
                existSuggestion.Description = suggestion.Description;
                existSuggestion.DeliveryDate = suggestion.DeliveryDate;
                existSuggestion.Price = suggestion.Price;
                existSuggestion.RequestId = suggestion.RequestId;

                await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
            
        }
        catch
        {
            throw new Exception("Logic Error");
        }
       
    }

}
