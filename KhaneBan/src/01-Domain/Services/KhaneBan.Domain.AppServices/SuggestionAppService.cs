using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.Domain.Core.Enums;
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

    public async Task<Suggestion?> GetByIdAsync(int suggestionId, CancellationToken cancellationToken)
        => await _suggestionService.GetByIdAsync(suggestionId, cancellationToken);

    public async Task<List<Suggestion>?> GetRequestSuggestions(int requestId, CancellationToken cancellationToken)
        => await _suggestionService.GetRequestSuggestions(requestId, cancellationToken);

    public async Task<Result> UpdateStatusAsync(int suggestionId, StatusEnum newStatus, CancellationToken cancellationToken)
        => await _suggestionService.UpdateStatusAsync(suggestionId, newStatus, cancellationToken);
}
