﻿using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.AppService;

public interface IHomeServiceAppService
{
    //Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken);
    Task<HomeService> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> CreateAsync(HomeService homeService, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(HomeService homeService, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<bool> ActiveHomeServiceAsync(int homeServiceId, CancellationToken cancellationToken);
    Task<List<HomeService>> GetHomeServicesBySubCategoryId(int subCategoryId, CancellationToken cancellationToken);
    Task<double> GetBasePriceByRequestId(int requestId, CancellationToken cancellationToken);
}
