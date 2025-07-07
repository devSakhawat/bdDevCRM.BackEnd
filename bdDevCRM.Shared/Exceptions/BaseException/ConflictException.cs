using System.Net;

namespace bdDevCRM.Utilities.Exceptions.BaseException;

public abstract class ConflictException(string message) : Exception(message)
{
   public int StatusCode { get; } = (int)HttpStatusCode.Conflict; // 409
}