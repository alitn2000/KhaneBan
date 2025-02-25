using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.AppServices;

public class CityAppService : ICityAppService
{
    private readonly ICityService _cityService;

    public CityAppService(ICityService cityService)
    {
        _cityService = cityService;
    }

    public Task<List<CityDTO>> GetCitiesAsync(CancellationToken cancellationToken)
        => _cityService.GetCitiesAsync(cancellationToken);
}
