﻿using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.Domain.Core.Enums;
using KhaneBan.InfraStructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.InfraStructure.EfCore.Repositories;

public class SuggestionRepository : ISuggestionRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<SuggestionRepository> _logger;

    public SuggestionRepository(AppDbContext appDbContext, ILogger<SuggestionRepository> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }

    public async Task<List<Suggestion>?> GetSuggestionesAsync(CancellationToken cancellationToken)
    => await _appDbContext.Suggestions
    .ToListAsync(cancellationToken);

    public async Task<List<Suggestion>?> GetUserSuggestionsAsync(int expertId, CancellationToken cancellationToken)
    => await _appDbContext.Suggestions
        .Where(s => s.ExpertId == expertId)
        .ToListAsync(cancellationToken);

    public async Task<List<Suggestion>?> GetRequestSuggestions(int requestId, CancellationToken cancellationToken)
        => await _appDbContext.Suggestions
        .Where(r => r.Request.Id == requestId)
        .Include(c => c.Request)
        .Include(c => c.Expert)                                        
        .ThenInclude(c => c.User)

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

    public async Task<Suggestion?> GetByIdAsync(int id, CancellationToken cancellationToken)

           => await _appDbContext.Suggestions
                             .Include(x => x.Request)
                             .ThenInclude(x => x.HomeService)
                             .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    


    public async Task<bool> CreateAsync(Suggestion suggestion, CancellationToken cancellationToken)
    {
        try
        {
            await _appDbContext.Suggestions.AddAsync(suggestion, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(" create request Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in request repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }

    }

    public async Task<bool> DeleteAsync(Suggestion suggestion, CancellationToken cancellationToken)
    {
        try
        {
            _appDbContext.Suggestions.Remove(suggestion);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(" delete request Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in request repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }
    }

    public async Task<bool> IsDelete(int suggestionId, CancellationToken cancellationToken)
    {
        try
        {
            var existSuggestion = await _appDbContext.Suggestions.FirstOrDefaultAsync(r => r.Id == suggestionId, cancellationToken);
            if (existSuggestion == null)
                return false;
            existSuggestion.IsDeleted = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(" isdelete request Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in request repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }


    }
    public async Task<bool> UpdateAsync(Suggestion suggestion, CancellationToken cancellationToken)
    {
        try
        {

            var existSuggestion = await _appDbContext.Suggestions.FirstOrDefaultAsync(x => x.Id == suggestion.Id, cancellationToken);
            if (existSuggestion == null)
                return false;

            existSuggestion.RegisterDate = suggestion.RegisterDate;
            existSuggestion.Description = suggestion.Description;
            existSuggestion.StartDate = suggestion.StartDate;
            existSuggestion.Price = suggestion.Price;
            existSuggestion.RequestId = suggestion.RequestId;

            await _appDbContext.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation(" update request Succesfully");
            return true;

        }
        catch(Exception ex)
        {
            _logger.LogError("Error in request repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }

        

    }

    public async Task<bool>UpdateStatusAsync(Suggestion suggestion,StatusEnum newStatus, CancellationToken cancellationToken)
    {
        try
        {
            suggestion.SuggestionStatus = newStatus;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" updatestatus request Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in request repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }

    }

}
