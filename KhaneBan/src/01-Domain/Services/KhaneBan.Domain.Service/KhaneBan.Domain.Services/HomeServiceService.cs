using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.InfraStructure.EfCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Services;

public class HomeServiceService : IHomeServiceService
{
    private readonly IHomeServiceRepository _homeServiceRepository;

    public HomeServiceService(IHomeServiceRepository homeServiceRepository)
    {
        _homeServiceRepository = homeServiceRepository;
    }
    public async Task<bool> CreateAsync(HomeService homeService, CancellationToken cancellationToken)

        => await _homeServiceRepository.CreateAsync(homeService, cancellationToken);

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)

        => await _homeServiceRepository.DeleteAsync(id, cancellationToken);

    //public async Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken)

    //   => await _homeServiceRepository.GetAllAsync(cancellationToken);
        
    public async Task<HomeService> GetByIdAsync(int id, CancellationToken cancellationToken)

      => await _homeServiceRepository.GetByIdAsync(id, cancellationToken);

    public async Task<bool> UpdateAsync(HomeService homeService, CancellationToken cancellationToken)

     => await _homeServiceRepository.UpdateAsync(homeService, cancellationToken);

    public async Task<bool> ActiveHomeServiceAsync(int homeServiceId, CancellationToken cancellationToken)
    {
        var homeService = await _homeServiceRepository.GetByIdAsync(homeServiceId, cancellationToken);
        if (homeService != null)
        {
            await _homeServiceRepository.ActiveHomeServiceAsync(homeServiceId, cancellationToken);
            return true;

        }

        return false;
    }

    //public async Task<List<HomeService>> GetHomeServicesBySubCategoryId(int subCategoryId, CancellationToken cancellationToken)
    //    => await _homeServiceRepository.GetHomeServicesBySubCategoryId(subCategoryId, cancellationToken);
}

