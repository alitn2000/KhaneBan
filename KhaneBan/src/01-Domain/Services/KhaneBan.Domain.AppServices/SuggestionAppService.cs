using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.AppServices;

public class SuggestionAppService : ISuggestionAppService
{
    private readonly ISuggestionService _suggestionService;

    public SuggestionAppService(ISuggestionService suggestionService)
    {
        _suggestionService = suggestionService;
    }

    public Task<Suggestion?> GetByIdAsync(int suggestionId, CancellationToken cancellationToken)
        => _suggestionService.GetByIdAsync(suggestionId, cancellationToken);

    public async Task<List<Suggestion>?> GetRequestSuggestions(int requestId, CancellationToken cancellationToken)
        => await _suggestionService.GetRequestSuggestions(requestId, cancellationToken);
}
