using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.AppServices
{
    public class AdminAppService : IAdminAppService
    {
        private readonly IAdminService _adminService;
        public AdminAppService(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<bool> PlusMoney(string userId, double amount, CancellationToken cancellationToken)
            => await _adminService.PlusMoney(userId, amount, cancellationToken);
    }
}
