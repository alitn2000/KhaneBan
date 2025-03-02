using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.DTOs;
using KhaneBan.Domain.Core.Entites.User;
using Microsoft.AspNetCore.Identity;

namespace KhaneBan.Domain.Services;

public class AccountService : IAccountService
{

    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }


    public async Task<SignInResult> Login(UserLoginDTO dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user != null && await _userManager.IsInRoleAsync(user, dto.Role))
        {
            return await _signInManager.PasswordSignInAsync(user,dto.Password,dto.IsPresistent, false);
        }

        return SignInResult.Failed;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
       
    }

}
