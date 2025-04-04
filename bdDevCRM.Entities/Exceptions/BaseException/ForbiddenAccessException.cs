using System.Net;

namespace bdDevCRM.Entities.Exceptions.BaseException;

public abstract class ForbiddenAccessException : Exception
{
   public int StatusCode { get; } = (int)HttpStatusCode.Forbidden; // 403

   protected ForbiddenAccessException(string message) : base(message)
   {
   }
}
