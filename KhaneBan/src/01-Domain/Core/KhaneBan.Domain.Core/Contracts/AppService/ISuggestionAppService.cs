using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.Domain.Core.Enums;

namespace KhaneBan.Domain.Core.Contracts.AppService;

public interface ISuggestionAppService
{
    Task<Suggestion?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<Suggestion>?> GetRequestSuggestions(int requestId, CancellationToken cancellationToken);
    Task<Result> UpdateStatusAsync(int suggestionId, StatusEnum newStatus, CancellationToken cancellationToken);
    Task<bool> CreateAsync(Suggestion suggestion, CancellationToken cancellationToken);
}
