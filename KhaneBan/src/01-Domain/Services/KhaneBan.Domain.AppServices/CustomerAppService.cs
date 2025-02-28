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

public class CustomerAppService : ICustomerAppService
{
    private readonly ICustomerService _customerService;

    public CustomerAppService(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<int> GetCountCustomerAsync(CancellationToken cancellationToken)
        => await _customerService.GetCountCustomerAsync(cancellationToken);
    public async Task<List<Customer>> GetCustomerInfoAsync()
        => await _customerService.GetCustomerInfoAsync();

    public async Task<bool> DeleteAsync(int userId, CancellationToken cancellationToken)
        => await _customerService.DeleteAsync(userId, cancellationToken);

    public async Task<bool> CreateAsync(Customer customer, CancellationToken cancellationToken)
        => await _customerService.CreateAsync(customer, cancellationToken);

    public async Task<Customer?> GetCustomerInfoByIdAsync(int id, CancellationToken cancellationToken)
        => await _customerService.GetCustomerInfoByIdAsync(id, cancellationToken);

    public async Task<bool> UpdateAsync(Customer customer, CancellationToken cancellationToken)
        => await _customerService.UpdateAsync(customer,cancellationToken);

    public async Task<bool> ActiveCustomer(int userId, CancellationToken cancellationToken)
        => await _customerService.ActiveCustomer(userId, cancellationToken);

    public Task<IdentityResult> RegisterAsync(User user, string pass)
        => _customerService.RegisterAsync(user, pass);

    public Task<IdentityResult> UpdateAsync(User user)
        => _customerService.UpdateAsync(user);
}
