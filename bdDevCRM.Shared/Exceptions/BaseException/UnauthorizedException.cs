using System.Net;

namespace bdDevCRM.Shared.Exceptions.BaseException;

public class UnauthorizedException(string message) : Exception(message)
{
  public int StatusCode { get; } = (int)HttpStatusCode.Unauthorized; // 401
}
