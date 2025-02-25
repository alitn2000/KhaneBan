using KhaneBan.Domain.Core.Entites.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Service;

public interface IAdminAccountService
{
    Task<IdentityResult> RegisterAsync(User user, string pass, CancellationToken cancel);
    Task<SignInResult> LoginAsync(string userName, string pass);
}
