using bdDevCRM.Entities.Exceptions.BaseException;

namespace bdDevCRM.Entities.Exceptions;

public sealed class IdMismatchBadRequestException : BadRequestException
{
  public IdMismatchBadRequestException(string entityName, string paramName, string paramValue)
      : base($"{entityName}: The provided {paramName} ({paramValue}) does not match the expected value.")
  {
  }
  public IdMismatchBadRequestException(string key, string model) : base($"The provided {key} does not match the {model} id.")
  {
  }
}