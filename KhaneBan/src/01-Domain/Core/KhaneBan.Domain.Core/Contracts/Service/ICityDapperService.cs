using KhaneBan.Domain.Core.Entites.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Service;

public interface ICityDapperService
{
    Task<List<City>> GetAllAsync(CancellationToken cancellationToken);
}
