using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Services;
 
public class AdminAccountService : IAdminAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;


    public AdminAccountService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task<IdentityResult> RegisterAsync(User user, string pass, CancellationToken cancel)
    {
        return await Task.Run(() => _userManager.CreateAsync(user, pass), cancel);


    }


    public async Task<SignInResult> LoginAsync(string userName, string pass)
    {

        var user = await _userManager.FindByNameAsync(userName);
        if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
        {
            return await _signInManager.PasswordSignInAsync(userName, pass, false, false);
        }

        return SignInResult.Failed;

    }
}
