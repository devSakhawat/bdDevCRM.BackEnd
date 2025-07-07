using System.Net;

namespace bdDevCRM.Utilities.Exceptions.BaseException;

public abstract class NotFoundException(string message) : Exception(message)
{
   public int StatusCode { get; } = (int)HttpStatusCode.NotFound; // 404
}
