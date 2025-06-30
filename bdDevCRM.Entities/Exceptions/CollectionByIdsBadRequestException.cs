using bdDevCRM.Entities.Exceptions.BaseException;

namespace bdDevCRM.Entities.Exceptions;

public class CollectionByIdsBadRequestException : BadRequestException
{
  public CollectionByIdsBadRequestException(string entityName) : base($"The number of {entityName} returned does not match the number of {entityName} requested.")
  {
  }
}
