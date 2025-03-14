using KhaneBan.Domain.Core.Entites.UserRequests;

namespace KhaneBan.Domain.Core.Contracts.AppService;

public interface IHomeServiceDapperAppService
{
    Task<List<HomeService>> GetAllAsync(CancellationToken cancellationToken);
    //Task<List<HomeService>> GetHomeServicesBySubCategoryId(int subCategoryId, CancellationToken cancellationToken);
}
