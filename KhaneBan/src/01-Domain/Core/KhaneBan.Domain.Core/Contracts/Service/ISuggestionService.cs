using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Service;

public interface ISuggestionService
{
    Task<List<Suggestion>?> GetRequestSuggestions(int requestId, CancellationToken cancellationToken);
    Task<Suggestion?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Result> UpdateStatusAsync(int suggestionId, StatusEnum newStatus, CancellationToken cancellationToken);
    Task<bool> CreateAsync(Suggestion suggestion, CancellationToken cancellationToken);
}
