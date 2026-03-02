using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace bdDevCRM.Presentation.ActionFIlters;

public class ValidateMediaTypeAttribute : IActionFilter
{
  public void OnActionExecuting(ActionExecutingContext context)
  {
    // ✅ IgnoreMediaTypeValidation attribute check
    var hasIgnoreAttribute = context.ActionDescriptor.EndpointMetadata
        .Any(meta => meta is IgnoreMediaTypeValidationAttribute);

    if (hasIgnoreAttribute) return;

    var acceptHeader = context.HttpContext.Request.Headers.Accept.FirstOrDefault();

    if (string.IsNullOrWhiteSpace(acceptHeader))
    {
      context.Result = new BadRequestObjectResult(new
      {
        Message = "Accept header is missing.",
        ErrorCode = "MISSING_ACCEPT_HEADER"
      });
      return;
    }

    if (!MediaTypeHeaderValue.TryParse(acceptHeader, out var outMediaType))
    {
      context.Result = new BadRequestObjectResult(new
      {
        Message = "Invalid media type in Accept header.",
        ErrorCode = "INVALID_MEDIA_TYPE"
      });
      return;
    }

    context.HttpContext.Items["AcceptHeaderMediaType"] = outMediaType;
  }

  public void OnActionExecuted(ActionExecutedContext context) { }
}

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class IgnoreMediaTypeValidationAttribute : Attribute { }