using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly UserManager<User> _userManager;

    public CustomerService(ICustomerRepository customerRepository, UserManager<User> userManager)
    {
        _customerRepository = customerRepository;
        _userManager = userManager;
    }

    public  Task<IdentityResult> RegisterAsync(User user, string pass)
    {
        return  _userManager.CreateAsync(user, pass);
    }

    public Task<IdentityResult> UpdateAsync(User user)
    {
        return _userManager.UpdateAsync(user);
    }

    public async Task<int> GetCountCustomerAsync(CancellationToken cancellationToken)
        => await _customerRepository.GetCountCustomerAsync(cancellationToken);

    public async Task<List<Customer>> GetCustomerInfoAsync()
        => await _customerRepository.GetCustomerInfoAsync();

    public async Task<Customer?> GetCustomerInfoByIdAsync(int id, CancellationToken cancellationToken)
       => await _customerRepository.GetCustomerInfoByIdAsync(id, cancellationToken);


    public async Task<bool> DeleteAsync(int userId, CancellationToken cancellationToken)
    {
        var user = await _customerRepository.GetCustomerByIdAsync(userId, cancellationToken);
        if (user != null)                                                                         //////////check kardan hazf shavad
                                                                                                  //////////
        {
            return await _customerRepository.DeleteAsync(user.UserId, cancellationToken);

        }

        return false;
    }

    public async Task<bool> CreateAsync(Customer customer, CancellationToken cancellationToken)
    {
        var flag = await _customerRepository.GetCustomerByIdAsync(customer.UserId, cancellationToken);
        if (flag != null)
        {
            return false;
        }
        return await _customerRepository.CreateAsync(customer, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Customer customer, CancellationToken cancellationToken)
        => await _customerRepository.UpdateAsync(customer, cancellationToken);

    public async Task<bool> ActiveCustomer(int userId, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(userId, cancellationToken);
        if (customer != null)
        {
            await _customerRepository.ActiveCustomer(userId, cancellationToken);
            return true;

        }                                                                        

        return false;
    }

}
