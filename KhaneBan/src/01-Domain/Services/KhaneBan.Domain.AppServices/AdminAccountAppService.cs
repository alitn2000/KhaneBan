using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.AppServices;


public class AdminAccountAppService : IAdminAccountAppService
{
    private readonly IAdminAccountService _adminAccountService;

    public AdminAccountAppService(IAdminAccountService adminAccountService)
    {
        _adminAccountService = adminAccountService;
    }

    public Task<SignInResult> LoginAsync(string userName, string pass)
    {
        return _adminAccountService.LoginAsync(userName,pass);
    }

    public Task<IdentityResult> RegisterAsync(User user, string pass, CancellationToken cancel)
    {
        return _adminAccountService.RegisterAsync(user, pass, cancel);
    }
}
