using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.DTOs;
using KhaneBan.Domain.Core.Entites.User;
using Microsoft.AspNetCore.Identity;

namespace KhaneBan.Domain.Services;

public class AccountService : IAccountService
{

    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ICustomerService _customerService;
    private readonly IExpertService _expertService;
    public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, ICustomerService customerService, IExpertService expertService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _customerService = customerService;
        _expertService = expertService;
    }


    public async Task<SignInResult> Login(UserLoginDTO dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user != null && await _userManager.IsInRoleAsync(user, dto.Role))
        {
            return await _signInManager.PasswordSignInAsync(user, dto.Password, dto.IsPresistent, false);
        }

        return SignInResult.Failed;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();

    }
    public async Task<IdentityResult> RegisterAsync(User user, string pass, string role, CancellationToken cancellationToken)
    {
        object registerUser;
        if (role != "Customer" && role != "Expert")
        {
            return IdentityResult.Failed();
        }
        var result = await _userManager.CreateAsync(user, pass);
        if (result.Succeeded)
        {
            if(role == "Customer")
            {
                 registerUser = new Customer
                {
                    UserId = user.Id,
                    User = user
                };
            }
            else
            {
                 registerUser = new Expert
                {
                    UserId = user.Id,
                    User = user
                };
            }

                var roleResult = await _userManager.AddToRoleAsync(user, role);

            if (roleResult.Succeeded)
            {
                if(registerUser.GetType() == typeof(Customer))
                {
                    if (await _customerService.CreateAsync((Customer)registerUser, cancellationToken))
                    {
                        return IdentityResult.Success;
                    }
                    return IdentityResult.Failed();
                }

                else if (registerUser.GetType() == typeof(Expert))
                {
                    if (await _expertService.CreateAsync((Expert)registerUser, cancellationToken))
                    {
                        return IdentityResult.Success;
                    }
                    return IdentityResult.Failed();
                }
                
                  
            }




            return IdentityResult.Failed();



        }

        return result;
    }

}
