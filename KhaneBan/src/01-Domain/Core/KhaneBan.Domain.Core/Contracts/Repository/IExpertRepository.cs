using KhaneBan.Domain.Core.Entites.DTOs;
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
        Task<ExpertProfileDTO?> GetExpertProfileByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<Expert>> GetExpertInfoAsync(CancellationToken cancellationToken);
        Task<bool> CreateAsync(Expert expert, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int userId, CancellationToken cancellationToken);
        Task ActiveExpert(int userId, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Expert expert, CancellationToken cancellationToken);
        Task<int> GetCountExpertAsync(CancellationToken cancellationToken);
        Task<Expert?> GetExpertInfoByIdAsync(int id, CancellationToken cancellationToken);
    }
}
