//using bdDevCRM.Presentation.Extensions;
//using bdDevCRM.Shared.ApiResponse;
//using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.Extensions.DependencyInjection;

//namespace bdDevCRM.Presentation.ActionFIlters
//{
//    public class AuthenticatedUserAttribute : ActionFilterAttribute
//    {
//        public override void OnActionExecuting(ActionExecutingContext context)
//        {
//            var controller = context.Controller as BaseApiController;
//            if (controller == null)
//            {
//                context.Result = new UnauthorizedResult();
//                return;
//            }

//            var userIdClaim = context.HttpContext.User.FindFirst("UserId")?.Value;

//            if (string.IsNullOrEmpty(userIdClaim))
//            {
//                context.Result = new UnauthorizedObjectResult(
//                    ResponseHelper.Unauthorized("User authentication required"));
//                return;
//            }

//            if (!int.TryParse(userIdClaim, out int userId))
//            {
//                context.Result = new BadRequestObjectResult(
//                    ResponseHelper.BadRequest("Invalid user ID format"));
//                return;
//            }

//            var serviceManager = context.HttpContext.RequestServices.GetService<bdDevCRM.ServicesContract.IServiceManager>();

//            var currentUser = serviceManager?.GetCache<UsersDto>(userId);

//            if (currentUser == null)
//            {
//                context.Result = new UnauthorizedObjectResult(
//                    ResponseHelper.Unauthorized("User session expired"));
//                return;
//            }

//            // Use extension methods (cleaner!)
//            context.HttpContext.SetCurrentUser(currentUser);
//            context.HttpContext.SetUserId(userId);
//        }
//    }
//}