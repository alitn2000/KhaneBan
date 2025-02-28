using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.InfraStructure.EfCore.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Services
{
    public class ExpertService : IExpertService
    {
        private readonly IExpertRepository _expertRepository;
        private readonly UserManager<User> _userManager;


        public ExpertService(IExpertRepository expertRepository, UserManager<User> userManager)
        {
            _expertRepository = expertRepository;
            _userManager = userManager;
        }


        public Task<IdentityResult> RegisterAsync(User user, string pass)
        {
            return _userManager.CreateAsync(user, pass);
        }
        public Task<IdentityResult> UpdateAsync(User user)
        {
            return _userManager.UpdateAsync(user);
        }

        public Task<int> GetCountExpertAsync(CancellationToken cancellationToken)
            => _expertRepository.GetCountExpertAsync(cancellationToken);

        public async Task<List<Expert>> GetExpertInfoAsync(CancellationToken cancellationToken)
            => await _expertRepository.GetExpertInfoAsync(cancellationToken);

        public async Task<bool> ActiveExpertAsync(int userId, CancellationToken cancellationToken)
        {
            var customer = await _expertRepository.GetExpertByIdAsync(userId, cancellationToken);
            if (customer != null)
            {
                await _expertRepository.ActiveExpert(userId, cancellationToken);
                return true;

            }

            return false;
        }


        public async Task<bool> DeleteAsync(int userId, CancellationToken cancellationToken)
        {
            var expert = await _expertRepository.GetExpertByIdAsync(userId, cancellationToken);
            if (expert != null)
            {
                var Flag = await _expertRepository.DeleteAsync(expert.UserId, cancellationToken);

                if (Flag)
                    return true;

                return false;
            }

            return false;
        }

        public async Task<bool> CreateAsync(Expert expert, CancellationToken cancellationToken)
        {
            var flag = await _expertRepository.GetExpertByIdAsync(expert.UserId, cancellationToken);
            if (flag != null)
            {
                return false;
            }
            return await _expertRepository.CreateAsync(expert, cancellationToken);
        }

        public async Task<Expert?> GetExpertInfoByIdAsync(int id, CancellationToken cancellationToken)

          => await _expertRepository.GetExpertInfoByIdAsync(id, cancellationToken);

        public async Task<bool> UpdateAsync(Expert expert, CancellationToken cancellationToken)

          => await _expertRepository.UpdateAsync(expert, cancellationToken);
    }
}
