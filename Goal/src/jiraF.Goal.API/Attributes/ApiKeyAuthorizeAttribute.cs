using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace jiraF.Goal.API.Attributes;

[AttributeUsage(validOn: AttributeTargets.Method | AttributeTargets.Class)]
public class ApiKeyAuthorizeAttribute : Attribute, IAsyncActionFilter
{
    public bool Disabled { get; set; } = false;

    private const string APIKEYNAME = "GoalApiKey";
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (Disabled) await next();
        if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out StringValues extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "Api Key was not provided."
            };
            return;
        }
        IConfiguration appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        string apiKey = appSettings.GetValue<string>(APIKEYNAME);
        if (!apiKey.Equals(extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "Api Key is not valid."
            };
            return;
        }
        await next();
    }
}
