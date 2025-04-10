using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.AppService;

public interface IAdminAppService
{
    Task<bool> PlusMoney(string userId, double amount, CancellationToken cancellationToken);
}
