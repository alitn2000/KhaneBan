using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Service;

public interface ICustomerService
{
    Task<int> GetCountCustomerAsync(CancellationToken cancellationToken);
    Task<List<Customer>> GetCustomerInfoAsync();
    Task<bool> DeleteAsync(int userId, CancellationToken cancellationToken);
    Task<bool> CreateAsync(Customer customer, CancellationToken cancellationToken);
    Task<Customer?> GetCustomerInfoByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Customer customer, CancellationToken cancellationToken);
    Task<bool> ActiveCustomer(int userId, CancellationToken cancellationToken);
    Task<IdentityResult> RegisterAsync(User user, string pass);
    Task<IdentityResult> UpdateAsync(User user);
    Task<Customer?> GetCustomerByIdWithDetailsAsync(int userId, CancellationToken cancellationToken);
    Task<Result> MinusBalanceAsync(int userId, double minusBalance, CancellationToken cancellationToken);

}