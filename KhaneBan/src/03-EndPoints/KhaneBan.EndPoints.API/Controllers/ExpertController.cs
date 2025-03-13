using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.EndPoints.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KhaneBan.EndPoints.API.Controllers;

[Route("[controller]")]
[ApiController]
public class ExpertController : ControllerBase
{
    private readonly IAccountAppService _accountAppService;

    public ExpertController(IAccountAppService accountAppService)
    {
        _accountAppService = accountAppService;
    }

    [HttpPost("Add")]
    public async Task<IActionResult> AddExpert(AddCustomerViewModel model,CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("خطا در مقدار دهی");
        }
        var user = new User()
        {
            Email = model.Email,
            UserName = model.UserName,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Address = model.Address,
            Balance = model.Balance,
            CityId = model.CityId
        };

        var result = await _accountAppService.RegisterAsync(user, model.Password, "Customer", cancellationToken);
        if (result.Succeeded)
        {
            return Ok("مشتری با موفقیت اضافه شد");
        }
        return BadRequest("خطا در اضافه کردن کاربر");
    }

}
