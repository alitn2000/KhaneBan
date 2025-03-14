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

public class HomeServiceDapperRepository : IHomeServiceDapperRepository
{
    private readonly DapperAppDbContext _context;
    public HomeServiceDapperRepository(DapperAppDbContext context)
    {
        _context = context;
    }
    public async Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = "SELECT * FROM HomeServices";
        var connection = _context.CreateConnection();
        using (connection)
        {
            var homeServices = await connection.QueryAsync<HomeService>(query, cancellationToken);
            return homeServices.ToList();
        }
    }

    //public async Task<List<HomeService>> GetHomeServicesBySubCategoryId(int subCategoryId, CancellationToken cancellationToken)
    //{
    //    var query = "SELECT hs.* FROM HomeServices hs JOIN SubCategories sc ON hs.SubCategoryId = sc.Id JOIN Categories c ON sc.CategoryId = c.Id WHERE hs.SubCategoryId = @subCategoryId;";
    //    var connection = _context.CreateConnection();
    //    using (connection)
    //    {
    //        var homeServices = await connection.QueryAsync<HomeService>(query, new { subCategoryId });
    //        return homeServices.ToList();
    //    }
    //}
}
