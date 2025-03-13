using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.AppService;

public interface ICategoryDapperAppService
{
    Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
    Task<List<Category>> GetAllWithDetailsAsync(CancellationToken cancellationToken);
}
