using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.AppServices
{
    public class HomeServiceAppService : IHomeServiceAppService
    {
        private readonly IHomeServiceService _homeServiceService;

        public HomeServiceAppService(IHomeServiceService homeServiceService)
        {
            _homeServiceService = homeServiceService;
        }

        public Task<bool> ActiveHomeServiceAsync(int homeServiceId, CancellationToken cancellationToken)
            => _homeServiceService.ActiveHomeServiceAsync(homeServiceId, cancellationToken);

        public async Task<bool> CreateAsync(HomeService homeService, CancellationToken cancellationToken)

          => await _homeServiceService.CreateAsync(homeService, cancellationToken);

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

          => await _homeServiceService.DeleteAsync(id, cancellationToken);

        //public async Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken)

        //  => await _homeServiceService.GetAllAsync(cancellationToken);

        public async Task<HomeService> GetByIdAsync(int id, CancellationToken cancellationToken)

         => await _homeServiceService.GetByIdAsync(id, cancellationToken);

        //public async Task<List<HomeService>> GetHomeServicesBySubCategoryId(int subCategoryId, CancellationToken cancellationToken)
        //    => await _homeServiceService.GetHomeServicesBySubCategoryId(subCategoryId, cancellationToken);

        public async Task<bool> UpdateAsync(HomeService homeService, CancellationToken cancellationToken)

        => await _homeServiceService.UpdateAsync(homeService, cancellationToken);
    }
}
