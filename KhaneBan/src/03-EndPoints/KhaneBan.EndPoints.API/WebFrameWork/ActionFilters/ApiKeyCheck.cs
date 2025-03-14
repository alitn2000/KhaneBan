using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KhaneBan.EndPoints.API.WebFrameWork.ActionFilters;

public class ApiKeyCheck : IAuthorizationFilter
{
    private readonly string _apiKey;

    public ApiKeyCheck(IConfiguration configuration)
    {
        _apiKey = configuration["ApiSettings:ApiKey"];
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var request = context.HttpContext.Request;
        if (!request.Headers.TryGetValue("ApiKey", out var receivedApiKey))
        {
            context.Result = new UnauthorizedObjectResult("API Key is missing.");
            return;
        }
 
        if (!string.Equals(receivedApiKey, _apiKey, StringComparison.OrdinalIgnoreCase))
        {
            context.Result = new UnauthorizedObjectResult("Invalid API Key.");
        }
    }
}
