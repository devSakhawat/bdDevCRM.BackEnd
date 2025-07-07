using bdDevCRM.Utilities.Exceptions.BaseException;

namespace bdDevCRM.Utilities.Exceptions;

public sealed class GenericBadRequestException(string message) : BadRequestException(message)
{
}