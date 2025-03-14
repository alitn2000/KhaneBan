using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Services;

public class CityDapperService : ICityDapperService
{
    private readonly ICityDapperRepository _cityDapperRepository;

    public CityDapperService(ICityDapperRepository cityDapperRepository)
    {
        _cityDapperRepository = cityDapperRepository;
    }

    public async Task<List<City>> GetAllAsync(CancellationToken cancellationToken)
        =>await  _cityDapperRepository.GetAllAsync(cancellationToken);
}
