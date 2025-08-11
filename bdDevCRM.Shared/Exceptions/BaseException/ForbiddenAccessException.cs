using System.Net;

namespace bdDevCRM.Utilities.Exceptions.BaseException;

public abstract class ForbiddenAccessException(string message) : Exception(message)
{
   public int StatusCode { get; } = (int)HttpStatusCode.Forbidden; // 403
}
