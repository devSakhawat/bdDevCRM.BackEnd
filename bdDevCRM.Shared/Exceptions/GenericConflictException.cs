using bdDevCRM.Shared.Exceptions.BaseException;

namespace bdDevCRM.Shared.Exceptions;

public sealed class GenericConflictException(string message) : ConflictException(message)
{
}