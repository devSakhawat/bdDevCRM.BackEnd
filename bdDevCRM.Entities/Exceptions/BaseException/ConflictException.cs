using System.Net;

namespace bdDevCRM.Entities.Exceptions.BaseException;

public abstract class ConflictException : Exception
{
   public int StatusCode { get; } = (int)HttpStatusCode.Conflict; // 409

   public ConflictException(string message) : base(message)
   {
   }
}