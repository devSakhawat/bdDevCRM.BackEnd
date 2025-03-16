namespace bdDevCRM.Entities.Exceptions;

public sealed class IdMismatchBadRequestException : BadRequestException
{
  public IdMismatchBadRequestException(string entityName, string paramName, string paramValue)
      : base($"{entityName}: The provided {paramName} ({paramValue}) does not match the expected value.")
  {
  }
}