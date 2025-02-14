using KhaneBan.Domain.Core.Entites.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Repository;

public interface ICityRepository
{
    Task<List<City>> GetCitiesAsync(CancellationToken cancellationToken);
    Task<City?> GetCityByIdAsync(int id, CancellationToken cancellationToken);
    Task<City?> GetCityByName(string title, CancellationToken cancellationToken);
    Task<bool> CreateAsync(City city, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(City city, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(City city, CancellationToken cancellationToken);
}
