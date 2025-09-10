using bdDevCRM.Shared.Exceptions.BaseException;

namespace bdDevCRM.Shared.Exceptions;

public sealed class IdParametersBadRequestException : BadRequestException
{
  //public IdParametersBadRequestException(string message) : base("Parameter ids is null")
  public IdParametersBadRequestException() : base("Parameter id cannot be zero or null.")
  {
  }
}
