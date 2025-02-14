using KhaneBan.Domain.Core.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Repository;

public interface ICustomerRepository
{
    Task<List<Customer>> GetCustomersAsync(CancellationToken cancellationToken);
    Task<Customer?> GetCustomerByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<Customer>> GetCustomersWithDetailsAsync(CancellationToken cancellationToken);
    Task<Customer?> GetCustomerByIdWithDetailsAsync(int userId, CancellationToken cancellationToken);
    Task<bool> CreateAsync(Customer customer, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Customer customer, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Customer customer, CancellationToken cancellationToken);
}
