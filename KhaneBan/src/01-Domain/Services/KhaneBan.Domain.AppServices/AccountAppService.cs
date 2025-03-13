using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.DTOs;
using KhaneBan.Domain.Core.Entites.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.AppServices;

public class AccountAppService : IAccountAppService
{
    private readonly IAccountService _accountService;

    public AccountAppService(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public Task<SignInResult> Login(UserLoginDTO dto)
        => _accountService.Login(dto);

    public Task Logout()
        => _accountService.Logout();

    public async Task<IdentityResult> RegisterAsync(User user, string pass, string role, CancellationToken cancellationToken)
        => await _accountService.RegisterAsync(user, pass, role, cancellationToken);
}
