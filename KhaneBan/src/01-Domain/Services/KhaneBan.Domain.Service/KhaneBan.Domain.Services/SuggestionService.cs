using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Services;

public class SuggestionService : ISuggestionService
{
    private readonly ISuggestionRepository _suggestionRepository;

    public SuggestionService(ISuggestionRepository suggestionRepository)
    {
        _suggestionRepository = suggestionRepository;
    }

    public async Task<Suggestion?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await _suggestionRepository.GetByIdAsync(id, cancellationToken);

    public async Task<List<Suggestion>?> GetRequestSuggestions(int requestId, CancellationToken cancellationToken)
        => await _suggestionRepository.GetRequestSuggestions(requestId, cancellationToken);
}
