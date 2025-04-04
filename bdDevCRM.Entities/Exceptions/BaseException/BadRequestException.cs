using System.Net;

namespace bdDevCRM.Entities.Exceptions.BaseException;

public abstract class BadRequestException : Exception
{
   public int StatusCode { get; } = (int)HttpStatusCode.BadRequest; // 400

   protected BadRequestException(string message) : base(message)
   {
   }
}