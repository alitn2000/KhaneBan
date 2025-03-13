using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using KhaneBan.InfraStructure.Dapper.Common;
using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.BaseEntities;
using Dapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace KhaneBan.InfraStructure.Dapper.DapperRepositories;

public class CityDapperRepository : ICityDapperRepository
{
    private readonly DapperAppDbContext _context;
    public CityDapperRepository(DapperAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<City>> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = "SELECT * FROM Cities";
        using (var connection = _context.CreateConnection())
        {
            var Cities =  await connection.QueryAsync<City>(query,cancellationToken);
            return Cities.ToList();
        }
    }

    public async Task<City?> GetByIdAsync(int cityId, CancellationToken cancellationToken)
    {
        var query = "SELECT * FROM Cities WHERE Id = @Id";
        var connection = _context.CreateConnection();
        using (connection)
        {
            return await connection.QueryFirstOrDefaultAsync<City>(query, new { Id = cityId });
        }
    }

}
