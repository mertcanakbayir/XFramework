using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using XFramework.Helper.ViewModels;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ValidateFilter : Attribute, IAsyncActionFilter

{
    private readonly ILogger<ValidateFilter> _logger;
    public ValidateFilter(ILogger<ValidateFilter> logger)
    {
        _logger = logger;
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var executedContext = await next();

        if (executedContext.Result is ObjectResult objectResult)
        {
            var resultType = objectResult.Value?.GetType();
            if (resultType != null &&
                resultType.IsGenericType &&
                resultType.GetGenericTypeDefinition() == typeof(ResultViewModel<>))
            {
                var isSuccess = (bool)resultType.GetProperty("IsSuccess").GetValue(objectResult.Value);
                var statusCode = (int?)resultType.GetProperty("StatusCode")?.GetValue(objectResult.Value) ?? 500;
                var message = resultType.GetProperty("Message")?.GetValue(objectResult.Value).ToString();
                if (!isSuccess)
                {
                    _logger.LogWarning(
                        "Action {ActionName} returned validation/business error. StatusCode {StatusCode}, Message:{Message}, Response: {@Response}",
                        context.ActionDescriptor.DisplayName,
                        statusCode,
                        message,
                        objectResult.Value
                        );

                    executedContext.Result = new ObjectResult(objectResult.Value)
                    {
                        StatusCode = statusCode
                    };
                }

            }
        }
    }
}
