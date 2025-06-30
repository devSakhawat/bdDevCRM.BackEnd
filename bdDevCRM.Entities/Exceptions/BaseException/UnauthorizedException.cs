using System.Net;

namespace bdDevCRM.Entities.Exceptions.BaseException;

public abstract class UnauthorizedException : Exception
{
  public int StatusCode { get; } = (int)HttpStatusCode.Unauthorized; // 401

  protected UnauthorizedException(string message) : base(message)
  {
  }
}
