using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.AppServices;

public class ExpertAppService : IExpertAppService
{
    private readonly IExpertService _expertService;

    public ExpertAppService(IExpertService expertService)
    {
        _expertService = expertService;
    }

    public async Task<bool> CreateAsync(Expert expert, CancellationToken cancellationToken)
    => await _expertService.CreateAsync(expert, cancellationToken);

    public Task<bool> DeleteAsync(int userId, CancellationToken cancellationToken)
        => _expertService.DeleteAsync(userId, cancellationToken);

    public async Task<int> GetCountExpertAsync(CancellationToken cancellationToken)
        => await _expertService.GetCountExpertAsync(cancellationToken);

    public async Task<List<Expert>> GetExpertInfoAsync(CancellationToken cancellationToken)
        => await _expertService.GetExpertInfoAsync(cancellationToken);

    public async Task<Expert?> GetExpertInfoByIdAsync(int id, CancellationToken cancellationToken)
        => await _expertService.GetExpertInfoByIdAsync(id, cancellationToken);

    public async Task<bool> UpdateAsync(Expert expert, CancellationToken cancellationToken)
       => await _expertService.UpdateAsync(expert, cancellationToken);

    public async Task<bool> ActiveExpertAsync(int userId, CancellationToken cancellationToken)
        => await _expertService.ActiveExpertAsync(userId, cancellationToken);

    public Task<IdentityResult> RegisterAsync(User user, string pass)
        => _expertService.RegisterAsync(user, pass);

    public Task<IdentityResult> UpdateAsync(User user)
        => _expertService.UpdateAsync(user);
}
