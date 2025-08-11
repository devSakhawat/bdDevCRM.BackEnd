using bdDevCRM.Utilities.Exceptions.BaseException;

namespace bdDevCRM.Utilities.Exceptions;

public class CollectionByIdsBadRequestException(string entityName) : BadRequestException($"The number of {entityName} returned does not match the number of {entityName} requested.")
{
}
