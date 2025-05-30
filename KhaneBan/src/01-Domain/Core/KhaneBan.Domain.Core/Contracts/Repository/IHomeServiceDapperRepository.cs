﻿using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Repository;

public interface IHomeServiceDapperRepository
{
    Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken);
    //Task<List<HomeService>> GetHomeServicesBySubCategoryId(int subCategoryId, CancellationToken cancellationToken);
}
