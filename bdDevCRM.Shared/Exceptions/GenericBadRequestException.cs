using bdDevCRM.Shared.Exceptions.BaseException;

namespace bdDevCRM.Shared.Exceptions;

public sealed class GenericBadRequestException(string message) : BadRequestException(message)
{
}