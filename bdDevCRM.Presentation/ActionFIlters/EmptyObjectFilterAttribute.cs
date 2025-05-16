using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace bdDevCRM.Presentation.ActionFIlters;

//public class EmptyObjectFilterAttribute : IActionFilter
//{
//  public EmptyObjectFilterAttribute()
//  {
//  }

//  public void OnActionExecuting(ActionExecutingContext context)
//  {
//    var action = context.RouteData.Values["action"];
//    var controller = context.RouteData.Values["controller"];
//    var param = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;
//    if (param is null)
//    {
//      context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller}, action: {action}");
//      return;
//    }
//    if (!context.ModelState.IsValid) context.Result = new UnprocessableEntityObjectResult(context.ModelState);
//  }

//  public void OnActionExecuted(ActionExecutedContext context) { }
//}

public class EmptyObjectFilterAttribute : IActionFilter
{
  public void OnActionExecuting(ActionExecutingContext context)
  {
    var action = context.RouteData.Values["action"]?.ToString();
    var controller = context.RouteData.Values["controller"]?.ToString();

    // Check if any argument contains "Dto" in its type name
    var dtoArgument = context.ActionArguments
        .FirstOrDefault(arg => arg.Value != null && arg.Value.GetType().Name.Contains("Dto"));

    if (dtoArgument.Value == null)
    {
      context.Result = new BadRequestObjectResult(
          $"Object is null. Controller: {controller}, Action: {action}"
      );
      return;
    }

    // Get the DTO object
    var model = dtoArgument.Value;

    // If ModelState is invalid, return detailed errors
    if (!context.ModelState.IsValid)
    {
      var errors = context.ModelState
          .Where(e => e.Value.Errors.Count > 0)
          .ToDictionary(
              kvp => kvp.Key,
              kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
          );

      context.Result = new BadRequestObjectResult(new
      {
        Message = "Validation failed",
        Errors = errors
      });
    }
  }

  public void OnActionExecuted(ActionExecutedContext context)
  {
    // You can log or modify result here if needed
  }
}
