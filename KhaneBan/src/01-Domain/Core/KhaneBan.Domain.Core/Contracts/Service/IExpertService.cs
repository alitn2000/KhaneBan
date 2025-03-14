using KhaneBan.Domain.Core.Entites.DTOs;
using KhaneBan.Domain.Core.Entites.User;
using Microsoft.AspNetCore.Identity;
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
        Task<IdentityResult> RegisterAsync(User user, string pass);
        Task<bool> UpdateAsync(Expert expert, List<int> selectedHomeServiceIds, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateAsync(User user);
        Task<ExpertProfileDTO?> GetExpertProfileByIdAsync(int id, CancellationToken cancellationToken);
        Task<Expert?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Expert?> GetExpertByIdWithDetailsAsync(int id, CancellationToken cancellationToken);
    }
}
