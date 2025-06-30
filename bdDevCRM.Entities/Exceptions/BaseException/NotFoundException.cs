using System.Net;

namespace bdDevCRM.Entities.Exceptions.BaseException;

public abstract class NotFoundException : Exception
{
   public int StatusCode { get; } = (int)HttpStatusCode.NotFound; // 404

   protected NotFoundException(string message) : base(message)
   {
   }
}
