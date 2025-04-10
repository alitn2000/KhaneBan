using Dapper;
using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.InfraStructure.Dapper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace KhaneBan.InfraStructure.Dapper.DapperRepositories;

public class CategoryDapperRepository : ICategoryDapperRepository
{ 
    private readonly DapperAppDbContext _context;
    public CategoryDapperRepository(DapperAppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = "SELECT * FROM Categories";
        var connection = _context.CreateConnection();
        using (connection)
        {
            var categories = await connection.QueryAsync<Category>(query, cancellationToken);
            return categories.ToList();
        }
    }

    //public async Task<List<Category>> GetAllWithDetailsAsync(CancellationToken cancellationToken)
    //{
    //    var query = "SELECT \r\n    c.Id, c.Title, c.IsDeleted, \r\n    sc.Id, sc.Title, sc.IsDeleted \r\nFROM Categories c  \r\nJOIN SubCategories sc ON c.Id = sc.CategoryId  \r\nWHERE c.IsDeleted = 0 AND sc.IsDeleted = 0;";
    //    var connection = _context.CreateConnection();
    //    using (connection)
    //    {
    //        var categoryDictionary = new Dictionary<int, Category>();

    //        var categoriesWithSubCategories = connection.Query<Category, SubCategory, Category>(
    //            query,
    //            (category, subCategory) =>
    //            {
    //                if (!categoryDictionary.TryGetValue(category.Id, out var currentCategory))
    //                {
    //                    currentCategory = category;
    //                    currentCategory.SubCategories = new List<SubCategory>();
    //                    categoryDictionary.Add(currentCategory.Id, currentCategory);
    //                }

    //                if (subCategory != null)
    //                {
    //                    currentCategory.SubCategories.Add(subCategory);
    //                }

    //                return currentCategory;
    //            },
    //            splitOn: "Id"
    //        ).Distinct().ToList();

    //        // OUTPUT
    //        return categoriesWithSubCategories;
    //    }

    //}
}
