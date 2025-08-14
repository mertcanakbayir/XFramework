using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using XFM.BLL.Result;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ResultViewModelFilter : Attribute, IAsyncActionFilter
{
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
                if (!isSuccess)
                {
                    var statusCode = (int)resultType.GetProperty("StatusCode").GetValue(objectResult.Value);
                    executedContext.Result = new ObjectResult(objectResult.Value)
                    {
                        StatusCode = statusCode
                    };
                }
            }
        }
    }
}
