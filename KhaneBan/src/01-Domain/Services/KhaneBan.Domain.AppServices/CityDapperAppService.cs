using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.AppServices;

public class CityDapperAppService : ICityDapperAppService
{
    private readonly ICityDapperService _cityDapperService;

    public CityDapperAppService(ICityDapperService cityDapperService)
    {
        _cityDapperService = cityDapperService;
    }

    public async Task<List<City>> GetAllAsync(CancellationToken cancellationToken)
        => await _cityDapperService.GetAllAsync(cancellationToken);
}
