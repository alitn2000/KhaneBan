using KhaneBan.Domain.Core.Entites.DTOs;
using KhaneBan.Domain.Core.Entites.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Service;

public interface IAccountService
{
    Task<SignInResult> Login(UserLoginDTO dto);
    Task Logout();
    Task<IdentityResult> RegisterAsync(User user, string pass, string role,CancellationToken cancellationToken);
}
