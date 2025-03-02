using KhaneBan.Domain.Core.Entites.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.AppService;

public interface IAccountAppService
{
    Task<SignInResult> Login(UserLoginDTO dto);
    Task Logout();
}
