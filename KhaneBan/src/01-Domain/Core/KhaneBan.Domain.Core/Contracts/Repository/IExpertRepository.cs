using KhaneBan.Domain.Core.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Repository
{
    public interface IExpertRepository
    {
        Task<List<Expert>> GetExpertsAsync(CancellationToken cancellationToken);
        Task<Expert?> GetExpertByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<Expert>> GetExpertsWithDetailsAsync(CancellationToken cancellationToken);
        Task<Expert?> GetExpertByIdWithDetailsAsync(int id, CancellationToken cancellationToken);
        Task<bool> CreateAsync(Expert expert, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Expert expert, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Expert expert, CancellationToken cancellationToken);
    }
}
