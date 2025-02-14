using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Repository;

public interface ISuggestionRepository
{
    Task<List<Suggestion>?> GetSuggestionesAsync(CancellationToken cancellationToken);
    Task<List<Suggestion>?> GetUserSuggestionsAsync(int expertId, CancellationToken cancellationToken);
    Task<Suggestion?> GetSuggestionByIdAsync(int suggestionId, CancellationToken cancellationToken);
    Task<Suggestion?> GetUserSuggestionByIdAsync(int expertId, int suggestionId, CancellationToken cancellationToken);
    Task<List<Suggestion>> GetSuggestionsWithDetailsAsync(CancellationToken cancellationToken);
    Task<List<Suggestion>> GetUserSuggestionsWithDetailsAsync(int expertId, CancellationToken cancellationToken);
    Task<Suggestion?> GeSuggestionByIdWithDetailsAsync(int id, CancellationToken cancellationToken);
    Task<Suggestion?> GetUserSuggestionByIdWithDetailsAsync(int expertId, int suggestionId, CancellationToken cancellationToken);
    Task<bool> CreateAsync(Suggestion suggestion, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Suggestion suggestion, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Suggestion suggestion, CancellationToken cancellationToken);
    Task<bool> IsDelete(int suggestionId, CancellationToken cancellationToken);
    Task<bool> ChangeStatus(StatusEnum status, int suggestionId, CancellationToken cancellationToken);
}
