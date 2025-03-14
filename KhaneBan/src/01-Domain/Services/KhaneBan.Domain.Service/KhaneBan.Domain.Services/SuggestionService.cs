using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.Domain.Core.Enums;
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

    public Task<bool> CreateAsync(Suggestion suggestion, CancellationToken cancellationToken)
        => _suggestionRepository.CreateAsync(suggestion, cancellationToken);

    public async Task<Suggestion?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await _suggestionRepository.GetByIdAsync(id, cancellationToken);

    public async Task<List<Suggestion>?> GetRequestSuggestions(int requestId, CancellationToken cancellationToken)
        => await _suggestionRepository.GetRequestSuggestions(requestId, cancellationToken);



    public async Task<Result> UpdateStatusAsync(int suggestionId, StatusEnum newStatus, CancellationToken cancellationToken)
    {
        var suggestion = await _suggestionRepository.GetSuggestionByIdAsync(suggestionId, cancellationToken);
        if (suggestion == null)
            return new Result("پیشنهاد یافت نشد", false);

        if (await _suggestionRepository.UpdateStatusAsync(suggestion, newStatus, cancellationToken))
            return new Result("تغغیر وضعیت با موفقیت انجام شد", true);

        return new Result("تغغیر وضعیت انجام نشد", false);


    }

}
