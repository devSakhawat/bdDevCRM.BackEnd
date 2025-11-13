using bdDevCRM.Shared.Exceptions.BaseException;

namespace bdDevCRM.Shared.Exceptions;

public class CollectionByIdsBadRequestException(string entityName) : BadRequestException($"The number of {entityName} returned does not match the number of {entityName} requested.")
{
}
