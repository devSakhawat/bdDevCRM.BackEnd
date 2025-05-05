using bdDevCRM.Entities.Exceptions.BaseException;

namespace bdDevCRM.Entities.Exceptions;

public class CommonBadReuqestException : BadRequestException
{
  public CommonBadReuqestException(string message) : base(message)
  {
  }
}
// pass only your message