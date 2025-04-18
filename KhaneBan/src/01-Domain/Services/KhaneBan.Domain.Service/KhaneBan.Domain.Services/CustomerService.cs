﻿using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.BaseEntities;
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

    public async Task<Customer?> GetCustomerByIdWithDetailsAsync(int userId, CancellationToken cancellationToken)
        => await _customerRepository.GetCustomerByIdWithDetailsAsync(userId, cancellationToken);

    public async Task<Result> MinusBalanceAsync(int userId, double minusBalance, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerInfoByUserIdAsync(userId, cancellationToken);
        if (customer == null)
            return new Result("مشکلی رخ داده است لطفا مجددا تلاش کنید", false);

        if(customer.User.Balance - minusBalance <0 || customer.User.Balance < minusBalance)
            return new Result("موجودی ناکافی است ", false);

        if (await _customerRepository.MinusBalanceAsync(customer, minusBalance, cancellationToken))
        {
            return new Result("پرداخت با موفقیت انجام شد", true);
        }

        return new Result("مشکلی رخ داده است لطفا مجددا تلاش کنید", false);
    }
}
