using System.Net;

namespace bdDevCRM.Shared.Exceptions.BaseException;

public class BadRequestException(string message) : Exception(message)
{
   public int StatusCode { get; } = (int)HttpStatusCode.BadRequest; // 400
}