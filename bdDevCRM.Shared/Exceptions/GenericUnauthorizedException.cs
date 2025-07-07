using bdDevCRM.Utilities.Exceptions.BaseException;

namespace bdDevCRM.Utilities.Exceptions;

public sealed class GenericUnauthorizedException(string message) : UnauthorizedException(message)
{
}