using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Contracts.AppService;

public interface IPictureAppService
{
    Task<string> UploadImage(IFormFile FormFile, string folderName, CancellationToken cancellation);
}
