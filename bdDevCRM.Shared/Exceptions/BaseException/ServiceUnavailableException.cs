using System.Net;

namespace bdDevCRM.Shared.Exceptions.BaseException;

public abstract class ServiceUnavailableException(string message) : Exception(message)
{
   public int StatusCode { get; } = (int)HttpStatusCode.ServiceUnavailable; // 503
}
