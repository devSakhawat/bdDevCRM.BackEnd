using System.Net;

namespace bdDevCRM.Utilities.Exceptions.BaseException;

public abstract class UnauthorizedException(string message) : Exception(message)
{
  public int StatusCode { get; } = (int)HttpStatusCode.Unauthorized; // 401
}
