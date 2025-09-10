using System.Net;

namespace bdDevCRM.Shared.Exceptions.BaseException;

public abstract class ForbiddenAccessException(string message) : Exception(message)
{
   public int StatusCode { get; } = (int)HttpStatusCode.Forbidden; // 403
}
