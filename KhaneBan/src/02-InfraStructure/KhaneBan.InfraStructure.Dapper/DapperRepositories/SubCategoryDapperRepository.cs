using Dapper;
using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.InfraStructure.Dapper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.InfraStructure.Dapper.DapperRepositories;

public class SubCategoryDapperRepository : ISubCategoryDapperRepository
{
    private readonly DapperAppDbContext _context;
    public SubCategoryDapperRepository(DapperAppDbContext context)
    {
        _context = context;
    }
    public async Task<List<SubCategory>> GetAllAsync(CancellationToken cancellationToken)
    {

        var query = "SELECT * FROM SubCategories";
        var connection = _context.CreateConnection();
        using (connection)
        {
            var subCategories = await connection.QueryAsync<SubCategory>(query, cancellationToken);
            return subCategories.ToList();
        }

    }
}
