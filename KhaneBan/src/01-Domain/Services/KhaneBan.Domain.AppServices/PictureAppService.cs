using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Service;
using Microsoft.AspNetCore.Http;

namespace KhaneBan.Domain.AppServices
{
    public class PictureAppService : IPictureAppService
    {
        private readonly IPictureService _pictureService;

        public PictureAppService(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        public Task<string> UploadImage(IFormFile FormFile, string folderName, CancellationToken cancellation)
            => _pictureService.UploadImage(FormFile, folderName, cancellation);
    }
}
