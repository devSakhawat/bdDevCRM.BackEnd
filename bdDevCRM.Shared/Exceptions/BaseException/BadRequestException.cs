using System.Net;

namespace bdDevCRM.Utilities.Exceptions.BaseException;

public abstract class BadRequestException(string message) : Exception(message)
{
   public int StatusCode { get; } = (int)HttpStatusCode.BadRequest; // 400
}