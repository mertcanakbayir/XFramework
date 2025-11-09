using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyApp.Helper.ViewModels;

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

        if (executedContext.Result is ObjectResult { Value: ResultViewModel<object> result })
        {
            if (!result.IsSuccess)
            {
                var statusCode = result.StatusCode;
                var message = result.Message;

                _logger.LogWarning(
                         "Action {ActionName} returned validation/business error. StatusCode {StatusCode}, Message:{Message}, Response: {@Response}",
                    context.ActionDescriptor.DisplayName,
                    statusCode,
                    message,
                    result);

                executedContext.Result = new ObjectResult(result)
                {
                    StatusCode = statusCode
                };
            }
        }
    }
}
