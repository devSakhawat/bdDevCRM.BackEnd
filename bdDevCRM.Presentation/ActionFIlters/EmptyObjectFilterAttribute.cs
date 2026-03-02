using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace bdDevCRM.Presentation.ActionFIlters;

/// <summary>
/// Request body DTO null check ও ModelState validation।
/// 
/// </summary>
public class EmptyObjectFilterAttribute : IActionFilter
{
  public void OnActionExecuting(ActionExecutingContext context)
  {
    var action = context.RouteData.Values["action"]?.ToString();
    var controller = context.RouteData.Values["controller"]?.ToString();
    var method = context.HttpContext.Request.Method;

    if (method is "GET" or "DELETE" or "HEAD" or "OPTIONS")
    {
      if (!context.ModelState.IsValid)
      {
        context.Result = new UnprocessableEntityObjectResult(
            CreateValidationError(context));
      }
      return;
    }

    // POST/PUT/PATCH — body DTO check করো
    var bodyParam = context.ActionArguments
        .FirstOrDefault(arg =>
            arg.Value == null ||                                        // null value
            IsComplexType(arg.Value?.GetType()));                       // complex type (DTO/Model)

    if (bodyParam.Key != null && bodyParam.Value == null)
    {
      context.Result = new BadRequestObjectResult(new
      {
        Message = $"Request body is null. Controller: {controller}, Action: {action}",
        ErrorCode = "NULL_REQUEST_BODY"
      });
      return;
    }

    // ModelState validation
    if (!context.ModelState.IsValid)
    {
      context.Result = new UnprocessableEntityObjectResult(
          CreateValidationError(context));
    }
  }

  public void OnActionExecuted(ActionExecutedContext context) { }

  private static bool IsComplexType(Type? type)
  {
    if (type == null) return false;

    // Primitive types, strings, value types → no complex type 
    return !type.IsPrimitive
           && type != typeof(string)
           && type != typeof(decimal)
           && type != typeof(DateTime)
           && type != typeof(DateTimeOffset)
           && type != typeof(Guid)
           && !type.IsEnum;
  }

  private static object CreateValidationError(ActionExecutingContext context)
  {
    var errors = context.ModelState
        .Where(e => e.Value?.Errors.Count > 0)
        .ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray());

    return new
    {
      Message = "Validation failed",
      ErrorCode = "VALIDATION_FAILED",
      Errors = errors
    };
  }
}