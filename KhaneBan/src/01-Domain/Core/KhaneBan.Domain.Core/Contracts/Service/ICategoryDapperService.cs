﻿using KhaneBan.Domain.Core.Entites.UserRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.Service;

public interface ICategoryDapperService
{
    Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
    //Task<List<Category>> GetAllWithDetailsAsync(CancellationToken cancellationToken);
}
