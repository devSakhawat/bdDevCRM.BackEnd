using System.Net;

namespace bdDevCRM.Entities.Exceptions.BaseException;

public abstract class ServiceUnavailableException : Exception
{
   public int StatusCode { get; } = (int)HttpStatusCode.ServiceUnavailable; // 503

   public ServiceUnavailableException(string message) : base(message)
   {
   }
}
