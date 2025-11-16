using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using Microsoft.AspNetCore.Http;

namespace bdDevCRM.Presentation.Extensions
{
    public static class HttpContextExtensions
    {
        private const string CurrentUserKey = "CurrentUser";
        private const string UserIdKey = "UserId";

        public static UsersDto? GetCurrentUser(this HttpContext context)
        {
            return context.Items[CurrentUserKey] as UsersDto;
        }

        public static int GetUserId(this HttpContext context)
        {
            return context.Items[UserIdKey] is int userId ? userId : 0;
        }

        public static void SetCurrentUser(this HttpContext context, UsersDto user)
        {
            context.Items[CurrentUserKey] = user;
        }

        public static void SetUserId(this HttpContext context, int userId)
        {
            context.Items[UserIdKey] = userId;
        }
    }
}