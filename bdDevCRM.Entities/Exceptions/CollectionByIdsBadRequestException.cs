namespace bdDevCRM.Entities.Exceptions;

public class CollectionByIdsBadRequestException : BadRequestException
{
  public CollectionByIdsBadRequestException(string entityName) : base($"The number of {entityName} returned does not match the number of {entityName} requested.")
  {
  }

  //(string entityName) : base($"No data found for {entityName}.") { }
}
