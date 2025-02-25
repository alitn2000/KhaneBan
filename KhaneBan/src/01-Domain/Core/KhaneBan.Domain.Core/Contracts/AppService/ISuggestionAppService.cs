using KhaneBan.Domain.Core.Entites.UserRequests;

namespace KhaneBan.Domain.Core.Contracts.AppService;

public interface ISuggestionAppService
{
    Task<List<Suggestion>?> GetRequestSuggestions(int requestId, CancellationToken cancellationToken);
}
