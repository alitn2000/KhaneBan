using KhaneBan.Domain.Core.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Repository;

public interface IAdminRepository
{
    Task<List<Admin>> GetAdminsAsync(CancellationToken cancellationToken);
    Task<Admin?> GetAdminByIdAsync(int userId, CancellationToken cancellationToken);
    Task<List<Admin>> GetAdminsWithDetailsAsync(CancellationToken cancellationToken);
    Task<bool> AddBalanceAsync(Admin admin, double addBalance, CancellationToken cancellationToken);
    Task<bool> CreateAsync(Admin admin, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Admin admin, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Admin admin, CancellationToken cancellationToken);
    Task<bool> PlusMoney(string userId, double amount, CancellationToken cancellationToken);
}
