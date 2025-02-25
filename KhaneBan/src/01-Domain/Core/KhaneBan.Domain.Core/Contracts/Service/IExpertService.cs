using KhaneBan.Domain.Core.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Service
{
    public interface IExpertService
    {
        Task<int> GetCountExpertAsync(CancellationToken cancellationToken);
        Task<List<Expert>> GetExpertInfoAsync(CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int userId, CancellationToken cancellationToken);
        Task<bool> CreateAsync(Expert expert, CancellationToken cancellationToken);
        Task<Expert?> GetExpertInfoByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> ActiveExpertAsync(int userId, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Expert expert, CancellationToken cancellationToken);
    }
}
