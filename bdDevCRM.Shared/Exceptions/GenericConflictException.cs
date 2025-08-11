using bdDevCRM.Utilities.Exceptions.BaseException;

namespace bdDevCRM.Utilities.Exceptions;

public sealed class GenericConflictException(string message) : ConflictException(message)
{
}